using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Alarm_and_Clock_App
{
    public partial class uc_Module4 : UserControl
    {
        public static uc_Module4 _instance;

        public static uc_Module4 Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_Module4();
                return _instance;
            }
        }
        
        public uc_Module4()
        {
            InitializeComponent();
            AddItems();
        }

        private void AddItems()
        {
            comboBox1.BackColor = Color.Beige;
            comboBox1.Items.Add("Military");
            comboBox1.Items.Add("Sweet");
            comboBox1.Items.Add("Classic Iphone");
            comboBox1.Items.Add("Special Iphone");
            comboBox1.SelectedIndex = comboBox1.FindString("Classic Iphone");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://forms.gle/umVmrRpzTXusTDVU7");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://www.facebook.com/ceilingprogressproductions2001/");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://www.instagram.com/alva.chan.306/");
        }


        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://alvachanit.wordpress.com/");
        }
    }
}