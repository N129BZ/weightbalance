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
					        "weight and min/max CG values in kilograms and centimeters and your " +
					        "station entries will be correctly interpreted on the CG chart.";
        BindingContext = this;
    }

	public string Name { get { return $"Aircraft: {aircraft.Name}"; } } 
	
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

    private void ExitHangar_Clicked(object? sender, EventArgs e)
    {
        Application.Current?.Quit();
    }

    private async void ViewStations_Clicked(object? sender, EventArgs e)
    {
		CgPage cgp = new CgPage(aircraft, hangar);
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