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
using Windows.ApplicationModel.Resources.Core;
using Windows.Storage;
using Windows.ApplicationModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StreamingInstallDemoApp
{
    /// <summary>
    /// A page to display the content groups images from the resource pack
    /// </summary>
    public sealed partial class LanguagePicturePage : Page
    {
        String asset;
        public LanguagePicturePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            String contentgroup = e.Parameter.ToString();
            asset = contentgroup + ".png";
        }

        private void PicturePageGrid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadImageFromResourcePack(asset);
        }

        private async System.Threading.Tasks.Task LoadImageFromResourcePack(string resourceKey)
        {
            var context = ResourceContext.GetForCurrentView().Clone();
            context.QualifierValues["language"] = "fr-fr";

            var resourcesMap = ResourceManager.Current.MainResourceMap.GetSubtree("Files").GetSubtree("Assets");
            NamedResource namedResource;
            if (resourcesMap.TryGetValue(resourceKey, out namedResource))
            {
                var resourceCandidates = namedResource.ResolveAll(context);
                StorageFile file = await resourceCandidates.FirstOrDefault().GetValueAsFileAsync();
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(file.Path));
                ((Grid)this.PicturePageGrid).Background = brush;
            }
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
