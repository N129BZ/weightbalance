using System.Collections.ObjectModel;
using System.Text.Json;
using WeightBalance.Models;
using Syncfusion.Maui.DataGrid;
using WeightBalance.Data;

namespace WeightBalance
{
    public partial class CoGPage : ContentPage
	{
        private bool isDirty = false;

        private ObservableCollection<Aircraft> theHangar = new();
        
        private ObservableCollection<CoGUnit> _cogunits;
		public ObservableCollection<CoGUnit> CoGUnits { get { return _cogunits;	} }
		
		private Aircraft aircraft;

		public CoGPage(Aircraft selectedaircraft, ObservableCollection<Aircraft> hangar)
		{
            theHangar = hangar;
            aircraft = selectedaircraft;
			_cogunits = aircraft.CoGUnits;

            InitializeComponent();
            
			Title = $"{aircraft.Name} CG Stations";

            StationGrid.CellValueChanged += StationGrid_CellValueChanged;

            BindingContext = this;
		}

        private void ViewChart_Clicked(object sender, EventArgs e)
        {
            HandleDirty();
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