namespace SonicConn
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
            this.connState = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TcpServer = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uC_servertcp1 = new SonicConn.UC_servertcp();
            this.TcpClient = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.uC_clienttco1 = new SonicConn.TCP.UC_clienttco();
            this.tabControl1.SuspendLayout();
            this.TcpServer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.TcpClient.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // connState
            // 
            this.connState.BackColor = System.Drawing.Color.Red;
            this.connState.Location = new System.Drawing.Point(12, 22);
            this.connState.Name = "connState";
            this.connState.Size = new System.Drawing.Size(25, 25);
            this.connState.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TcpServer);
            this.tabControl1.Controls.Add(this.TcpClient);
            this.tabControl1.Location = new System.Drawing.Point(74, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(432, 608);
            this.tabControl1.TabIndex = 6;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // TcpServer
            // 
            this.TcpServer.Controls.Add(this.panel1);
            this.TcpServer.Location = new System.Drawing.Point(4, 22);
            this.TcpServer.Name = "TcpServer";
            this.TcpServer.Size = new System.Drawing.Size(424, 582);
            this.TcpServer.TabIndex = 0;
            this.TcpServer.Text = "TcpServer";
            this.TcpServer.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uC_servertcp1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(421, 582);
            this.panel1.TabIndex = 0;
            // 
            // uC_servertcp1
            // 
            this.uC_servertcp1.Location = new System.Drawing.Point(3, 0);
            this.uC_servertcp1.Name = "uC_servertcp1";
            this.uC_servertcp1.Size = new System.Drawing.Size(416, 582);
            this.uC_servertcp1.TabIndex = 0;
            // 
            // TcpClient
            // 
            this.TcpClient.Controls.Add(this.panel2);
            this.TcpClient.Location = new System.Drawing.Point(4, 22);
            this.TcpClient.Name = "TcpClient";
            this.TcpClient.Size = new System.Drawing.Size(424, 582);
            this.TcpClient.TabIndex = 1;
            this.TcpClient.Text = "Tcp Client";
            this.TcpClient.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.uC_clienttco1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(424, 582);
            this.panel2.TabIndex = 0;
            // 
            // uC_clienttco1
            // 
            this.uC_clienttco1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_clienttco1.Location = new System.Drawing.Point(0, 0);
            this.uC_clienttco1.Name = "uC_clienttco1";
            this.uC_clienttco1.Size = new System.Drawing.Size(424, 582);
            this.uC_clienttco1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 608);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.connState);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.TcpServer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.TcpClient.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label connState;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TcpServer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage TcpClient;
        private System.Windows.Forms.Panel panel2;
        private UC_servertcp uC_servertcp1;
        private TCP.UC_clienttco uC_clienttco1;
    }
}

