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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lily.Controls.Settings
{
    /// <summary>
    /// Interaction logic for SettingButton.xaml
    /// </summary>
    public partial class SettingButton : UserControl, IComponentConnector
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title), typeof(string), typeof(SettingButton), new PropertyMetadata(null));

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            nameof(Description), typeof(string), typeof(SettingButton), new PropertyMetadata(null));

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            nameof(Icon), typeof(object), typeof(SettingButton), new PropertyMetadata(null));

        public static readonly DependencyProperty Icon2Property = DependencyProperty.Register(
            nameof(Icon2), typeof(object), typeof(SettingButton), new PropertyMetadata(null));

        public string Title
        {
            get => (string)this.GetValue(SettingButton.TitleProperty);
            set => this.SetValue(SettingButton.TitleProperty, (object)value);
        }

        public string Description
        {
            get => (string)this.GetValue(SettingButton.DescriptionProperty);
            set => this.SetValue(SettingButton.DescriptionProperty, (object)value);
        }

        public string Icon
        {
            get => (string)this.GetValue(SettingButton.IconProperty);
            set => this.SetValue(SettingButton.IconProperty, (object)value);
        }

        public string Icon2
        {
            get => (string)this.GetValue(SettingButton.Icon2Property);
            set => this.SetValue(SettingButton.Icon2Property, (object)value);
        }

        public SettingButton()
        {
            InitializeComponent();
        }
    }
}
