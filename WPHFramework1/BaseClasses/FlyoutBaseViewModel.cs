using Caliburn.Micro;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPHFramework1
{
    public abstract class FlyoutBaseViewModel : Screen
    {
        private string _header;
        public string Header
        {
            get { return _header; }

            set
            {
                if (value != this._header)
                {
                    _header = value;
                    NotifyOfPropertyChange(() => Header);
                }
            }
        }

        private bool _isOpen;
        public bool IsOpen
        {
            get { return this._isOpen;
            }

            set
            {
                if (!value.Equals(_isOpen))
                {
                    _isOpen = value;
                    NotifyOfPropertyChange(() => IsOpen);
                }
            }
        }

        private Position _position;
        public Position Position
        {
            get {return this._position;}

            set
            {
                if (value != this._position)
                {
                    _position = value;
                    NotifyOfPropertyChange(() => Position);
                }
            }
        }
    }
}
