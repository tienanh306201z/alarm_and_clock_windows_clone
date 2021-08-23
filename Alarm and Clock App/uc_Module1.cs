using System;
using System.Drawing;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Alarm_and_Clock_App
{
    public partial class uc_Module1 : UserControl
    {
        private static uc_Module1 _instance;
        public static uc_Module1 Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_Module1();
                return _instance;
            }
        }
        
        private Timer[] _timers = new Timer[9];
        private Time[] _times = new Time[9];
        public uc_Module1()
        {
            InitializeComponent();
            for (int i = 0; i < 9; i++)
            {
                _timers[i] = new Timer() {Interval = 1000, Enabled = false};
            }

            for (int i = 0; i < 9; i++)
            {
                _times[i] = new Time();
            }
        }

        private int check = 1;
        private int lastLocation = -55;
        private int distance = 63;
        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            if (check <= 9 && check >= 1)
            {
                CreateNewPanel();
            }
            else if (check > 9)
            {
                MessageBox.Show("Đã đạt đến giới hạn số stopwatch!!!!", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void CreateNewPanel()
        {
            Panel panel = new Panel();
            panel.Size = new Size(895, 57);
            panel.Location = new Point(12, lastLocation + distance);
            lastLocation = lastLocation + distance;
            if (check % 2 == 1)
            {
                panel.BackColor = Color.LightGray;
            }
            else
            {
                panel.BackColor = Color.Gainsboro;
            }
            Label label1 = new Label()
            {
                Text = "Stopwatch " + check, Font = new Font(new FontFamily("Century Gothic"), 14.25f, FontStyle.Regular),
                Location = new Point(14, 0), Size = new Size(134, 57), TextAlign = ContentAlignment.MiddleCenter
            };
            check++;
            Label label2 = new Label()
            {
                Text = "00:00:00", Font = new Font(new FontFamily("Century Gothic"), 27.75f, FontStyle.Regular),
                Location = new Point(320, 0), Size = new Size(224, 57), TextAlign = ContentAlignment.MiddleCenter
            };
            Label label3 = new Label(){Size = new Size(134, 57)};
            Button button1 = new Button()
            {
                Text = "Reset", Dock = DockStyle.Right, Size = new Size(85, 57),
                Font = new Font(new FontFamily("Century Gothic"), 14.25f, FontStyle.Regular)
            };
            Button button2 = new Button()
            {
                Text = "Start", Dock = DockStyle.Right, Size = new Size(85, 57),
                Font = new Font(new FontFamily("Century Gothic"), 14.25f, FontStyle.Regular)
            };
            panel.Controls.Add(label1);
            panel.Controls.Add(label3);
            panel.Controls.Add(label2);
            panel.Controls.Add(button2);
            panel.Controls.Add(button1);
            Instance.Controls.Add(panel);
            Instance.Controls.SetChildIndex(panel, check);
            
            bool check1 = false;
            button2.Click += (o, args) =>
            {
                check1 = !check1;
                if (check1)
                {
                    _timers[Int32.Parse(label1.Text.Substring(10)) - 1] = new Timer(){Interval = 1000, Enabled = true};
                    button2.Text = "Stop";
                    //_timers[Int32.Parse(label1.Text.Substring(10))-1].Start();
                    _timers[Int32.Parse(label1.Text.Substring(10))-1].Elapsed += (obj, args1) =>
                    {
                        TimeUp.check = true;
                        button2.Text = "Stop";
                        label3.Text = Thread.CurrentThread.ManagedThreadId.ToString();
                        _times[Int32.Parse(label1.Text.Substring(10))-1].Second ++;
                        _times[Int32.Parse(label1.Text.Substring(10))-1].Minute += _times[Int32.Parse(label1.Text.Substring(10))-1].Second / 60;
                        _times[Int32.Parse(label1.Text.Substring(10))-1].Hour += _times[Int32.Parse(label1.Text.Substring(10))-1].Minute / 60;
                        _times[Int32.Parse(label1.Text.Substring(10))-1].Minute %= 60;
                        _times[Int32.Parse(label1.Text.Substring(10))-1].Second %= 60;

                        string hourToString = _times[Int32.Parse(label1.Text.Substring(10))-1].Hour >= 10 ? _times[Int32.Parse(label1.Text.Substring(10))-1].Hour.ToString() : "0" + _times[Int32.Parse(label1.Text.Substring(10))-1].Hour;
                        string minuteToString = _times[Int32.Parse(label1.Text.Substring(10))-1].Minute >= 10 ? _times[Int32.Parse(label1.Text.Substring(10))-1].Minute.ToString() : "0" + _times[Int32.Parse(label1.Text.Substring(10))-1].Minute;
                        string secondToString = _times[Int32.Parse(label1.Text.Substring(10))-1].Second >= 10 ? _times[Int32.Parse(label1.Text.Substring(10))-1].Second.ToString() : "0" + _times[Int32.Parse(label1.Text.Substring(10))-1].Second;

                        label2.Text = hourToString + ":" + minuteToString + ":" + secondToString;
                    };
                }
                else
                {
                    TimeUp.check = false;
                    button2.Text = "Start";
                    _timers[Int32.Parse(label1.Text.Substring(10))-1].Stop();
                    _timers[Int32.Parse(label1.Text.Substring(10))-1].Enabled = false;
                    _timers[Int32.Parse(label1.Text.Substring(10))-1].Dispose();
                }
            };
            button1.Click += (sender, args) =>
            {
                if (!check1)
                {
                    _times[Int32.Parse(label1.Text.Substring(10))-1].Hour = 0;
                    _times[Int32.Parse(label1.Text.Substring(10))-1].Minute = 0;
                    _times[Int32.Parse(label1.Text.Substring(10))-1].Second = 0;
                    label2.Text = "00:00:00";
                }
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (check > 2)
            {
                _timers[check - 2].Dispose();
                TimeUp.check = false;
                lastLocation -= distance;
                Instance.Controls.RemoveAt(check);
                _timers[check-2].Close();
                _times[check-2].Hour = 0;
                _times[check-2].Minute = 0;
                _times[check-2].Second = 0;
                check--;
            }
            else if (check == 2)
            {
                _timers[check - 2].Dispose();
                TimeUp.check = false;
                lastLocation -= distance;
                _timers[check-2].Close();
                _times[check-2].Hour = 0;
                _times[check-2].Minute = 0;
                _times[check-2].Second = 0;
                panel1.Visible = true;
                Instance.Controls.RemoveAt(check);
                check--;
            }
        }
    }
}