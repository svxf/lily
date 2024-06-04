using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace lily
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Process lspProc;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.lspProc = Process.Start(new ProcessStartInfo(Environment.CurrentDirectory + "/dist/lily.exe")
            {
                WorkingDirectory = Environment.CurrentDirectory + "/dist",
                Arguments = "server",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true
            });
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (this.lspProc.HasExited)
                return;
            this.lspProc.Kill();
        }
    }
}
