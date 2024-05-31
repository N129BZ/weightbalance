using Syncfusion.Maui.DataGrid;
using System.Collections.ObjectModel;
using WeightBalance.Data;
using WeightBalance.Models;

namespace WeightBalance;

public partial class CgPage : ContentPage
{
    private readonly Hangar Hangar;

    public ObservableCollection<CoGUnit> CgUnits { get; private set; } = []; 

    public Aircraft SelectedAircraft { get; private set; } = new();

    public string PageTitle { get; private set; } = string.Empty; 

    public CgPage(Aircraft selectedAircraft, Hangar hangar)
    {
        this.Hangar = hangar;
        SelectedAircraft = selectedAircraft;
        SelectedAircraft.CalculateCg();
        CgUnits = SelectedAircraft.CoGUnits;
        
        InitializeComponent();

        DataGridTableSummaryCellRendererExt summaryRenderer = new()
        {
            SelectedAircraft = this.SelectedAircraft,
            CgLabel = CgLabel,
            CgFrame = CgFrame
        };

        StationGrid.CellRenderers.Remove("TableSummary");
        StationGrid.CellRenderers.Add("TableSummary", summaryRenderer);
        PageTitle = $"{SelectedAircraft.Name} CG Stations";
        
        BindingContext = this;
    }

    public string GetTotalWeight
    {
        get
        {
            return $"Weight: " + SelectedAircraft.TotalWeight.ToString("#0.0");
        }
    }

    public string GetTotalMoment
    {
        get
        {
            return "Moment:" + SelectedAircraft.TotalMoment.ToString("#0.0");
        }
    }

    public string CalculatedCoG
    {
        get
        {
            return "CG: " + SelectedAircraft.CoG.ToString("#0.00");
        }
    }

    private async void ViewChart_Clicked(object sender, EventArgs e)
    {
        StationGrid.Refresh();
        SaveData();
        await Navigation.PushAsync(new ChartPage(SelectedAircraft, SelectedAircraft.CoG));
    }

    private async void ViewAircraftList_Clicked(object sender, EventArgs e)
    {
        StationGrid.Refresh();
        SaveData();
        await Navigation.PopToRootAsync();
    }

    private void SaveData()
    {
        SelectedAircraft.CalculateCg();
        if (!this.Hangar.SaveHangarList())
            throw new Exception("Data not saved!");
    }

    private void StationGrid_CurrentCellEndEdit(object sender, DataGridCurrentCellEndEditEventArgs e)
    {
        Console.WriteLine(e.ToString());
        SaveData();
    }

    private void StationGrid_CurrentCellActivated(object sender, DataGridCurrentCellActivatedEventArgs e)
    {
        SelectedAircraft.CalculateCg();
    }
}

public class DataGridTableSummaryCellRendererExt : DataGridTableSummaryCellRenderer
{
    public Aircraft SelectedAircraft { get; set; } = new();
    
    public Label CgLabel { get; set; } = new();
    
    public Frame CgFrame {  get; set; } = new();
    
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
        SelectedAircraft.CalculateCg();
       
        if (SelectedAircraft.IsWithinRange && SelectedAircraft.IsWithinWeight)
        {
            Color bg = Color.FromRgba("#E8F8F5");
            CgFrame.BackgroundColor = bg;
            CgLabel.TextColor = Colors.Navy;
            CgLabel.BackgroundColor = bg;
            CgLabel.Text = "CG: " + SelectedAircraft.CoG.ToString("#0.00");
        }
        else
        {
            CgFrame.BackgroundColor = Colors.Red;
            CgLabel.TextColor = Colors.White;
            CgLabel.BackgroundColor = Colors.Red;
            if (SelectedAircraft.TotalWeight > SelectedAircraft.MaxGross)
            {
                CgLabel.Text = "OVERWEIGHT! CG: " + SelectedAircraft.CoG.ToString("#0.00");
            }
        }
    }
}