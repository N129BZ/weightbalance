
using Syncfusion.Maui.DataGrid;
using Syncfusion.Maui.DataGrid.Helper;
using System.Collections.ObjectModel;
using WeightBalance.Data;
using WeightBalance.Models;

namespace WeightBalance
{
    public partial class CoGPage : ContentPage
    {
        private bool isDirty = false;
        
        private readonly ObservableCollection<Aircraft> _hangarList = [];

        private readonly ObservableCollection<CoGUnit> _cogUnits;
        public ObservableCollection<CoGUnit> CoGUnits { get { return _cogUnits; } }

        private string _pageTitle = String.Empty;
        public string PageTitle { get { return _pageTitle; } set { _pageTitle = value; } }

        private readonly Aircraft _aircraft = new();
        private Hangar _hangar;

        public CoGPage(Aircraft selectedAircraft, Hangar hangar)
        {
            _hangar = hangar;
            _hangarList = hangar.HangarList;
            _aircraft = selectedAircraft;
            _aircraft.CalculateCoG();
            _cogUnits = _aircraft.CoGUnits;

            InitializeComponent();

            DataGridTableSummaryCellRendererExt summaryRenderer = new()
            {
                SelectedAircraft = _aircraft,
                CogLabel = CoGLabel
            };

            StationGrid.CellRenderers.Remove("TableSummary");
            StationGrid.CellRenderers.Add("TableSummary", summaryRenderer);
            StationGrid.CurrentCellEndEdit += StationGrid_EndEdit;
            _pageTitle = $"{_aircraft.Name} CG Stations";

            BindingContext = this;
        }

        public string GetTotalWeight
        {
            get
            {
                return $"Weight: " + _aircraft.TotalWeight.ToString("#0.0");
            }
        }

        public string GetTotalMoment
        {
            get
            {
                return $"Moment: " + _aircraft.TotalMoment.ToString("#0.0");
            }
        }

        public string CalculatedCoG
        {
            get
            {
                return "CG: " + _aircraft.CoG.ToString("#0.00");
            }
        }

        private void ViewChart_Clicked(object sender, EventArgs e)
        {
            HandleDirty();
            Navigation.PushAsync(new AircraftPage(_aircraft));
        }

        private void ViewAircraftList_Clicked(object sender, EventArgs e)
        {
            HandleDirty();
            Navigation.PopToRootAsync();
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
                _aircraft.CalculateCoG();
                if (_hangar.SaveHangarList(_hangarList))
                {
                    isDirty = false;
                }
            }
        }

        private void StationGrid_EndEdit(object sender, DataGridCurrentCellEndEditEventArgs e)
        {
            if (isDirty)
            {
                _aircraft.CalculateCoG();
                StationGrid.RefreshColumns();
            }
        }
    }

    public class DataGridTableSummaryCellRendererExt : DataGridTableSummaryCellRenderer
    {
        private Aircraft _aircraft = new();
        public Aircraft SelectedAircraft
        {
            get { return _aircraft; }
            set { _aircraft = value; }
        }

        private Label _cogLabel = new();
        public Label CogLabel
        {
            get { return _cogLabel; }
            set { _cogLabel = value; }
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
            _aircraft.CalculateCoG();
            _cogLabel.Text = "CG: " + _aircraft.CoG.ToString("#0.00");
        }
    }
}