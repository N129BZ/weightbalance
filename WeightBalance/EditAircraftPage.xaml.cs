using System.Diagnostics;
using WeightBalance.Data;
using WeightBalance.Models;

namespace WeightBalance;

public partial class EditAircraftPage : ContentPage
{
	private readonly Aircraft aircraft = new();
	
	public EditAircraftPage(Aircraft selectedAircraft)
    {
		aircraft = selectedAircraft;
        
		InitializeComponent();

		Instructions.Text = "You can edit the CG limits for your aircraft here. For example, " +
					        "if your weights and arms are Metric, you can enter the min/max " +
					        "weight and min/max CG values in kilograms and millimeters and your " +
					        "station entries will be correctly interpreted on the CG chart.";
        BindingContext = this;
    }

	public string Name { get { return $"{aircraft.Name} CG Limits"; } } 
	
	public bool IsMetric 
	{ 
		get { return aircraft.IsMetric;	} 
		set 
		{
			if (aircraft.IsMetric != value)
			{
				aircraft.IsMetric = value;
				Hangar.SaveHangarList();
			}
		} 
	}

	public double MinGross 
	{ 
		get { return aircraft.MinGross; } 
		set 
		{ 
			aircraft.MinGross = value; 
			Hangar.SaveHangarList();
		} 
	}

	public double MaxGross 
	{ 
		get { return aircraft.MaxGross;} 
		set 
		{ 
			aircraft.MaxGross = value;
            Hangar.SaveHangarList();
		} 
	}

	public double MinCg 
	{  
		get { return aircraft.MinCg; } 
		set 
		{ 
			aircraft.MinCg = value;
            Hangar.SaveHangarList();
		} 
	}

	public double MaxCg 
	{ 
		get { return aircraft.MaxCg;} 
		set 
		{  
			aircraft.MaxCg = value;
            Hangar.SaveHangarList();
		} 
	}

    private async void ViewStations_Clicked(object? sender, EventArgs e)
    {
		CgPage cgp = new(aircraft);
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