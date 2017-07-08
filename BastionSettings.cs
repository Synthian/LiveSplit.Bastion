using System;
using System.Windows.Forms;
using System.Xml;
namespace LiveSplit.Bastion.Settings
{
    public partial class BastionSettings : UserControl
    {
        public bool Reset { get; set; }
        public bool Start { get; set; }
        public bool End { get; set; }
        public bool Split { get; set; }
        public bool Tazal { get; set; }
        public bool Ram { get; set; }
        public bool SoleRegret { get; set; }
        public bool IL { get; set; }
        private BastionComponent component;
        private bool isLoading;

        public BastionSettings(BastionComponent comp)
        {
            isLoading = true;
            InitializeComponent();

            //Defaults
            Reset = true;
            Start = true;
            End = true;
            Split = true;
            Tazal = true;
            Ram = false;
            SoleRegret = false;
            IL = false;

            component = comp;
            isLoading = false;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            isLoading = true;
            LoadSettings();
            isLoading = false;
        }
        public void LoadSettings()
        {
            chkEnd.Checked = End;
            chkStart.Checked = Start;
            chkReset.Checked = Reset;
            chkSplit.Checked = Split;
            chkTazal.Checked = Tazal;
            chkSoleRegret.Checked = SoleRegret;
            chkRam.Checked = Ram;
            chkIL.Checked = IL;
        }
        private void chkBox_CheckedChanged(object sender, EventArgs e)
        {
            chkReset.Enabled = !chkIL.Checked;
            chkStart.Enabled = !chkIL.Checked;
            chkEnd.Enabled = !chkIL.Checked;
            chkSplit.Enabled = !chkIL.Checked;
            chkTazal.Enabled = chkSplit.Checked && !chkIL.Checked;
            chkRam.Enabled = chkSplit.Checked && !chkIL.Checked;
            chkSoleRegret.Enabled = chkSplit.Checked && !chkIL.Checked;

            UpdateSplits();
        }
        public void UpdateSplits()
        {
            if (isLoading) return;

            SoleRegret = chkSoleRegret.Checked;
            Ram = chkRam.Checked;
            Reset = chkReset.Checked;
            Start = chkStart.Checked;
            End = chkEnd.Checked;
            Split = chkSplit.Checked;
            Tazal = chkTazal.Checked;
        }
        public XmlNode UpdateSettings(XmlDocument document)
        {
            XmlElement xmlSettings = document.CreateElement("Settings");

            SetSetting(document, xmlSettings, chkSoleRegret, "SoleRegret");
            SetSetting(document, xmlSettings, chkRam, "Ram");
            SetSetting(document, xmlSettings, chkReset, "Reset");
            SetSetting(document, xmlSettings, chkStart, "Start");
            SetSetting(document, xmlSettings, chkEnd, "End");
            SetSetting(document, xmlSettings, chkSplit, "Split");
            SetSetting(document, xmlSettings, chkTazal, "Tazal");
            SetSetting(document, xmlSettings, chkIL, "IL");

            return xmlSettings;
        }
        private void SetSetting(XmlDocument document, XmlElement settings, CheckBox chk, string name)
        {
            XmlElement xmlOption = document.CreateElement(name);
            xmlOption.InnerText = chk.Checked.ToString();
            settings.AppendChild(xmlOption);
        }
        public void SetSettings(XmlNode settings)
        {
            SoleRegret = GetSetting(settings, "//SoleRegret");
            Ram = GetSetting(settings, "//Ram");
            Reset = GetSetting(settings, "//Reset");
            Start = GetSetting(settings, "//Start");
            End = GetSetting(settings, "//End");
            Split = GetSetting(settings, "//Split");
            Tazal = GetSetting(settings, "//Tazal");
            IL = GetSetting(settings, "//IL");
        }
        private bool GetSetting(XmlNode settings, string name)
        {
            XmlNode option = settings.SelectSingleNode(name);
            if (option != null && option.InnerText != "")
            {
                return bool.Parse(option.InnerText);
            }
            return false;
        }
    }
}