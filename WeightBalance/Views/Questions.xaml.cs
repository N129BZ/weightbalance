using System.Collections.ObjectModel;

namespace WeightBalance.Views;

public partial class Questions : ContentView
{
	public Aircraft? SelectedAircraft { get; set; }
	public ObservableCollection<CoGUnit> CoGUnits { get; set; } = [];

	public Questions(Aircraft selectedAircraft)
	{
		InitializeComponent();

		SelectedAircraft = selectedAircraft;
		CoGUnits = selectedAircraft.CoGUnits;

		BindingContext = this;
	}

	private void BuildGrid()
	{
		
    }
}