using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MWZX0D_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MWZX0D_HFT_2021222.WPFClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Driver> Drivers { get; set; }
        private Driver selectedDriver;

        public Driver SelectedDriver
        {
            get { return selectedDriver; }
            set
            {
                if (value != null)
                {
                    selectedDriver = new Driver()
                    {
                        Name = value.Name,
                        DriverId = value.DriverId,
                        Team = value.Team,
                        Born = value.Born,
                        Nationality = value.Nationality,
                        Number = value.Number,
                        TeamId = value.TeamId
                    };
                }

                OnPropertyChanged();
                (DeleteDriverCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand CreateDriverCommand { get; set; }
        public ICommand DeleteDriverCommand { get; set; }
        public ICommand UpdateDriverCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                string baseURL = "http://localhost:43412/";

                Drivers = new RestCollection<Driver>(baseURL, "driver", "hub");

                CreateDriverCommand = new RelayCommand(() =>
                {
                    Drivers.Add(new Driver() { Name = SelectedDriver.Name });
                });

                UpdateDriverCommand = new RelayCommand(() =>
                {
                    Drivers.Update(SelectedDriver);
                });

                DeleteDriverCommand = new RelayCommand(() =>
                {
                    Drivers.Delete(SelectedDriver.DriverId);
                }, () => SelectedDriver != null);

                SelectedDriver = new Driver();
            }
        }
    }
}
