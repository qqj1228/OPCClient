namespace OPCClient
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbServerName = new System.Windows.Forms.ComboBox();
            this.btnConnServer = new System.Windows.Forms.Button();
            this.tsslServerState = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.txtTagValue = new System.Windows.Forms.Label();
            this.txtQualities = new System.Windows.Forms.Label();
            this.txtTimeStamps = new System.Windows.Forms.Label();
            this.txtRemoteServerIP = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tsslServerStartTime = new System.Windows.Forms.Label();
            this.tsslversion = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtWriteTagValue = new System.Windows.Forms.TextBox();
            this.lsbGroups = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // cmbServerName
            // 
            this.cmbServerName.FormattingEnabled = true;
            this.cmbServerName.Location = new System.Drawing.Point(35, 59);
            this.cmbServerName.Name = "cmbServerName";
            this.cmbServerName.Size = new System.Drawing.Size(225, 20);
            this.cmbServerName.TabIndex = 0;
            // 
            // btnConnServer
            // 
            this.btnConnServer.Location = new System.Drawing.Point(36, 94);
            this.btnConnServer.Name = "btnConnServer";
            this.btnConnServer.Size = new System.Drawing.Size(143, 27);
            this.btnConnServer.TabIndex = 1;
            this.btnConnServer.Text = "连接";
            this.btnConnServer.UseVisualStyleBackColor = true;
            this.btnConnServer.Click += new System.EventHandler(this.btnConnServer_Click);
            // 
            // tsslServerState
            // 
            this.tsslServerState.AutoSize = true;
            this.tsslServerState.Location = new System.Drawing.Point(34, 288);
            this.tsslServerState.Name = "tsslServerState";
            this.tsslServerState.Size = new System.Drawing.Size(41, 12);
            this.tsslServerState.TabIndex = 2;
            this.tsslServerState.Text = "label1";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(34, 261);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(41, 12);
            this.lblState.TabIndex = 2;
            this.lblState.Text = "label1";
            // 
            // txtTagValue
            // 
            this.txtTagValue.AutoSize = true;
            this.txtTagValue.Location = new System.Drawing.Point(533, 42);
            this.txtTagValue.Name = "txtTagValue";
            this.txtTagValue.Size = new System.Drawing.Size(41, 12);
            this.txtTagValue.TabIndex = 3;
            this.txtTagValue.Text = "label1";
            // 
            // txtQualities
            // 
            this.txtQualities.AutoSize = true;
            this.txtQualities.Location = new System.Drawing.Point(533, 67);
            this.txtQualities.Name = "txtQualities";
            this.txtQualities.Size = new System.Drawing.Size(41, 12);
            this.txtQualities.TabIndex = 3;
            this.txtQualities.Text = "label1";
            // 
            // txtTimeStamps
            // 
            this.txtTimeStamps.AutoSize = true;
            this.txtTimeStamps.Location = new System.Drawing.Point(533, 94);
            this.txtTimeStamps.Name = "txtTimeStamps";
            this.txtTimeStamps.Size = new System.Drawing.Size(41, 12);
            this.txtTimeStamps.TabIndex = 3;
            this.txtTimeStamps.Text = "label1";
            // 
            // txtRemoteServerIP
            // 
            this.txtRemoteServerIP.Location = new System.Drawing.Point(35, 17);
            this.txtRemoteServerIP.Name = "txtRemoteServerIP";
            this.txtRemoteServerIP.Size = new System.Drawing.Size(128, 21);
            this.txtRemoteServerIP.TabIndex = 4;
            this.txtRemoteServerIP.Text = "172.20.33.82";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(147, 138);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(311, 112);
            this.listBox1.TabIndex = 5;
            // 
            // tsslServerStartTime
            // 
            this.tsslServerStartTime.AutoSize = true;
            this.tsslServerStartTime.Location = new System.Drawing.Point(34, 445);
            this.tsslServerStartTime.Name = "tsslServerStartTime";
            this.tsslServerStartTime.Size = new System.Drawing.Size(41, 12);
            this.tsslServerStartTime.TabIndex = 7;
            this.tsslServerStartTime.Text = "label1";
            // 
            // tsslversion
            // 
            this.tsslversion.AutoSize = true;
            this.tsslversion.Location = new System.Drawing.Point(34, 483);
            this.tsslversion.Name = "tsslversion";
            this.tsslversion.Size = new System.Drawing.Size(41, 12);
            this.tsslversion.TabIndex = 7;
            this.tsslversion.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(512, 180);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 29);
            this.button1.TabIndex = 8;
            this.button1.Text = "写入";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtWriteTagValue
            // 
            this.txtWriteTagValue.Location = new System.Drawing.Point(512, 138);
            this.txtWriteTagValue.Name = "txtWriteTagValue";
            this.txtWriteTagValue.Size = new System.Drawing.Size(62, 21);
            this.txtWriteTagValue.TabIndex = 9;
            // 
            // lsbGroups
            // 
            this.lsbGroups.FormattingEnabled = true;
            this.lsbGroups.ItemHeight = 12;
            this.lsbGroups.Location = new System.Drawing.Point(35, 138);
            this.lsbGroups.Name = "lsbGroups";
            this.lsbGroups.Size = new System.Drawing.Size(106, 112);
            this.lsbGroups.TabIndex = 10;
            this.lsbGroups.SelectedIndexChanged += new System.EventHandler(this.lsbGroups_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(486, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tag值";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(486, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "品质";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(486, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "时间错";
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(36, 317);
            this.listBox2.Name = "listBox2";
            this.listBox2.ScrollAlwaysVisible = true;
            this.listBox2.Size = new System.Drawing.Size(158, 100);
            this.listBox2.TabIndex = 11;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(268, 303);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(359, 177);
            this.listView1.TabIndex = 12;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 504);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.lsbGroups);
            this.Controls.Add(this.txtWriteTagValue);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tsslversion);
            this.Controls.Add(this.tsslServerStartTime);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.txtRemoteServerIP);
            this.Controls.Add(this.txtTimeStamps);
            this.Controls.Add(this.txtQualities);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTagValue);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.tsslServerState);
            this.Controls.Add(this.btnConnServer);
            this.Controls.Add(this.cmbServerName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbServerName;
        private System.Windows.Forms.Button btnConnServer;
        private System.Windows.Forms.Label tsslServerState;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label txtTagValue;
        private System.Windows.Forms.Label txtQualities;
        private System.Windows.Forms.Label txtTimeStamps;
        private System.Windows.Forms.TextBox txtRemoteServerIP;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label tsslServerStartTime;
        private System.Windows.Forms.Label tsslversion;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtWriteTagValue;
        private System.Windows.Forms.ListBox lsbGroups;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListView listView1;
    }
}

