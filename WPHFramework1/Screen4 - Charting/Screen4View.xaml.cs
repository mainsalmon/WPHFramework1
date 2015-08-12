using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for Screen4View.xaml
    /// </summary>
    public partial class Screen4View : UserControl
    {
        private Cursor _cacheCursor;

        public Screen4View()
        {
            InitializeComponent();
            this.Loaded += Screen4View_Loaded;
         
           
        }

      

        void Screen4View_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(string.Format("Loaded: {0}", DateTime.Now.ToLongTimeString()));
           // this.progressIndicator.IsActive = false;
            this.Cursor = Cursors.Arrow;
          
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Debug.WriteLine(string.Format("Render Started: {0}", DateTime.Now.ToLongTimeString()));
          //  this.progressIndicator.IsActive = true;
          
            this.Cursor = Cursors.Wait;

        }
       
        
    }
}
