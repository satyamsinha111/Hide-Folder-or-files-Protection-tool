using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HideFolders
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void checkBoxShow_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxShow.Checked)
            {
                checkBoxShow.Text = "Hide";
                textBoxPassword.UseSystemPasswordChar = false;
                textBoxConfirmPassword.UseSystemPasswordChar = false;
            }
            else if(!checkBoxShow.Checked)
            {
                checkBoxShow.Text = "Show";
                textBoxPassword.UseSystemPasswordChar = true;
                textBoxConfirmPassword.UseSystemPasswordChar = true;
            }
        }

        private void timerWar_Tick(object sender, EventArgs e)
        {
            labelFlag.Hide();
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            if(textBoxConfirmPassword.Text!="" && textBoxPassword.Text!="")
            {
                //code goes here
                if(textBoxPassword.Text==textBoxConfirmPassword.Text)
                {
                    Properties.Settings.Default.password = textBoxPassword.Text;
                    Properties.Settings.Default.Save();
                    labelFlag.Text = "Password changed";
                    timerWar.Stop();
                    labelFlag.Show();
                    timerWar.Start();
                }
                else if(textBoxPassword.Text!=textBoxConfirmPassword.Text)
                {
                    labelFlag.Text = "Password don`t match!";
                    timerWar.Stop();
                    labelFlag.Show();
                    timerWar.Start();
                }
            }
            else if(textBoxConfirmPassword.Text == "" && textBoxPassword.Text == "")
            {
                timerWar.Stop();
                labelFlag.Show();
                timerWar.Start();
            }
            else if(textBoxConfirmPassword.Text != "" && textBoxPassword.Text == "")
            {
                timerWar.Stop();
                labelFlag.Show();
                timerWar.Start();
            }
            else if(textBoxConfirmPassword.Text == "" && textBoxPassword.Text != "")
            {
                timerWar.Stop();
                labelFlag.Show();
                timerWar.Start();
            }
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = true;
            textBoxConfirmPassword.UseSystemPasswordChar = true;
        }
    }
}
