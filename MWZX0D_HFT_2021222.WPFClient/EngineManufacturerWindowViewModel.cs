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
    public class EngineManufacturerWindowViewModel : ObservableRecipient
    {
        public RestCollection<EngineManufacturer> EngineManufacturers { get; set; }
        private EngineManufacturer selectedEngineManufacturer;

        public EngineManufacturer SelectedEngineManufacturer
        {
            get { return selectedEngineManufacturer; }
            set
            {
                if (value != null)
                {
                    selectedEngineManufacturer = new EngineManufacturer()
                    {
                        Name = value.Name,
                        EngineManufacturerId = value.EngineManufacturerId,
                    };
                }

                OnPropertyChanged();
                (DeleteEngineManufacturerCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand CreateEngineManufacturerCommand { get; set; }
        public ICommand DeleteEngineManufacturerCommand { get; set; }
        public ICommand UpdateEngineManufacturerCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public EngineManufacturerWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                string baseURL = "http://localhost:43412/";

                EngineManufacturers = new RestCollection<EngineManufacturer>(baseURL, "enginemanufacturer", "hub");

                CreateEngineManufacturerCommand = new RelayCommand(() =>
                {
                    EngineManufacturers.Add(new EngineManufacturer() { Name = SelectedEngineManufacturer.Name });
                });

                UpdateEngineManufacturerCommand = new RelayCommand(() =>
                {
                    EngineManufacturers.Update(SelectedEngineManufacturer);
                });

                DeleteEngineManufacturerCommand = new RelayCommand(() =>
                {
                    EngineManufacturers.Delete(SelectedEngineManufacturer.EngineManufacturerId);
                }, () => SelectedEngineManufacturer != null);

                SelectedEngineManufacturer = new EngineManufacturer();
            }
        }
    }
}
