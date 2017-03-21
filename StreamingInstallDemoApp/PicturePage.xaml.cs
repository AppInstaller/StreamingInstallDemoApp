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
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StreamingInstallDemoApp
{
    /// <summary>
    /// A page to display the content groups images from the main pack
    /// </summary>
    public sealed partial class PicturePage : Page
    {
        String asset;
        public PicturePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            String contentgroup = e.Parameter.ToString();

            asset = "ms-appx:///Assets/" + contentgroup + "/" + contentgroup + ".png";
        }

        private void PicturePageGrid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadImage();
        }

        private async System.Threading.Tasks.Task LoadImage()
        {
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(asset));
            ((Grid)this.PicturePageGrid).Background = brush;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null && this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
            else
            {
                this.Frame.Navigate(typeof(ContentGroupPage));
            }
        }
    }
}
