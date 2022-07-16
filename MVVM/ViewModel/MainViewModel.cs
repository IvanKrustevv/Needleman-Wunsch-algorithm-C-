using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenXmlPowerTools;
using SequenceAllignProject.Core;

namespace SequenceAllignProject.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand AboutViewCommand { get; set; }
        public HomeViewModel HomeVm { get; set; }

        public AboutViewModel AboutVm { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value;
                OnPropertyChanged();

            }

        }

        public MainViewModel()
        {
            HomeVm = new HomeViewModel();
            AboutVm = new AboutViewModel();
            CurrentView = HomeVm;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVm;
            });

            AboutViewCommand = new RelayCommand(o => {
                CurrentView = AboutVm;
            });

        }
    }
}
