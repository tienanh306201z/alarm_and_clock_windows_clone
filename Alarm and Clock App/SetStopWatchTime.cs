using System;
using System.Threading;
using System.Windows.Forms;

namespace Alarm_and_Clock_App
{
    public partial class SetStopWatchTime : Form
    {
        public bool isClicked;
        public SetStopWatchTime()
        {
            InitializeComponent();
            ControlBox = false;
        }

        private void SetStopWatchTime_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 60; i++)
            {
                comboBox2.Items.Add(i);
                comboBox1.Items.Add(i);
            }

            for (int i = 0; i < 24; i++)
            {
                comboBox3.Items.Add(i);
            }
            comboBox1.SelectedIndex = 5;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            isClicked = true;
            Close();
            Thread thread = new Thread(o =>
            {
                Thread.Sleep(3000);
                Dispose();
            });
            thread.Start();
            //Dispose();
        }
    }
}