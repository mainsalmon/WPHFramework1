namespace WPHFramework1 {

    using Caliburn.Micro;
    using MahApps.Metro.Controls.Dialogs;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    public class ShellViewModel : Conductor<object>, IShell, IHandle<StatusMessage>
    {
        IWindowManager _windowManager;
        IEventAggregator _eventAggregator;

        public ShellViewModel(IEventAggregator ea,  IWindowManager wm)
        {
            _windowManager = wm;
            _eventAggregator = ea;
            DisplayName = "WPH Framework1 (Mahapps + Caliburn)";
            StatusMessageText = string.Empty;
        }

        #region Lifecycle stuff

        protected override void OnInitialize(){
            base.OnInitialize();
            _eventAggregator.Subscribe(this);
            _settingVM = new SettingsViewModel(_eventAggregator);
            ShowScreen1(); // Initial default
        }

        protected override void OnDeactivate(bool close)
        {
            _eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }

        #endregion Lifecycle Stuff

        #region Settings Flyout

        private SettingsViewModel _settingVM;
        public SettingsViewModel SettingsVM
        {
            get { return _settingVM; }
        }

        public void ShowSettingsFlyout(ShellView view)
        {
            var flyout = view.settingsFlyout;
            flyout.IsOpen = !flyout.IsOpen;
        }

        #endregion Settings Flyout

        #region Show Screens

        public void ShowScreen1()
        {
            ActivateItem(new Screen1ViewModel(DialogCoordinator.Instance));
            RefreshMenuButtonGuards();
        }

        public void ShowScreen2()
        {
            ActivateItem(new Screen2ViewModel(_eventAggregator, DialogCoordinator.Instance, _windowManager));
            RefreshMenuButtonGuards();
        }

        public void ShowScreen3()
        {
            ActivateItem(new Screen3ViewModel());
            RefreshMenuButtonGuards();
        }

        public void ShowScreen4()
        {
            ActivateItem(new Screen4ViewModel());
            RefreshMenuButtonGuards();
        }

        public void ShowScreen5()
        {
            ActivateItem(new Screen5ViewModel());
            RefreshMenuButtonGuards();
        }

        public void ShowScreen6()
        {
            ActivateItem(new Screen6ViewModel());
            RefreshMenuButtonGuards();
        }

        #endregion Show Screens

        #region Disable Menu Button if it's screen is current selection

        private void RefreshMenuButtonGuards()
        {
            NotifyOfPropertyChange(() => CanShowScreen1);
            NotifyOfPropertyChange(() => CanShowScreen2);
            NotifyOfPropertyChange(() => CanShowScreen3);
            NotifyOfPropertyChange(() => CanShowScreen4);
            NotifyOfPropertyChange(() => CanShowScreen5);
            NotifyOfPropertyChange(() => CanShowScreen6);

        }

        public bool CanShowScreen1
        {
            get { return ActiveItem == null || ActiveItem.GetType() != typeof(Screen1ViewModel); }
        }

        public bool CanShowScreen2
        {
            get { return ActiveItem == null || ActiveItem.GetType() != typeof(Screen2ViewModel); }
        }

        public bool CanShowScreen3
        {
            get { return ActiveItem == null || ActiveItem.GetType() != typeof(Screen3ViewModel); }
        }

        public bool CanShowScreen4
        {
            get { return ActiveItem == null || ActiveItem.GetType() != typeof(Screen4ViewModel); }
        }

        public bool CanShowScreen5
        {
            get { return ActiveItem == null || ActiveItem.GetType() != typeof(Screen5ViewModel); }
        }

        public bool CanShowScreen6
        {
            get { return ActiveItem == null || ActiveItem.GetType() != typeof(Screen6ViewModel); }
        }

        #endregion Disable Menu Button if it's screen is current selection

        #region Handle Status Message

        public async void Handle(StatusMessage messageObj)
        {
            // Handle the message here.
            // Messages can be differentiated by type.  So create various message objects
            // and handle each separately.  also note polymorphism.  So handler for object will also handle string msg
            // and if both an object and string handlers are defined, both will be called.
        
            StatusMessageText = messageObj.Message;

            await Task.Delay(3000);
            StatusMessageText = string.Empty;
        }

        private string _statusMessageText;
        public string StatusMessageText
        {
            get { return _statusMessageText; }
            set {
                _statusMessageText = value;
                NotifyOfPropertyChange(() => StatusMessageText);
                NotifyOfPropertyChange(() => IsStatusMessageSet);
            }
        }

        public bool IsStatusMessageSet
        {
            get { return StatusMessageText != string.Empty; }
        }
        #endregion Handle Status Message 
    }
}