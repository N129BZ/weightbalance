using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using System.Reflection;

namespace WeightBalance.Models
{
    public class Aircraft
    {
        [field: JsonIgnore]
        public long _id;
        public long ID { get { return _id; } set { _id = value; } }

        [field: JsonIgnore]
        private string _name = string.Empty;
        public string Name { get { return _name; } set { _name = value; } }

        [field: JsonIgnore]
        private double _maxgross;
        public double MaxGross { get { return _maxgross; } set { _maxgross = value; } }

        [field: JsonIgnore]
        private double _mingross;
        public double MinGross { get { return _mingross; } set { _mingross = value; } }

        [field: JsonIgnore]
        private double _mincg;
        public double MinCg { get { return _mincg; } set { _mincg = value; } }

        [field: JsonIgnore]
        private double _maxcg;
        public double MaxCg { get { return _maxcg; } set { _maxcg = value; } }

        [field: JsonIgnore]
        private float _emptyWeight;
        [property: JsonIgnore]
        public float EmptyWeight { get { return _emptyWeight; } set { _emptyWeight = value; } }

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

        [property: JsonIgnore]
        public double TotalWeight { get { return _emptyWeight + _loadWeight; } }

        [property: JsonIgnore]
        public double TotalArm { get { return _emptyArm + _loadArm; } }

        [property: JsonIgnore]
        public double TotalMoment { get { return _emptyMoment + _loadMoment; } }

        [field: JsonIgnore]
        private string _dispname = String.Empty;
        public string DisplayName { get { return _dispname; } set { _dispname = value; } }

        [field: JsonIgnore]
        private string _acimgpath = String.Empty;
        public string AircraftImagePath { get { return _acimgpath; } set { _acimgpath = value; } }

        [field: JsonIgnore]
        private string _chartimgpath = String.Empty;
        public string ChartImagePath { get { return _chartimgpath; } set { _chartimgpath = value; } }

        [field: JsonIgnore]
        private double[] _cgrectcoords = new double[4];
        public double[] CgRectCoordinates { get { return _cgrectcoords; } set { _cgrectcoords = value; } }

        [property: JsonIgnore]
        public Rect CgRectangle 
        { 
            get 
            {
                Rect rect = new Rect
                {
                    X = _cgrectcoords[0],
                    Y = _cgrectcoords[1],
                    Width = _cgrectcoords[2],
                    Height = _cgrectcoords[3]
                };
                return rect;
            } 
        }

        [field: JsonIgnore]
        private bool _isdefault = false;
        public bool IsDefault { get { return _isdefault; } set { _isdefault = value; } }

        [field: JsonIgnore]
        private ObservableCollection<CoGUnit> _cogs = [];

        public ObservableCollection<CoGUnit> CoGUnits { get { return _cogs; } set { _cogs = value; } }

        [property: JsonIgnore]
        public double CoG
        {
            get
            {
                double cog = 0;
                _loadWeight = 0;
                _loadArm = 0;
                _loadMoment = 0;
                _emptyWeight = 0;
                _emptyArm = 0;
                _emptyMoment = 0;

                if (_cogs != null)
                {
                    foreach (var item in _cogs)
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

                    cog = TotalMoment / TotalWeight;
                }
                return cog;
            }
        }

        [property: JsonIgnore]
        public string AircraftResourcePath { get { return $"WeightBalance.Resources.Images.{_acimgpath}"; } }

        [property: JsonIgnore]
        public string ChartResourcePath { get { return $"WeightBalance.Resources.Images.{_chartimgpath}"; } }

        [property: JsonIgnore]
        public string GreenDotResourcePath { get { return $"WeightBalance.Resources.Images.greendot.png"; } }

        [property: JsonIgnore]
        public string RedDotResourcePath { get { return $"WeightBalance.Resources.Images.reddot.png"; } }

        [property: JsonIgnore]
        public ImageSource AircraftImageSource { get { return ImageSource.FromResource(AircraftResourcePath, typeof(Aircraft).GetTypeInfo().Assembly); } }
    }
}