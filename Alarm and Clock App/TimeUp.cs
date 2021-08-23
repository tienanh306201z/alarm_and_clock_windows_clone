using System;
using System.Diagnostics;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace Alarm_and_Clock_App
{
    public partial class TimeUp : Form
    {
        SoundPlayer soundPlayer;
        public static bool check = false;
        public TimeUp()
        {
            InitializeComponent();
            ControlBox = false;
        }

        private void TimeUp_Load(object sender, EventArgs e)
        {
            string soundPath;
            switch (uc_Module4.Instance.comboBox1.SelectedItem.ToString())
            {
                case "Military": soundPath = "\\Coi-bao-thuc-trong-quan-doi-Ken.wav";
                    break;
                case "Sweet": soundPath = "\\best_sweet_alarm_1.wav";
                    break;
                case "Classic Iphone": soundPath = "\\iphone_alarm_morning_1.wav";
                    break;
                case "Special Iphone": soundPath = "\\iphone_alarm.wav";
                    break;
                default:
                    soundPath = "\\iphone_alarm_morning_1.wav";
                    break;
            }
            soundPlayer = new SoundPlayer(Application.StartupPath + soundPath);
            soundPlayer.Play();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            soundPlayer.Stop();
            Close();
            Thread thread = new Thread(o =>
            {
                Thread.Sleep(3000);
                Dispose();
            });
            thread.Start();
            check = false;
        }

        public static bool checkThreadAlive()
        {
            return check;
        }
    }
}