namespace WPHFramework1 {

    using Caliburn.Micro;
    using MahApps.Metro.Controls.Dialogs;
    using System.Collections.ObjectModel;

    public class ShellViewModel : Conductor<object>, IShell
    {
        public ShellViewModel(IEventAggregator ea)
        {
            _eventAggr = ea;
            DisplayName = "WH Framework";
        }

        IEventAggregator _eventAggr;
     //   IDialogCoordinator _dialogCoord;

        private SettingsViewModel _settingVM ;
        public SettingsViewModel SettingsVM
        {
            get { return _settingVM; }
        }
      
        protected override void OnInitialize(){
            base.OnInitialize();
            _settingVM = new SettingsViewModel(_eventAggr);
        }

        public void ShowSettingsFlyout(ShellView view)
        {
            var flyout = view.settingsFlyout;
            flyout.IsOpen = !flyout.IsOpen;
        }

        public void ShowScreen2()
        {
            ActivateItem(new Screen2ViewModel(_eventAggr, DialogCoordinator.Instance));
            RefreshButtonGuards();
        }

        public void ShowScreen1()
        {
            ActivateItem(new Screen1ViewModel(DialogCoordinator.Instance));
            RefreshButtonGuards();
        }

        private void RefreshButtonGuards()
        {
            NotifyOfPropertyChange(() => CanShowScreen1);
            NotifyOfPropertyChange(() => CanShowScreen2);
        }

        public bool CanShowScreen1
        {
            get { return ActiveItem == null || ActiveItem.GetType() != typeof(Screen1ViewModel); }
        }

        public bool CanShowScreen2
        {
            get { return ActiveItem == null || ActiveItem.GetType() != typeof(Screen2ViewModel); }
        }
        //private string _test = "XXXXXXX";
        //public string Test
        //{
        //    get { return _test; }
        //    set { _test = value;
        //    NotifyOfPropertyChange(() => Test);
        //    }
        //}
        
    }
}