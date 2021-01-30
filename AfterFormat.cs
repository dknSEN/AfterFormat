using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace AfterFormat
{
    public partial class AfterFormat : Form
    {
        public AfterFormat()
        { InitializeComponent(); }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShadowForm.SetShadowForm(this);
            foreach (ManagementObject obj in Information.myCPU.Get())
            { label2.Text = ("CPU Name:  " + obj["Name"]); }
            foreach (ManagementObject obj in Information.myGPU.Get())
            { label5.Text = ("GPU Name:  " + obj["Name"]); }
            foreach (ManagementObject obj in Information.myOperatingSystem.Get())
            { label7.Text = ("Operating System:  " + obj["Caption"] + " " + obj["Version"]); }

            long memKb;
            Information.GetPhysicallyInstalledSystemMemory(out memKb);
            label6.Text = ("Total Memory: " + (memKb / 1024 / 1024) + " GB");

            toolTip1.SetToolTip(ButtonNTI, "Required for maximum network performance.");
            toolTip1.SetToolTip(ButtonSR, "Sets the application priority.");
            toolTip1.SetToolTip(ButtonPTO, "Removes the CPU energy limit.");
            toolTip1.SetToolTip(ButtonFSO, "Spectre and Meltdown solver.");
            toolTip1.SetToolTip(ButtonWPS, "Helps to manage Process Priority Efficiently.");
            toolTip1.SetToolTip(ButtonDDN, "Trim for healthy SSD");
            toolTip1.SetToolTip(ButtonJIQ, "Improves Wallpaper quality.");
            toolTip1.SetToolTip(ButtonDLC, "Disable keyboard layout changer shortcut.");
            toolTip1.SetToolTip(ButtonSSD, "Smart Screen disable.");
            toolTip1.SetToolTip(ButtonBSARD, "Disable Blue Screen Auto restart.");
            toolTip1.SetToolTip(ButtonWDS, "Disable Windows Defender.");
            toolTip1.SetToolTip(ButtonVPS, "Prevents vulnerabilities.");
            toolTip1.SetToolTip(ButtonTSD, "Disable Tablet mode.");
            toolTip1.SetToolTip(ButtonHPETD, "Disable HPET.");
        }



        private void ImageButtonExit_Click(object sender, EventArgs e)
        { Application.Exit(); }

        private void timer1_Tick(object sender, EventArgs e)
        {
            float cpu = Information.theCPUCounter.NextValue();
            float ram = Information.theRAMCounter.NextValue() / 1024;
            CircleProgressBar1.Value = (int)cpu;
            labelCPU.Text = ((cpu < 10) ? $" {(int)cpu}%" : $"{(int)cpu}%");
            CircleProgressBar2.Value = (int)ram;
            labelRAM.Text = ((ram < 10) ? $" {(int)ram}%" : $"{(int)ram}%");
        }

        private void ImageButtonInfo_Click(object sender, EventArgs e)
        { PanelInfo.BringToFront(); GradientPanelTop.BringToFront(); Panel.BringToFront(); }

        private void ImageButtonSetting_Click(object sender, EventArgs e)
        { PanelSettings.BringToFront(); GradientPanelTop.BringToFront(); Panel.BringToFront(); }

        private void ImageButtonClean_Click(object sender, EventArgs e)
        {
            var tmpPath = Path.GetTempPath();
            var files = Directory.GetFiles(tmpPath, "*.*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                try
                {
                    if (File.Exists(file))
                    { File.Delete(file); }
                }
                catch { }
            }
            String tempFolder = Environment.ExpandEnvironmentVariables("%TEMP%");
            foreach (var file in Directory.GetFiles(tempFolder))
            {
                try
                {
                    if (File.Exists(file))
                    { File.Delete(file); }
                }
                catch { }
            }
            String prefetch = Environment.ExpandEnvironmentVariables("%SYSTEMROOT%") + "\\Prefetch";
            foreach (var file in Directory.GetFiles(prefetch))
            {
                try
                {
                    if (File.Exists(file))
                    { File.Delete(file); }
                }
                catch { }
            }
            MessageBox.Show("All junk files has been deleted.","AfterFormat");
        }

        private void ImageButtonTweak_Click(object sender, EventArgs e)
        { PanelTweaks.BringToFront(); GradientPanelTop.BringToFront(); Panel.BringToFront(); }

        private void ImageButtonGaming_Click(object sender, EventArgs e)
        { PanelGaming.BringToFront(); GradientPanelTop.BringToFront(); Panel.BringToFront(); }

        private void ButtonNTI_Click(object sender, EventArgs e)
        { Settings.SetLocalMachineRegistry(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile", "NetworkThrottlingIndex", "ffffffff"); }

        private void ButtonSR_Click(object sender, EventArgs e)
        { Settings.SetLocalMachineRegistry(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile", "SystemResponsiveness", "00000000"); }

        private void ButtonPTO_Click(object sender, EventArgs e)
        { Settings.SetLocalMachineRegistry(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile", "PowerThrottlingOff", "00000001"); }

        private void ButtonWPS_Click(object sender, EventArgs e)
        { Settings.SetLocalMachineRegistry(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile", "Win32PrioritySeparation", "00000026"); }

        private void ButtonDLC_Click(object sender, EventArgs e)
        {
            Settings.SetCurrentUserRegistry(@"Keyboard Layout\Toggle", "Language HotKey", "3");
            Settings.SetCurrentUserRegistry(@"Keyboard Layout\Toggle", "Layout HotKey", "3");
            Settings.SetUsersRegistry(@".DEFAULT\Keyboard Layout\Toggle", "Language HotKey", "3");
            Settings.SetUsersRegistry(@".DEFAULT\Keyboard Layout\Toggle", "Layout HotKey", "3");
        }

        private void ButtonJIQ_Click(object sender, EventArgs e)
        { Settings.SetCurrentUserRegistry(@"Control Panel\Desktop", "JPEGImportQuality", "00000100"); }

        private void ButtonFSO_Click(object sender, EventArgs e)
        {
            Settings.SetLocalMachineRegistry(@"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management", "FeatureSettingsOverride", "3");
            Settings.SetLocalMachineRegistry(@"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management", "FeatureSettingsOverrideMask", "3");
        }

        private void ButtonDDN_Click(object sender, EventArgs e)
        { Settings.ExecuteShellCommand("/k fsutil behavior set DisableDeleteNotify 0");    }

        private void ButtonSSD_Click(object sender, EventArgs e)
        { Settings.SetLocalMachineRegistry(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", "SmartScreenEnabled", "Off"); }

        private void ButtonBSARD_Click(object sender, EventArgs e)
        { Settings.SetLocalMachineRegistry(@"SYSTEM\CurrentControlSet\Control\CrashControl", "AutoReboot", "0"); }

        private void ButtonWDS_Click(object sender, EventArgs e)
        {
            Settings.ExecuteShellCommand(@"/k regsvr32 /u /s  %ProgramFiles%\Windows Defender\shellext.dll");
            Settings.ExecuteShellCommand(@"/k reg delete HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Run /v WindowsDefender /f");
            Settings.ExecuteShellCommand(@"/k reg delete HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Run /v SecurityHealth /f");
            Settings.SetLocalMachineRegistry(@"SOFTWARE\Policies\Microsoft\Windows Defender\UX Configuration", "Notification_Suppress", "1");
            Settings.SetLocalMachineRegistry(@"SOFTWARE\Microsoft\Windows Defender\Spynet", "SpyNetReporting", "0");
            Settings.SetLocalMachineRegistry(@"SOFTWARE\Microsoft\Windows Defender\Spynet", "SubmitSamplesConsent", "2");
            Settings.ExecuteShellCommand(@"/k wmic startup where name=Windows Defender notification icon delete");
            Settings.ExecuteShellCommand(@"/k wmic startup where name=Windows Defender User Inferface delete");
        }

        private void ButtonVPS_Click(object sender, EventArgs e)
        {
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Application Experience\AitAgent"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Application Experience\Microsoft Compatibility Appraiser"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks / Change / TN ""\Microsoft\Windows\Application Experience\ProgramDataUpdater"" / DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Customer Experience Improvement Program\Consolidator"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Customer Experience Improvement Program\KernelCeipTask"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Customer Experience Improvement Program\UsbCeip"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\DiskDiagnostic\Microsoft - Windows - DiskDiagnosticDataCollector"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Maintenance\WinSAT"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Media Center\ActivateWindowsSearch"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Media Center\ConfigureInternetTimeService"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Media Center\DispatchRecoveryTasks"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Media Center\ehDRMInit"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Media Center\InstallPlayReady"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Media Center\mcupdate"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Media Center\MediaCenterRecoveryTask"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Media Center\ObjectStoreRecoveryTask"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Media Center\OCURActivate"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Media Center\OCURDiscovery"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Media Center\PBDADiscovery"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Media Center\PBDADiscoveryW1"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Media Center\PBDADiscoveryW2"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Media Center\PvrRecoveryTask"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Media Center\PvrScheduleTask"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Media Center\RegisterSearch"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Media Center\ReindexSearchRoot"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Media Center\SqlLiteRecoveryTask"" /DISABLE");
            Settings.ExecuteShellCommand(@"/k schtasks /Change /TN ""\Microsoft\Windows\Media Center\UpdateRecordPath"" /DISABLE");

        }

        private void ButtonTSD_Click(object sender, EventArgs e)
        {
            Settings.SetLocalMachineRegistry(@"SOFTWARE\Policies\Microsoft\Windows\Gwx", "DisableGwx", "1");
            Settings.SetLocalMachineRegistry(@"SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", "DisableOSUpgrade", "1");
            Settings.ExecuteShellCommand(@"/k sc stop ""TabletInputService");
            Settings.ExecuteShellCommand(@"/k sc config ""TabletInputService"" start= disabled");
        }

        private void ButtonHPETD_Click(object sender, EventArgs e)
        {
            Settings.ExecuteShellCommand(@"/k bcdedit /set useplatformclock no");
            Settings.ExecuteShellCommand(@"/k bcdedit / set useplatformtick yes");
            Settings.ExecuteShellCommand(@"/k bcdedit /set disabledynamictick yes");
        }

        private void ButtonA_Click(object sender, EventArgs e)
        { Settings.SetLocalMachineRegistry(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Games", "Affinity", "00000000"); }

        private void ButtonBO_Click(object sender, EventArgs e)
        { Settings.SetLocalMachineRegistry(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Games", "Background Only", "False"); }

        private void ButtonGP_Click(object sender, EventArgs e)
        { Settings.SetLocalMachineRegistry(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Games", "GPU Priority", "8"); }

        private void ButtonP_Click(object sender, EventArgs e)
        { Settings.SetLocalMachineRegistry(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Games", "Priority", "6"); }

        private void ButtonSC_Click(object sender, EventArgs e)
        { Settings.SetLocalMachineRegistry(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Games", "Scheduling Category", "High"); }

        private void ButtonSP_Click(object sender, EventArgs e)
        { Settings.SetLocalMachineRegistry(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Games", "SFIO Priority", "High"); }

        private void ButtonAGD_Click(object sender, EventArgs e)
        { Settings.SetCurrentUserRegistry(@"System\GameConfigStore", "GameDVR_FSEBehaviorMode", "2"); }

        private void ButtonFSE_Click(object sender, EventArgs e)
        { Settings.SetCurrentUserRegistry(@"System\GameConfigStore", "GameDVR_HonorUserFSEBehaviorMode", "1"); }

        private void ButtonHWC_Click(object sender, EventArgs e)
        { Settings.SetCurrentUserRegistry(@"System\GameConfigStore", "GameDVR_FSEBehavior", "2"); }

        private void ButtonHUM_Click(object sender, EventArgs e)
        { Settings.SetCurrentUserRegistry(@"System\GameConfigStore", "GameDVR_DXGIHonorFSEWindowsCompatible", "1"); }


        //3B Viewer

        private void pictureBox1_Click(object sender, EventArgs e)
        {   }

        private void Download01_Click(object sender, EventArgs e)
        {   }

        private void Uninstall01_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *3dbuilder* | Remove-AppxPackage"); }

        //Groove Music

        private void pictureBox2_Click(object sender, EventArgs e)
        {   }

        private void Download02_Click(object sender, EventArgs e)
        {   }

        private void Uninstall02_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *zunemusic* | Remove-AppxPackage"); }

        //People

        private void pictureBox3_Click(object sender, EventArgs e)
        {   }

        private void Download03_Click(object sender, EventArgs e)
        {   }

        private void Uninstall03_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *people* | Remove-AppxPackage"); }

        //Skype

        private void pictureBox4_Click(object sender, EventArgs e)
        {   }

        private void Download04_Click(object sender, EventArgs e)
        {   }

        private void Uninstall04_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *skypeapp* | Remove-AppxPackage"); }

        //Alarm

        private void pictureBox8_Click(object sender, EventArgs e)
        {   }

        private void Download05_Click(object sender, EventArgs e)
        {   }

        private void Uninstall05_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *windowsalarms* | Remove-AppxPackage"); }

        //Maps

        private void pictureBox7_Click(object sender, EventArgs e)
        {   }

        private void Download06_Click(object sender, EventArgs e)
        {   }

        private void Uninstall06_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *windowsmaps* | Remove-AppxPackage"); }

        //Get Help


        private void pictureBox6_Click(object sender, EventArgs e)
        {   }

        private void Download07_Click(object sender, EventArgs e)
        {   }

        private void Uninstall07_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *windowscalculator* | Remove-AppxPackage"); }

        //Snip and Sketch
        
        private void pictureBox5_Click(object sender, EventArgs e)
        {   }

        private void Download08_Click(object sender, EventArgs e)
        {   }

        private void Uninstall08_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *screensketch* | Remove-AppxPackage"); }

        //MSN Weather

        private void pictureBox12_Click(object sender, EventArgs e)
        {   }

        private void Download09_Click(object sender, EventArgs e)
        {   }

        private void Uninstall09_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *bingweather* | Remove-AppxPackage"); }

        //Mobile Plans

        private void pictureBox11_Click(object sender, EventArgs e)
        {   }

        private void Download10_Click(object sender, EventArgs e)
        {   }

        private void Uninstall10_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *oneconnect* | Remove-AppxPackage"); }

        //Calendar

        private void pictureBox10_Click(object sender, EventArgs e)
        {   }

        private void Download11_Click(object sender, EventArgs e)
        {   }

        private void Uninstall11_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *windowscommunicationsapps* | Remove-AppxPackage"); }

        //Office

        private void pictureBox9_Click(object sender, EventArgs e)
        {   }

        private void Download12_Click(object sender, EventArgs e)
        {   }

        private void Uninstall12_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *microsoftofficehub* | Remove-AppxPackage"); }

        //Feedback Hub

        private void pictureBox16_Click(object sender, EventArgs e)
        {   }

        private void Download13_Click(object sender, EventArgs e)
        {   }

        private void Uninstall13_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *windowsfeedbackhub* | Remove-AppxPackage"); }

        //Onenote

        private void pictureBox15_Click(object sender, EventArgs e)
        {   }

        private void Download14_Click(object sender, EventArgs e)
        {   }

        private void Uninstall14_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *onenote* | Remove-AppxPackage"); }

        //My Phone

        private void pictureBox14_Click(object sender, EventArgs e)
        {   }

        private void Download15_Click(object sender, EventArgs e)
        {   }

        private void Uninstall15_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *yourphone* | Remove-AppxPackage"); }

        //Movies and TV

        private void pictureBox13_Click(object sender, EventArgs e)
        {   }

        private void Download16_Click(object sender, EventArgs e)
        {   }

        private void Uninstall16_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *zunevideo* | Remove-AppxPackage"); }

        //Tips

        private void pictureBox20_Click(object sender, EventArgs e)
        {   }

        private void Download17_Click(object sender, EventArgs e)
        {   }

        private void Uninstall17_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *getstarted* | Remove-AppxPackage"); }

        //Paint 3D

        private void pictureBox19_Click(object sender, EventArgs e)
        {   }

        private void Download18_Click(object sender, EventArgs e)
        {   }

        private void Uninstall18_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *mspaint* | Remove-AppxPackage"); }

        //Photos

        private void pictureBox18_Click(object sender, EventArgs e)
        {   }

        private void Download19_Click(object sender, EventArgs e)
        {   }

        private void Uninstall19_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *photos* | Remove-AppxPackage"); }

        //Camera

        private void pictureBox17_Click(object sender, EventArgs e)
        {   }

        private void Download20_Click(object sender, EventArgs e)
        {   }

        private void Uninstall20_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *camera* | Remove-AppxPackage"); }

        //Mail

        private void pictureBox24_Click(object sender, EventArgs e)
        {   }

        private void Download21_Click(object sender, EventArgs e)
        {   }

        private void Uninstall21_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *windowscommunicationsapps* | Remove-AppxPackage"); }

        //Recorder

        private void pictureBox23_Click(object sender, EventArgs e)
        {   }

        private void Download22_Click(object sender, EventArgs e)
        {   }

        private void Uninstall22_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *windowssoundrecorder* | Remove-AppxPackage"); }

        //Sticky Note

        private void pictureBox22_Click(object sender, EventArgs e)
        {   }

        private void Download23_Click(object sender, EventArgs e)
        {   }

        private void Uninstall23_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *microsoftstickynotes* | Remove-AppxPackage"); }

        //Store

        private void pictureBox21_Click(object sender, EventArgs e)
        {   }

        private void Download24_Click(object sender, EventArgs e)
        {   }

        private void Uninstall24_Click(object sender, EventArgs e)
        { Cleaning.UninstallWindowsApps("Get-AppxPackage *windowsstore* | Remove-AppxPackage"); }
    }
}
