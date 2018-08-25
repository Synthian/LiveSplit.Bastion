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
            this.grpboxMode = new System.Windows.Forms.GroupBox();
            this.radioIL_Mode = new System.Windows.Forms.RadioButton();
            this.radioLoad_Mode = new System.Windows.Forms.RadioButton();
            this.radioSkyway_Mode = new System.Windows.Forms.RadioButton();
            this.grpboxBasic = new System.Windows.Forms.GroupBox();
            this.chkStart = new System.Windows.Forms.CheckBox();
            this.chkSplit = new System.Windows.Forms.CheckBox();
            this.chkReset = new System.Windows.Forms.CheckBox();
            this.grpboxSpecial = new System.Windows.Forms.GroupBox();
            this.chkSoleRegret = new System.Windows.Forms.CheckBox();
            this.chkRam = new System.Windows.Forms.CheckBox();
            this.chkTazal = new System.Windows.Forms.CheckBox();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.chkEnd = new System.Windows.Forms.CheckBox();
            this.flowMain.SuspendLayout();
            this.flowOptions.SuspendLayout();
            this.grpboxMode.SuspendLayout();
            this.grpboxBasic.SuspendLayout();
            this.grpboxSpecial.SuspendLayout();
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
            this.flowMain.Size = new System.Drawing.Size(354, 195);
            this.flowMain.TabIndex = 0;
            this.flowMain.WrapContents = false;
            // 
            // flowOptions
            // 
            this.flowOptions.Controls.Add(this.grpboxMode);
            this.flowOptions.Controls.Add(this.grpboxBasic);
            this.flowOptions.Controls.Add(this.grpboxSpecial);
            this.flowMain.SetFlowBreak(this.flowOptions, true);
            this.flowOptions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowOptions.Location = new System.Drawing.Point(0, 0);
            this.flowOptions.Margin = new System.Windows.Forms.Padding(0);
            this.flowOptions.Name = "flowOptions";
            this.flowOptions.Size = new System.Drawing.Size(354, 195);
            this.flowOptions.TabIndex = 0;
            // 
            // grpboxMode
            // 
            this.grpboxMode.AutoSize = true;
            this.grpboxMode.Controls.Add(this.radioIL_Mode);
            this.grpboxMode.Controls.Add(this.radioLoad_Mode);
            this.grpboxMode.Controls.Add(this.radioSkyway_Mode);
            this.flowOptions.SetFlowBreak(this.grpboxMode, true);
            this.grpboxMode.Location = new System.Drawing.Point(3, 3);
            this.grpboxMode.Name = "grpboxMode";
            this.grpboxMode.Size = new System.Drawing.Size(90, 101);
            this.grpboxMode.TabIndex = 17;
            this.grpboxMode.TabStop = false;
            this.grpboxMode.Text = "Split Mode";
            // 
            // radioIL_Mode
            // 
            this.radioIL_Mode.AutoSize = true;
            this.radioIL_Mode.Location = new System.Drawing.Point(6, 19);
            this.radioIL_Mode.Name = "radioIL_Mode";
            this.radioIL_Mode.Size = new System.Drawing.Size(34, 17);
            this.radioIL_Mode.TabIndex = 16;
            this.radioIL_Mode.TabStop = true;
            this.radioIL_Mode.Text = "IL";
            this.toolTips.SetToolTip(this.radioIL_Mode, "Splits upon entering a level from the bastion, and upon leaving levels.\r\n\r\nIgnore" +
        "s all other settings.");
            this.radioIL_Mode.UseVisualStyleBackColor = true;
            this.radioIL_Mode.CheckedChanged += new System.EventHandler(this.radioIL_Mode_CheckedChanged);
            // 
            // radioLoad_Mode
            // 
            this.radioLoad_Mode.AutoSize = true;
            this.radioLoad_Mode.Location = new System.Drawing.Point(6, 65);
            this.radioLoad_Mode.Name = "radioLoad_Mode";
            this.radioLoad_Mode.Size = new System.Drawing.Size(78, 17);
            this.radioLoad_Mode.TabIndex = 14;
            this.radioLoad_Mode.TabStop = true;
            this.radioLoad_Mode.Text = "Level Load";
            this.toolTips.SetToolTip(this.radioLoad_Mode, "Splits upon the start of loading at the end of each applicable level.");
            this.radioLoad_Mode.UseVisualStyleBackColor = true;
            this.radioLoad_Mode.CheckedChanged += new System.EventHandler(this.radioLoad_Mode_CheckedChanged);
            // 
            // radioSkyway_Mode
            // 
            this.radioSkyway_Mode.AutoSize = true;
            this.radioSkyway_Mode.Location = new System.Drawing.Point(6, 42);
            this.radioSkyway_Mode.Name = "radioSkyway_Mode";
            this.radioSkyway_Mode.Size = new System.Drawing.Size(62, 17);
            this.radioSkyway_Mode.TabIndex = 15;
            this.radioSkyway_Mode.TabStop = true;
            this.radioSkyway_Mode.Text = "Skyway";
            this.toolTips.SetToolTip(this.radioSkyway_Mode, "Splits upon losing control of the kid at the end of every applicable level.");
            this.radioSkyway_Mode.UseVisualStyleBackColor = true;
            this.radioSkyway_Mode.CheckedChanged += new System.EventHandler(this.radioSkyway_Mode_CheckedChanged);
            // 
            // grpboxBasic
            // 
            this.grpboxBasic.AutoSize = true;
            this.grpboxBasic.Controls.Add(this.chkEnd);
            this.grpboxBasic.Controls.Add(this.chkStart);
            this.grpboxBasic.Controls.Add(this.chkSplit);
            this.grpboxBasic.Controls.Add(this.chkReset);
            this.flowOptions.SetFlowBreak(this.grpboxBasic, true);
            this.grpboxBasic.Location = new System.Drawing.Point(99, 3);
            this.grpboxBasic.Name = "grpboxBasic";
            this.grpboxBasic.Size = new System.Drawing.Size(66, 125);
            this.grpboxBasic.TabIndex = 18;
            this.grpboxBasic.TabStop = false;
            this.grpboxBasic.Text = "Basic";
            // 
            // chkStart
            // 
            this.chkStart.AutoSize = true;
            this.chkStart.Location = new System.Drawing.Point(6, 19);
            this.chkStart.Name = "chkStart";
            this.chkStart.Size = new System.Drawing.Size(48, 17);
            this.chkStart.TabIndex = 7;
            this.chkStart.Text = "Start";
            this.toolTips.SetToolTip(this.chkStart, "Splits upon gaining control of the Kid in the Rippling Walls");
            this.chkStart.UseVisualStyleBackColor = true;
            // 
            // chkSplit
            // 
            this.chkSplit.AutoSize = true;
            this.chkSplit.Location = new System.Drawing.Point(6, 66);
            this.chkSplit.Name = "chkSplit";
            this.chkSplit.Size = new System.Drawing.Size(46, 17);
            this.chkSplit.TabIndex = 9;
            this.chkSplit.Text = "Split";
            this.toolTips.SetToolTip(this.chkSplit, "Split when you finish each level");
            this.chkSplit.UseVisualStyleBackColor = true;
            // 
            // chkReset
            // 
            this.chkReset.AutoSize = true;
            this.chkReset.Location = new System.Drawing.Point(6, 43);
            this.chkReset.Name = "chkReset";
            this.chkReset.Size = new System.Drawing.Size(54, 17);
            this.chkReset.TabIndex = 8;
            this.chkReset.Text = "Reset";
            this.toolTips.SetToolTip(this.chkReset, "Resets the splits whenever you load the Rippling Walls.");
            this.chkReset.UseVisualStyleBackColor = true;
            // 
            // grpboxSpecial
            // 
            this.grpboxSpecial.AutoSize = true;
            this.grpboxSpecial.Controls.Add(this.chkSoleRegret);
            this.grpboxSpecial.Controls.Add(this.chkRam);
            this.grpboxSpecial.Controls.Add(this.chkTazal);
            this.grpboxSpecial.Location = new System.Drawing.Point(171, 3);
            this.grpboxSpecial.Name = "grpboxSpecial";
            this.grpboxSpecial.Size = new System.Drawing.Size(143, 101);
            this.grpboxSpecial.TabIndex = 19;
            this.grpboxSpecial.TabStop = false;
            this.grpboxSpecial.Text = "Special Splits";
            // 
            // chkSoleRegret
            // 
            this.chkSoleRegret.AutoSize = true;
            this.chkSoleRegret.Location = new System.Drawing.Point(6, 19);
            this.chkSoleRegret.Name = "chkSoleRegret";
            this.chkSoleRegret.Size = new System.Drawing.Size(129, 17);
            this.chkSoleRegret.TabIndex = 12;
            this.chkSoleRegret.Text = "Split after Sole Regret";
            this.toolTips.SetToolTip(this.chkSoleRegret, "Splits when you exit the Sole Regret (Rondy\'s Bar)");
            this.chkSoleRegret.UseVisualStyleBackColor = true;
            // 
            // chkRam
            // 
            this.chkRam.AutoSize = true;
            this.chkRam.Location = new System.Drawing.Point(6, 65);
            this.chkRam.Name = "chkRam";
            this.chkRam.Size = new System.Drawing.Size(131, 17);
            this.chkRam.TabIndex = 11;
            this.chkRam.Text = "Split on Battering Ram";
            this.toolTips.SetToolTip(this.chkRam, "As done in valentinoIAN\'s ASL runs");
            this.chkRam.UseVisualStyleBackColor = true;
            // 
            // chkTazal
            // 
            this.chkTazal.AutoSize = true;
            this.chkTazal.Location = new System.Drawing.Point(6, 42);
            this.chkTazal.Name = "chkTazal";
            this.chkTazal.Size = new System.Drawing.Size(81, 17);
            this.chkTazal.TabIndex = 6;
            this.chkTazal.Text = "Split Tazal I";
            this.toolTips.SetToolTip(this.chkTazal, "Splits when loading the map where you pick up the Battering Ram. This is the midw" +
        "ay point of Tazal Terminals.");
            this.chkTazal.UseVisualStyleBackColor = true;
            // 
            // chkEnd
            // 
            this.chkEnd.AutoSize = true;
            this.chkEnd.Location = new System.Drawing.Point(6, 89);
            this.chkEnd.Name = "chkEnd";
            this.chkEnd.Size = new System.Drawing.Size(45, 17);
            this.chkEnd.TabIndex = 10;
            this.chkEnd.Text = "End";
            this.toolTips.SetToolTip(this.chkEnd, "Splits at the end of the game.");
            this.chkEnd.UseVisualStyleBackColor = true;
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
            this.Size = new System.Drawing.Size(354, 195);
            this.Load += new System.EventHandler(this.BastionSettings_Load);
            this.flowMain.ResumeLayout(false);
            this.flowOptions.ResumeLayout(false);
            this.flowOptions.PerformLayout();
            this.grpboxMode.ResumeLayout(false);
            this.grpboxMode.PerformLayout();
            this.grpboxBasic.ResumeLayout(false);
            this.grpboxBasic.PerformLayout();
            this.grpboxSpecial.ResumeLayout(false);
            this.grpboxSpecial.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.FlowLayoutPanel flowMain;
		private System.Windows.Forms.FlowLayoutPanel flowOptions;
		private System.Windows.Forms.CheckBox chkTazal;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.CheckBox chkStart;
        private System.Windows.Forms.CheckBox chkSplit;
        private System.Windows.Forms.CheckBox chkReset;
        private System.Windows.Forms.CheckBox chkRam;
        private System.Windows.Forms.CheckBox chkSoleRegret;
        private System.Windows.Forms.RadioButton radioIL_Mode;
        private System.Windows.Forms.RadioButton radioSkyway_Mode;
        private System.Windows.Forms.RadioButton radioLoad_Mode;
        private System.Windows.Forms.GroupBox grpboxMode;
        private System.Windows.Forms.GroupBox grpboxBasic;
        private System.Windows.Forms.GroupBox grpboxSpecial;
        private System.Windows.Forms.CheckBox chkEnd;
    }
}
