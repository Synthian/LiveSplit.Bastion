using System;
using System.Windows.Forms;

namespace LiveSplit.Bastion.Settings
{
    public partial class BastionSettings : UserControl
    {
        public bool IL_Mode { get; set; }
        public bool Skyway_Mode { get; set; }
        public bool Load_Mode { get; set; }

        public bool Reset { get; set; }
        public bool Start { get; set; }
        public bool Split { get; set; }

        public bool Tazal { get; set; }
        public bool Ram { get; set; }
        public bool SoleRegret { get; set; }

        public BastionSettings()
        {
            InitializeComponent();

            chkReset.DataBindings.Add("Checked", this, "Reset", false, DataSourceUpdateMode.OnPropertyChanged);
            chkStart.DataBindings.Add("Checked", this, "Start", false, DataSourceUpdateMode.OnPropertyChanged);
            chkSplit.DataBindings.Add("Checked", this, "Split", false, DataSourceUpdateMode.OnPropertyChanged);
            chkTazal.DataBindings.Add("Checked", this, "Tazal", false, DataSourceUpdateMode.OnPropertyChanged);
            chkRam.DataBindings.Add("Checked", this, "Ram", false, DataSourceUpdateMode.OnPropertyChanged);
            chkSoleRegret.DataBindings.Add("Checked", this, "SoleRegret", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void BastionSettings_Load(object sender, EventArgs e)
        {
            radioIL_Mode.Checked = IL_Mode;
            radioSkyway_Mode.Checked = Skyway_Mode;
            radioLoad_Mode.Checked = Load_Mode;
        }

        void UpdateMode()
        {
            IL_Mode = radioIL_Mode.Checked;
            Skyway_Mode = radioSkyway_Mode.Checked;
            Load_Mode = radioLoad_Mode.Checked;
        }

        public System.Xml.XmlNode GetSettings(System.Xml.XmlDocument document)
        {
            var settingsNode = document.CreateElement("Settings");

            var il_modeNode = document.CreateElement("IL_Mode");
            il_modeNode.InnerText = IL_Mode ? "True" : "False";
            settingsNode.AppendChild(il_modeNode);

            var skyway_modeNode = document.CreateElement("Skyway_Mode");
            skyway_modeNode.InnerText = Skyway_Mode ? "True" : "False";
            settingsNode.AppendChild(skyway_modeNode);

            var load_modeNode = document.CreateElement("Load_Mode");
            load_modeNode.InnerText = Load_Mode ? "True" : "False";
            settingsNode.AppendChild(load_modeNode);

            var resetNode = document.CreateElement("Reset");
            resetNode.InnerText = Reset ? "True" : "False";
            settingsNode.AppendChild(resetNode);

            var startNode = document.CreateElement("Start");
            startNode.InnerText = Start ? "True" : "False";
            settingsNode.AppendChild(startNode);

            var splitNode = document.CreateElement("Split");
            splitNode.InnerText = Split ? "True" : "False";
            settingsNode.AppendChild(splitNode);

            var tazalNode = document.CreateElement("Tazal");
            tazalNode.InnerText = Tazal ? "True" : "False";
            settingsNode.AppendChild(tazalNode);

            var ramNode = document.CreateElement("Ram");
            ramNode.InnerText = Ram ? "True" : "False";
            settingsNode.AppendChild(ramNode);

            var soleregretNode = document.CreateElement("SoleRegret");
            soleregretNode.InnerText = SoleRegret ? "True" : "False";
            settingsNode.AppendChild(soleregretNode);

            return settingsNode;
        }

        public void SetSettings(System.Xml.XmlNode settings)
        {
            try {
                IL_Mode = (settings["IL_Mode"].InnerText == "True");
                Skyway_Mode = (settings["Skyway_Mode"].InnerText == "True");
                Load_Mode = (settings["Load_Mode"].InnerText == "True");
            } catch (NullReferenceException nre) {
                IL_Mode = false;
                Skyway_Mode = true;
                Load_Mode = false;
            }

            try {
                Reset = (settings["Reset"].InnerText == "True");
                Start = (settings["Start"].InnerText == "True");
                Split = (settings["Split"].InnerText == "True");
                Tazal = (settings["Tazal"].InnerText == "True");
                Ram = (settings["Ram"].InnerText == "True");
                SoleRegret = (settings["SoleRegret"].InnerText == "True");
            } catch (NullReferenceException nre) {
                Reset = true;
                Start = true;
                Split = true;
                Tazal = false;
                Ram = false;
                SoleRegret = false;
            }
        }

        private void radioIL_Mode_CheckedChanged(object sender, EventArgs e)
        {
            UpdateMode();
        }

        private void radioSkyway_Mode_CheckedChanged(object sender, EventArgs e)
        {
            UpdateMode();
        }

        private void radioLoad_Mode_CheckedChanged(object sender, EventArgs e)
        {
            UpdateMode();
        }


    }
}