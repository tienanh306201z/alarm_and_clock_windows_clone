using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace Alarm_and_Clock_App
{
    public partial class Form1 : Form
    {
        private bool mouseDown;
        private Point lastLocation;

        public Form1()
        {
            InitializeComponent();
        }

        #region addUC

        private void button1_Click(object sender, EventArgs e)
        {
            if (!panel4.Controls.Contains(uc_Module1.Instance))
            {
                panel4.Controls.Add(uc_Module1.Instance);
                uc_Module1.Instance.Dock = DockStyle.Fill;
                uc_Module1.Instance.BringToFront();
            }
            else
                uc_Module1.Instance.BringToFront();

            panel3.Location = new Point(0, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!panel4.Controls.Contains(uc_Module2.Instance))
            {
                panel4.Controls.Add(uc_Module2.Instance);
                uc_Module2.Instance.Dock = DockStyle.Fill;
                uc_Module2.Instance.BringToFront();
            }
            else
                uc_Module2.Instance.BringToFront();

            panel3.Location = new Point(0, 35);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Displaynotify();

            if (!panel4.Controls.Contains(uc_Module1.Instance))
            {
                panel4.Controls.Add(uc_Module1.Instance);
                uc_Module1.Instance.Dock = DockStyle.Fill;
                uc_Module1.Instance.BringToFront();
            }
            else
                uc_Module1.Instance.BringToFront();

            panel3.Location = new Point(0, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!panel4.Controls.Contains(uc_Module3.Instance))
            {
                panel4.Controls.Add(uc_Module3.Instance);
                uc_Module3.Instance.Dock = DockStyle.Fill;
                uc_Module3.Instance.BringToFront();
            }
            else
                uc_Module3.Instance.BringToFront();

            panel3.Location = new Point(0, 70);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!panel4.Controls.Contains(uc_Module4.Instance))
            {
                panel4.Controls.Add(uc_Module4.Instance);
                uc_Module4.Instance.Dock = DockStyle.Fill;
                uc_Module4.Instance.BringToFront();
            }
            else
                uc_Module4.Instance.BringToFront();

            panel3.Location = new Point(0, 640);
        }
        

        #endregion
        
        #region Custom Escape Button

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (TimeUp.checkThreadAlive())
            {
                Hide();
            }
            else
            {
                Application.Exit();
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            PictureBox button = sender as PictureBox;
            button.BackColor = Color.IndianRed;
            ;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            PictureBox button = sender as PictureBox;
            button.BackColor = Color.Gainsboro;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            PictureBox button = sender as PictureBox;
            button.BackColor = Color.Teal;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            PictureBox button = sender as PictureBox;
            button.BackColor = Color.Gainsboro;
        }

        #endregion

        #region Custom Drag Winform

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }


        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Location = new Point((Location.X - lastLocation.X) + e.X, (Location.Y - lastLocation.Y) + e.Y);

                Update();
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        #endregion

        #region Custom NotifyIcon

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
        }

        protected void Displaynotify()
        {
            try
            {
                notifyIcon1.Text = "Alarm and Clock v.10";
                notifyIcon1.Visible = true;
                notifyIcon1.BalloonTipTitle = "Thanks for using Alarm and Clock v1.0";
                notifyIcon1.BalloonTipText = "Click Here to see details";
                notifyIcon1.ShowBalloonTip(100);
            }
            catch (Exception ex)
            {
            }
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            if (!panel4.Controls.Contains(uc_Module4.Instance))
            {
                panel4.Controls.Add(uc_Module4.Instance);
                uc_Module4.Instance.Dock = DockStyle.Fill;
                uc_Module4.Instance.BringToFront();
            }
            else
                uc_Module4.Instance.BringToFront();

            panel3.Location = new Point(0, 640);
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                MenuItem menuItem = new MenuItem() {Text = "Alarm and Clock v1.0"};
                MenuItem menuItem2 = new MenuItem() {Text = "Setting"};
                MenuItem menuItem3 = new MenuItem() {Text = "Close"};
                ContextMenu contextMenu = new ContextMenu();
                contextMenu.MenuItems.Add(menuItem);
                contextMenu.MenuItems.Add(menuItem2);
                contextMenu.MenuItems.Add(menuItem3);

                notifyIcon1.ContextMenu = contextMenu;

                menuItem.Click += (o, args) =>
                {
                    Show();
                };

                menuItem2.Click += (o, args) =>
                {
                    Show();
                    if (!panel4.Controls.Contains(uc_Module4.Instance))
                    { 
                        panel4.Controls.Add(uc_Module4.Instance); 
                        uc_Module4.Instance.Dock = DockStyle.Fill; 
                        uc_Module4.Instance.BringToFront();
                    }
                    else uc_Module4.Instance.BringToFront();

                    panel3.Location = new Point(0, 640);
                };

                menuItem3.Click += (o, args) =>
                {
                    Application.ExitThread();
                    Application.Exit();
                };
            }
        }

        #endregion
        
    }
}