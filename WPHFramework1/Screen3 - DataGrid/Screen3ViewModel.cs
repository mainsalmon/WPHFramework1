using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Caliburn.Micro;

namespace WPHFramework1
{
    public class Screen3ViewModel : Screen
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();
            _albums = new ObservableCollection<Album>()
            {
                new Album(){Title = "XXXX", Category=AlbumCategory.Country, Quantity=3, UnitPrice=8.95},
                new Album(){Title = "YYYY", Category=AlbumCategory.Pop, Quantity=55, UnitPrice=12.49}
            };
        }

        private ObservableCollection<Album> _albums;

        public ObservableCollection<Album> Albums
        {
            get { return _albums; }
            set {
                
                _albums = value;
                NotifyOfPropertyChange(() => Albums);
            }
        }
        
    }
}
