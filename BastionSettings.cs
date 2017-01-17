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
        public bool Classic { get; set; }
        public bool Ram { get; set; }
        private BastionComponent component;
        private bool isLoading;

        public BastionSettings(BastionComponent comp)
        {
            isLoading = true;
            InitializeComponent();

            //Defaults
            Classic = true;
            Reset = true;
            Start = true;
            End = true;
            Split = true;

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
            chkRam.Checked = Ram;
            chkEnd.Checked = End;
            chkClassic.Checked = Classic;
            chkStart.Checked = Start;
            chkReset.Checked = Reset;
            chkSplit.Checked = Split;
            chkTazal.Checked = Tazal;
        }
        private void chkBox_CheckedChanged(object sender, EventArgs e)
        {
            chkTazal.Enabled = chkSplit.Checked;
            chkRam.Enabled = chkSplit.Checked;

            if (!chkSplit.Checked)
            {
                chkTazal.Checked = false;
                chkRam.Checked = false;
            }

            UpdateSplits();
        }
        public void UpdateSplits()
        {
            if (isLoading) return;

            Ram = chkRam.Checked;
            Classic = chkClassic.Checked;
            Reset = chkReset.Checked;
            Start = chkStart.Checked;
            End = chkEnd.Checked;
            Split = chkSplit.Checked;
            Tazal = chkTazal.Checked;
        }
        public XmlNode UpdateSettings(XmlDocument document)
        {
            XmlElement xmlSettings = document.CreateElement("Settings");

            SetSetting(document, xmlSettings, chkRam, "Ram");
            SetSetting(document, xmlSettings, chkClassic, "Classic");
            SetSetting(document, xmlSettings, chkReset, "Reset");
            SetSetting(document, xmlSettings, chkStart, "Start");
            SetSetting(document, xmlSettings, chkEnd, "End");
            SetSetting(document, xmlSettings, chkSplit, "Split");
            SetSetting(document, xmlSettings, chkTazal, "Tazal");

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
            Ram = GetSetting(settings, "//Ram");
            Classic = GetSetting(settings, "//Classic");
            Reset = GetSetting(settings, "//Reset");
            Start = GetSetting(settings, "//Start");
            End = GetSetting(settings, "//End");
            Split = GetSetting(settings, "//Split");
            Tazal = GetSetting(settings, "//Tazal");
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