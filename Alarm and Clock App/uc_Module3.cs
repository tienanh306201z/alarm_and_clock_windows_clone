using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Alarm_and_Clock_App
{
    public partial class uc_Module3 : UserControl
    {
        private static uc_Module3 _instance;
        public static uc_Module3 Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_Module3();
                return _instance;
            }
        }
        private Timer[] _timers = new Timer[6];
        private Time[] _times = new Time[6];
        private int[] totalSeconds = new int[6];
        private int[] timeTemp = new int[6];
        public uc_Module3()
        {
            InitializeComponent();
            for (int i = 0; i < 6; i++)
            {
                _timers[i] = new Timer() {Interval = 1000, Enabled = false};
            }

            for (int i = 0; i < 6; i++)
            {
                _times[i] = new Time();
            }
        }

        private int check = 1;
        private int lastLocationX = -296;
        private int lastLocationY = 5;
        private int distance = 301;
        private void button4_Click(object sender, EventArgs e)
        {
            button4.BringToFront();
            button1.BringToFront();
            SetStopWatchTime form = new SetStopWatchTime();
            form.isClicked = false;
            form.ShowDialog();
            if (form.isClicked)
            {
                panel1.Visible = false;
                if (check <=6 && check >= 1)
                {
                    totalSeconds[check-1] = Int32.Parse(form.comboBox3.SelectedItem.ToString()) * 3600 + Int32.Parse(form.comboBox1.SelectedItem.ToString()) * 60 + Int32.Parse(form.comboBox2.SelectedItem.ToString());
                    timeTemp[check - 1] = totalSeconds[check - 1]; 
                    CreateNewPanel();
                }
                else if (check > 6)
                {
                    MessageBox.Show("Đã đạt đến giới hạn số timer!!!!", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void CreateNewPanel()
        {
            if (check == 4)
            {
                lastLocationX = -296;
                lastLocationY = 306;
            }
            _times[check-1].Hour = totalSeconds[check-1] / 3600;
            _times[check-1].Minute = (totalSeconds[check-1] - _times[check-1].Hour * 3600) / 60;
            _times[check-1].Second = totalSeconds[check-1] - _times[check-1].Hour * 3600 - _times[check-1].Minute * 60;
                            
            string hourToString = _times[check-1].Hour >= 10 ? _times[check-1].Hour.ToString() : "0" + _times[check-1].Hour;
            string minuteToString = _times[check-1].Minute >= 10 ? _times[check-1].Minute.ToString() : "0" + _times[check-1].Minute;
            string secondToString = _times[check-1].Second >= 10 ? _times[check-1].Second.ToString() : "0" + _times[check-1].Second;
            
            Panel panel = new Panel();
            panel.Size = new Size(new Point(296, 296));
            panel.Location = new Point(lastLocationX + distance, lastLocationY);
            panel.BackColor = Color.WhiteSmoke;
            lastLocationX += distance;
            Label label1 = new Label()
            {
                Text = "Timer " + check, Font = new Font(new FontFamily("Century Gothic"), 12), Location = new Point(0,0)
            };
            check++;
            //Label label2 = new Label(){Location = new Point(0,55)};
            global::CircularProgressBar.CircularProgressBar circularProgressBar = new global::CircularProgressBar.CircularProgressBar()
            {
                Size = new Size(206,206), Location = new Point(45,21), OuterMargin = -20, OuterWidth = 20, 
                ProgressWidth = 15, TextMargin = new Padding(0,5,0,0), Text = hourToString + ":" + minuteToString + ":" + secondToString, Font = new Font(new FontFamily("Century Gothic"), 24),
                InnerColor = Color.FromArgb(224,224,224), SubscriptText = "", SuperscriptText = "", Maximum = totalSeconds[Int32.Parse(label1.Text.Substring(6)) - 1], OuterColor = Color.Gray,
                ProgressColor = Color.LightSeaGreen, Value = totalSeconds[Int32.Parse(label1.Text.Substring(6)) - 1], Minimum = 0
            };
            Button button = new Button() {Text = "Start", Location = new Point(66,252), BackColor = Color.LightSeaGreen};
            Button button2 = new Button() {Text = "Reset", Location = new Point(158,252), BackColor = Color.Salmon};
            panel.Controls.Add(circularProgressBar);
            panel.Controls.Add(button);
            panel.Controls.Add(button2);
            panel.Controls.Add(label1);
            //panel.Controls.Add(label2);
            Instance.Controls.Add(panel);
            Instance.Controls.SetChildIndex(panel, check);

            bool check1 = false;
            button.Click += (sender, args) =>
            {
                check1 = !check1;
                if (check1)
                {
                    _timers[Int32.Parse(label1.Text.Substring(6)) - 1] = new Timer(){Interval = 1000, Enabled = true};
                    button.Text = "Stop";
                    //_timers[Int32.Parse(label1.Text.Substring(10))-1].Start();
                    _timers[Int32.Parse(label1.Text.Substring(6))-1].Elapsed += (obj, args1) =>
                    {
                        //label2.Text = Thread.CurrentThread.ManagedThreadId.ToString();
                        if (totalSeconds[Int32.Parse(label1.Text.Substring(6)) - 1] > 0)
                        {
                            TimeUp.check = true;
                            totalSeconds[Int32.Parse(label1.Text.Substring(6)) - 1]--;
                            circularProgressBar.Value = totalSeconds[Int32.Parse(label1.Text.Substring(6)) - 1];
                            circularProgressBar.Update();
                            hourToString = setTimeForProgressBar(label1, circularProgressBar, out minuteToString, out secondToString);
                        }
                        else
                        {
                            _timers[Int32.Parse(label1.Text.Substring(6))-1].Stop();
                            check1 = !check1;
                            button.Text = "Start";
                            totalSeconds[Int32.Parse(label1.Text.Substring(6)) - 1] =
                                timeTemp[Int32.Parse(label1.Text.Substring(6)) - 1];
                            circularProgressBar.Value = totalSeconds[Int32.Parse(label1.Text.Substring(6)) - 1];
                            hourToString = setTimeForProgressBar(label1, circularProgressBar, out minuteToString, out secondToString);
                            TimeUp timeUp = new TimeUp();
                            timeUp.ShowDialog();
                        }
                    };
                }
                else
                {
                    TimeUp.check = false;
                    button.Text = "Start";
                    _timers[Int32.Parse(label1.Text.Substring(6))-1].Stop();
                    _timers[Int32.Parse(label1.Text.Substring(6))-1].Enabled = false;
                    _timers[Int32.Parse(label1.Text.Substring(6))-1].Dispose();
                    button2.Click += (o, eventArgs) =>
                    {
                        if (!check1)
                        {
                            totalSeconds[Int32.Parse(label1.Text.Substring(6)) - 1] =
                                timeTemp[Int32.Parse(label1.Text.Substring(6)) - 1];
                            circularProgressBar.Value = totalSeconds[Int32.Parse(label1.Text.Substring(6)) - 1];
                            hourToString = setTimeForProgressBar(label1, circularProgressBar, out minuteToString, out secondToString);
                        }
                    };
                }
            };

            if (!check1)
            {
                panel.Click += (sender, args) =>
                {
                    SetStopWatchTime form = new SetStopWatchTime();
                    form.isClicked = false;
                    form.ShowDialog();
                    if (form.isClicked)
                    {
                        totalSeconds[Int32.Parse(label1.Text.Substring(6))-1] = Int32.Parse(form.comboBox3.SelectedItem.ToString()) * 3600 + Int32.Parse(form.comboBox1.SelectedItem.ToString()) * 60 + Int32.Parse(form.comboBox2.SelectedItem.ToString());
                        timeTemp[Int32.Parse(label1.Text.Substring(6))-1] = totalSeconds[Int32.Parse(label1.Text.Substring(6))-1];

                        circularProgressBar.Maximum = totalSeconds[Int32.Parse(label1.Text.Substring(6)) - 1];
                        circularProgressBar.Value = totalSeconds[Int32.Parse(label1.Text.Substring(6)) - 1];

                        hourToString = setTimeForProgressBar(label1, circularProgressBar, out minuteToString, out secondToString);
                    }
                };
            }

            if (!check1)
            {
                circularProgressBar.Click += (sender, args) =>
                {
                    SetStopWatchTime form = new SetStopWatchTime();
                    form.isClicked = false;
                    form.ShowDialog();
                    if (form.isClicked)
                    {
                        totalSeconds[Int32.Parse(label1.Text.Substring(6))-1] = Int32.Parse(form.comboBox3.SelectedItem.ToString()) * 3600 + Int32.Parse(form.comboBox1.SelectedItem.ToString()) * 60 + Int32.Parse(form.comboBox2.SelectedItem.ToString());
                        timeTemp[Int32.Parse(label1.Text.Substring(6))-1] = totalSeconds[Int32.Parse(label1.Text.Substring(6))-1];

                        circularProgressBar.Maximum = totalSeconds[Int32.Parse(label1.Text.Substring(6)) - 1];
                        circularProgressBar.Value = totalSeconds[Int32.Parse(label1.Text.Substring(6)) - 1];

                        hourToString = setTimeForProgressBar(label1, circularProgressBar, out minuteToString, out secondToString);
                    }
                };
            }
        }

        private string setTimeForProgressBar(Label label1, CircularProgressBar.CircularProgressBar circularProgressBar, out string minuteToString,
            out string secondToString)
        {
            string hourToString;
            _times[Int32.Parse(label1.Text.Substring(6)) - 1].Hour =
                totalSeconds[Int32.Parse(label1.Text.Substring(6)) - 1] / 3600;
            _times[Int32.Parse(label1.Text.Substring(6)) - 1].Minute =
                (totalSeconds[Int32.Parse(label1.Text.Substring(6)) - 1] -
                 _times[Int32.Parse(label1.Text.Substring(6)) - 1].Hour * 3600) / 60;
            _times[Int32.Parse(label1.Text.Substring(6)) - 1].Second = totalSeconds[Int32.Parse(label1.Text.Substring(6)) - 1] -
                                                                       _times[Int32.Parse(label1.Text.Substring(6)) - 1].Hour *
                                                                       3600 - _times[Int32.Parse(label1.Text.Substring(6)) - 1]
                                                                           .Minute * 60;

            hourToString = _times[Int32.Parse(label1.Text.Substring(6)) - 1].Hour >= 10
                ? _times[Int32.Parse(label1.Text.Substring(6)) - 1].Hour.ToString()
                : "0" + _times[Int32.Parse(label1.Text.Substring(6)) - 1].Hour;
            minuteToString = _times[Int32.Parse(label1.Text.Substring(6)) - 1].Minute >= 10
                ? _times[Int32.Parse(label1.Text.Substring(6)) - 1].Minute.ToString()
                : "0" + _times[Int32.Parse(label1.Text.Substring(6)) - 1].Minute;
            secondToString = _times[Int32.Parse(label1.Text.Substring(6)) - 1].Second >= 10
                ? _times[Int32.Parse(label1.Text.Substring(6)) - 1].Second.ToString()
                : "0" + _times[Int32.Parse(label1.Text.Substring(6)) - 1].Second;

            circularProgressBar.Text = hourToString + ":" + minuteToString + ":" + secondToString;
            return hourToString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*Instance.Controls.SetChildIndex(button1, 8);*/
            button1.BringToFront();
            if (check > 2)
            {
                _timers[check - 2].Dispose();
                TimeUp.check = false;
                if(check != 5)
                    lastLocationX -= distance;
                else
                {
                    lastLocationY = 5;
                    lastLocationX = 607;
                }
                Instance.Controls.RemoveAt(check);
                _timers[check-2].Close();
                check--; 
            }
            else if (check == 2)
            {
                _timers[check - 2].Dispose();
                TimeUp.check = false;
                lastLocationX -= distance;
                _timers[check-2].Close();
                panel1.Visible = true;
                Instance.Controls.RemoveAt(check);
                check--;
            }
        }
    }
}