using PipeControl.Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace PipeControl
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Process[] myProcesses = Process.GetProcessesByName("PipeControl");
            if (myProcesses.Length > 1)
            {
                MessageBox.Show("不可重复启动本系统", "温控系统", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DirectoryInfo dir = new(AppDomain.CurrentDomain.BaseDirectory + "experiments\\");
            if (!dir.Exists)
            {
                dir.Create();
            }
            DataObjects.SystemConfig.ReadConfig();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frm_Main());
        }
    }
}
