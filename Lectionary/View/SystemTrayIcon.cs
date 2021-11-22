using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Lectionary.View
{
    public class SystemTrayIcon
    {

        public event EventHandler OnSettingsChange;
        public event EventHandler OnMoveWindow;

        public NotifyIcon notifyIcon;
        private ToolStripLabel titleBlock;
        private ToolStripDropDownButton colorDropDown;

        public SystemTrayIcon()
        {

            // Initialize Tray Icon
            notifyIcon = new NotifyIcon()
            {
                Icon = Resources.AppIcon,
                ContextMenuStrip = new ContextMenuStrip(),
                Visible = true,
                Text = "Orthodox Lectionary Overlay"
            };

            // Title Block
            titleBlock = new ToolStripLabel("--- Orthodox Lectionary Overlay ---");

            // Background Color Selection
            colorDropDown = new ToolStripDropDownButton();
            //colorDropDown.Dock = DockStyle.Fill;
            colorDropDown.Text = ("Background Color");

            var colorMenuStrip = new ContextMenuStrip();
            colorMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(OnColorMenuStrip_Clicked);
            colorDropDown.DropDown = colorMenuStrip;

            ColorPalette colorOptions = new ColorPalette();

            for (int i = 0; i < colorOptions.Colors.Count; i++)
            {
                colorMenuStrip.Items.Add(colorOptions.Colors[i]);
                colorMenuStrip.Items[i].BackColor = Color.FromName(colorOptions.Colors[i]);
            }

            CheckBox CHECKBOX_RUNONSTARTUP = new CheckBox();
            CHECKBOX_RUNONSTARTUP.Checked = Properties.Settings.Default.RunOnStartup;
            CHECKBOX_RUNONSTARTUP.BackColor = Color.Transparent;
            CHECKBOX_RUNONSTARTUP.CheckedChanged += CHECKBOX_RUNONSTARTUP_CheckedChanged;
            ToolStripControlHost host = new ToolStripControlHost(CHECKBOX_RUNONSTARTUP);
            host.Control.Text = "Run on Startup";

            notifyIcon.ContextMenuStrip.Items.Add(titleBlock);
            notifyIcon.ContextMenuStrip.Items.Add(colorDropDown);
            notifyIcon.ContextMenuStrip.Items.Add("Move Position...");
            notifyIcon.ContextMenuStrip.Items.Add(host);
            notifyIcon.ContextMenuStrip.Items.Add("Quit");

            notifyIcon.ContextMenuStrip.ItemClicked += ContextMenuStrip_ItemClicked;

        }

        private void CHECKBOX_RUNONSTARTUP_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            Properties.Settings.Default.RunOnStartup = chk.Checked;
            Properties.Settings.Default.Save();

            string executableFilePath = Application.StartupPath + "\\Lectionary.exe";
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
           ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (chk.Checked)
                rk.SetValue("Lectionary", executableFilePath);
            else
                rk.DeleteValue("Lectionary", false);
        }

        private void ContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Move Position...")
            {
                OnMoveWindow?.Invoke(this, EventArgs.Empty);
            }
            else if (e.ClickedItem.Text == "Quit")
            {
                this.notifyIcon.Visible = false;
                Application.Exit();
            }
        }

        void OnColorMenuStrip_Clicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            Properties.Settings.Default.BackgroundColor = Color.FromName(item.Text);
            Properties.Settings.Default.Save();
            OnSettingsChange?.Invoke(this, EventArgs.Empty);
        }
    }

    public class ColorPalette
    {
        public List<string> Colors { get; set; }

        public ColorPalette()
        {
            Colors = new List<string>();
            Colors.Add("ButtonShadow");
            Colors.Add("BurlyWood");
            Colors.Add("CornflowerBlue");
            Colors.Add("DarkGoldenrod");
            Colors.Add("DarkSalmon");
            Colors.Add("DarkSeaGreen");
            Colors.Add("DeepSkyBlue");
            Colors.Add("Goldenrod");
            Colors.Add("Lavender");
            Colors.Add("LightSalmon");
            Colors.Add("LightSteelBlue");
            Colors.Add("PaleGreen");
            Colors.Add("Pink");
            Colors.Add("Tan");
            Colors.Add("White");
        }
    }
}
