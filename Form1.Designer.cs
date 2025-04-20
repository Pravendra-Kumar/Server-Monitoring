namespace DevicePer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblCPU = new System.Windows.Forms.Label();
            this.lblRAM = new System.Windows.Forms.Label();
            this.lblDisk = new System.Windows.Forms.Label();
            this.lblNet = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chkSave = new System.Windows.Forms.CheckBox();
            this.lblIp = new System.Windows.Forms.Label();
            this.lblMac = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCPU
            // 
            this.lblCPU.AutoSize = true;
            this.lblCPU.Location = new System.Drawing.Point(1, 78);
            this.lblCPU.Name = "lblCPU";
            this.lblCPU.Size = new System.Drawing.Size(36, 17);
            this.lblCPU.TabIndex = 0;
            this.lblCPU.Text = "CPU";
            // 
            // lblRAM
            // 
            this.lblRAM.AutoSize = true;
            this.lblRAM.Location = new System.Drawing.Point(1, 117);
            this.lblRAM.Name = "lblRAM";
            this.lblRAM.Size = new System.Drawing.Size(38, 17);
            this.lblRAM.TabIndex = 1;
            this.lblRAM.Text = "RAM";
            this.lblRAM.Click += new System.EventHandler(this.label2_Click);
            // 
            // lblDisk
            // 
            this.lblDisk.AutoSize = true;
            this.lblDisk.Location = new System.Drawing.Point(6, 166);
            this.lblDisk.Name = "lblDisk";
            this.lblDisk.Size = new System.Drawing.Size(35, 17);
            this.lblDisk.TabIndex = 2;
            this.lblDisk.Text = "Disk";
            this.lblDisk.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblNet
            // 
            this.lblNet.AutoSize = true;
            this.lblNet.Location = new System.Drawing.Point(9, 214);
            this.lblNet.Name = "lblNet";
            this.lblNet.Size = new System.Drawing.Size(30, 17);
            this.lblNet.TabIndex = 3;
            this.lblNet.Text = "Net";
            this.lblNet.Click += new System.EventHandler(this.label4_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chkSave
            // 
            this.chkSave.AutoSize = true;
            this.chkSave.Location = new System.Drawing.Point(411, 210);
            this.chkSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkSave.Name = "chkSave";
            this.chkSave.Size = new System.Drawing.Size(109, 21);
            this.chkSave.TabIndex = 4;
            this.chkSave.Text = "Save to CSV";
            this.chkSave.UseVisualStyleBackColor = true;
            this.chkSave.CheckedChanged += new System.EventHandler(this.chkSave_CheckedChanged);
            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.Location = new System.Drawing.Point(11, 18);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(20, 17);
            this.lblIp.TabIndex = 5;
            this.lblIp.Text = "IP";
            this.lblIp.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblMac
            // 
            this.lblMac.AutoSize = true;
            this.lblMac.Location = new System.Drawing.Point(408, 18);
            this.lblMac.Name = "lblMac";
            this.lblMac.Size = new System.Drawing.Size(37, 17);
            this.lblMac.TabIndex = 6;
            this.lblMac.Text = "MAC";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 260);
            this.Controls.Add(this.lblMac);
            this.Controls.Add(this.lblIp);
            this.Controls.Add(this.chkSave);
            this.Controls.Add(this.lblNet);
            this.Controls.Add(this.lblDisk);
            this.Controls.Add(this.lblRAM);
            this.Controls.Add(this.lblCPU);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "System Performance";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCPU;
        private System.Windows.Forms.Label lblRAM;
        private System.Windows.Forms.Label lblDisk;
        private System.Windows.Forms.Label lblNet;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox chkSave;
        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.Label lblMac;
    }
}

