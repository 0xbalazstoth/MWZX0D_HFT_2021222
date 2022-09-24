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
    public class TeamWindowViewModel : ObservableRecipient
    {
        public RestCollection<Team> Teams { get; set; }
        private Team selectedTeam;

        public Team SelectedTeam
        {
            get { return selectedTeam; }
            set
            {
                if (value != null)
                {
                    selectedTeam = new Team()
                    {
                        Name = value.Name,
                        TeamId = value.TeamId,
                        Principal = value.Principal,
                        LicensedIn = value.LicensedIn,
                        EngineManufacturer = value.EngineManufacturer,
                        EngineManufacturerId = value.EngineManufacturerId,
                        Drivers = value.Drivers
                    };
                }

                OnPropertyChanged();
                (DeleteTeamCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand CreateTeamCommand { get; set; }
        public ICommand DeleteTeamCommand { get; set; }
        public ICommand UpdateTeamCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public TeamWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                string baseURL = "http://localhost:43412/";
                Teams = new RestCollection<Team>(baseURL, "team", "hub");

                CreateTeamCommand = new RelayCommand(() =>
                {
                    Teams.Add(new Team() { Drivers = SelectedTeam.Drivers, EngineManufacturer = SelectedTeam.EngineManufacturer, EngineManufacturerId = SelectedTeam.EngineManufacturerId, Name = SelectedTeam.Name, LicensedIn = SelectedTeam.LicensedIn, Principal = SelectedTeam.Principal });
                });

                DeleteTeamCommand = new RelayCommand(() =>
                {
                    Teams.Delete(SelectedTeam.TeamId);
                }, () => SelectedTeam != null);

                UpdateTeamCommand = new RelayCommand(() =>
                {
                    Teams.Update(SelectedTeam);
                });

                SelectedTeam = new Team();
            }
        }
    }
}
