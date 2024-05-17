using Syncfusion.Maui.DataGrid;
using Syncfusion.Maui.DataGrid.Helper;
using System.Collections.ObjectModel;
using WeightBalance.Data;
using WeightBalance.Models;

namespace WeightBalance;

public partial class CgPage : ContentPage
{
    private readonly Hangar? hangar;

    private ObservableCollection<CoGUnit>? cgUnits;
    public ObservableCollection<CoGUnit>? CgUnits { get { return cgUnits; } set { cgUnits = value; } }

    private Aircraft? aircraft = new();
    public Aircraft? SelectedAircraft { get { return aircraft; } set { aircraft = value; } }

    private readonly string pageTitle = string.Empty;
    public string PageTitle { get { return pageTitle; } }

    public CgPage(Aircraft selectedAircraft, Hangar hangar)
    {
        this.hangar = hangar;
        aircraft = selectedAircraft;
        aircraft.CalculateCg();
        cgUnits = aircraft.CoGUnits;

        InitializeComponent();

        DataGridTableSummaryCellRendererExt summaryRenderer = new()
        {
            SelectedAircraft = aircraft,
            CgLabel = CgLabel,
            CgFrame = CgFrame
        };

        StationGrid.CellRenderers.Remove("TableSummary");
        StationGrid.CellRenderers.Add("TableSummary", summaryRenderer);
        pageTitle = $"{aircraft.Name} CG Stations";
        
        BindingContext = this;
    }

    public string GetTotalWeight
    {
        get
        {
            return $"Weight: " + aircraft.TotalWeight.ToString("#0.0");
        }
    }

    public string GetTotalMoment
    {
        get
        {
            return "Moment:" + aircraft.TotalMoment.ToString("#0.0");
        }
    }

    public string CalculatedCoG
    {
        get
        {
            return "CG: " + aircraft.CoG.ToString("#0.00");
        }
    }

    private async void ViewChart_Clicked(object sender, EventArgs e)
    {
        StationGrid.Refresh();
        SaveData();
        await Navigation.PushAsync(new AircraftPage(aircraft));
    }

    private async void ViewAircraftList_Clicked(object sender, EventArgs e)
    {
        StationGrid.Refresh();
        SaveData();
        await Navigation.PopToRootAsync();
    }

    private void ExitHangar_Clicked(object sender, EventArgs e)
    {
        StationGrid.Refresh();
        SaveData();
        Application.Current?.Quit();
    }

    private void SaveData()
    {
        aircraft.CalculateCg();
        if (!hangar.SaveHangarList())
            throw new Exception("Data not saved!");
    }

    private void StationGrid_CurrentCellEndEdit(object sender, DataGridCurrentCellEndEditEventArgs e)
    {
        SaveData();
    }
}

public class DataGridTableSummaryCellRendererExt : DataGridTableSummaryCellRenderer
{
    private Aircraft aircraft = new();
    public Aircraft SelectedAircraft
    {
        get { return aircraft; }
        set { aircraft = value; }
    }

    private Label cgLabel = new();
    public Label CgLabel
    {
        get { return cgLabel; }
        set { cgLabel = value; }
    }

    private Frame cgFrame = new();
    public Frame CgFrame
    {
        get { return cgFrame; }
        set { cgFrame = value; }
    }

    protected override void OnSetCellStyle(DataColumnBase dataColumn)
    {
        base.OnSetCellStyle(dataColumn);

        if (dataColumn.ColumnElement != null && dataColumn.ColumnElement.Content is SfDataGridLabel label)
        {
            dataColumn.ColumnElement.Background = Colors.Navy;

            label.HorizontalTextAlignment = TextAlignment.End;
            label.FontSize = 16;
            label.FontAttributes = FontAttributes.Bold;
            label.TextColor = Colors.White;
        }
    }

    protected override void OnUpdateCellValue(DataColumnBase dataColumn)
    {
        base.OnUpdateCellValue(dataColumn);
        
        CheckCgStatus();
    }

    private void CheckCgStatus()
    {
        aircraft.CalculateCg();
        

        if (aircraft.IsWithinRange && aircraft.IsWithinWeight)
        {
            Color bg = Color.FromRgba("#E8F8F5");
            cgFrame.BackgroundColor = bg;
            cgLabel.TextColor = Colors.Navy;
            cgLabel.BackgroundColor = bg;
            cgLabel.Text = "CG: " + aircraft.CoG.ToString("#0.00");
        }
        else
        {
            cgFrame.BackgroundColor = Colors.Red;
            cgLabel.TextColor = Colors.White;
            cgLabel.BackgroundColor = Colors.Red;
            if (aircraft.TotalWeight > aircraft.MaxGross)
            {
                cgLabel.Text = "OVERWEIGHT! CG: " + aircraft.CoG.ToString("#0.00");
            }
        }
    }
}