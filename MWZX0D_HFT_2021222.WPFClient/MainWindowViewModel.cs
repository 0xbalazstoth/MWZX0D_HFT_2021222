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
    public class MainWindowViewModel
    {
        public ICommand OpenDriversWindowCommand { get; set; }
        public ICommand OpenTeamsWindowCommand { get; set; }
        public ICommand OpenEngineManufacturersWindowCommand { get; set; }
        public ICommand ExitCommand { get; set; }

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
                OpenDriversWindowCommand = new RelayCommand(() =>
                {
                    DriverWindow driverWindow = new DriverWindow();
                    driverWindow.ShowDialog();
                });

                OpenTeamsWindowCommand = new RelayCommand(() =>
                {
                    TeamWindow teamWindow = new TeamWindow();
                    teamWindow.ShowDialog();
                });

                OpenEngineManufacturersWindowCommand = new RelayCommand(() =>
                {
                    EngineManufacturerWindow engineManufacturerWindow = new EngineManufacturerWindow();
                    engineManufacturerWindow.ShowDialog();
                });

                ExitCommand = new RelayCommand(() =>
                {
                    Environment.Exit(0);
                });
            }
        }
    }
}
