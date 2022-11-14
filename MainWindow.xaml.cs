using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Act2SlotMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Sound effect
        SoundPlayer coinInsertfx = new SoundPlayer(Media.SoundRes.coinInsert);
        SoundPlayer donefx = new SoundPlayer(Media.SoundRes.done);

        Random rand = new Random();
        const int maxInt = 6;
        const int minInt = 0;
        int spins = 0;
        int guaranteeChance;
        //pls update
        // effects
        // button
        public MainWindow()
        {
            InitializeComponent();
            guaranteeChance = rand.Next(3, 6);
        }

        private bool asyncSpinRunning = false;
        async private void Button_ClickSpin(object sender, RoutedEventArgs e)
        {
            if (!asyncSpinRunning)
            {
                coinInsertfx.Play();
                asyncSpinRunning = true;
                SpinBTN.IsEnabled = false;
                spins++;
                Random rand = new Random();
                await Task.Delay(200);
                for (int i = 0; i < 7; i++)
                {
                    if (i < 5) Slot0.ChangeValue(rand.Next(minInt, maxInt));
                    if (i < 6) Slot1.ChangeValue((i == 5 && spins % guaranteeChance == 0 ? Slot0.Value : rand.Next(minInt, maxInt)));
                    Slot2.ChangeValue((i == 6 && spins % guaranteeChance == 0 ? Slot0.Value : rand.Next(minInt, maxInt)));
                    await Task.Delay(500);
                }
                donefx.Load();
                donefx.Play();
                await Task.Delay(500);
                CheckResult();
                asyncSpinRunning = false;
                SpinBTN.IsEnabled = true;
            }
        }

        private void CheckResult()
        {
            int val0, val1, val2;
            val0 = Slot0.Value;
            val1 = Slot1.Value;
            val2 = Slot2.Value;
            if (val0 == val1 && val2 == val0)
            {
                winDialog.Show(Slot0.main.Source);
                guaranteeChance = rand.Next(6, 21);
                spins = 0;
            }
        }

        private void MediaPlayer_Loaded(object sender, RoutedEventArgs e) => ((MediaElement)sender).Play();
        private void MediaPlayer_Unloaded(object sender, RoutedEventArgs e) => ((MediaElement)sender).Stop();

        private void MediaPlayer_MediaEnd(object sender, RoutedEventArgs e) => ((MediaElement)sender).Position = TimeSpan.Zero;

        private void winDialog_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (winDialog.Visibility == Visibility.Visible)
                MediaPlayer.IsMuted = true;
            else
                MediaPlayer.IsMuted = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
