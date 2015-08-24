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

        public Album Clone()
        {
               Album newInstance = new Album()
               {
                    Title = this.Title,
                    Quantity = this.Quantity,
                    UnitPrice = this.UnitPrice,
                    Category = this.Category 
               };
               return newInstance;
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set {
                if (value != _quantity)
                {
                    _quantity = value;
                    NotifyOfPropertyChange(() => Quantity);
                    NotifyOfPropertyChange(() => Extension);
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
                    NotifyOfPropertyChange(() => Extension);
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

        public double Extension
        {
            get
            {
                return UnitPrice * Quantity;
            }
        }
   
    }
}
