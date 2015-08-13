using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro;
using MahApps.Metro.Controls.Dialogs;
using Caliburn.Micro;

namespace WPHFramework1
{
    public class Screen8ViewModel : PropertyChangedBase
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        private ProgressDialogController _controller;

        public Screen8ViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;
        }

        #region Use Busy Dialog

        public async void StartProcess()
        {
            ShowProgressDialog();
            await DoWork();
            await _controller.CloseAsync();
        }

        public async void ShowProgressDialog()
        {
            
            MetroDialogSettings settings = new MetroDialogSettings() { NegativeButtonText = "Cancel", AnimateShow = true, AnimateHide=true };
            _controller = await _dialogCoordinator.ShowProgressAsync(this, "Screen8ViewModel is working on something", "", true, settings);
   
        }

        private async Task DoWork(){

            await Task.Delay(5000);

            if (_controller.IsCanceled == true)
            {

            }

        }

        #endregion Use Busy Dialog

        #region Use Busy Ring

        public async void StartProcess2()
        {
            IsBusy = true;
            await Task.Delay(5000);
            IsBusy = false;

        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { 
                _isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
            }
        }
        
        #endregion Use Busy Ring

    }
}
