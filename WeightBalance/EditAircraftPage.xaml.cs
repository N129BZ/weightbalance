using System.Diagnostics;
using WeightBalance.Data;
using WeightBalance.Models;

namespace WeightBalance;

public partial class EditAircraftPage : ContentPage
{
	private readonly Aircraft aircraft = new();
	private readonly Hangar hangar = new();

	public EditAircraftPage(Aircraft selectedAircraft, Hangar hangar)
    {
		aircraft = selectedAircraft;
        this.hangar = hangar;
        InitializeComponent();

		Instructions.Text = "You can edit the CG limits for your aircraft here. For example, " +
					        "if your weights and arms are Metric, you can enter the min/max " +
					        "weight and min/max CG values in kilograms and millimeters and your " +
					        "station entries will be correctly interpreted on the CG chart.";
        BindingContext = this;
    }

	public string Name { get { return $"{aircraft.Name} CG Limits"; } } 
	
	public double MinGross 
	{ 
		get { return aircraft.MinGross; } 
		set 
		{ 
			aircraft.MinGross = value; 
			hangar.SaveHangarList();
		} 
	}

	public double MaxGross 
	{ 
		get { return aircraft.MaxGross;} 
		set 
		{ 
			aircraft.MaxGross = value; 
			hangar.SaveHangarList();
		} 
	}

	public double MinCg 
	{  
		get { return aircraft.MinCg; } 
		set 
		{ 
			aircraft.MinCg = value;
			hangar.SaveHangarList();
		} 
	}

	public double MaxCg 
	{ 
		get { return aircraft.MaxCg;} 
		set 
		{  
			aircraft.MaxCg = value;
			hangar.SaveHangarList();
		} 
	}

    private async void ViewChart_Clicked(object sender, EventArgs e)
    {
        hangar.SaveHangarList();
        await Navigation.PushAsync(new ChartPage(aircraft, aircraft.CoG));
    }

    private async void ViewStations_Clicked(object? sender, EventArgs e)
    {
		CgPage cgp = new(aircraft, hangar);
        await Navigation.PushAsync(cgp, true);
    }

    private async void ViewAircraftSelection_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PopToRootAsync(true);
    }

    private void NumericText_Changed(object sender, TextChangedEventArgs e)
    {
		Debug.Print(e.ToString());
    }
}