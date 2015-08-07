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
        public Screen2ChildViewModel(string displayName)
        {
            DisplayName = displayName;
        }
        protected override void OnInitialize()
        {
            base.OnInitialize();
            
        }

        public void Close()
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
        
    }
}
