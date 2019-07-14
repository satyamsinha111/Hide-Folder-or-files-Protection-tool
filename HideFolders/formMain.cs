using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;

namespace HideFolders
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
        }
        [System.Runtime.InteropServices.DllImport("Shell32.dll")]
        private static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);
        OpenFileDialog fileDialog = new OpenFileDialog();
        FolderBrowserDialog folderDialog = new FolderBrowserDialog();
        string[] files;
        List<string> myFiles = new List<string> { };
        private void browseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (globalVar.isUser == true)
            {
                fileDialog.Title = "Innovative minds hide file and folders";
                fileDialog.Multiselect = true;
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    files = fileDialog.FileNames;
                    foreach (string file in files)
                    {
                        dataGridViewMyFiles.Rows.Add(false, file.Substring(file.LastIndexOf(@"\") + 1), file, DateTime.Now.ToString(), "Unhidden");
                    }
                }
            }
            else if(globalVar.isUser==false)
            {
                ucLogin1.labelFlag.Show();
                timerShowHide.Start();
                
            }
        }

        private void browseFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (globalVar.isUser == true)
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string folder = folderDialog.SelectedPath;
                    dataGridViewMyFiles.Rows.Add(false, folder.Substring(folder.LastIndexOf(@"\") + 1), folder, DateTime.Now.ToString(), "Unhidden");
                }
            }
            else if(globalVar.isUser==false)
            {
                ucLogin1.labelFlag.Show();
                timerShowHide.Start();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (globalVar.isUser == true)
            {
                foreach (DataGridViewRow row in dataGridViewMyFiles.Rows)
                {
                    try
                    {
                        if ((bool)row.Cells[0].Value == true)
                        {
                            string fileName = row.Cells[1].Value.ToString();
                            string file = row.Cells[2].Value.ToString();
                            string lockDate = DateTime.Now.ToString();
                            string protection = "Hidden";
                            Modules.LockFileOrFolder(file);
                            Modules.addPermissons(file);
                            row.Cells[4].Value = "Hidden";
                            string query = "INSERT INTO [FileDet]([File],[FilePath],[LockingDate],[Protection])VALUES('" + fileName + "','" + file + "','" + lockDate + "','" + protection + "');";
                            Modules.sqlInsert(query);

                        }
                        else if ((bool)row.Cells[0].Value == false)
                        {

                        }
                    }
                    catch
                    {

                    }
                }
                SHChangeNotify(0x8000000, 0x1000, IntPtr.Zero, IntPtr.Zero);
            }
            else if(globalVar.isUser==false)
            {
                ucLogin1.labelFlag.Show();
                timerShowHide.Start();
            }
        }

        private void unHideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (globalVar.isUser == true)
            {
                foreach (DataGridViewRow row in dataGridViewMyFiles.Rows)
                {
                    try
                    {
                        if ((bool)row.Cells[0].Value == true)
                        {
                            string file = row.Cells[2].Value.ToString();
                            Modules.removePermisson(file);
                            Modules.unlockFilesAndFolders(file);
                            row.Cells[4].Value = "Unhidden";
                            string datasource = "DataSource=data.db";
                            DataTable dt = new DataTable();
                            using (SQLiteConnection conn = new SQLiteConnection(datasource))
                            {
                                string query = "SELECT [Id],[File],[FilePath],[LockingDate],[Protection]FROM [FileDet];";
                                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
                                adapter.Fill(dt);
                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (dr[2].ToString() == file)
                                    {
                                        string id = dr[0].ToString();
                                        string que = "DELETE FROM [FileDet]WHERE Id='" + id + "';";
                                        Modules.sqlInsert(que);
                                    }
                                }

                            }



                        }
                        else if ((bool)row.Cells[0].Value == false)
                        {

                        }
                    }
                    catch
                    {

                    }
                }
                SHChangeNotify(0x8000000, 0x1000, IntPtr.Zero, IntPtr.Zero);
            }
            else if(globalVar.isUser==false)
            {
                ucLogin1.labelFlag.Show();
                timerShowHide.Start();
            }
        }

        private void checkBoxFlag_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxFlag.Checked==true)
            {
                foreach(DataGridViewRow row in dataGridViewMyFiles.Rows)
                {
                    try
                    {
                        row.Cells[0].Value = true;
                    }
                    catch
                    {

                    }
                   
                }
            }
            else if(checkBoxFlag.Checked==false)
            {
                foreach (DataGridViewRow row in dataGridViewMyFiles.Rows)
                {
                    try
                    {
                        row.Cells[0].Value = false;
                    }
                    catch
                    {

                    }

                }
            }
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            //ucLogin1.Show();
            SHChangeNotify(0x8000000, 0x1000, IntPtr.Zero, IntPtr.Zero);
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(globalVar.isUser==true)
            {

            }
            else
            {
                //ucLogin1.labelFlag.Show();
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (globalVar.isUser == true)
            {
            }
            else if(globalVar.isUser == false)
            {
                ucLogin1.labelFlag.Show();
                timerShowHide.Start();
            }
        }

        private void timerShowHide_Tick(object sender, EventArgs e)
        {
            ucLogin1.labelFlag.Hide();
            
            
         
            //timerShowHide.Stop();
            
            
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            if(globalVar.isUser==true)
            {
                frmSettings setting = new frmSettings();
                setting.ShowDialog();
            }
            else if(globalVar.isUser==false)
            {
                ucLogin1.labelFlag.Show();
                timerShowHide.Start();
            }
        }



        //private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    ucLogin1.labelFlag.Show();
        //    ucLogin1.labelFlag.Hide();
        //}
    }
}
