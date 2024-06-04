using CefSharp.DevTools.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lily.Controls.Settings
{
    /// <summary>
    /// Interaction logic for SettingCheckbox.xaml
    /// </summary>
    public partial class SettingCheckbox : UserControl, IComponentConnector
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title), typeof(string), typeof(SettingCheckbox), new PropertyMetadata(null));

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            nameof(Description), typeof(string), typeof(SettingCheckbox), new PropertyMetadata(null));

        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(
            nameof(IsChecked), typeof(bool), typeof(SettingCheckbox), new PropertyMetadata((object)false));

        public string Title
        {
            get => (string)this.GetValue(SettingCheckbox.TitleProperty);
            set => this.SetValue(SettingCheckbox.TitleProperty, (object)value);
        }

        public string Description
        {
            get => (string)this.GetValue(SettingCheckbox.DescriptionProperty);
            set => this.SetValue(SettingCheckbox.DescriptionProperty, (object)value);
        }

        public bool IsChecked
        {
            get => (bool)this.GetValue(SettingCheckbox.IsCheckedProperty);
            set
            {
                this.SetValue(SettingCheckbox.IsCheckedProperty, (object)value);
                EventHandler<EventArgs> onCheckedChanged = this.OnCheckedChanged;
                if (onCheckedChanged != null)
                    onCheckedChanged((object)this, new EventArgs());

                var indicatorPathStoryboard = new Storyboard();
                var indicatorPathDoubleAnimation = new DoubleAnimation
                {
                    Duration = TimeSpan.FromSeconds(0.2),
                    To = value ? 1 : 0
                };
                Storyboard.SetTarget(indicatorPathDoubleAnimation, IndicatorPath);
                Storyboard.SetTargetProperty(indicatorPathDoubleAnimation, new PropertyPath("Opacity"));
                indicatorPathStoryboard.Children.Add(indicatorPathDoubleAnimation);

                var mainBorderStoryboard = new Storyboard();
                var mainBorderColorAnimation = new ColorAnimation
                {
                    Duration = TimeSpan.FromSeconds(0.2),
                    To = value ? (Color)ColorConverter.ConvertFromString("#FFB0D8E3") : (Color)ColorConverter.ConvertFromString("#FF202020")
                };
                Storyboard.SetTarget(mainBorderColorAnimation, MainBorder);
                Storyboard.SetTargetProperty(mainBorderColorAnimation, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));
                mainBorderStoryboard.Children.Add(mainBorderColorAnimation);

                indicatorPathStoryboard.Begin();
                mainBorderStoryboard.Begin();
            }
        }

        public event EventHandler<EventArgs> OnCheckedChanged;

        public SettingCheckbox()
        {
            InitializeComponent();
            UpdateVisuals(IsChecked);
        }

        private void UpdateVisuals(bool isChecked)
        {
            IndicatorPath.Opacity = isChecked ? 1 : 0;
            MainBorder.Background = new SolidColorBrush(isChecked ? (Color)ColorConverter.ConvertFromString("#FFB3D7E3") : (Color)ColorConverter.ConvertFromString("#FF202020"));
        }

        private void MainBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsChecked = !this.IsChecked;
        }
    }
}
