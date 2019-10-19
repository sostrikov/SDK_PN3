using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ParsecIntegrationClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            using (LoginForm frmLogin = new LoginForm())
            {
                if (frmLogin.ShowDialog() != DialogResult.OK)
                    return;
                frmLogin.Close();
            }
            MainForm frmMain = new MainForm();
            Application.Run(frmMain);
        }
    }
}
