using System.Collections.ObjectModel;
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
		public string Title { get { return _title; } }

		private Aircraft _aircraft;

		public CoGPage(Aircraft aircraft)
		{
			_aircraft = aircraft;
			_title = $"Selected: {aircraft.Name}";
			_cogunits = aircraft.CoGUnits;
            InitializeComponent();
            BindingContext = this;
		}

        private void ViewChart_Clicked(object sender, EventArgs e)
        {
			var ap = new AircraftPage(_aircraft);
            Navigation.PushAsync(ap);
        }
    }
}