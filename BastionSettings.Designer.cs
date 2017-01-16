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
            this.chkRockInSky = new System.Windows.Forms.CheckBox();
            this.chkTazal = new System.Windows.Forms.CheckBox();
            this.chkEnd = new System.Windows.Forms.CheckBox();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.chkStart = new System.Windows.Forms.CheckBox();
            this.chkReset = new System.Windows.Forms.CheckBox();
            this.chkTown = new System.Windows.Forms.CheckBox();
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
            this.flowMain.Size = new System.Drawing.Size(279, 78);
            this.flowMain.TabIndex = 0;
            this.flowMain.WrapContents = false;
            // 
            // flowOptions
            // 
            this.flowOptions.Controls.Add(this.chkStart);
            this.flowOptions.Controls.Add(this.chkEnd);
            this.flowOptions.Controls.Add(this.chkReset);
            this.flowOptions.Controls.Add(this.chkTown);
            this.flowOptions.Controls.Add(this.chkRockInSky);
            this.flowOptions.Controls.Add(this.chkTazal);
            this.flowOptions.Location = new System.Drawing.Point(0, 0);
            this.flowOptions.Margin = new System.Windows.Forms.Padding(0);
            this.flowOptions.Name = "flowOptions";
            this.flowOptions.Size = new System.Drawing.Size(279, 78);
            this.flowOptions.TabIndex = 0;
            // 
            // chkRockInSky
            // 
            this.chkRockInSky.AutoSize = true;
            this.chkRockInSky.Location = new System.Drawing.Point(3, 26);
            this.chkRockInSky.Name = "chkRockInSky";
            this.chkRockInSky.Size = new System.Drawing.Size(151, 17);
            this.chkRockInSky.TabIndex = 2;
            this.chkRockInSky.Text = "Split after The Sole Regret";
            this.toolTips.SetToolTip(this.chkRockInSky, "Splits upon loading the Wharf District map. Most people don\'t split here.");
            this.chkRockInSky.UseVisualStyleBackColor = true;
            this.chkRockInSky.CheckedChanged += new System.EventHandler(this.chkBox_CheckedChanged);
            // 
            // chkTazal
            // 
            this.chkTazal.Location = new System.Drawing.Point(3, 49);
            this.chkTazal.Name = "chkTazal";
            this.chkTazal.Size = new System.Drawing.Size(228, 17);
            this.chkTazal.TabIndex = 6;
            this.chkTazal.Text = "Split Tazal I";
            this.toolTips.SetToolTip(this.chkTazal, "Splits when loading the map where you pick up the Battering Ram. This is the midw" +
        "ay point of Tazal Terminals.");
            this.chkTazal.UseVisualStyleBackColor = true;
            this.chkTazal.CheckedChanged += new System.EventHandler(this.chkBox_CheckedChanged);
            // 
            // chkEnd
            // 
            this.chkEnd.AutoSize = true;
            this.chkEnd.Location = new System.Drawing.Point(57, 3);
            this.chkEnd.Name = "chkEnd";
            this.chkEnd.Size = new System.Drawing.Size(45, 17);
            this.chkEnd.TabIndex = 1;
            this.chkEnd.Text = "End";
            this.toolTips.SetToolTip(this.chkEnd, "Splits when you lose control in the Heart of the Bastion. It will split early if " +
        "you choose a dialogue option.");
            this.chkEnd.UseVisualStyleBackColor = true;
            this.chkEnd.CheckedChanged += new System.EventHandler(this.chkBox_CheckedChanged);
            // 
            // chkStart
            // 
            this.chkStart.AutoSize = true;
            this.chkStart.Location = new System.Drawing.Point(3, 3);
            this.chkStart.Name = "chkStart";
            this.chkStart.Size = new System.Drawing.Size(48, 17);
            this.chkStart.TabIndex = 7;
            this.chkStart.Text = "Start";
            this.toolTips.SetToolTip(this.chkStart, "Splits upon gaining control of the Kid in the Rippling Walls");
            this.chkStart.UseVisualStyleBackColor = true;
            // 
            // chkReset
            // 
            this.chkReset.AutoSize = true;
            this.chkReset.Location = new System.Drawing.Point(108, 3);
            this.chkReset.Name = "chkReset";
            this.chkReset.Size = new System.Drawing.Size(54, 17);
            this.chkReset.TabIndex = 8;
            this.chkReset.Text = "Reset";
            this.toolTips.SetToolTip(this.chkReset, "Resets the splits whenever you load the Rippling Walls.");
            this.chkReset.UseVisualStyleBackColor = true;
            // 
            // chkTown
            // 
            this.chkTown.AutoSize = true;
            this.chkTown.Location = new System.Drawing.Point(168, 3);
            this.chkTown.Name = "chkTown";
            this.chkTown.Size = new System.Drawing.Size(84, 17);
            this.chkTown.TabIndex = 9;
            this.chkTown.Text = "Split Bastion";
            this.toolTips.SetToolTip(this.chkTown, "Split when loading the Bastion. Core functionality of the autosplitter.");
            this.chkTown.UseVisualStyleBackColor = true;
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
            this.Size = new System.Drawing.Size(279, 78);
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
		private System.Windows.Forms.CheckBox chkRockInSky;
		private System.Windows.Forms.CheckBox chkTazal;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.CheckBox chkStart;
        private System.Windows.Forms.CheckBox chkTown;
        private System.Windows.Forms.CheckBox chkReset;
    }
}
