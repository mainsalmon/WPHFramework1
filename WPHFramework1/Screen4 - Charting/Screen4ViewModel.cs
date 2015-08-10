using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPHFramework1
{
    public class Screen4ViewModel : Screen
    {

        public Screen4ViewModel()
        {
            ChartData = new ObservableCollection<KeyValuePair<string, int>>();
            ChartData.Add(new KeyValuePair<string, int>("Developer", 7));
            ChartData.Add(new KeyValuePair<string, int>("Misc", 5));
            ChartData.Add(new KeyValuePair<string, int>("Tester", 3));
        }

        private ObservableCollection<KeyValuePair<string, int>> _chartData;

        public ObservableCollection<KeyValuePair<string, int>> ChartData
        {
            get { return _chartData; }
            set { _chartData = value; }
        }
    }
}
