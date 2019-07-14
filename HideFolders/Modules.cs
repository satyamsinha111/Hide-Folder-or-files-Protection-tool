using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.AccessControl;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Management;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Data.SQLite;
using System.Runtime.InteropServices;
using System.Data;

namespace HideFolders
{
    class Modules
    {
        public static void addPermissons(string path)
        {
            try
            {
                string folderPath = path;
                string adminUserName = Environment.UserName;// getting your adminUserName
                DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
                ds.AddAccessRule(fsa);
                Directory.SetAccessControl(folderPath, ds);

            }
            catch (Exception ex)
            {

            }
        }
        public static void removePermisson(string path)
        {
            try
            {
                string folderPath = path;
                string adminUserName = Environment.UserName;// getting your adminUserName
                DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
                ds.RemoveAccessRule(fsa);
                Directory.SetAccessControl(folderPath, ds);
            }
            catch (Exception ex)
            {

            }
        }
        public static void LockFileOrFolder(string path)
        {
            try
            {
                Process cmd = new Process();
                cmd.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();
                //MessageBox.Show("attrib " + '"' + path + '"' + " +s +r +h");
                cmd.StandardInput.WriteLine("attrib " + '"' + path + '"' + " +s +r +h");
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        public static void unlockFilesAndFolders(string path)
        {
            try
            {
                Process cmd = new Process();
                cmd.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();
                cmd.StandardInput.WriteLine("attrib " + '"' + path + '"' + " -s -r -h");
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
            }
            catch
            {

            }

        }
        public static void sqlInsert(string sql)
        {
            string data = "DataSource=data.db";
            SQLiteCommand sqlcmd;
            using (SQLiteConnection conn = new SQLiteConnection(data))
            {
                
                conn.Open();
                string query = sql;
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
                sqlcmd = conn.CreateCommand();
                sqlcmd.CommandText = query;
                sqlcmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        
        public static void sqlGet(string sql)
        {
            globalVar.dt.Clear();
            string data = "DataSource=data.db";
            using (SQLiteConnection conn = new SQLiteConnection(data))
            {

                conn.Open();
                string query = sql;
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
                adapter.Fill(globalVar.dt);
                foreach(DataRow dr in globalVar.dt.Rows)
                {
                    MessageBox.Show(dr[0].ToString());
                }
                conn.Close();
            }
            
        }
    }
}
