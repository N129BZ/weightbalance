using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json.Serialization;

namespace WeightBalance.Models
{
    public class Aircraft : INotifyPropertyChanged
    {
        [field: JsonIgnore]
        public long _id;
        public long ID { get { return _id; } set { _id = value; } }

        [field: JsonIgnore]
        private string _name = string.Empty;
        public string Name { get { return _name; } set { _name = value; } }

        [field: JsonIgnore]
        private double _maxGross;
        public double MaxGross { get { return _maxGross; } set { _maxGross = value; } }

        [field: JsonIgnore]
        private double _minGross;
        public double MinGross { get { return _minGross; } set { _minGross = value; } }

        [field: JsonIgnore]
        private double _minCg;
        public double MinCg { get { return _minCg; } set { _minCg = value; } }

        [field: JsonIgnore]
        private double _maxCg;
        public double MaxCg { get { return _maxCg; } set { _maxCg = value; } }

        [field: JsonIgnore]
        private double _emptyWeight;
        [property: JsonIgnore]
        public double EmptyWeight { get { return _emptyWeight; } set { _emptyWeight = value; } }

        [field: JsonIgnore]
        private double _emptyArm;
        [property: JsonIgnore]
        public double EmptyArm { get { return _emptyArm; } set { _emptyArm = value; } }

        [field: JsonIgnore]
        private double _emptyMoment;
        [property: JsonIgnore]
        public double EmptyMoment { get { return _emptyMoment; } set { _emptyMoment = value; } }

        [field: JsonIgnore]
        private double _loadWeight;
        [property: JsonIgnore]
        public double LoadWeight { get { return _loadWeight; } set { _loadWeight = value; } }

        [field: JsonIgnore]
        private double _loadArm;
        [property: JsonIgnore]
        public double LoadArm { get { return _loadArm; } set { _loadArm = value; } }

        [field: JsonIgnore]
        private double _loadMoment;
        [property: JsonIgnore]
        public double LoadMoment { get { return _loadMoment; } set { _loadMoment = value; } }

        [field: JsonIgnore]
        private double _totalWeight = 0;
        [property: JsonIgnore]
        public double TotalWeight { get { return _totalWeight; } }

        [field: JsonIgnore]
        private double _totalArm = 0;
        [property: JsonIgnore]
        public double TotalArm { get { return _totalArm; } }

        [field: JsonIgnore]
        private double _totalMoment = 0;
        [property: JsonIgnore]
        public double TotalMoment { get { return _totalMoment; } }

        [field: JsonIgnore]
        private string _displayName = String.Empty;
        public string DisplayName { get { return _displayName; } set { _displayName = value; } }

        [field: JsonIgnore]
        private string _acImagePath = String.Empty;
        public string AircraftImagePath { get { return _acImagePath; } set { _acImagePath = value; } }

        [field: JsonIgnore]
        private string _chartImagePath = String.Empty;
        public string ChartImagePath { get { return _chartImagePath; } set { _chartImagePath = value; } }

        [field: JsonIgnore]
        private double[] _cgRectangleCoordinates = new double[4];
        public double[] CgRectCoordinates { get { return _cgRectangleCoordinates; } set { _cgRectangleCoordinates = value; } }

        [property: JsonIgnore]
        public Rect CgRectangle
        {
            get
            {
                Rect rect = new()
                {
                    X = _cgRectangleCoordinates[0],
                    Y = _cgRectangleCoordinates[1],
                    Width = _cgRectangleCoordinates[2],
                    Height = _cgRectangleCoordinates[3]
                };
                return rect;
            }
        }

        [field: JsonIgnore]
        private bool _isDefault = false;
        public bool IsDefault { get { return _isDefault; } set { _isDefault = value; } }

        [field: JsonIgnore]
        private ObservableCollection<CoGUnit> _cogUnits = [];

        public ObservableCollection<CoGUnit> CoGUnits { get { return _cogUnits; } set { _cogUnits = value; } }

        [field: JsonIgnore]
        private double _cog = 0;

        [property: JsonIgnore]
        public double CoG { get { CalculateCg(); return _cog; } }

        public event PropertyChangedEventHandler? PropertyChanged
        {
            add
            {
                ((INotifyPropertyChanged)CoGUnits).PropertyChanged += value;
            }

            remove
            {
                ((INotifyPropertyChanged)CoGUnits).PropertyChanged -= value;
            }
        }

        public void CalculateCg()
        {
            double cog = 0;
            _loadWeight = 0;
            _loadArm = 0;
            _loadMoment = 0;
            _emptyWeight = 0;
            _emptyArm = 0;
            _emptyMoment = 0;

            if (_cogUnits != null)
            {
                foreach (var item in _cogUnits)
                {
                    if (item.Station == "NoseWheel" ||
                        item.Station == "TailWheel" ||
                        item.Station == "LeftMain" ||
                        item.Station == "RightMain")
                    {
                        _emptyWeight += item.Weight;
                        _emptyArm += item.Arm;
                        _emptyMoment += item.Weight * item.Arm;
                    }
                    else
                    {
                        _loadWeight += item.Weight;
                        _loadArm += item.Arm;
                        _loadMoment += (item.Weight * item.Arm);
                    }
                }

                _totalWeight = _emptyWeight + _loadWeight;
                _totalArm = _emptyArm + _loadArm;
                _totalMoment = _emptyMoment + _loadMoment;

                cog = _totalMoment / _totalWeight;

                _isWithinRange = cog >= _minCg && cog <= MaxCg;
                _isWithinWeight = _totalWeight <= _maxGross;
            }

            if (cog > 0)
                _cog = Math.Round(cog, 2);
            else
                _cog = 0;
        }

        [field: JsonIgnore]
        private bool _isWithinRange = false;
        [property: JsonIgnore]
        public bool IsWithinRange { get { return _isWithinRange; } }

        [field: JsonIgnore]
        private bool _isWithinWeight = false;
        [property: JsonIgnore]
        public bool IsWithinWeight { get { return _isWithinWeight; } }

        [property: JsonIgnore]
        public string AircraftResourcePath {  get { return $"WeightBalance.Resources.Images.{_acImagePath}"; } }
        
        [property: JsonIgnore]
        public string ChartResourcePath { get { return $"WeightBalance.Resources.Images.{_chartImagePath}"; } }

        [property: JsonIgnore]
        public static string GreenDotResourcePath { get { return $"WeightBalance.Resources.Images.greendot.png"; } }

        [property: JsonIgnore]
        public static string RedDotResourcePath { get { return $"WeightBalance.Resources.Images.reddot.png"; } }

        [property: JsonIgnore]
        public ImageSource AircraftImageSource { get { return ImageSource.FromResource(AircraftResourcePath, typeof(Aircraft).GetTypeInfo().Assembly); } }

        public override string ToString()
        {
            return this.Name;
        }
    }
}