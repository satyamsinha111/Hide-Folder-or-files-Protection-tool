using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HideFolders
{
    public partial class ucLogin : UserControl
    {
        public ucLogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if(textBoxPass.Text!="")
            {
                if(textBoxPass.Text==Properties.Settings.Default.password)
                {
                    labelFlag.Text = "Logged in successfully";
                    labelFlag.Show();
                    timerWarmngs.Start();
                    this.Hide();
                    globalVar.isUser = true;
                }
                else if(textBoxPass.Text != Properties.Settings.Default.password)
                {
                    labelFlag.Text = "Incorrect credentials given";
                    labelFlag.Show();
                    timerWarmngs.Start();
                    globalVar.isUser = false;
                }
            }
            else if(textBoxPass.Text=="")
            {
                labelFlag.Text = "Password field is empty";
                labelFlag.Show();
                timerWarmngs.Start();

            }
        }

        private void checkBoxFlag_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxFlag.Checked)
            {
                checkBoxFlag.Text = "Hide";
                textBoxPass.UseSystemPasswordChar = false;
            }
            else if(!checkBoxFlag.Checked)
            {
                checkBoxFlag.Text = "Show";
                
                textBoxPass.UseSystemPasswordChar = true;
            }
        }

        private void ucLogin_Load(object sender, EventArgs e)
        {
            textBoxPass.UseSystemPasswordChar = true;
        }

        private void timerWarmngs_Tick(object sender, EventArgs e)
        {
            labelFlag.Hide();
            
        }
    }
}
