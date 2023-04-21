namespace KrakenFanControl
{
    partial class SimpleFanControl
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimpleFanControl));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            fan1Value = new Label();
            fan2Value = new Label();
            pumpSpdValue = new Label();
            cpuTemp = new Label();
            liquidTemp = new Label();
            menus = new ContextMenuStrip(components);
            Show = new ToolStripMenuItem();
            Quit = new ToolStripMenuItem();
            trayIcon = new NotifyIcon(components);
            maintainer = new System.Windows.Forms.Timer(components);
            label6 = new Label();
            label7 = new Label();
            ramTemp2 = new Label();
            ramTemp1 = new Label();
            menus.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(40, 42);
            label1.Name = "label1";
            label1.Size = new Size(136, 38);
            label1.TabIndex = 0;
            label1.Text = "Fan1 Spd";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(40, 80);
            label2.Name = "label2";
            label2.Size = new Size(136, 38);
            label2.TabIndex = 0;
            label2.Text = "Fan2 Spd";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(40, 232);
            label3.Name = "label3";
            label3.Size = new Size(176, 38);
            label3.TabIndex = 0;
            label3.Text = "Liquid Temp";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(40, 270);
            label4.Name = "label4";
            label4.Size = new Size(153, 38);
            label4.TabIndex = 0;
            label4.Text = "CPU Temp";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(40, 118);
            label5.Name = "label5";
            label5.Size = new Size(150, 38);
            label5.TabIndex = 0;
            label5.Text = "Pump Spd";
            // 
            // fan1Value
            // 
            fan1Value.AutoSize = true;
            fan1Value.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point);
            fan1Value.Location = new Point(265, 42);
            fan1Value.Name = "fan1Value";
            fan1Value.Size = new Size(137, 38);
            fan1Value.TabIndex = 0;
            fan1Value.Text = "Loading...";
            // 
            // fan2Value
            // 
            fan2Value.AutoSize = true;
            fan2Value.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point);
            fan2Value.Location = new Point(265, 80);
            fan2Value.Name = "fan2Value";
            fan2Value.Size = new Size(137, 38);
            fan2Value.TabIndex = 0;
            fan2Value.Text = "Loading...";
            // 
            // pumpSpdValue
            // 
            pumpSpdValue.AutoSize = true;
            pumpSpdValue.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point);
            pumpSpdValue.Location = new Point(265, 118);
            pumpSpdValue.Name = "pumpSpdValue";
            pumpSpdValue.Size = new Size(137, 38);
            pumpSpdValue.TabIndex = 0;
            pumpSpdValue.Text = "Loading...";
            // 
            // cpuTemp
            // 
            cpuTemp.AutoSize = true;
            cpuTemp.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point);
            cpuTemp.Location = new Point(265, 270);
            cpuTemp.Name = "cpuTemp";
            cpuTemp.Size = new Size(137, 38);
            cpuTemp.TabIndex = 0;
            cpuTemp.Text = "Loading...";
            // 
            // liquidTemp
            // 
            liquidTemp.AutoSize = true;
            liquidTemp.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point);
            liquidTemp.Location = new Point(265, 232);
            liquidTemp.Name = "liquidTemp";
            liquidTemp.Size = new Size(137, 38);
            liquidTemp.TabIndex = 0;
            liquidTemp.Text = "Loading...";
            // 
            // menus
            // 
            menus.ImageScalingSize = new Size(24, 24);
            menus.Items.AddRange(new ToolStripItem[] { Show, Quit });
            menus.Name = "contextMenuStrip1";
            menus.Size = new Size(129, 68);
            // 
            // Show
            // 
            Show.Name = "Show";
            Show.Size = new Size(128, 32);
            Show.Text = "Show";
            Show.Click += Show_Click;
            // 
            // Quit
            // 
            Quit.Name = "Quit";
            Quit.Size = new Size(128, 32);
            Quit.Text = "Exit";
            Quit.Click += Quit_Click;
            // 
            // trayIcon
            // 
            trayIcon.ContextMenuStrip = menus;
            trayIcon.Icon = (Icon)resources.GetObject("trayIcon.Icon");
            trayIcon.Text = "Simple FanControl";
            trayIcon.Visible = true;
            trayIcon.MouseDoubleClick += trayIcon_MouseDoubleClick;
            // 
            // maintainer
            // 
            maintainer.Enabled = true;
            maintainer.Interval = 1000;
            maintainer.Tick += maintainer_Tick;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(40, 360);
            label6.Name = "label6";
            label6.Size = new Size(175, 38);
            label6.TabIndex = 0;
            label6.Text = "RAM2 Temp";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(40, 322);
            label7.Name = "label7";
            label7.Size = new Size(175, 38);
            label7.TabIndex = 0;
            label7.Text = "RAM1 Temp";
            // 
            // ramTemp2
            // 
            ramTemp2.AutoSize = true;
            ramTemp2.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point);
            ramTemp2.Location = new Point(265, 360);
            ramTemp2.Name = "ramTemp2";
            ramTemp2.Size = new Size(137, 38);
            ramTemp2.TabIndex = 0;
            ramTemp2.Text = "Loading...";
            // 
            // ramTemp1
            // 
            ramTemp1.AutoSize = true;
            ramTemp1.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point);
            ramTemp1.Location = new Point(265, 322);
            ramTemp1.Name = "ramTemp1";
            ramTemp1.Size = new Size(137, 38);
            ramTemp1.TabIndex = 0;
            ramTemp1.Text = "Loading...";
            // 
            // SimpleFanControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(463, 429);
            Controls.Add(pumpSpdValue);
            Controls.Add(label5);
            Controls.Add(ramTemp1);
            Controls.Add(liquidTemp);
            Controls.Add(ramTemp2);
            Controls.Add(cpuTemp);
            Controls.Add(label7);
            Controls.Add(label3);
            Controls.Add(label6);
            Controls.Add(label4);
            Controls.Add(fan2Value);
            Controls.Add(label2);
            Controls.Add(fan1Value);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SimpleFanControl";
            Text = "Simple FanControl";
            menus.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label fan1Value;
        private Label fan2Value;
        private Label pumpSpdValue;
        private Label cpuTemp;
        private Label liquidTemp;
        private NotifyIcon trayIcon;
        private ContextMenuStrip menus;
        private ToolStripMenuItem Show;
        private ToolStripMenuItem Quit;
        private System.Windows.Forms.Timer maintainer;
        private Label label6;
        private Label label7;
        private Label ramTemp2;
        private Label ramTemp1;
    }
}