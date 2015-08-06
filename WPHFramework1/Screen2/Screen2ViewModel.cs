using Caliburn.Micro;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPHFramework1
{
    class Screen2ViewModel : Screen , IHandle<string>
    {
        private readonly IEventAggregator _eventAggregator;

        public Screen2ViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
         //   _eventAggregator.Subscribe(this); // doing it in activate
        }

        public void Handle(string message)
        {
            // Handle the message here.
            // Messages can be differentiated by type.  So create various message objects
            // and handle each separately.  also note polymorphism.  So handler for object will also handle string msg
            // and if both an object and string handlers are defined, both will be called.
            MessageBox.Show(message);
        }


        protected override void OnActivate()
        {
            _eventAggregator.Subscribe(this);
          //  MetroWindow.ShowMessageAsync("Screen2 Activated");
            MessageBox.Show("Screen Two Activated"); //Don't do this in a real VM.
            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
            _eventAggregator.Unsubscribe(this);

            MessageBox.Show("Screen Two DeActivated"); //Don't do this in a real VM.
            base.OnDeactivate(close);
        }
    }
}
