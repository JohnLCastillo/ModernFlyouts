using ModernFlyouts.Helpers;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using Wpf.Ui.Controls.Window;

namespace ModernFlyouts
{
    public partial class SettingsWindow : FluentWindow
    {
        private bool _isActive;

        public SettingsWindow()
        {
            // Wpf.Ui.Appearance.Watcher.Watch(this);
            InitializeComponent();
            Loaded += (_, _) => NavView.Navigate(typeof(Navigation.GeneralSettingsPage));

            KeyDown += (s, e) =>
            {
                if (e.Key == Key.Back || (e.Key == Key.Left && Keyboard.Modifiers == ModifierKeys.Alt))
                {
                    
                }
            };
        }

        protected override void OnActivated(EventArgs e)
        {
            if (!_isActive)
            {
                Workarounds.RenderLoopFix.ApplyFix();
                _isActive = true;
            }

            base.OnActivated(e);
        }

        protected override void OnDeactivated(EventArgs e)
        {
            _isActive = false;

            base.OnDeactivated(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            AppDataHelper.SettingsWindowPlacement = WindowPlacementHelper.GetPlacement(new WindowInteropHelper(this).Handle);
            e.Cancel = true;
            Hide();

            base.OnClosing(e);
        }
    }
}
