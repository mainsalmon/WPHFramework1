using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPHFramework1
{
    public class Screen2ChildViewModel: Screen
    {
        private IEventAggregator _eventAggregator;

        public Screen2ChildViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void CloseMe()
        {
            TryClose();
        }

        private string _someValue;
        public string SomeValue
        {
            get { return _someValue; }
            set {
                _someValue = value;
                NotifyOfPropertyChange(() => SomeValue);
            
            }
        }

        protected override void OnDeactivate(bool close)
        {
            _eventAggregator.PublishOnUIThread(new StatusMessage(Criticality.Informational, "The child form just closed."));
            base.OnDeactivate(close);
        }
        
    }
}
