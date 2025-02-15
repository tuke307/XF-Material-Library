﻿using System.Threading.Tasks;

using Microsoft.Maui;
using Microsoft.Maui.Controls.Xaml;
using XF.Material.Maui.UI.Dialogs.Configurations;

namespace XF.Material.Maui.UI.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialAlertDialog : BaseMaterialModalPage, IMaterialAwaitableDialog<bool?>
    {
        internal MaterialAlertDialog(string message, string title, string action1Text, string action2Text, MaterialAlertDialogConfiguration configuration = null) : this(configuration)
        {
            Message.Text = message;
            DialogTitle.Text = title;
            PositiveButton.Text = action1Text;
            PositiveButton.Command = new Command(async () =>
            {
                await DismissAsync();
                InputTaskCompletionSource?.SetResult(true);
            });
            NegativeButton.Text = action2Text;
            NegativeButton.Command = new Command(async () =>
            {
                await DismissAsync();
                InputTaskCompletionSource?.SetResult(false);
            });
        }

        internal MaterialAlertDialog(MaterialAlertDialogConfiguration configuration)
        {
            InitializeComponent();
            Configure(configuration);
        }

        public TaskCompletionSource<bool?> InputTaskCompletionSource { get; set; }

        internal static MaterialAlertDialogConfiguration GlobalConfiguration { get; set; }

        internal static async Task AlertAsync(string message, string title = null, MaterialAlertDialogConfiguration configuration = null)
        {
            var dialog = new MaterialAlertDialog(message, title, "Ok", null, configuration: configuration);
            await dialog.ShowAsync();
        }

        internal static async Task AlertAsync(string message, string title, string acknowledgementText = "Ok", MaterialAlertDialogConfiguration configuration = null)
        {
            var dialog = new MaterialAlertDialog(message, title, acknowledgementText, null, configuration: configuration);
            await dialog.ShowAsync();
        }

        internal static async Task<bool?> ConfirmAsync(string message, string title, string confirmingText, string dismissiveText = "Cancel", MaterialAlertDialogConfiguration configuration = null)
        {
            var dialog = new MaterialAlertDialog(message, title, confirmingText, dismissiveText, configuration)
            {
                InputTaskCompletionSource = new TaskCompletionSource<bool?>()
            };

            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        protected override void OnOrientationChanged(DisplayOrientation orientation)
        {
            base.OnOrientationChanged(orientation);

            ChangeLayout();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ChangeLayout();
        }

        private void ChangeLayout()
        {
            switch (DisplayOrientation)
            {
                case DisplayOrientation.Landscape when Device.Idiom == TargetIdiom.Phone:
                    Container.WidthRequest = 560;
                    Container.HorizontalOptions = LayoutOptions.Center;
                    break;
                case DisplayOrientation.Portrait when Device.Idiom == TargetIdiom.Phone:
                    Container.WidthRequest = -1;
                    Container.HorizontalOptions = LayoutOptions.FillAndExpand;
                    break;
            }
        }

        protected override void OnBackButtonDismissed()
        {
            InputTaskCompletionSource?.SetResult(null);
        }

        protected override bool OnBackgroundClicked()
        {
            InputTaskCompletionSource?.SetResult(null);

            return base.OnBackgroundClicked();
        }

        private void Configure(MaterialAlertDialogConfiguration configuration)
        {
            var preferredConfig = configuration ?? GlobalConfiguration;

            if (preferredConfig == null)
            {
                return;
            }

            BackgroundColor = preferredConfig.ScrimColor;
            Container.CornerRadius = preferredConfig.CornerRadius;
            Container.BackgroundColor = preferredConfig.BackgroundColor;
            DialogTitle.TextColor = preferredConfig.TitleTextColor;
            DialogTitle.FontFamily = preferredConfig.TitleFontFamily;
            Message.TextColor = preferredConfig.MessageTextColor;
            Message.FontFamily = preferredConfig.MessageFontFamily;
            PositiveButton.TextColor = NegativeButton.TextColor = preferredConfig.TintColor;
            PositiveButton.AllCaps = NegativeButton.AllCaps = preferredConfig.ButtonAllCaps;
            PositiveButton.FontFamily = NegativeButton.FontFamily = preferredConfig.ButtonFontFamily;
            Container.Margin = preferredConfig.Margin == default ? Material.GetResource<Thickness>("Material.Dialog.Margin") : preferredConfig.Margin;
        }
    }
}
