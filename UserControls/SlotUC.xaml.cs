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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Act2SlotMachine.UserControls
{
    /// <summary>
    /// Interaction logic for SlotUC.xaml
    /// </summary>
    public partial class SlotUC : UserControl
    {
        public int Value { get; private set; }

        private Storyboard swapAnimation;
        Random rand = new Random();
        private BitmapImage[] SlotImages = new BitmapImage[]
        {
            new BitmapImage(new Uri(@"/Images/banana.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"/Images/cherry.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"/Images/grape.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"/Images/melon.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"/Images/orange.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"/Images/seven.png", UriKind.Relative))
        };
        public SlotUC()
        {
            InitializeComponent();
            swapAnimation = (Storyboard)myGrid.Resources["swapAnimation"];
        }

        async public void ChangeValue(int value)
        {
            Value = value;
            await Task.Delay(rand.Next(0, 10) * 10);
            main.Source = SlotImages[value];
            swapAnimation.Begin();
            await Task.Delay(100);
            sub.Source = SlotImages[value];
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            int value = rand.Next(0, 6);
            main.Source = SlotImages[value];
            sub.Source = SlotImages[value];
        }
    }
}
