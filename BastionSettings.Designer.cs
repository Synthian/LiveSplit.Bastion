namespace LiveSplit.Bastion.Settings {
	partial class BastionSettings {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.flowMain = new System.Windows.Forms.FlowLayoutPanel();
            this.flowOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.chkClassic = new System.Windows.Forms.CheckBox();
            this.chkStart = new System.Windows.Forms.CheckBox();
            this.chkEnd = new System.Windows.Forms.CheckBox();
            this.chkReset = new System.Windows.Forms.CheckBox();
            this.chkSplit = new System.Windows.Forms.CheckBox();
            this.chkTazal = new System.Windows.Forms.CheckBox();
            this.chkRam = new System.Windows.Forms.CheckBox();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.chkSoleRegret = new System.Windows.Forms.CheckBox();
            this.flowMain.SuspendLayout();
            this.flowOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowMain
            // 
            this.flowMain.AutoSize = true;
            this.flowMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowMain.Controls.Add(this.flowOptions);
            this.flowMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowMain.Location = new System.Drawing.Point(0, 0);
            this.flowMain.Margin = new System.Windows.Forms.Padding(0);
            this.flowMain.Name = "flowMain";
            this.flowMain.Size = new System.Drawing.Size(279, 107);
            this.flowMain.TabIndex = 0;
            this.flowMain.WrapContents = false;
            // 
            // flowOptions
            // 
            this.flowOptions.Controls.Add(this.chkClassic);
            this.flowOptions.Controls.Add(this.chkStart);
            this.flowOptions.Controls.Add(this.chkEnd);
            this.flowOptions.Controls.Add(this.chkReset);
            this.flowOptions.Controls.Add(this.chkSplit);
            this.flowOptions.Controls.Add(this.chkSoleRegret);
            this.flowOptions.Controls.Add(this.chkTazal);
            this.flowOptions.Controls.Add(this.chkRam);
            this.flowOptions.Location = new System.Drawing.Point(0, 0);
            this.flowOptions.Margin = new System.Windows.Forms.Padding(0);
            this.flowOptions.Name = "flowOptions";
            this.flowOptions.Size = new System.Drawing.Size(279, 107);
            this.flowOptions.TabIndex = 0;
            // 
            // chkClassic
            // 
            this.chkClassic.AutoSize = true;
            this.chkClassic.Location = new System.Drawing.Point(3, 3);
            this.chkClassic.Name = "chkClassic";
            this.chkClassic.Size = new System.Drawing.Size(242, 17);
            this.chkClassic.TabIndex = 10;
            this.chkClassic.Text = "Split using the Skyway method (Classic timing)";
            this.toolTips.SetToolTip(this.chkClassic, "This is the way that most people split.");
            this.chkClassic.UseVisualStyleBackColor = true;
            this.chkClassic.CheckedChanged += new System.EventHandler(this.chkBox_CheckedChanged);
            // 
            // chkStart
            // 
            this.chkStart.AutoSize = true;
            this.chkStart.Location = new System.Drawing.Point(3, 26);
            this.chkStart.Name = "chkStart";
            this.chkStart.Size = new System.Drawing.Size(48, 17);
            this.chkStart.TabIndex = 7;
            this.chkStart.Text = "Start";
            this.toolTips.SetToolTip(this.chkStart, "Splits upon gaining control of the Kid in the Rippling Walls");
            this.chkStart.UseVisualStyleBackColor = true;
            this.chkStart.CheckedChanged += new System.EventHandler(this.chkBox_CheckedChanged);
            // 
            // chkEnd
            // 
            this.chkEnd.AutoSize = true;
            this.chkEnd.Location = new System.Drawing.Point(57, 26);
            this.chkEnd.Name = "chkEnd";
            this.chkEnd.Size = new System.Drawing.Size(45, 17);
            this.chkEnd.TabIndex = 1;
            this.chkEnd.Text = "End";
            this.toolTips.SetToolTip(this.chkEnd, "Splits when you lose control in the Heart of the Bastion. It will split early if " +
        "you choose a dialogue option.");
            this.chkEnd.UseVisualStyleBackColor = true;
            this.chkEnd.CheckedChanged += new System.EventHandler(this.chkBox_CheckedChanged);
            // 
            // chkReset
            // 
            this.chkReset.AutoSize = true;
            this.chkReset.Location = new System.Drawing.Point(108, 26);
            this.chkReset.Name = "chkReset";
            this.chkReset.Size = new System.Drawing.Size(54, 17);
            this.chkReset.TabIndex = 8;
            this.chkReset.Text = "Reset";
            this.toolTips.SetToolTip(this.chkReset, "Resets the splits whenever you load the Rippling Walls.");
            this.chkReset.UseVisualStyleBackColor = true;
            this.chkReset.CheckedChanged += new System.EventHandler(this.chkBox_CheckedChanged);
            // 
            // chkSplit
            // 
            this.chkSplit.AutoSize = true;
            this.chkSplit.Location = new System.Drawing.Point(168, 26);
            this.chkSplit.Name = "chkSplit";
            this.chkSplit.Size = new System.Drawing.Size(46, 17);
            this.chkSplit.TabIndex = 9;
            this.chkSplit.Text = "Split";
            this.toolTips.SetToolTip(this.chkSplit, "Split when you finish each level");
            this.chkSplit.UseVisualStyleBackColor = true;
            this.chkSplit.CheckedChanged += new System.EventHandler(this.chkBox_CheckedChanged);
            // 
            // chkTazal
            // 
            this.chkTazal.Location = new System.Drawing.Point(142, 49);
            this.chkTazal.Name = "chkTazal";
            this.chkTazal.Size = new System.Drawing.Size(111, 17);
            this.chkTazal.TabIndex = 6;
            this.chkTazal.Text = "Split Tazal I";
            this.toolTips.SetToolTip(this.chkTazal, "Splits when loading the map where you pick up the Battering Ram. This is the midw" +
        "ay point of Tazal Terminals.");
            this.chkTazal.UseVisualStyleBackColor = true;
            this.chkTazal.CheckedChanged += new System.EventHandler(this.chkBox_CheckedChanged);
            // 
            // chkRam
            // 
            this.chkRam.Location = new System.Drawing.Point(3, 72);
            this.chkRam.Name = "chkRam";
            this.chkRam.Size = new System.Drawing.Size(215, 21);
            this.chkRam.TabIndex = 11;
            this.chkRam.Text = "Split upon picking up the Battering Ram";
            this.toolTips.SetToolTip(this.chkRam, "As done in valentinoIAN\'s ASL runs");
            this.chkRam.UseVisualStyleBackColor = true;
            this.chkRam.CheckedChanged += new System.EventHandler(this.chkBox_CheckedChanged);
            // 
            // chkSoleRegret
            // 
            this.chkSoleRegret.Location = new System.Drawing.Point(3, 49);
            this.chkSoleRegret.Name = "chkSoleRegret";
            this.chkSoleRegret.Size = new System.Drawing.Size(133, 17);
            this.chkSoleRegret.TabIndex = 12;
            this.chkSoleRegret.Text = "Split after Sole Regret";
            this.toolTips.SetToolTip(this.chkSoleRegret, "Splits when you exit the Sole Regret (Rondy\'s Bar)");
            this.chkSoleRegret.UseVisualStyleBackColor = true;
            this.chkSoleRegret.CheckedChanged += new System.EventHandler(this.chkBox_CheckedChanged);
            // 
            // BastionSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.flowMain);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "BastionSettings";
            this.Size = new System.Drawing.Size(279, 107);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.flowMain.ResumeLayout(false);
            this.flowOptions.ResumeLayout(false);
            this.flowOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.FlowLayoutPanel flowMain;
		private System.Windows.Forms.FlowLayoutPanel flowOptions;
		private System.Windows.Forms.CheckBox chkEnd;
		private System.Windows.Forms.CheckBox chkTazal;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.CheckBox chkStart;
        private System.Windows.Forms.CheckBox chkSplit;
        private System.Windows.Forms.CheckBox chkReset;
        private System.Windows.Forms.CheckBox chkClassic;
        private System.Windows.Forms.CheckBox chkRam;
        private System.Windows.Forms.CheckBox chkSoleRegret;
    }
}
