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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StreamingInstallDemoApp
{
    /// <summary>
    /// A page to display the content groups of the resource pack and their progress
    /// </summary>
    public sealed partial class LanguagesPage : Page
    {
        ApplicationDataContainer localSettings = null;

        public LanguagesPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

            localSettings = ApplicationData.Current.LocalSettings;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            setup();
        }

        private async void setup()
        {
            foreach (var package in Package.Current.Dependencies)
            {
                if (package.Id.FullName.Contains("language-fr") && package.IsResourcePackage)
                {
                    var resourceContentGroups = await package.GetContentGroupsAsync();
                    foreach (var contentGroup in resourceContentGroups)
                    {
                        if (contentGroup.Name == "Required")
                        {
                            ProgressBar _ProgressBar = AllChildren(this.LanguagePacksList).OfType<ProgressBar>().ToList()[0];
                            Button _Button = AllChildren(this.LanguagePacksList).OfType<Button>().ToList()[0];
                            if (contentGroup.State == PackageContentGroupState.Staged)
                            {
                                _ProgressBar.Visibility = Visibility.Collapsed;
                                _Button.IsEnabled = true;
                            }
                            else
                            {
                                _ProgressBar.Visibility = Visibility.Visible;
                                _Button.IsEnabled = false;
                                if (localSettings.Values[contentGroup.Name] != null)
                                {
                                    _ProgressBar.Value = (double)localSettings.Values[contentGroup.Name];
                                }
                            }
                        }
                        else if (contentGroup.Name == "FrenchPack1")
                        {
                            ProgressBar _ProgressBar = AllChildren(this.LanguagePacksList).OfType<ProgressBar>().ToList()[1];
                            Button _Button = AllChildren(this.LanguagePacksList).OfType<Button>().ToList()[1];
                            if (contentGroup.State == PackageContentGroupState.Staged)
                            {
                                _ProgressBar.Visibility = Visibility.Collapsed;
                                _Button.IsEnabled = true;
                            }
                            else
                            {
                                _ProgressBar.Visibility = Visibility.Visible;
                                _Button.IsEnabled = false;
                                if (localSettings.Values[contentGroup.Name] != null)
                                {
                                    _ProgressBar.Value = (double)localSettings.Values[contentGroup.Name];
                                }
                            }
                        }
                        else if (contentGroup.Name == "FrenchPack2")
                        {
                            ProgressBar _ProgressBar = AllChildren(this.LanguagePacksList).OfType<ProgressBar>().ToList()[2];
                            Button _Button = AllChildren(this.LanguagePacksList).OfType<Button>().ToList()[2];
                            if (contentGroup.State == PackageContentGroupState.Staged)
                            {
                                _ProgressBar.Visibility = Visibility.Collapsed;
                                _Button.IsEnabled = true;
                            }
                            else
                            {
                                _ProgressBar.Visibility = Visibility.Visible;
                                _Button.IsEnabled = false;
                                if (localSettings.Values[contentGroup.Name] != null)
                                {
                                    _ProgressBar.Value = (double)localSettings.Values[contentGroup.Name];
                                }
                            }
                        }
                    }
                }
            }
        }

        public List<Control> AllChildren(DependencyObject parent)
        {
            var _List = new List<Control>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var _Child = VisualTreeHelper.GetChild(parent, i);
                if (_Child is Control)
                    _List.Add(_Child as Control);
                _List.AddRange(AllChildren(_Child));
            }
            return _List;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null && this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
            else
            {
                this.Frame.Navigate(typeof(MainPage));
            }
        }

        public void Set_CG_Status(PackageContentGroupStagingEventArgs args)
        {
            switch (args.ContentGroupName)
            {
                case "Required":
                    {
                        ProgressBar _ProgressBar = AllChildren(this.LanguagePacksList).OfType<ProgressBar>().ToList()[0];
                        Button _Button = AllChildren(this.LanguagePacksList).OfType<Button>().ToList()[0];
                        _ProgressBar.Value = args.Progress;
                        if (args.IsComplete)
                        {
                            _Button.IsEnabled = true;
                            _ProgressBar.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            _ProgressBar.Visibility = Visibility.Visible;
                            _Button.IsEnabled = false;
                        }
                    }
                    break;
                case "FrenchPack1":
                    {
                        ProgressBar _ProgressBar = AllChildren(this.LanguagePacksList).OfType<ProgressBar>().ToList()[1];
                        Button _Button = AllChildren(this.LanguagePacksList).OfType<Button>().ToList()[1];
                        _ProgressBar.Value = args.Progress;
                        if (args.IsComplete)
                        {
                            _Button.IsEnabled = true;
                            _ProgressBar.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            _ProgressBar.Visibility = Visibility.Visible;
                            _Button.IsEnabled = false;
                        }
                    }
                    break;
                case "FrenchPack2":
                    {
                        ProgressBar _ProgressBar = AllChildren(this.LanguagePacksList).OfType<ProgressBar>().ToList()[2];
                        Button _Button = AllChildren(this.LanguagePacksList).OfType<Button>().ToList()[2];
                        _ProgressBar.Value = args.Progress;
                        if (args.IsComplete)
                        {
                            _Button.IsEnabled = true;
                            _ProgressBar.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            _Button.IsEnabled = false;
                            _ProgressBar.Visibility = Visibility.Visible;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void ProgressValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (e.NewValue == 100)
            {
                ((FrameworkElement)sender).Visibility = Visibility.Collapsed;
            }
        }

        private void LanguagePackItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (var package in Package.Current.Dependencies)
            {
                if (package.Id.FullName.Contains("language-fr") && package.IsResourcePackage)
                {
                    String item = (String)e.ClickedItem;
                    item = new string(item.Where(c => !Char.IsWhiteSpace(c)).ToArray());
                    List<String> contentGroupList = new List<String>() { item };
                    package.StageContentGroupsAsync(contentGroupList);
                }
            }

            this.LanguagePacksList.SelectedIndex = -1;
        }

        private static T FindParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null) return null;

            var parentT = parent as T;
            return parentT ?? FindParent<T>(parent);
        }

        private void ViewLanguagePackClick(object sender, RoutedEventArgs e)
        {
            Button _Button = (Button)e.OriginalSource;
            ListViewItem item = FindParent<ListViewItem>(_Button);
            String contentgroup = (String)item.Content;
            contentgroup = new string(contentgroup.Where(c => !Char.IsWhiteSpace(c)).ToArray());

            this.Frame.Navigate(typeof(LanguagePicturePage), contentgroup);
        }
    }
}
