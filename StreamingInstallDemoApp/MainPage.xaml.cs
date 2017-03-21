using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace StreamingInstallDemoApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ApplicationDataContainer localSettings = null;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

            localSettings = ApplicationData.Current.LocalSettings;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title_textBox.Text += " " + Package.Current.Id.Version.Major + "." + Package.Current.Id.Version.Minor + "." + Package.Current.Id.Version.Build + "." + Package.Current.Id.Version.Revision;

            if (localSettings.Values["OverallProgress"] != null)
            {
                this.Overall_ProgressBar.Value = (double)localSettings.Values["OverallProgress"];
            }
        }

        private void Overall_Progress_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            ((FrameworkElement)sender).Visibility = (e.NewValue == 100) ? Visibility.Collapsed : Visibility.Visible;
        }

        private void ToContentGroupPage(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ContentGroupPage));
        }

        private void ToLanguagesPage(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LanguagesPage));
        }

        public void Set_Overall_ProgressBar_Value(double value)
        {
            this.Overall_ProgressBar.Value = value;
        }
    }
}
