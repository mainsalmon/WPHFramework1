using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPHFramework1
{

    public enum AlbumCategory
    {
        Jazz,
        Rock,
        Pop,
        Country  
    }

    public class Album : PropertyChangedBase
    {
        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set {
                if (value != _quantity)
                {
                    _quantity = value;
                    NotifyOfPropertyChange(() => Quantity);
                }
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    NotifyOfPropertyChange(() => Title);
                }
            }
        }

        private double _unitPrice;
        public double UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                if (value != _unitPrice)
                {
                    _unitPrice = value;
                    NotifyOfPropertyChange(() => UnitPrice);
                }
            }
        }

        private AlbumCategory _category;
        public AlbumCategory Category
        {
            get { return _category; }
            set {
                if (value != _category)
                {
                    _category = value;
                    NotifyOfPropertyChange(() => Category);
                }
            }
        }
   
    }
}
