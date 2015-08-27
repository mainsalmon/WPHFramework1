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

        private DateTime _releaseDate;
        public DateTime ReleaseDate
        {
            get { return _releaseDate; }
            set {
             _releaseDate = value;
             NotifyOfPropertyChange(() => ReleaseDate);
             }
        }
        

        public double Extension
        {
            get
            {
                return UnitPrice * Quantity;
            }
        }

        private string _vendorId;
        public string VendorId
        {
            get { return _vendorId; }
            set {
                _vendorId = value;
                NotifyOfPropertyChange(() => VendorId);
              }
        }

        #region Plumbing for reverting changes
        // This stuff could move to a IRevertable interface and/or a DataModelBase class that derives from PropertyChangedBase

        public static void CopyValues(Album from, Album to)
        {
            //to.Title = from.Title;
            //to.Quantity = from.Quantity;
            //etc via Reflection:

            Type type = from.GetType();
            System.Reflection.PropertyInfo[] allProperties = type.GetProperties();

            foreach (System.Reflection.PropertyInfo property in allProperties)
            {
                if (property.CanWrite) //TODO: this could be made more flexible by honoring a custom 'DoNotCopy' attribute
                {
                    property.SetValue(to, property.GetValue(from, null), null);
                }
            }
        }

        public Album Clone()
        {
            // Assumes this is a flat, data model object
            Album newInstance = new Album();
            CopyValues(this, newInstance);
            return newInstance;
        }

        #endregion Plumbing for reverting changes
    }
}
