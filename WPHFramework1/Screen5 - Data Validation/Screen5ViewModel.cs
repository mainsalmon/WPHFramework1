using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WPHFramework1
{
    public class Screen5ViewModel : PropertyChangedBase
    {
        public Screen5ViewModel()
        {
            Members = new ObservableCollection<Person>(){
                new Person("James", 41, "brown"),
                new Person("Ed", 17, "black"),
                new Person("Fred", 43, "brown")
            };
        }

        private ObservableCollection<Person> _members;
        public ObservableCollection<Person> Members
        {
            get { return _members; }
            set {
                _members = value;
                NotifyOfPropertyChange(() => Members);
            }
        }

        private Person _selectedMember;

        public Person SelectedMember
        {
            get { return _selectedMember; }
            set { 
                _selectedMember = value;
                NotifyOfPropertyChange(() => SelectedMember);
            }
        }
        
    }
}
