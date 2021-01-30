using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AfterFormat
{
    public class Cleaning
    {
        public static void UninstallWindowsApps(string name)
        {
            Process pr = new Process();
            pr.StartInfo.FileName = @"powershell.exe";
            pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            pr.StartInfo.Arguments = name;
            pr.Start();
        }
    }
}
