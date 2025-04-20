using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;

namespace DevicePer
{

    public partial class Form1 : Form
    {
        PerformanceCounter cpuCounter;
        PerformanceCounter ramCounter;
        PerformanceCounter diskCounter;
        PerformanceCounter netCounter;
        ulong totalRAM;

        string csvPath = @"D:\PRAVENDRA\SystemStatsLog.csv";

        ClientWebSocket wsClient;
        bool isWebSocketConnected = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            diskCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
            string netInterface = new PerformanceCounterCategory("Network Interface").GetInstanceNames()[0];
            netCounter = new PerformanceCounter("Network Interface", "Bytes Total/sec", netInterface);

            totalRAM = new ComputerInfo().TotalPhysicalMemory;

            if (!File.Exists(csvPath))
            {
                File.WriteAllText(csvPath, "Timestamp,CPU (%),RAM (%),Disk (%),Network (KB)\n");
            }

            Task.Run(() => ConnectWebSocket());

            timer1.Interval = 3000;
            timer1.Tick += async (s, ev) => await timer1_Tick_1Async(s, ev);
            timer1.Start();
        }
        private async Task ConnectWebSocket()
        {
            try
            {
                wsClient = new ClientWebSocket();
                await wsClient.ConnectAsync(new Uri("ws://localhost:59257/ws"), CancellationToken.None);
                isWebSocketConnected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"WebSocket connection failed: {ex.Message}");
            }
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            await timer1_Tick_1Async(sender, e);
        }


        private async Task timer1_Tick_1Async(object sender, EventArgs e)


        {
            float cpu = GetCpuUsage();
            float ram = GetRamUsagePercent();
            float disk = GetDiskUsage();
            float net = GetNetworkUsageInKBps();
            string ip = GetLocalIPAddress();
            string mac = GetMacAddress();

            lblCPU.Text = $"CPU: {cpu}%";
            lblRAM.Text = $"RAM: {ram}%";
            lblDisk.Text = $"Disk: {disk}%";
            lblNet.Text = $"Net: {net} KB/s";
            lblIp.Text = $"IP: {ip}";
            lblMac.Text = $"MAC: {mac}";

            if (chkSave.Checked)
            {
                string line = $"{DateTime.Now},{cpu},{ram},{disk},{net}";
                File.AppendAllText(csvPath, line + "\n");
            }

            if (isWebSocketConnected && wsClient?.State == WebSocketState.Open)
            {
                var data = new
                {
                    Timestamp = DateTime.Now,
                    CPU = cpu,
                    RAM = ram,
                    Disk = disk,
                    NetworkKB = net,
                    IP = ip,
                    MAC = mac
                };

                string json = JsonSerializer.Serialize(data);
                byte[] buffer = Encoding.UTF8.GetBytes(json);

                try
                {
                    await wsClient.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"WebSocket send failed: {ex.Message}");
                    isWebSocketConnected = false;
                }
            }
        }

        //private void timer1_Tick_1(object sender, EventArgs e)
        //{
        //    float cpu = GetCpuUsage();
        //    float ram = GetRamUsagePercent();
        //    float disk = GetDiskUsage();
        //    float net = GetNetworkUsageInKBps();
        //    string ip = GetLocalIPAddress();
        //    string mac = GetMacAddress();

        //    lblCPU.Text = $"CPU: {cpu}%";
        //    lblRAM.Text = $"RAM: {ram}%";
        //    lblDisk.Text = $"Disk: {disk}%";
        //    lblNet.Text = $"Net: {net} KB/s";
        //    lblIp.Text = $"IP: {ip}";
        //    lblMac.Text = $"MAC: {mac}";

        //    if (chkSave.Checked)
        //    {
        //        string line = $"{DateTime.Now},{cpu},{ram},{disk},{net}";
        //        File.AppendAllText(csvPath, line + "\n");
        //    }
        //}

        private float GetCpuUsage()
        {
            cpuCounter.NextValue();
            System.Threading.Thread.Sleep(500);
            return (float)Math.Round(cpuCounter.NextValue(), 2);
        }

        private float GetRamUsagePercent()
        {
            float available = ramCounter.NextValue();
            float used = ((totalRAM / (1024 * 1024)) - available) / (totalRAM / (1024 * 1024)) * 100;
            return (float)Math.Round(used, 2);
        }

        private float GetDiskUsage()
        {
            diskCounter.NextValue();
            System.Threading.Thread.Sleep(500);
            return (float)Math.Round(diskCounter.NextValue(), 2);
        }

        private float GetNetworkUsageInKBps()
        {
            netCounter.NextValue();
            System.Threading.Thread.Sleep(1000); // Wait for accurate reading
            float usageMB = netCounter.NextValue() / 1024; // Convert Bytes to MB
            return (float)Math.Round(usageMB, 2);
        }

        private string GetLocalIPAddress()
        {
            string localIP = "Not available";
            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }

            return localIP;
        }

        private string GetMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up &&
                    nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    return nic.GetPhysicalAddress().ToString();
                }
            }

            return "MAC not found";
        }



        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void chkSave_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
