using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPHFramework1
{
    public class Screen6ViewModel : PropertyChangedBase
    {

        private readonly IDialogCoordinator _dialogCoordinator;

        public Screen6ViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;
        }

        public void Employees()
        {
            _dialogCoordinator.ShowMessageAsync(this, "Dialog launched from Screen6ViewModel", "You clicked on the 'Employees' tile.");
        }

        public void HandleTileClick(object param)
        {
            var tile = param as MahApps.Metro.Controls.Tile;
            if (tile != null)
            {
                _dialogCoordinator.ShowMessageAsync(this, "Dialog launched from Screen6ViewModel", string.Format("You clicked the tile titled '{0}'", tile.Title));
            }
           
        }

    }
}
