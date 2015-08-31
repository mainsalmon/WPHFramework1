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

        private Album _originalValues;
        private bool _isNew;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            _vendors = new ObservableCollection<Vendor>()
            {
                new Vendor(){Id="FRY", Name="Fry's"},
                new Vendor(){Id="AMZ", Name="Amazon"},
                new Vendor(){Id="FM", Name="Fred Meyer"}
            };


            _albums = new ObservableCollection<Album>();

            _albums.Add(new Album(1) { Title = "XXXX", Category = AlbumCategory.Country, Quantity = 3, UnitPrice = 8.95, ReleaseDate = new DateTime(2012, 3, 14), VendorId = "FRY" });
            _albums.Add(new Album(2) { Title = "YYYY", Category = AlbumCategory.Pop, Quantity = 55, UnitPrice = 12.49, ReleaseDate = new DateTime(2010, 7, 22), VendorId = "AMZ" });

            SelectedAlbum = Albums[0];

            // Test the repository and json backing
            // json files can be intially added via the 'add' button, then 'save'
            JsonRepository<Album> repo = new JsonRepository<Album>();

           // int nextID = repo.GetNextId();

            //var rslt = repo.SearchFor(a => a.VendorId == "FM");
            //if (rslt != null)
            //{
            //    foreach (var a in rslt)
            //    {
            //        _albums.Add(a);
            //    }
            //}

            //Album a3 = repo.GetById(3);
            //if (a3 != null)
            //{
            //    _albums.Add(a3);
            //}

            //var fromFiles = repo.GetAll();
            //_albums = new ObservableCollection<Album>(fromFiles);

         
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
                        _originalValues = SelectedAlbum.Clone();
                        
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
                Album.CopyValues(_originalValues, SelectedAlbum); // revert to original values
                _originalValues = null;
            }
            
            SelectedAlbum = null;
            IsEditMode = false;
            _isNew = false;
        }

        public void DoSave()
        {
            int i = SelectedAlbum.ID;

            // the actual save to local folder of json files
            JsonRepository<Album> repo = new JsonRepository<Album>();
            repo.Save(SelectedAlbum);

            SelectedAlbum = null;
            IsEditMode = false;
            _originalValues = null;
            _isNew = false;

        }

        public void DoAddNew()
        {
            int nextId = Albums.OrderBy(a => a.ID).Last().ID + 1; 
            // TODO: if using repo:
            // JsonRepository<Album> repo = new JsonRepository<Album>();
            // int nextId2 = repo.GetNextId();

            Album newAlbum = new Album(nextId) {Title = "(new)"};
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
            // todo the actual delete here

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

        private ObservableCollection<Vendor> _vendors;

        public ObservableCollection<Vendor> Vendors
        {
            get {
             return _vendors; }
            set {
             _vendors = value;
             NotifyOfPropertyChange(() => Vendors);
             
             }
        }
        
    }
}
