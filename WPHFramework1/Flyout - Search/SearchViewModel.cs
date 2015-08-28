using Caliburn.Micro;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPHFramework1
{
    public class SearchViewModel : FlyoutBaseViewModel
    {
        IEventAggregator _eventAggr;

        public SearchViewModel(IEventAggregator ea)
        {
            _eventAggr = ea;
            Header = "Search";
            Position = Position.Left;
            Text = "Search feature would go here.";

        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set {
                _text = value;
                NotifyOfPropertyChange(() => Text);
            }
        }
        
     
    }
}
