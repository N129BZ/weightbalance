using System.Collections.ObjectModel;
using WeightBalance.Models;
using Syncfusion.Maui.DataGrid;
using WeightBalance.Data;

namespace WeightBalance
{
    public partial class CoGPage : ContentPage
	{
        private bool isDirty = false;

        private ObservableCollection<Aircraft> theHangar = [];
        
        private ObservableCollection<CoGUnit> _cogunits;
		public ObservableCollection<CoGUnit> CoGUnits { get { return _cogunits;	} }

        private string _pagetitle = String.Empty;
        public string PageTitle { get { return _pagetitle; } set { _pagetitle = value; } }  

		private Aircraft aircraft;

		public CoGPage(Aircraft selectedaircraft, ObservableCollection<Aircraft> hangar)
		{
            theHangar = hangar;
            aircraft = selectedaircraft;
			_cogunits = aircraft.CoGUnits;

            InitializeComponent();
            
			_pagetitle = $"{aircraft.Name} CG Stations";

            StationGrid.CellValueChanged += StationGrid_CellValueChanged;

            BindingContext = this;
		}

        private void ViewChart_Clicked(object sender, EventArgs e)
        {
            HandleDirty();
            aircraft.CalculateCoG();
			var ap = new AircraftPage(aircraft);
			Navigation.PushAsync(ap);
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            HandleDirty();
			Navigation.PopToRootAsync();
        }

        private void StationGrid_CellValueChanged(object? sender, DataGridCellValueChangedEventArgs e)
        {
            isDirty = true;
        }

        private void ExitHangar_Clicked(object sender, EventArgs e)
        {
            Application.Current?.Quit();
        }

        private void HandleDirty()
        {
            if (isDirty) 
            { 
                if (Hangar.SaveTheHangar(theHangar))
                {
                    isDirty = false;
                }
            }
        }
    }
}