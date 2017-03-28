namespace BND.Services.IbanStore.ProxyTestApp
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
            this.comboaction = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUidPrefix = new System.Windows.Forms.TextBox();
            this.txtUid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtApiUrl = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboaction
            // 
            this.comboaction.FormattingEnabled = true;
            this.comboaction.Items.AddRange(new object[] {
            "reserve",
            "assign",
            "get",
            "ReserveAndAsssign"});
            this.comboaction.Location = new System.Drawing.Point(97, 126);
            this.comboaction.Name = "comboaction";
            this.comboaction.Size = new System.Drawing.Size(121, 21);
            this.comboaction.TabIndex = 0;
            this.comboaction.SelectedIndexChanged += new System.EventHandler(this.comboaction_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Action";
            // 
            // txtUidPrefix
            // 
            this.txtUidPrefix.Location = new System.Drawing.Point(296, 126);
            this.txtUidPrefix.Name = "txtUidPrefix";
            this.txtUidPrefix.Size = new System.Drawing.Size(100, 20);
            this.txtUidPrefix.TabIndex = 2;
            this.txtUidPrefix.Text = "VO";
            // 
            // txtUid
            // 
            this.txtUid.Location = new System.Drawing.Point(454, 126);
            this.txtUid.Name = "txtUid";
            this.txtUid.Size = new System.Drawing.Size(100, 20);
            this.txtUid.TabIndex = 3;
            this.txtUid.Text = "1234";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "UriPrefix";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(425, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Uid";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(479, 446);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 6;
            this.btnRun.Text = "Execute";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(97, 236);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(100, 20);
            this.txtStatus.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 239);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Status";
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(301, 233);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(253, 20);
            this.txtLocation.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(247, 236);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Location";
            // 
            // txtResponse
            // 
            this.txtResponse.Location = new System.Drawing.Point(97, 292);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.Size = new System.Drawing.Size(457, 138);
            this.txtResponse.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 295);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Response";
            // 
            // txtApiUrl
            // 
            this.txtApiUrl.Location = new System.Drawing.Point(97, 69);
            this.txtApiUrl.Name = "txtApiUrl";
            this.txtApiUrl.Size = new System.Drawing.Size(457, 20);
            this.txtApiUrl.TabIndex = 13;
            this.txtApiUrl.Text = "http://localhost:50421/";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Api Address";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 481);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtApiUrl);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUid);
            this.Controls.Add(this.txtUidPrefix);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboaction);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboaction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUidPrefix;
        private System.Windows.Forms.TextBox txtUid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtApiUrl;
        private System.Windows.Forms.Label label7;
    }
}

