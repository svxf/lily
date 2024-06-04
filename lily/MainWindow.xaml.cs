using CefSharp;
using CefSharp.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lily
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int tabReferences;
        private WindowState previousWindowState;

        public MainWindow()
        {
            InitializeComponent();
            this.NewTab();
        }

        private void HomeWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;
            try
            {
                this.DragMove();
            }
            catch
            {
            }
        }

        private void HomeWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.WindowState == this.previousWindowState)
                return;
            this.previousWindowState = this.WindowState;
        }

        private async void NewTab(string tabName = "x", string content = "print(\"synapse winning!\")", bool autoSave = true)
        {
            if (tabName == "x")
            {
                ++this.tabReferences;
                tabName = "Tab " + this.tabReferences.ToString() + ".lua";
            }

            Border border = new Border();
            border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2d2d2d"));
            border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF323232"));
            border.BorderThickness = new Thickness(1.0);
            border.Width = 111;
            border.Height = 21;
            border.Margin = new Thickness(6.0, 0.0, 0.0, 2.0);
            border.CornerRadius = new CornerRadius(3);
            border.VerticalAlignment = VerticalAlignment.Bottom;
            border.Tag = (object)tabName;

            Grid grid = new Grid();
            grid.VerticalAlignment = VerticalAlignment.Center;
            grid.Height = 26;

            Label titleLabel = new Label();
            titleLabel.Content = tabName;
            titleLabel.Foreground = new SolidColorBrush(Colors.White);
            titleLabel.FontSize = 12.0;
            titleLabel.HorizontalAlignment = HorizontalAlignment.Left;
            titleLabel.VerticalAlignment = VerticalAlignment.Center;
            titleLabel.FontWeight = FontWeights.Light;

            Label closeButton = new Label();
            closeButton.Content = "X";
            closeButton.Foreground = new SolidColorBrush(Colors.White);
            closeButton.HorizontalAlignment = HorizontalAlignment.Right;
            closeButton.VerticalAlignment = VerticalAlignment.Center;
            closeButton.FontWeight = FontWeights.Light;
            closeButton.MouseLeftButtonDown += (sender, e) =>
            {
                //RemoveTab(tabName);
            };

            grid.Children.Add(titleLabel);
            grid.Children.Add(closeButton);
            border.Child = grid;

            ContextMenu contextMenu = new ContextMenu();
            contextMenu.Style = (Style)FindResource("tabContextMenu");

            MenuItem duplicateMenuItem = new MenuItem();
            duplicateMenuItem.Header = "Duplicate";
            duplicateMenuItem.Click += (sender, e) =>
            {
            };
            contextMenu.Items.Add(duplicateMenuItem);

            MenuItem executeMenuItem = new MenuItem();
            executeMenuItem.Header = "Execute";
            executeMenuItem.Click += (sender, e) =>
            {
            };
            contextMenu.Items.Add(executeMenuItem);

            MenuItem formatMenuItem = new MenuItem();
            formatMenuItem.Header = "Format";
            formatMenuItem.Click += (sender, e) =>
            {
            };
            contextMenu.Items.Add(formatMenuItem);

            MenuItem customizeMenuItem = new MenuItem();
            customizeMenuItem.Header = "Customize";
            contextMenu.Items.Add(customizeMenuItem);

            MenuItem renameMenuItem = new MenuItem();
            renameMenuItem.Header = "Rename";
            renameMenuItem.Click += (sender, e) =>
            {
            };
            customizeMenuItem.Items.Add(renameMenuItem);

            MenuItem togglePinMenuItem = new MenuItem();
            togglePinMenuItem.Header = "Toggle Pin";
            togglePinMenuItem.Click += (sender, e) =>
            {
            };
            customizeMenuItem.Items.Add(togglePinMenuItem);

            MenuItem toggleReadonlyMenuItem = new MenuItem();
            toggleReadonlyMenuItem.Header = "Toggle Readonly";
            toggleReadonlyMenuItem.Click += (sender, e) =>
            {
            };
            customizeMenuItem.Items.Add(toggleReadonlyMenuItem);

                MenuItem setIconMenuItem = new MenuItem();
                setIconMenuItem.Header = "Set Icon";

                MenuItem noneIconMenuItem = new MenuItem();
                noneIconMenuItem.Header = "None";
                noneIconMenuItem.Click += (sender, e) =>
                {
                };
                setIconMenuItem.Items.Add(noneIconMenuItem);

                MenuItem starIconMenuItem = new MenuItem();
                starIconMenuItem.Header = "Star";
                starIconMenuItem.Click += (sender, e) =>
                {
                };
                setIconMenuItem.Items.Add(starIconMenuItem);

                MenuItem lightbulbIconMenuItem = new MenuItem();
                lightbulbIconMenuItem.Header = "Lightbulb";
                lightbulbIconMenuItem.Click += (sender, e) =>
                {
                };
                setIconMenuItem.Items.Add(lightbulbIconMenuItem);

                customizeMenuItem.Items.Add(setIconMenuItem);

            MenuItem setDirectoryMenuItem = new MenuItem();
            setDirectoryMenuItem.Header = "Set Directory";
            setDirectoryMenuItem.Click += (sender, e) =>
            {
            };
            contextMenu.Items.Add(setDirectoryMenuItem);

            MenuItem resetTargetsMenuItem = new MenuItem();
            resetTargetsMenuItem.Header = "Reset Targets";
            resetTargetsMenuItem.Click += (sender, e) =>
            {
            };
            contextMenu.Items.Add(resetTargetsMenuItem);

            MenuItem toggleAutoExecuteMenuItem = new MenuItem();
            toggleAutoExecuteMenuItem.Header = "Toggle Auto-Execute";
            toggleAutoExecuteMenuItem.Click += (sender, e) =>
            {
            };
            contextMenu.Items.Add(toggleAutoExecuteMenuItem);

            MenuItem closeAllButThisMenuItem = new MenuItem();
            closeAllButThisMenuItem.Header = "Close All But This";
            closeAllButThisMenuItem.Click += (sender, e) =>
            {
            };
            contextMenu.Items.Add(closeAllButThisMenuItem);

            border.ContextMenu = contextMenu;

            TabItem newItem = new TabItem();
            newItem.Tag = (object)tabName;
            ChromiumWebBrowser chromiumWebBrowser = new ChromiumWebBrowser();
            chromiumWebBrowser.BorderThickness = new Thickness(0.0);
            ChromiumWebBrowser newBrowser = chromiumWebBrowser;
            newBrowser.FrameLoadEnd += (EventHandler<FrameLoadEndEventArgs>)(async (sender, e) =>
            {
                string script = $@"
                    window.SetText(`{content}`);
                    window.SwitchMinimap(true);
                    window.SwitchReadonly(false);
                    window.SwitchRenderWhitespace(true);
                    window.SwitchLinks(true);
                    window.SwitchLineHeight(19);
                    window.SwitchFontSize(14);
                    window.SwitchFolding(true);
                    window.SwitchAutoIndent(true);
                    window.SwitchFontFamily(""'Cascadia Code', Consolas, 'Courier New', monospace"");
                    window.SwitchFontLigatures(true);
                    window.AddIntellisense(true);
                ";

                newBrowser.ExecuteScriptAsync(script);
                newBrowser.GetBrowserHost().WindowlessFrameRate = 144;
            });

            newItem.Content = (object)newBrowser;
            newBrowser.Load("http://localhost:3000");
            newItem.IsSelected = true;

            border.MouseLeftButtonDown += (sender, e) =>
            {
                newItem.IsSelected = true;
            };

            this.Tabs.Children.Add(border);
            this.ScriptTabs.Items.Add((object)newItem);
        }

        private void PopupNotification(string message, int duration)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {

                var newNotificationBorder = new Border
                {
                    Name = "NotificationBorder",
                    BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2D")),
                    BorderThickness = new Thickness(1),
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1D1D1E")),
                    Width = 280,
                    CornerRadius = new CornerRadius(6),
                    Margin = new Thickness(0, 0, 5, 10),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Bottom
                };

                var notificationGrid = new Grid { Name = "NotificationGrid" };
                var textBlock = new TextBlock
                {
                    Name = "NotificationContent",
                    Text = message,
                    Margin = new Thickness(16, 16, 32, 18),
                    TextWrapping = TextWrapping.Wrap,
                    FontFamily = new FontFamily("SF Pro"),
                    FontSize = 13,
                    Foreground = Brushes.White,
                    Background = Brushes.Transparent,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Width = 230
                };

                var durationIndicator = new Border
                {
                    Name = "DurationIndicator",
                    Width = 278,
                    Height = 4,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    CornerRadius = new CornerRadius(2),
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF383839"))
                };

                var closeButton = new Button
                {
                    Name = "CloseNotificationButton",
                    Content = "M480-424 284-228q-11 11-28 11t-28-11q-11-11-11-28t11-28l196-196-196-196q-11-11-11-28t11-28q11-11 28-11t28 11l196 196 196-196q11-11 28-11t28 11q11 11 11 28t-11 28L536-480l196 196q11 11 11 28t-11 28q-11 11-28 11t-28-11L480-424Z",
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1D1D1E")),
                    Padding = new Thickness(7),
                    Width = 26,
                    Height = 26,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(0, 2, 2, 0),
                    BorderThickness = new Thickness(0, 6, 0, 0),
                    Style = FindResource("tabButton") as Style
                };
                closeButton.Click += CloseNotificationButton_Click;

                notificationGrid.Children.Add(textBlock);
                notificationGrid.Children.Add(durationIndicator);
                notificationGrid.Children.Add(closeButton);

                newNotificationBorder.Child = notificationGrid;

                IGrid.Children.Add(newNotificationBorder);

                double initialWidth = durationIndicator.Width;

                var hideStoryboard = new Storyboard();

                var widthAnimation = new DoubleAnimation(initialWidth, 0, TimeSpan.FromSeconds(duration));
                Storyboard.SetTarget(widthAnimation, durationIndicator);
                Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(WidthProperty));
                hideStoryboard.Children.Add(widthAnimation);


                hideStoryboard.Completed += (sender, args) =>
                {
                    var opacityAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.1));
                    opacityAnimation.Completed += (s, a) =>
                    {
                        IGrid.Children.Remove(newNotificationBorder);
                    };
                    newNotificationBorder.BeginAnimation(OpacityProperty, opacityAnimation);
                };

                hideStoryboard.Begin();
            });
        }

        private void CloseNotificationButton_Click(object sender, RoutedEventArgs e)
        {
        }

        //

        private void CloseButton_Click(object sender, RoutedEventArgs e) =>
            Environment.Exit(0);

        private void MaximizeButton_Click(object sender, RoutedEventArgs e) =>
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

        private void MinimizeButton_Click(object sender, RoutedEventArgs e) =>
            this.WindowState = WindowState.Minimized;

        //

        private void CodeButton_Click(object sender, RoutedEventArgs e)
        {
            CodeContainer.Visibility = Visibility.Visible;
            SettingsContainer.Visibility = Visibility.Collapsed;
            ThemeContainer.Visibility = Visibility.Collapsed;
            PackageContainer.Visibility = Visibility.Collapsed;

            SwitchToCode.BorderBrush = Brushes.White;
            SwitchToSetting.BorderBrush = Brushes.Transparent;
            SwitchToTheme.BorderBrush = Brushes.Transparent;
            SwitchToPackage.BorderBrush = Brushes.Transparent;
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            CodeContainer.Visibility = Visibility.Collapsed;
            SettingsContainer.Visibility = Visibility.Visible;
            ThemeContainer.Visibility = Visibility.Collapsed;
            PackageContainer.Visibility = Visibility.Collapsed;

            SwitchToCode.BorderBrush = Brushes.Transparent;
            SwitchToSetting.BorderBrush = Brushes.White;
            SwitchToTheme.BorderBrush = Brushes.Transparent;
            SwitchToPackage.BorderBrush = Brushes.Transparent;
        }

        private void ThemeSetting_Click(object sender, RoutedEventArgs e)
        {
            CodeContainer.Visibility = Visibility.Collapsed;
            SettingsContainer.Visibility = Visibility.Collapsed;
            ThemeContainer.Visibility = Visibility.Visible;
            PackageContainer.Visibility = Visibility.Collapsed;

            SwitchToCode.BorderBrush = Brushes.Transparent;
            SwitchToSetting.BorderBrush = Brushes.Transparent;
            SwitchToTheme.BorderBrush = Brushes.White;
            SwitchToPackage.BorderBrush = Brushes.Transparent;
        }

        private void PackageSetting_Click(object sender, RoutedEventArgs e)
        {
            CodeContainer.Visibility = Visibility.Collapsed;
            SettingsContainer.Visibility = Visibility.Collapsed;
            ThemeContainer.Visibility = Visibility.Collapsed;
            PackageContainer.Visibility = Visibility.Visible;

            SwitchToCode.BorderBrush = Brushes.Transparent;
            SwitchToSetting.BorderBrush = Brushes.Transparent;
            SwitchToTheme.BorderBrush = Brushes.Transparent;
            SwitchToPackage.BorderBrush = Brushes.White;
        }

        //

        private void TabHeader_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                TabHeader.LineLeft();
            }
            else
            {
                TabHeader.LineRight();
            }
        }

        private void TabHeader_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentWidthChange <= 0.0)
                return;
            ((ScrollViewer)sender).ScrollToRightEnd();
        }
        
        //

        private async void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            Encoding encoding = Encoding.UTF8;
            byte[] bytes = encoding.GetBytes((await((IChromiumWebBrowserBase)this.ScriptTabs.SelectedContent).EvaluateScriptAsync("window.GetText();", new TimeSpan?(), false)).Result.ToString());
            encoding = (Encoding)null;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "lily - Save Script";
            saveFileDialog1.Filter = "Lua Script|*.lua";
            SaveFileDialog saveFileDialog2 = saveFileDialog1;
            bool? nullable = saveFileDialog2.ShowDialog();
            bool flag = true;
            if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
                return;
            FileStream fileStream = (FileStream)saveFileDialog2.OpenFile();
            fileStream.Write(bytes, 0, bytes.Length);
            fileStream.Close();
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "lily - Open Script";
            openFileDialog1.Filter = "Lua Script|*.lua|Text File|*.txt";
            OpenFileDialog openFileDialog2 = openFileDialog1;
            bool? nullable = openFileDialog2.ShowDialog();
            bool flag = true;
            if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
                return;
            this.NewTab(System.IO.Path.GetFileName(openFileDialog2.FileName), File.ReadAllText(openFileDialog2.FileName));
        }

        private void NewTabB_Click(object sender, RoutedEventArgs e)
        {
            NewTab();
        }

        private void ExecuteFileButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            string code = (await((IChromiumWebBrowserBase)this.ScriptTabs.SelectedContent).EvaluateScriptAsync("window.GetText();", new TimeSpan?(), false)).Result.ToString();
            //code = code.Trim('"');
            //ws.Send(JsonConvert.DeserializeObject<string>("\"" + code + "\""));
        }

        private void ConsoleButton_Click(object sender, RoutedEventArgs e)
        {
            PopupNotification("Injecting", 1);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ((IChromiumWebBrowserBase)this.ScriptTabs.SelectedContent).ExecuteScriptAsync("window.SetText('');");
        }

        
    }
}
