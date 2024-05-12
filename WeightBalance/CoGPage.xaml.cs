using System.Collections.ObjectModel;
using System.Text.Json;
using WeightBalance.Models;

namespace WeightBalance
{
    public partial class CoGPage : ContentPage
	{
		private ObservableCollection<CoGUnit> _cogunits;
		
		public ObservableCollection<CoGUnit> CoGUnits { get { return _cogunits;	} }
		
		private Aircraft aircraft;

		public CoGPage(Aircraft selectedaircraft)
		{
			aircraft = selectedaircraft;
			_cogunits = aircraft.CoGUnits;

            InitializeComponent();
            
			this.Title = $"{aircraft.Name} CG Stations";
	
            BindingContext = this;
		}

        private void ViewChart_Clicked(object sender, EventArgs e)
        {
			var ap = new AircraftPage(aircraft);
			Navigation.PushAsync(ap);
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
			Navigation.PopToRootAsync();
        }
    }
}