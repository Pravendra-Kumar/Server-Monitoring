using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;

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

            timer1.Interval = 3000;
            timer1.Start();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            float cpu = GetCpuUsage();
            float ram = GetRamUsagePercent();
            float disk = GetDiskUsage();
            float net = GetNetworkUsageInKBps();

            lblCPU.Text = $"CPU: {cpu}%";
            lblRAM.Text = $"RAM: {ram}%";
            lblDisk.Text = $"Disk: {disk}%";
            lblNet.Text = $"Net: {net}KB";

            if (chkSave.Checked)
            {
                string line = $"{DateTime.Now},{cpu},{ram},{disk},{net}";
                File.AppendAllText(csvPath, line + "\n");
            }
        }

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
            float usageMB = netCounter.NextValue() / 1024 ; // Convert Bytes to MB
            return (float)Math.Round(usageMB, 2);
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
    }
}
