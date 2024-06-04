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
    public partial class SettingButtonText : UserControl, IComponentConnector
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title), typeof(string), typeof(SettingButtonText), new PropertyMetadata(null));

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            nameof(Description), typeof(string), typeof(SettingButtonText), new PropertyMetadata(null));

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text), typeof(string), typeof(SettingButtonText), new PropertyMetadata(null));

        public string Title
        {
            get => (string)this.GetValue(SettingButtonText.TitleProperty);
            set => this.SetValue(SettingButtonText.TitleProperty, (object)value);
        }

        public string Description
        {
            get => (string)this.GetValue(SettingButtonText.DescriptionProperty);
            set => this.SetValue(SettingButtonText.DescriptionProperty, (object)value);
        }

        public string Text
        {
            get => (string)this.GetValue(SettingButtonText.TextProperty);
            set => this.SetValue(SettingButtonText.TextProperty, (object)value);
        }

        public event EventHandler<EventArgs> OnClicked;

        public SettingButtonText()
        {
            InitializeComponent();
        }

        private void ClickButton_Click(object sender, RoutedEventArgs e)
        {
            EventHandler<EventArgs> onClicked = this.OnClicked;
            if (onClicked == null)
                return;
            onClicked((object)this, new EventArgs());
        }
    }
}
