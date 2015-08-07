using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro;
using MahApps.Metro.Controls.Dialogs;

namespace WPHFramework1
{
    public class Screen1ViewModel
    {
        private readonly IDialogCoordinator _dialogCoordinator;

        public Screen1ViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;
        }

        #region Show Message Dialog 

        public async void ShowMsgDialog()
        {
            await _dialogCoordinator.ShowMessageAsync(this, "Message from Screen1ViewModel", "MVVM based messages!").ContinueWith(t => HandleMsgBoxClose(t.Result));
        }

        private void HandleMsgBoxClose(MessageDialogResult rslt)
        {
            
        }

        #endregion Show Message Dialog

        #region Show Custom Timed Dialog

        public async void ShowCustomTimedDialog()
        {
            var customDialog = new CustomDialog() { Title = "Custom dialog, will close in 2 seconds." };
            await _dialogCoordinator.ShowMetroDialogAsync(this, customDialog);
            await Task.Delay(2000);
            await _dialogCoordinator.HideMetroDialogAsync(this, customDialog);
        }

        #endregion Show Custom Timed Dialog

        #region Show Custom Question Dialog

        public async void ShowCustomQuestionDialog()
        {
            MetroDialogSettings settings = new MetroDialogSettings() { AffirmativeButtonText = "Yes", NegativeButtonText = "No", FirstAuxiliaryButtonText = "Maybe" };
            await _dialogCoordinator.ShowMessageAsync(this, "Question from Screen1ViewModel", "Questions!", MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, settings).ContinueWith(t => HandleQuestionClose(t.Result));
        }

        private void HandleQuestionClose(MessageDialogResult rslt)
        {
            if (rslt == MessageDialogResult.Affirmative)
            {

            } else if (rslt == MessageDialogResult.Negative)
            {

            }
            else if (rslt == MessageDialogResult.FirstAuxiliary)
            {

            }
        }

        #endregion Show Custom Question Dialog

        #region Show Input Dialog

        public async void ShowInputDialog()
        {
            await _dialogCoordinator.ShowInputAsync(this, "From Screen1ViewModel", "Enter something here...").ContinueWith(t => HandleInputBoxClose(t.Result));
        }

        private void HandleInputBoxClose(string rslt)
        {
            // if they hit cancel, rslt == null
            // else it's the string they entered
        }

        #endregion Show Input Dialog

        #region Show Progress Dialog

        public async void ShowProgressDialog()
        {
            MetroDialogSettings settings = new MetroDialogSettings() { NegativeButtonText = "Hold the fort!" };
            var controller = await _dialogCoordinator.ShowProgressAsync(this, "Progress from VM", "All things are progressing nicely, wait 5 seconds", true, settings);

            await Task.Delay(5000);

            if (controller.IsCanceled == true)
            {

            }
            await controller.CloseAsync();
        }

        #endregion Show Progress Dialog

        #region Show Login Dialog

        public async void ShowLoginDialog()
        {
            // note that setting allows much additional functionality
            LoginDialogSettings settings = new LoginDialogSettings() { NegativeButtonText = "Cancel", NegativeButtonVisibility= System.Windows.Visibility.Visible };
           
            _dialogCoordinator.ShowLoginAsync(this, "Please Login", "This login dialog was shown from a VM.", MessageDialogStyle.AffirmativeAndNegative, settings).ContinueWith(t => HandleLoginClose(t.Result));
        }

        private void HandleLoginClose(LoginDialogData rslt)
        {
            // if they hit cancel, then rslt == null, else it contains user name and password
        }
        #endregion Show Login Dialog
    }
}
