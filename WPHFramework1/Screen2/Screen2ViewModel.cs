using Caliburn.Micro;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Controls.Primitives;

namespace WPHFramework1
{
    class Screen2ViewModel : Conductor<Screen> , IHandle<string>   // string message is for demo; use specific object type for specific message types
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly IWindowManager _windowManager;

        public Screen2ViewModel(IEventAggregator eventAggregator, IDialogCoordinator dialogCoordinator, IWindowManager windowManager)
        {
            _eventAggregator = eventAggregator;// .Subscribe(this) is happening in activate
            _dialogCoordinator = dialogCoordinator;
            _windowManager = windowManager;
        }

        #region Show Child Window

         private MetroWindow testWindow;

         private MetroWindow GetTestWindow(MetroWindow owner)
        {
            if (testWindow != null) {
                testWindow.Close();
            }
            testWindow = new MetroWindow() { Owner = owner, WindowStartupLocation = WindowStartupLocation.CenterOwner, Title = "Another Test...", Width = 500, Height = 300 };
            testWindow.Closed += (o, args) => testWindow = null;
            return testWindow;
        }

         public void ShowWindow()
         {
             // shows non-modal child form (can now close the main form, but app stays up until child is closed)
            var window = new Screen2ChildViewModel();
           
             _windowManager.ShowWindow(window);
         
             // It has no owner so centerowner must be changed to centerscreenWindowStartupLocation.CenterScreen
         }

        public void ShowModalDialogWindow(Screen2View view)
        {
            // shows child form modally
            bool? rslt = _windowManager.ShowDialog(new Screen2ChildViewModel());
            if (rslt != null)
            {
                // TODO: how to control rslt true/false from Screen2ChildViewModel?
            }
            
        
        }

        #endregion Show Child Window

#region Handle Message

        public void Handle(string message)
        {
            // Handle the message here.
            // Messages can be differentiated by type.  So create various message objects
            // and handle each separately.  also note polymorphism.  So handler for object will also handle string msg
            // and if both an object and string handlers are defined, both will be called.

            // TODO: switch this to dialogs - see code in Screen1ViewModel
            MessageBox.Show(message);
        }

#endregion Handle Message 

        protected override void OnActivate()
        {
            _eventAggregator.Subscribe(this);
            // TODO: switch this to child screen
            MessageBox.Show("Screen Two Activated"); //Don't do this in a real VM.
            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
           // If the app is not closing, then show dialog
            if ( Application.Current.MainWindow != null)
            {
                // TODO: switch this to child screen
                MessageBox.Show("Screen Two DeActivated"); //Don't do this in a real VM.
            }
           
            _eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }

       
    }
}
