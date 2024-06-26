﻿using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;

namespace FastbootEnhance
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow THIS;
        const string version = "1.4.3";
        public MainWindow()
        {
            InitializeComponent();
            THIS = this;

            string mutexName = "FastbootEnhance";
            bool createdNew;
            Mutex singleInstanceWatcher = new Mutex(false, mutexName, out createdNew);
            if (!createdNew)
            {
                MessageBox.Show(Properties.Resources.program_already_running, Properties.Resources.error, MessageBoxButton.OK, MessageBoxImage.Error);
                Process.GetCurrentProcess().Kill();
            }

            try
            {
                new DirectoryInfo(PayloadUI.PAYLOAD_TMP).Delete(true);
            }
            catch (DirectoryNotFoundException) { }

            try
            {
                new DirectoryInfo(FastbootUI.PAYLOAD_TMP).Delete(true);
            }
            catch (DirectoryNotFoundException) { }

            PayloadUI.init();
            FastbootUI.init();

            Title += " v" + version;

            Closed += delegate
            {
                if (PayloadUI.payload != null)
                    PayloadUI.payload.Dispose();

                try
                {
                    new DirectoryInfo(PayloadUI.PAYLOAD_TMP).Delete(true);
                }
                catch (DirectoryNotFoundException) { }
                catch (IOException) { }

                try
                {
                    new DirectoryInfo(FastbootUI.PAYLOAD_TMP).Delete(true);
                }
                catch (DirectoryNotFoundException) { }
                catch (IOException) { }

                Process.GetCurrentProcess().Kill();
            };
        }

        private void Thread_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/MisterZtr/FastbootEnhance/releases");
        }

        private void OSS_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/MisterZtr/FastbootEnhance");
        }

        private void payload_partition_info_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void fastboot_show_logs_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void fastboot_info_list_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void fastboot_partition_list_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
