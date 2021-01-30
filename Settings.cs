using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AfterFormat
{
    public class Settings
    {
        public static void SetLocalMachineRegistry(string code, string name, string value)
        {
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (var key = hklm.OpenSubKey(code, RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                key.SetValue(name, value);
                key.Close();
            }
        }
        public static void SetCurrentUserRegistry(string code, string name, string value)
        {
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
            using (var key = hklm.OpenSubKey(code, RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                key.SetValue(name, value);
                key.Close();
            }
        }
        public static void SetUsersRegistry(string code, string name, string value)
        {
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry64))
            using (var key = hklm.OpenSubKey(code, RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                key.SetValue(name, value);
                key.Close();
            }
        }
        public static void SetClassesRootRegistry(string code, string name, string value)
        {
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry64))
            using (var key = hklm.OpenSubKey(code, RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                key.SetValue(name, value);
                key.Close();
            }
        }
        public static void SetCurrentConfigRegistry(string code, string name, string value)
        {
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry64))
            using (var key = hklm.OpenSubKey(code, RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                key.SetValue(name, value);
                key.Close();
            }
        }
        public static void ExecuteShellCommand(string arg)
        {
            Process pr = new Process();
            pr.StartInfo.FileName = "cmd.exe";
            pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            pr.StartInfo.Arguments = arg;
            pr.Start();
        }
    }
}
