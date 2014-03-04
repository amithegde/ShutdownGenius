using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShutdownGenius
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var message = string.Empty;
            if (radHibernate.Checked)
            {
                message = "Hibernate";
            }
            else if (radShutdown.Checked)
            {
                message = "Shutdown";
            }else if (radRestart.Checked)
            {
                message = "Restart";
            }

            label3.Text = message + "Set at " + dateTimePicker1.Value.ToString("yyyy-MMM-dd HH:mm:ss");

            Task.Factory.StartNew(() =>
            {
                try
                {
                    var exectutionTime = dateTimePicker1.Value;

                    while (DateTime.Now < exectutionTime)
                    {
                        Thread.Sleep(10000);
                    }

                    DoAction();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("invalid time");
                }
            });
        }

        private void DoAction()
        {
            if (radHibernate.Checked)
            {
                // Hibernate
                Application.SetSuspendState(PowerState.Hibernate, true, true);
            }
            else if (radShutdown.Checked)
            {
                Process.Start("shutdown", "/s /t 0");
            }
            else if (radRestart.Checked)
            {
                Process.Start("shutdown", "/r /t 0");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddMinutes(10);
        }
    }
}