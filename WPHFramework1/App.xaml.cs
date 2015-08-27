using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPHFramework1
{
    using MahApps.Metro;
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    using System.Windows;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // To set the accent and theme...
            // 1) get the theme from the current application
            //var theme = ThemeManager.DetectAppStyle(Application.Current);

            // 2) now set the Green accent and dark theme
            //ThemeManager.ChangeAppStyle(Application.Current,
            //                            ThemeManager.GetAccent("Green"),
            //                            ThemeManager.GetAppTheme("BaseDark"));

            // for available 'stock' accents and themes and more info, see http://mahapps.com/guides/styles.html#app

            base.OnStartup(e);
        }

    }

}
