using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPHFramework1
{
    /// <summary>
    /// Interaction logic for Screen5View.xaml
    /// </summary>
    public partial class Screen5View : UserControl
    {
        public Screen5View()
        {
            InitializeComponent();
        }
        private void TextBox_ForceInteger(object sender, KeyEventArgs e)
        {
            bool isNumPadNumeric = (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9); // || e.Key == Key.Decimal;
            bool isNumeric = (e.Key >= Key.D0 && e.Key <= Key.D9);// || e.Key == Key.OemPeriod;

            if ((isNumeric || isNumPadNumeric) && Keyboard.Modifiers != ModifierKeys.None)
            {
                e.Handled = true;
                return;
            }

            bool isControl = ((Keyboard.Modifiers != ModifierKeys.None && Keyboard.Modifiers != ModifierKeys.Shift)
                || e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Insert
                || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up
                || e.Key == Key.Tab
                || e.Key == Key.PageDown || e.Key == Key.PageUp
                || e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Escape
                || e.Key == Key.Home || e.Key == Key.End);

            e.Handled = !isControl && !isNumeric && !isNumPadNumeric;
        }

     
    }
}
