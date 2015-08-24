using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Caliburn.Micro;

namespace WPHFramework1
{
    public class Screen10ViewModel : Screen
    {

        private Album _pendingChanges;
        private bool _isNew;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            _albums = new ObservableCollection<Album>()
            {
                new Album(){Title = "XXXX", Category=AlbumCategory.Country, Quantity=3, UnitPrice=8.95},
                new Album(){Title = "YYYY", Category=AlbumCategory.Pop, Quantity=55, UnitPrice=12.49}
            };
            SelectedAlbum = Albums[0];
        }

        private ObservableCollection<Album> _albums;

        public ObservableCollection<Album> Albums
        {
            get { return _albums; }
            set
            {

                _albums = value;
                NotifyOfPropertyChange(() => Albums);
            }
        }

        private Album _selectedAlbum;

        public Album SelectedAlbum
        {
            get { return _selectedAlbum; }
            set 
            { 
                _selectedAlbum = value;
                NotifyOfPropertyChange(()=> SelectedAlbum);
                RefreshActionGuards();
            }
        }

        private bool _isEditMode;

        public bool IsEditMode
        {
            get { return _isEditMode; }
            set 
            {
                if (value != _isEditMode)
                {
                    _isEditMode = value;
                    NotifyOfPropertyChange(() => IsEditMode);
                    RefreshActionGuards();

                    if (SelectedAlbum != null && _isEditMode)
                    {
                        // clone so we can undo if they cancel
                        _pendingChanges = SelectedAlbum.Clone();
                        
                    }
                   
                }
                
            }
        }

        private void RefreshActionGuards()
        {
            NotifyOfPropertyChange(() => CanDoEditMode);
            NotifyOfPropertyChange(() => CanDoAddNew);
            NotifyOfPropertyChange(() => CanDoDelete);
        }

        #region Actions

        public void DoEditMode()
        {
            IsEditMode = true;
        }

        public bool CanDoEditMode
        {
            get { return SelectedAlbum != null && IsEditMode == false; }
        }

        public void DoCancel()
        {
            if (_isNew)
            {
                DoDelete();
            }
            else
            {
                CopyValues(_pendingChanges, SelectedAlbum); // put the values back
                _pendingChanges = null;
            }
            
            SelectedAlbum = null;
            IsEditMode = false;
            _isNew = false;
        }

        public void DoSave()
        {
            Save();
            SelectedAlbum = null;
            IsEditMode = false;
            _pendingChanges = null;
            _isNew = false;
        }

        private void Save()
        {
        
        }

        public void DoAddNew()
        {
            Album newAlbum = new Album() {Title = "(new)"};
            Albums.Add(newAlbum);
            SelectedAlbum = newAlbum;
            _isNew = true;
            IsEditMode = true;
            RefreshActionGuards();
        }

        public bool CanDoAddNew
        {
            get { return IsEditMode == false; }
        }

        public void DoDelete()
        {
            Album selected = SelectedAlbum;
            SelectedAlbum = null;
            Albums.Remove(selected);
            RefreshActionGuards();
        }

        public bool CanDoDelete
        {
            get { return SelectedAlbum != null && IsEditMode == false; }
        }

        #endregion Actions

        private void CopyValues(Album from, Album to)
        {
            to.Title = from.Title;
            to.Quantity = from.Quantity;
            to.UnitPrice = from.UnitPrice;
            to.Category = from.Category;
        }
    }
}
