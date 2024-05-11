using System.Collections.ObjectModel;
using System.Text.Json;
using WeightBalance.Models;

namespace WeightBalance
{
    public partial class CoGPage : ContentPage
	{
		private ObservableCollection<CoGUnit> _cogunits;
		
		public ObservableCollection<CoGUnit> CoGUnits
		{
			get
			{
				return _cogunits;
			}
		}
		private string _title;
		public string PageTitle { get { return _title; } }

		private Aircraft _aircraft;

		public CoGPage(Aircraft aircraft)
		{
			_aircraft = aircraft;
			_title = $"{aircraft.Name} CG Stations";
			_cogunits = aircraft.CoGUnits;
            InitializeComponent();
            BindingContext = this;
		}

        private void ViewChart_Clicked(object sender, EventArgs e)
        {
			var ap = new AircraftPage(_aircraft);
			Navigation.PushAsync(ap);
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
			Navigation.PopToRootAsync();
        }
    }
}