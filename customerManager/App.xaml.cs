using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using DevExpress.Xpf.Core;

namespace CustomerManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //CultureInfo ci = new CultureInfo("zh-Hans");
            // Thread.CurrentThread.CurrentCulture = ci;
            //  Thread.CurrentThread.CurrentUICulture = ci;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("zh-Hans");

            // The following line provides localization for the application's user interface. �
            Thread.CurrentThread.CurrentUICulture = culture;

            // The following line provides localization for data formats. �
            Thread.CurrentThread.CurrentCulture = culture;

            // Set this culture as the default culture for all threads in this application. �
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            DXSplashScreen.Show<Statup>();
        }
    }
}
