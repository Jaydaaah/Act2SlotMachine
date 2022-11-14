using System;
using System.Collections.Generic;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Act2SlotMachine.UserControls
{
    /// <summary>
    /// Interaction logic for winnerDialog.xaml
    /// </summary>
    public partial class winnerDialog : UserControl
    {
        SoundPlayer soundefx = new SoundPlayer(Media.SoundRes.applause);
        public winnerDialog()
        {
            InitializeComponent();
            soundefx.Load();
        }

        private void UserControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        internal void Show(ImageSource image)
        {
            mainImage.Source = image;
            this.Visibility = Visibility.Visible;
            soundefx.Play();
        }
    }
}
