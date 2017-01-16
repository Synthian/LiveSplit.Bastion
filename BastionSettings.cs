using System;
using System.Windows.Forms;
using System.Xml;
namespace LiveSplit.Bastion.Settings
{
    public partial class BastionSettings : UserControl
    {
        public bool Reset { get; set; }
        public bool SplitStart { get; set; }
        public bool SplitEnd { get; set; }
        public bool Town { get; set; }
        public bool RockInSky { get; set; }
        public bool Tazal { get; set; }
        private BastionComponent component;
        private bool isLoading;

        public BastionSettings(BastionComponent comp)
        {
            isLoading = true;
            InitializeComponent();

            //Defaults
            Reset = true;
            SplitStart = true;
            SplitEnd = true;
            Town = true;

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
            chkEnd.Checked = SplitEnd;
            chkStart.Checked = SplitStart;
            chkReset.Checked = Reset;
            chkTown.Checked = Town;
            chkTazal.Checked = Tazal;
            chkRockInSky.Checked = RockInSky;
        }
        private void chkBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSplits();
        }
        public void UpdateSplits()
        {
            if (isLoading) return;

            Reset = chkReset.Checked;
            SplitStart = chkStart.Checked;
            SplitEnd = chkEnd.Checked;
            Town = chkTown.Checked;
            RockInSky = chkRockInSky.Checked;
            Tazal = chkTazal.Checked;
        }
        public XmlNode UpdateSettings(XmlDocument document)
        {
            XmlElement xmlSettings = document.CreateElement("Settings");

            SetSetting(document, xmlSettings, chkReset, "Reset");
            SetSetting(document, xmlSettings, chkStart, "SplitStart");
            SetSetting(document, xmlSettings, chkEnd, "SplitEnd");
            SetSetting(document, xmlSettings, chkRockInSky, "RockInSky");
            SetSetting(document, xmlSettings, chkTown, "Town");
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
            Reset = GetSetting(settings, "//Reset");
            SplitStart = GetSetting(settings, "//SplitStart");
            SplitEnd = GetSetting(settings, "//SplitEnd");
            RockInSky = GetSetting(settings, "//RockInSky");
            Town = GetSetting(settings, "//Town");
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