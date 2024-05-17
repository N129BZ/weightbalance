
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core.Platform;
using Syncfusion.Maui.DataGrid;
using Syncfusion.Maui.DataGrid.Helper;
using System.Collections.ObjectModel;
using WeightBalance.Data;
using WeightBalance.Models;

namespace WeightBalance;

public partial class CoGPage : ContentPage
{
    private bool isDirty = false;

    private readonly Hangar? hangar;

    //private readonly ObservableCollection<Aircraft>? hangarList;

    private ObservableCollection<CoGUnit>? cogUnits;
    public ObservableCollection<CoGUnit>? CoGUnits { get { return cogUnits; } set { cogUnits = value; } }

    private Aircraft? aircraft = new();
    public Aircraft? SelectedAircraft { get { return aircraft; } set { aircraft = value; } }

    private readonly string pageTitle = string.Empty;
    public string PageTitle { get { return pageTitle; } }

    public CoGPage(Aircraft selectedAircraft, Hangar hangar)
    {
        this.hangar = hangar;
        //hangarList = hangar.HangarList;
        aircraft = selectedAircraft;
        aircraft.CalculateCg();
        cogUnits = aircraft.CoGUnits;

        InitializeComponent();

        DataGridTableSummaryCellRendererExt summaryRenderer = new()
        {
            SelectedAircraft = aircraft,
            CgLabel = CgLabel,
            CgFrame = CgFrame
        };

        StationGrid.CellRenderers.Remove("TableSummary");
        StationGrid.CellRenderers.Add("TableSummary", summaryRenderer);
        StationGrid.CurrentCellEndEdit += StationGrid_EndEdit;
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
        HandleDirty();
        await Navigation.PushAsync(new AircraftPage(aircraft));
    }

    private async void ViewAircraftList_Clicked(object sender, EventArgs e)
    {
        HandleDirty();
        await Navigation.PopToRootAsync();
    }

    private void StationGrid_CellValueChanged(object sender, DataGridCellValueChangedEventArgs? e)
    {
        isDirty = true;
    }

   
    private void ExitHangar_Clicked(object sender, EventArgs e)
    {
        HandleDirty();
        Application.Current?.Quit();
    }

    private void HandleDirty()
    {
        if (isDirty)
        {
            aircraft.CalculateCg();

            if (hangar.SaveHangarList())
            {
                isDirty = false;
            }
        }
    }

    private void StationGrid_EndEdit(object sender, DataGridCurrentCellEndEditEventArgs e)
    {
        if (isDirty)
        {
            aircraft.CalculateCg();
            StationGrid.RefreshColumns();
        }
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