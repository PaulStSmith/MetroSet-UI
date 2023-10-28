using Microsoft.Win32;
using ModernUI.Windows;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace ModernUI
{
    public partial class FormBase : Form
    {
        private const string HelpChar = "\uE897";
        private const string CloseChar = "\uE8BB";
        private const string RestoreChar = "\uE923";
        private const string MinimizeChar = "\uE921";
        private const string MaximizeChar = "\uE922";
        private Point offset;

        /// <summary>
        /// Gets the system menu handle.
        /// </summary>
        /// <remarks>
        /// Used to display the system menu later on.
        /// </remarks>
        protected internal IntPtr SystemMenuHanle { get; private set; }

        private readonly bool isInitialized;

        public FormBase()
        {
            /*
             * Get the system menu handle.
             */
            CreateHandle();
            SystemMenuHanle = Utils.Native.GetSystemMenu(Handle, false);

            /*
             * Initialize the window
             */
            InitializeComponent();

            /*
             * Configure a few controls after initialization.
             */
            BackColor = Theme.Instance.Window;
            ForeColor = Theme.Instance.WindowText;
            isInitialized = true;
            btnHelp.Text = HelpChar;
            btnClose.Text = CloseChar;
            FormBorderStyle = FormBorderStyle.None;
            btnMinimize.Text = MinimizeChar;
            btnMaximize.Text = WindowState == FormWindowState.Normal ? MaximizeChar : RestoreChar;
            SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
        }

        /// <inheritdoc/>
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            Button? btn = (e.Control as Button);
            if (btn == null) return;

            btn.BackColor = Theme.Instance.ButtonFace;
            btn.ForeColor = Theme.Instance.ButtonText;
        }

        private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            // TODO: Add Dark Mode
        }

        /// <summary>
        /// Gets a value indicating whether or not the application should use a light theme.
        /// </summary>
        public static bool UseLightTheme => ((int?)Registry.CurrentUser.GetValue($@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize\AppsUseLightTheme", 0) == 0);

        /// <inheritdoc/>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            /*
             * Load the icon into the picture control.
             */
            picIcon.Image = Icon != null ? new Icon(Icon, new Size(32, 32)).ToBitmap() : new Bitmap(32, 32);
        }

        /// <inheritdoc/>
        protected override void OnStyleChanged(EventArgs e)
        {
            base.OnStyleChanged(e);
            if (!isInitialized) return;

            /*
             * Show or hide the window buttons.
             */
            btnClose.Visible = ControlBox;
            btnMaximize.Visible = ControlBox && MaximizeBox;
            btnMinimize.Visible = ControlBox && MinimizeBox;
            btnHelp.Visible = ControlBox && HelpButton;
            pnlIcon.Visible = ShowIcon;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            if (sender is not Button btn) return;
            btn.BackColor = Theme.Instance.CloseButtonHoverBackColor;
            btn.ForeColor = Theme.Instance.CloseButtonHoverForeColor;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            if (sender is not Button btn) return;
            btn.BackColor = Theme.Instance.ButtonFace;
            btn.ForeColor = Theme.Instance.ButtonText;
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
            btnMaximize.Text = WindowState == FormWindowState.Normal ? MaximizeChar : RestoreChar;
        }

        private void btnMinimize_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void FormHeader_MouseMove(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            var loc = PointToScreen(new Point(e.X, e.Y));
            loc.Offset(-offset.X, -offset.Y);
            Location = loc;
        }

        private void FormHeader_MouseDown(object? sender, MouseEventArgs e) => offset = new Point(e.X, e.Y);

        private void btnHelp_Click(object sender, EventArgs e) => OnHelpButtonClicked(new System.ComponentModel.CancelEventArgs());

        private void picIcon_Click(object sender, EventArgs e) => this.ShowSystemMenu(PointToScreen(new Point(picIcon.Left, picIcon.Bottom)));
    }
}