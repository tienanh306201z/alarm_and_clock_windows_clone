using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Alarm_and_Clock_App
{
    public partial class SetAlarmTime : Form
    {
        public bool isClicked;
        public MyCheckBox myCheckBox;
        public SetAlarmTime()
        {
            InitializeComponent();
            ControlBox = false;
            myCheckBox = new MyCheckBox() {Size = new Size(40, 20), Location = new Point(102, 157)};   
            Controls.Add(myCheckBox);
            panel1.Enabled = false;
            myCheckBox.Click += (sender, args) =>
            {
                if (myCheckBox.Checked)
                {
                    panel1.Enabled = true;
                }
                else
                {
                    panel1.Enabled = false;
                }
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isClicked = true;
            Close();
            Thread thread = new Thread(o =>
            {
                Thread.Sleep(3000);
                Dispose();
            });
            thread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            Thread thread = new Thread(o =>
            {
                Thread.Sleep(3000);
                Dispose();
            });
            thread.Start();
        }

        private void SetAlarmTime_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <= 12; i++)
            {
                comboBox1.Items.Add(i);
            }
            
            for (int i = 0; i < 60; i++)
            {
                comboBox2.Items.Add(i);
            }

            comboBox4.Items.Add("AM");
            comboBox4.Items.Add("PM");

            for (int i = 5; i <= 30; i+= 5)
            {
                comboBox3.Items.Add(i);
            }

            comboBox1.SelectedIndex = 7;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }
    }
}