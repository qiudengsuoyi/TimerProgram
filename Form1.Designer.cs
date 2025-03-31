namespace TimerOnTime
{
    partial class Form1
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
            timerSecond = new System.Windows.Forms.Timer(components);
            groupBox1 = new GroupBox();
            tvOrderLeaveDetail = new TextBox();
            btOrderLeave = new Button();
            groupBox2 = new GroupBox();
            tvLockCommandDetail = new TextBox();
            btCommand = new Button();
            timerLockCommand = new System.Windows.Forms.Timer(components);
            groupBox3 = new GroupBox();
            tvRentDetail = new TextBox();
            btRent = new Button();
            timerRent = new System.Windows.Forms.Timer(components);
            groupBox4 = new GroupBox();
            tvRevenueInterval = new TextBox();
            tvRevenueDetail = new TextBox();
            btRevenue = new Button();
            timerRevenue = new System.Windows.Forms.Timer(components);
            groupBox5 = new GroupBox();
            tvRentNearDate = new TextBox();
            btRentNearDate = new Button();
            groupBox6 = new GroupBox();
            tvRentTimeout = new TextBox();
            btTimeOut = new Button();
            timerNearDate = new System.Windows.Forms.Timer(components);
            timerTimeout = new System.Windows.Forms.Timer(components);
            groupBox7 = new GroupBox();
            tvRefund = new TextBox();
            btRefund = new Button();
            timerRefund = new System.Windows.Forms.Timer(components);
            groupBox8 = new GroupBox();
            tvMissOut = new TextBox();
            btOrderMissOut = new Button();
            timerOrderMissOut = new System.Windows.Forms.Timer(components);
            groupBox9 = new GroupBox();
            tvRegionDetail = new TextBox();
            btRegionCheck = new Button();
            timerRegionCheck = new System.Windows.Forms.Timer(components);
            groupBox10 = new GroupBox();
            tvDeviceDetail = new TextBox();
            btDeviceConnection = new Button();
            timerDeviceConnection = new System.Windows.Forms.Timer(components);
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox9.SuspendLayout();
            groupBox10.SuspendLayout();
            SuspendLayout();
            // 
            // timerSecond
            // 
            timerSecond.Interval = 10000;
            timerSecond.Tick += timerSecond_Tick;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.DarkGray;
            groupBox1.Controls.Add(tvOrderLeaveDetail);
            groupBox1.Controls.Add(btOrderLeave);
            groupBox1.ForeColor = SystemColors.ControlText;
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(300, 200);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "POS设备订单自动离场";
            // 
            // tvOrderLeaveDetail
            // 
            tvOrderLeaveDetail.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tvOrderLeaveDetail.BackColor = Color.White;
            tvOrderLeaveDetail.Location = new Point(6, 67);
            tvOrderLeaveDetail.Multiline = true;
            tvOrderLeaveDetail.Name = "tvOrderLeaveDetail";
            tvOrderLeaveDetail.ReadOnly = true;
            tvOrderLeaveDetail.ScrollBars = ScrollBars.Vertical;
            tvOrderLeaveDetail.Size = new Size(280, 125);
            tvOrderLeaveDetail.TabIndex = 3;
            tvOrderLeaveDetail.WordWrap = false;
            // 
            // btOrderLeave
            // 
            btOrderLeave.AutoSize = true;
            btOrderLeave.BackColor = SystemColors.ActiveBorder;
            btOrderLeave.Location = new Point(6, 34);
            btOrderLeave.Name = "btOrderLeave";
            btOrderLeave.Size = new Size(83, 27);
            btOrderLeave.TabIndex = 2;
            btOrderLeave.Text = "开启";
            btOrderLeave.UseVisualStyleBackColor = false;
            btOrderLeave.Click += btOrderLeave_Click;
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.DarkGray;
            groupBox2.Controls.Add(tvLockCommandDetail);
            groupBox2.Controls.Add(btCommand);
            groupBox2.ForeColor = SystemColors.ControlText;
            groupBox2.Location = new Point(624, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(306, 200);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "平板车位锁命令操作";
            // 
            // tvLockCommandDetail
            // 
            tvLockCommandDetail.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tvLockCommandDetail.BackColor = Color.White;
            tvLockCommandDetail.BorderStyle = BorderStyle.None;
            tvLockCommandDetail.Location = new Point(6, 67);
            tvLockCommandDetail.Multiline = true;
            tvLockCommandDetail.Name = "tvLockCommandDetail";
            tvLockCommandDetail.ReadOnly = true;
            tvLockCommandDetail.ScrollBars = ScrollBars.Vertical;
            tvLockCommandDetail.Size = new Size(294, 125);
            tvLockCommandDetail.TabIndex = 3;
            tvLockCommandDetail.WordWrap = false;
            // 
            // btCommand
            // 
            btCommand.AutoSize = true;
            btCommand.BackColor = SystemColors.ActiveBorder;
            btCommand.Location = new Point(6, 34);
            btCommand.Name = "btCommand";
            btCommand.Size = new Size(83, 27);
            btCommand.TabIndex = 2;
            btCommand.Text = "开启";
            btCommand.UseVisualStyleBackColor = false;
            btCommand.Click += btCommand_Click;
            // 
            // timerLockCommand
            // 
            timerLockCommand.Interval = 2000;
            timerLockCommand.Tick += timerLockCommand_Tick;
            // 
            // groupBox3
            // 
            groupBox3.BackColor = Color.DarkGray;
            groupBox3.Controls.Add(tvRentDetail);
            groupBox3.Controls.Add(btRent);
            groupBox3.ForeColor = SystemColors.ControlText;
            groupBox3.Location = new Point(12, 218);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(300, 200);
            groupBox3.TabIndex = 4;
            groupBox3.TabStop = false;
            groupBox3.Text = "固定月租回收";
            // 
            // tvRentDetail
            // 
            tvRentDetail.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tvRentDetail.BackColor = Color.White;
            tvRentDetail.Location = new Point(6, 67);
            tvRentDetail.Multiline = true;
            tvRentDetail.Name = "tvRentDetail";
            tvRentDetail.ReadOnly = true;
            tvRentDetail.ScrollBars = ScrollBars.Vertical;
            tvRentDetail.Size = new Size(280, 125);
            tvRentDetail.TabIndex = 3;
            tvRentDetail.WordWrap = false;
            // 
            // btRent
            // 
            btRent.AutoSize = true;
            btRent.BackColor = SystemColors.ActiveBorder;
            btRent.Location = new Point(6, 34);
            btRent.Name = "btRent";
            btRent.Size = new Size(83, 27);
            btRent.TabIndex = 2;
            btRent.Text = "开启";
            btRent.UseVisualStyleBackColor = false;
            btRent.Click += btRent_Click;
            // 
            // timerRent
            // 
            timerRent.Interval = 10000;
            timerRent.Tick += timerRent_Tick;
            // 
            // groupBox4
            // 
            groupBox4.BackColor = Color.DarkGray;
            groupBox4.Controls.Add(tvRevenueInterval);
            groupBox4.Controls.Add(tvRevenueDetail);
            groupBox4.Controls.Add(btRevenue);
            groupBox4.ForeColor = SystemColors.ControlText;
            groupBox4.Location = new Point(625, 218);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(305, 200);
            groupBox4.TabIndex = 5;
            groupBox4.TabStop = false;
            groupBox4.Text = "营收";
            // 
            // tvRevenueInterval
            // 
            tvRevenueInterval.Location = new Point(95, 36);
            tvRevenueInterval.Name = "tvRevenueInterval";
            tvRevenueInterval.Size = new Size(80, 23);
            tvRevenueInterval.TabIndex = 4;
            // 
            // tvRevenueDetail
            // 
            tvRevenueDetail.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tvRevenueDetail.BackColor = Color.White;
            tvRevenueDetail.Location = new Point(6, 67);
            tvRevenueDetail.Multiline = true;
            tvRevenueDetail.Name = "tvRevenueDetail";
            tvRevenueDetail.ReadOnly = true;
            tvRevenueDetail.ScrollBars = ScrollBars.Vertical;
            tvRevenueDetail.Size = new Size(293, 127);
            tvRevenueDetail.TabIndex = 3;
            tvRevenueDetail.WordWrap = false;
            // 
            // btRevenue
            // 
            btRevenue.AutoSize = true;
            btRevenue.BackColor = SystemColors.ActiveBorder;
            btRevenue.Location = new Point(6, 34);
            btRevenue.Name = "btRevenue";
            btRevenue.Size = new Size(83, 27);
            btRevenue.TabIndex = 2;
            btRevenue.Text = "开启";
            btRevenue.UseVisualStyleBackColor = false;
            btRevenue.Click += btRevenue_Click;
            // 
            // timerRevenue
            // 
            timerRevenue.Interval = 10000;
            timerRevenue.Tick += timerRevenue_Tick;
            // 
            // groupBox5
            // 
            groupBox5.BackColor = Color.DarkGray;
            groupBox5.Controls.Add(tvRentNearDate);
            groupBox5.Controls.Add(btRentNearDate);
            groupBox5.ForeColor = SystemColors.ControlText;
            groupBox5.Location = new Point(12, 424);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(300, 203);
            groupBox5.TabIndex = 6;
            groupBox5.TabStop = false;
            groupBox5.Text = "月租到期提醒";
            // 
            // tvRentNearDate
            // 
            tvRentNearDate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tvRentNearDate.BackColor = Color.White;
            tvRentNearDate.Location = new Point(6, 67);
            tvRentNearDate.Multiline = true;
            tvRentNearDate.Name = "tvRentNearDate";
            tvRentNearDate.ReadOnly = true;
            tvRentNearDate.ScrollBars = ScrollBars.Vertical;
            tvRentNearDate.Size = new Size(280, 128);
            tvRentNearDate.TabIndex = 3;
            tvRentNearDate.WordWrap = false;
            // 
            // btRentNearDate
            // 
            btRentNearDate.AutoSize = true;
            btRentNearDate.BackColor = SystemColors.ActiveBorder;
            btRentNearDate.Location = new Point(6, 34);
            btRentNearDate.Name = "btRentNearDate";
            btRentNearDate.Size = new Size(83, 27);
            btRentNearDate.TabIndex = 2;
            btRentNearDate.Text = "开启";
            btRentNearDate.UseVisualStyleBackColor = false;
            btRentNearDate.Click += btRentNearDate_Click;
            // 
            // groupBox6
            // 
            groupBox6.BackColor = Color.DarkGray;
            groupBox6.Controls.Add(tvRentTimeout);
            groupBox6.Controls.Add(btTimeOut);
            groupBox6.ForeColor = SystemColors.ControlText;
            groupBox6.Location = new Point(318, 12);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(300, 200);
            groupBox6.TabIndex = 7;
            groupBox6.TabStop = false;
            groupBox6.Text = "月租转临停提醒";
            // 
            // tvRentTimeout
            // 
            tvRentTimeout.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tvRentTimeout.BackColor = Color.White;
            tvRentTimeout.Location = new Point(6, 67);
            tvRentTimeout.Multiline = true;
            tvRentTimeout.Name = "tvRentTimeout";
            tvRentTimeout.ReadOnly = true;
            tvRentTimeout.ScrollBars = ScrollBars.Vertical;
            tvRentTimeout.Size = new Size(280, 125);
            tvRentTimeout.TabIndex = 3;
            tvRentTimeout.WordWrap = false;
            // 
            // btTimeOut
            // 
            btTimeOut.AutoSize = true;
            btTimeOut.BackColor = SystemColors.ActiveBorder;
            btTimeOut.Location = new Point(6, 34);
            btTimeOut.Name = "btTimeOut";
            btTimeOut.Size = new Size(83, 27);
            btTimeOut.TabIndex = 2;
            btTimeOut.Text = "开启";
            btTimeOut.UseVisualStyleBackColor = false;
            btTimeOut.Click += btTimeOut_Click;
            // 
            // timerNearDate
            // 
            timerNearDate.Interval = 2000;
            timerNearDate.Tick += timerNearDate_Tick;
            // 
            // timerTimeout
            // 
            timerTimeout.Interval = 2000;
            timerTimeout.Tick += timerTimeout_Tick;
            // 
            // groupBox7
            // 
            groupBox7.BackColor = Color.DarkGray;
            groupBox7.Controls.Add(tvRefund);
            groupBox7.Controls.Add(btRefund);
            groupBox7.ForeColor = SystemColors.ControlText;
            groupBox7.Location = new Point(318, 218);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(300, 200);
            groupBox7.TabIndex = 8;
            groupBox7.TabStop = false;
            groupBox7.Text = "临停订单自动退款";
            // 
            // tvRefund
            // 
            tvRefund.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tvRefund.BackColor = Color.White;
            tvRefund.Location = new Point(6, 67);
            tvRefund.Multiline = true;
            tvRefund.Name = "tvRefund";
            tvRefund.ReadOnly = true;
            tvRefund.ScrollBars = ScrollBars.Vertical;
            tvRefund.Size = new Size(280, 125);
            tvRefund.TabIndex = 3;
            tvRefund.WordWrap = false;
            // 
            // btRefund
            // 
            btRefund.AutoSize = true;
            btRefund.BackColor = SystemColors.ActiveBorder;
            btRefund.Location = new Point(6, 34);
            btRefund.Name = "btRefund";
            btRefund.Size = new Size(83, 27);
            btRefund.TabIndex = 2;
            btRefund.Text = "开启";
            btRefund.UseVisualStyleBackColor = false;
            btRefund.Click += btRefund_Click;
            // 
            // timerRefund
            // 
            timerRefund.Interval = 2000;
            timerRefund.Tick += timerRefund_Tick;
            // 
            // groupBox8
            // 
            groupBox8.BackColor = Color.DarkGray;
            groupBox8.Controls.Add(tvMissOut);
            groupBox8.Controls.Add(btOrderMissOut);
            groupBox8.ForeColor = SystemColors.ControlText;
            groupBox8.Location = new Point(318, 424);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(300, 203);
            groupBox8.TabIndex = 9;
            groupBox8.TabStop = false;
            groupBox8.Text = "遗漏订单检查";
            // 
            // tvMissOut
            // 
            tvMissOut.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tvMissOut.BackColor = Color.White;
            tvMissOut.Location = new Point(6, 67);
            tvMissOut.Multiline = true;
            tvMissOut.Name = "tvMissOut";
            tvMissOut.ReadOnly = true;
            tvMissOut.ScrollBars = ScrollBars.Vertical;
            tvMissOut.Size = new Size(280, 128);
            tvMissOut.TabIndex = 3;
            tvMissOut.WordWrap = false;
            // 
            // btOrderMissOut
            // 
            btOrderMissOut.AutoSize = true;
            btOrderMissOut.BackColor = SystemColors.ActiveBorder;
            btOrderMissOut.Location = new Point(6, 34);
            btOrderMissOut.Name = "btOrderMissOut";
            btOrderMissOut.Size = new Size(83, 27);
            btOrderMissOut.TabIndex = 2;
            btOrderMissOut.Text = "开启";
            btOrderMissOut.UseVisualStyleBackColor = false;
            btOrderMissOut.Click += btOrderMissOut_Click;
            // 
            // timerOrderMissOut
            // 
            timerOrderMissOut.Interval = 6000000;
            timerOrderMissOut.Tick += timerOrderMissOut_Tick;
            // 
            // groupBox9
            // 
            groupBox9.BackColor = Color.DarkGray;
            groupBox9.Controls.Add(tvRegionDetail);
            groupBox9.Controls.Add(btRegionCheck);
            groupBox9.ForeColor = SystemColors.ControlText;
            groupBox9.Location = new Point(624, 424);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(305, 203);
            groupBox9.TabIndex = 10;
            groupBox9.TabStop = false;
            groupBox9.Text = "区域打单检查";
            // 
            // tvRegionDetail
            // 
            tvRegionDetail.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tvRegionDetail.BackColor = Color.White;
            tvRegionDetail.Location = new Point(6, 67);
            tvRegionDetail.Multiline = true;
            tvRegionDetail.Name = "tvRegionDetail";
            tvRegionDetail.ReadOnly = true;
            tvRegionDetail.ScrollBars = ScrollBars.Vertical;
            tvRegionDetail.Size = new Size(298, 128);
            tvRegionDetail.TabIndex = 3;
            tvRegionDetail.WordWrap = false;
            // 
            // btRegionCheck
            // 
            btRegionCheck.AutoSize = true;
            btRegionCheck.BackColor = SystemColors.ActiveBorder;
            btRegionCheck.Location = new Point(6, 34);
            btRegionCheck.Name = "btRegionCheck";
            btRegionCheck.Size = new Size(83, 27);
            btRegionCheck.TabIndex = 2;
            btRegionCheck.Text = "开启";
            btRegionCheck.UseVisualStyleBackColor = false;
            btRegionCheck.Click += btRegionCheck_Click;
            // 
            // timerRegionCheck
            // 
            timerRegionCheck.Interval = 600000;
            timerRegionCheck.Tick += timerRegionCheck_Tick;
            // 
            // groupBox10
            // 
            groupBox10.BackColor = Color.DarkGray;
            groupBox10.Controls.Add(tvDeviceDetail);
            groupBox10.Controls.Add(btDeviceConnection);
            groupBox10.ForeColor = SystemColors.ControlText;
            groupBox10.Location = new Point(935, 12);
            groupBox10.Name = "groupBox10";
            groupBox10.Size = new Size(290, 200);
            groupBox10.TabIndex = 11;
            groupBox10.TabStop = false;
            groupBox10.Text = "设备断线检查";
            // 
            // tvDeviceDetail
            // 
            tvDeviceDetail.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tvDeviceDetail.BackColor = Color.White;
            tvDeviceDetail.BorderStyle = BorderStyle.None;
            tvDeviceDetail.Location = new Point(6, 67);
            tvDeviceDetail.Multiline = true;
            tvDeviceDetail.Name = "tvDeviceDetail";
            tvDeviceDetail.ReadOnly = true;
            tvDeviceDetail.ScrollBars = ScrollBars.Vertical;
            tvDeviceDetail.Size = new Size(278, 125);
            tvDeviceDetail.TabIndex = 3;
            tvDeviceDetail.WordWrap = false;
            // 
            // btDeviceConnection
            // 
            btDeviceConnection.AutoSize = true;
            btDeviceConnection.BackColor = SystemColors.ActiveBorder;
            btDeviceConnection.Location = new Point(6, 34);
            btDeviceConnection.Name = "btDeviceConnection";
            btDeviceConnection.Size = new Size(83, 27);
            btDeviceConnection.TabIndex = 2;
            btDeviceConnection.Text = "开启";
            btDeviceConnection.UseVisualStyleBackColor = false;
            btDeviceConnection.Click += btDeviceConnection_Click;
            // 
            // timerDeviceConnection
            // 
            timerDeviceConnection.Interval = 60000;
            timerDeviceConnection.Tick += timerDeviceConnection_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1237, 679);
            Controls.Add(groupBox10);
            Controls.Add(groupBox9);
            Controls.Add(groupBox8);
            Controls.Add(groupBox7);
            Controls.Add(groupBox6);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "整点定时器";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            groupBox10.ResumeLayout(false);
            groupBox10.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timerSecond;
        private GroupBox groupBox1;
        private TextBox tvOrderLeaveDetail;
        private Button btOrderLeave;
        private GroupBox groupBox2;
        private TextBox tvLockCommandDetail;
        private Button btCommand;
        private System.Windows.Forms.Timer timerLockCommand;
        private GroupBox groupBox3;
        private TextBox tvRentDetail;
        private Button btRent;
        private System.Windows.Forms.Timer timerRent;
        private GroupBox groupBox4;
        private TextBox tvRevenueInterval;
        private TextBox tvRevenueDetail;
        private Button btRevenue;
        private System.Windows.Forms.Timer timerRevenue;
        private GroupBox groupBox5;
        private TextBox tvRentNearDate;
        private Button btRentNearDate;
        private GroupBox groupBox6;
        private TextBox tvRentTimeout;
        private Button btTimeOut;
        private System.Windows.Forms.Timer timerNearDate;
        private System.Windows.Forms.Timer timerTimeout;
        private GroupBox groupBox7;
        private TextBox tvRefund;
        private Button btRefund;
        private System.Windows.Forms.Timer timerRefund;
        private GroupBox groupBox8;
        private TextBox tvMissOut;
        private Button btOrderMissOut;
        private System.Windows.Forms.Timer timerOrderMissOut;
        private GroupBox groupBox9;
        private TextBox tvRegionDetail;
        private Button btRegionCheck;
        private System.Windows.Forms.Timer timerRegionCheck;
        private GroupBox groupBox10;
        private TextBox tvDeviceDetail;
        private Button btDeviceConnection;
        private System.Windows.Forms.Timer timerDeviceConnection;
    }
}