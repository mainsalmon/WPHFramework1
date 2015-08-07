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
using System.Dynamic;

namespace WPHFramework1
{
    class Screen2ViewModel : Conductor<Screen>    // string message is for demo; use specific object type for specific message types
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

         public void ShowWindow()
         {
             // shows non-modal child form (can now close the main form, but app stays up until child is closed)
            var window = new Screen2ChildViewModel("Non Modal Child");
           
             _windowManager.ShowWindow(window);
         
             // It has no owner so centerowner must be changed to centerscreenWindowStartupLocation.CenterScreen
         }

        public void ShowModalDialogWindow(Screen2View view)
        {
            // shows child form modally
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.ResizeMode = ResizeMode.NoResize;
            settings.Width = 450;
           
            Screen2ChildViewModel vm = new Screen2ChildViewModel("Modal Child");
            _windowManager.ShowDialog(vm, null, settings);

            // We don't really need to use this as a dialog, just as a modal child form.
            // See Screen 1 for Mahapps dialogs which have all the build-in goodness.
            
            // Values of vm that have been set via the dialog are available here:
            var x = vm.SomeValue;
        
        }

        #endregion Show Child Window

     

        protected override void OnActivate()
        {
           // _eventAggregator.Subscribe(this);
            _eventAggregator.PublishOnUIThread(new StatusMessage(Criticality.Informational, "Screen 2 Activated"));

           // ShowTimedDialog("Screen Two Activated"); // can't show dialogs in activate because MetroWindow not up yet
            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
           // If the app is not closing, then show dialog
            if ( Application.Current.MainWindow != null)
            {
                // ShowTimedDialog("Screen Two Deactivated"); // can't show dialogs in deactivate because MetroWindow is null too soon
            }
            _eventAggregator.PublishOnUIThread(new StatusMessage(Criticality.Informational, "Screen 2 Deactivated"));

          //  _eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }

        public async void ShowTimedDialog(string message)
        {
            string msg = message + "; will close in 1 second.";
            var dialog = new CustomDialog() { Title = msg };
            await _dialogCoordinator.ShowMetroDialogAsync(this, dialog);
            await Task.Delay(1000);
            await _dialogCoordinator.HideMetroDialogAsync(this, dialog);
        }
    }
}
