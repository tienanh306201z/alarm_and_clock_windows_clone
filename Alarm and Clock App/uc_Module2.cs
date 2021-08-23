using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Alarm_and_Clock_App
{
    public partial class uc_Module2 : UserControl
    {
        private static uc_Module2 _instance;
        public static uc_Module2 Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_Module2();
                return _instance;
            }
        }
        private Time[] _times = new Time[9];
        private Timer[] _timers = new Timer[9];
        public uc_Module2()
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
        private int lastLocationX = -294;
        private int lastLocationY = 3;
        private int distance = 299;
        private void button4_Click(object sender, EventArgs e)
        {
            button4.BringToFront();
            button1.BringToFront();
            SetAlarmTime setAlarmTime = new SetAlarmTime();
            setAlarmTime.isClicked = false;
            setAlarmTime.ShowDialog();
            if (setAlarmTime.isClicked)
            {
                panel1.Visible = false;
                if (check <= 9 && check >= 1)
                {
                    _times[check - 1 ].Hour = Int32.Parse(setAlarmTime.comboBox1.SelectedItem.ToString());
                    _times[check - 1 ].Minute = Int32.Parse(setAlarmTime.comboBox2.SelectedItem.ToString());
                    _times[check - 1 ].Half = setAlarmTime.comboBox4.SelectedItem.ToString();
                    if (setAlarmTime.myCheckBox.Checked)
                    {
                        _times[check - 1 ].Snooze = Int32.Parse(setAlarmTime.comboBox3.SelectedItem.ToString());
                    }

                    else
                    {
                        _times[check - 1].Snooze = 0;
                    }
                    CreateNewPanel();
                }
                else if (check > 9)
                {
                    MessageBox.Show("Đã đạt đến giới hạn số alarm!!!!", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void CreateNewPanel()
        {
            if (check == 4)
            {
                lastLocationX = -294;
                lastLocationY = 179;
            }

            if (check == 7)
            {
                lastLocationX = -294;
                lastLocationY = 355;
            }
            
            string hourToString = _times[check-1].Hour >= 10 ? _times[check-1].Hour.ToString() : "0" + _times[check-1].Hour;
            string minuteToString = _times[check-1].Minute >= 10 ? _times[check-1].Minute.ToString() : "0" + _times[check-1].Minute;

            Panel panel = new Panel()
            {
                BackColor = Color.LightGray, Size = new Size(294, 171),
                Location = new Point(lastLocationX + distance, lastLocationY)
            };
            lastLocationX += distance;
            Label label = new Label()
            {
                Text = hourToString + ":" + minuteToString, Font = new Font(new FontFamily("Century Gothic"), 48),
                TextAlign = ContentAlignment.MiddleCenter, Size = new Size(192, 95),
                Location = new Point(3, 0), ForeColor = Color.Gray
            };
            Label label2 = new Label()
            {
                Text = "Alarm " + check, Font = new Font(new FontFamily("Century Gothic"), 8.25f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(53, 28), Location = new Point(18, 95), ForeColor = Color.Gray
            };
            check++;
            Label label1 = new Label()
            {
                Text = _times[Int32.Parse(label2.Text.Substring(6))-1].Half, Font = new Font(new FontFamily("Century Gothic"), 8.25f),
                TextAlign = ContentAlignment.MiddleCenter, Size = new Size(36, 28),
                Location = new Point(173, 53), ForeColor = Color.Gray
            };
            Label label3 = new Label()
            {
                Text = "Snooze in "+ _times[Int32.Parse(label2.Text.Substring(6))-1].Snooze +" minutes", Font = new Font(new FontFamily("Century Gothic"), 8.25f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(123, 28), Location = new Point(18, 132), ForeColor = Color.Gray
            };
            MyCheckBox myCheckBox = new MyCheckBox() {Size = new Size(40, 20), Location = new Point(245, 3)};
            panel.Controls.Add(label1);
            panel.Controls.Add(label);
            panel.Controls.Add(label2);
            panel.Controls.Add(label3);
            panel.Controls.Add(myCheckBox);
            Instance.Controls.Add(panel);
            Instance.Controls.SetChildIndex(panel, check);

            myCheckBox.CheckedChanged += (o, args) =>
            {
                if (myCheckBox.Checked)
                {
                    panel.BackColor = Color.Snow;
                    label.ForeColor = Color.Black;
                    label1.ForeColor = Color.Black;
                    label2.ForeColor = Color.Black;
                    label3.ForeColor = Color.Black;

                    bool check1 = false;
                    
                    RestartPoint:
                    _timers[Int32.Parse(label2.Text.Substring(6)) - 1] = new Timer(){Interval = 1000, Enabled = true};
                    _timers[Int32.Parse(label2.Text.Substring(6)) - 1].Elapsed += (sender, eventArgs) =>
                    {
                        TimeUp.check = true;
                        string now = hourToString + ":" +
                                     minuteToString + " " +
                                     _times[Int32.Parse(label2.Text.Substring(6)) - 1].Half;
                        if (DateTime.Now.ToString(@"hh:mm tt").Equals(now))
                        {
                            if (_times[Int32.Parse(label2.Text.Substring(6)) - 1].Snooze == 0)
                            {
                                _timers[Int32.Parse(label2.Text.Substring(6)) - 1].Close();
                                _timers[Int32.Parse(label2.Text.Substring(6)) - 1].Dispose();
                                myCheckBox.Checked = !myCheckBox.Checked;
                                panel.BackColor = Color.LightGray;
                                label.ForeColor = Color.Gray;
                                label1.ForeColor = Color.Gray;
                                label2.ForeColor = Color.Gray;
                                label3.ForeColor = Color.Gray;
                                
                                TimeUp.check = false;
                                check1 = false;
                                TimeUp timeUp = new TimeUp();
                                timeUp.ShowDialog();
                            }
                            else
                            {
                                _timers[Int32.Parse(label2.Text.Substring(6)) - 1].Close();
                                _timers[Int32.Parse(label2.Text.Substring(6)) - 1].Dispose();
                                
                                TimeUp timeUp = new TimeUp();
                                timeUp.ShowDialog();

                                _times[Int32.Parse(label2.Text.Substring(6)) - 1].Minute += _times[Int32.Parse(label2.Text.Substring(6)) - 1].Snooze;
                                _times[Int32.Parse(label2.Text.Substring(6)) - 1].Hour +=
                                    _times[Int32.Parse(label2.Text.Substring(6)) - 1].Minute / 60;
                                _times[Int32.Parse(label2.Text.Substring(6)) - 1].Minute %= 60;

                                if (_times[Int32.Parse(label2.Text.Substring(6)) - 1].Hour == 0)
                                {
                                    _times[Int32.Parse(label2.Text.Substring(6)) - 1].Half = "AM";
                                }
                                else if (_times[Int32.Parse(label2.Text.Substring(6)) - 1].Hour == 12)
                                {
                                    _times[Int32.Parse(label2.Text.Substring(6)) - 1].Half = "PM";
                                }

                                hourToString = _times[Int32.Parse(label2.Text.Substring(6))-1].Hour >= 10 ? _times[Int32.Parse(label2.Text.Substring(6))-1].Hour.ToString() : "0" + _times[Int32.Parse(label2.Text.Substring(6))-1].Hour;
                                minuteToString = _times[Int32.Parse(label2.Text.Substring(6)) - 1].Minute >= 10
                                    ? _times[Int32.Parse(label2.Text.Substring(6)) - 1].Minute.ToString()
                                    : "0" + _times[Int32.Parse(label2.Text.Substring(6)) - 1].Minute;

                                label.Text = hourToString + ":" + minuteToString;
                                label1.Text = _times[Int32.Parse(label2.Text.Substring(6)) - 1].Half;
                                check1 = true;
                                myCheckBox.Checked = !myCheckBox.Checked;
                                myCheckBox.Checked = !myCheckBox.Checked;
                            }
                        }
                    };
                }
                else
                {
                    panel.BackColor = Color.LightGray;
                    label.ForeColor = Color.Gray;
                    label1.ForeColor = Color.Gray;
                    label2.ForeColor = Color.Gray;
                    label3.ForeColor = Color.Gray;
                    
                    _timers[Int32.Parse(label2.Text.Substring(6)) - 1].Close();
                    _timers[Int32.Parse(label2.Text.Substring(6)) - 1].Dispose();
                    TimeUp.check = false;
                }
            };
            
            panel.Click += (sender, args) =>
            {
                SetAlarmTime setAlarmTime = new SetAlarmTime();
                setAlarmTime.isClicked = false;
                setAlarmTime.ShowDialog();
                if (setAlarmTime.isClicked)
                {
                    _times[Int32.Parse(label2.Text.Substring(6))-1].Hour = Int32.Parse(setAlarmTime.comboBox1.SelectedItem.ToString());
                    _times[Int32.Parse(label2.Text.Substring(6))-1].Minute = Int32.Parse(setAlarmTime.comboBox2.SelectedItem.ToString());
                    _times[Int32.Parse(label2.Text.Substring(6))-1].Half = setAlarmTime.comboBox4.SelectedItem.ToString();
                    
                    if (setAlarmTime.myCheckBox.Checked)
                    {
                        _times[Int32.Parse(label2.Text.Substring(6))-1].Snooze = Int32.Parse(setAlarmTime.comboBox3.SelectedItem.ToString());
                    }

                    else
                    {
                        _times[Int32.Parse(label2.Text.Substring(6))-1].Snooze = 0;
                    }
                    
                    hourToString = _times[Int32.Parse(label2.Text.Substring(6))-1].Hour >= 10 ? _times[Int32.Parse(label2.Text.Substring(6))-1].Hour.ToString() : "0" + _times[Int32.Parse(label2.Text.Substring(6))-1].Hour;
                    minuteToString = _times[Int32.Parse(label2.Text.Substring(6))-1].Minute >= 10 ? _times[Int32.Parse(label2.Text.Substring(6))-1].Minute.ToString() : "0" + _times[Int32.Parse(label2.Text.Substring(6))-1].Minute;
                    
                    label.Text = hourToString + ":" + minuteToString;
                    label1.Text = _times[Int32.Parse(label2.Text.Substring(6)) - 1].Half;
                    label3.Text = "Snooze in " + _times[Int32.Parse(label2.Text.Substring(6)) - 1].Snooze + " minutes";
                }
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.BringToFront();
            if (check > 2)
            {
                _timers[check - 2].Dispose();
                TimeUp.check = false;
                if(check != 5 && check != 8)
                    lastLocationX -= distance;
                else if (check == 5)
                {
                    lastLocationY = 3;
                    lastLocationX = 603;
                }
                else {
                    lastLocationY = 179;
                    lastLocationX = 603;
                }
                Instance.Controls.RemoveAt(check);
                check--; 
            }
            else if (check == 2)
            {
                _timers[check - 2].Dispose();
                TimeUp.check = false;
                lastLocationX -= distance;
                Instance.Controls.RemoveAt(check);
                panel1.Visible = true;
                check--;
            }
        }
    }
}