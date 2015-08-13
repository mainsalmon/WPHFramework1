namespace WPHFramework1 {

    using Caliburn.Micro;
    using MahApps.Metro.Controls.Dialogs;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Caliburn.Micro.Autofac;

    public class ShellViewModel : Conductor<object>, IShell, IHandle<StatusMessage>
    {
        IWindowManager _windowManager;
        IEventAggregator _eventAggregator;

        // Screen view models are injected into here and then activated from here via the menu
        Screen1ViewModel _screen1VM;
        Screen2ViewModel _screen2VM;
        Screen3ViewModel _screen3VM;
        Screen4ViewModel _screen4VM;
        Screen5ViewModel _screen5VM;
        Screen6ViewModel _screen6VM;
        Screen7ViewModel _screen7VM;
        SettingsViewModel _settingsVM;

        public ShellViewModel(IEventAggregator ea, 
            IWindowManager wm, 
            Screen1ViewModel s1,
            Screen2ViewModel s2,
            Screen3ViewModel s3,
            Screen4ViewModel s4,
            Screen5ViewModel s5,
            Screen6ViewModel s6,
            Screen7ViewModel s7,
            SettingsViewModel settings)
        {
            _windowManager = wm;
            _eventAggregator = ea;

            _screen1VM = s1;
            _screen2VM = s2;
            _screen3VM = s3;
            _screen4VM = s4;
            _screen5VM = s5;
            _screen6VM = s6;
            _screen7VM = s7;
            _settingsVM = settings;

            DisplayName = "WPH Framework1 (Caliburn.Micro + Mahapps + Autofac)";
            StatusMessageText = string.Empty;
        }

        #region Lifecycle stuff

        protected override void OnInitialize(){
            base.OnInitialize();
            _eventAggregator.Subscribe(this);
        
            ShowScreen1(); // Initial screen
        }

        protected override void OnDeactivate(bool close)
        {
            _eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }

        #endregion Lifecycle Stuff

        #region Settings Flyout

        public SettingsViewModel SettingsVM
        {
            get { return _settingsVM; }
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
            ActivateItem(_screen1VM);
            RefreshMenuButtonGuards();
        }

        public void ShowScreen2()
        {
            ActivateItem(_screen2VM);
            RefreshMenuButtonGuards();
        }

        public void ShowScreen3()
        {
            ActivateItem(_screen3VM);
            RefreshMenuButtonGuards();
        }

        public void ShowScreen4()
        {
            ActivateItem(_screen4VM);
            RefreshMenuButtonGuards();
        }

        public void ShowScreen5()
        {
            ActivateItem(_screen5VM);
            RefreshMenuButtonGuards();
        }

        public void ShowScreen6()
        {
            ActivateItem(_screen6VM);
            RefreshMenuButtonGuards();
        }

        public void ShowScreen7()
        {
            ActivateItem(_screen7VM);
            RefreshMenuButtonGuards();
        }

        #endregion Show Screens

        #region Disable Menu Button if its screen is the current selection

        // Hmmmm..... there's got to be a cleaner way to handle this...

        private void RefreshMenuButtonGuards()
        {
            NotifyOfPropertyChange(() => CanShowScreen1);
            NotifyOfPropertyChange(() => CanShowScreen2);
            NotifyOfPropertyChange(() => CanShowScreen3);
            NotifyOfPropertyChange(() => CanShowScreen4);
            NotifyOfPropertyChange(() => CanShowScreen5);
            NotifyOfPropertyChange(() => CanShowScreen6);
            NotifyOfPropertyChange(() => CanShowScreen7);
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

        public bool CanShowScreen7
        {
            get { return ActiveItem == null || ActiveItem.GetType() != typeof(Screen7ViewModel); }
        }

        #endregion Disable Menu Button if it's screen is current selection

        #region Handle Status Message

        public async void Handle(StatusMessage messageObj)
        {
            // Handle the message here.
            // Messages can be differentiated by type.  So create various message objects
            // and handle each separately.  Also note polymorphism.  So a handler for object will also handle string msg:
            // so if both an object and string handlers are defined, both will be called.
        
            StatusMessageText = messageObj.Message;

            await Task.Delay(2000);  // show the message for a couple seconds
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