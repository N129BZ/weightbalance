using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using System.Reflection;

namespace WeightBalance.Models
{
    public class Aircraft
    {
        public enum UnitSelector
        {
            Unknown = -1,
            Imperial = 0,
            Metric = 1
        }

        [field: JsonIgnore]
        public long _id;
        public long ID { get { return _id; } set { _id = value; } }

        [field: JsonIgnore]
        private string _name = string.Empty;
        public string Name { get { return _name; } set { _name = value; } }

        [field: JsonIgnore]
        private float _emptyWeight;
        [property: JsonIgnore]
        public float EmptyWeight { get { return _emptyWeight; } }

        [field: JsonIgnore]
        private float _emptyArm;
        [property: JsonIgnore]
        public float EmptyArm { get { return _emptyArm; } }

        [field: JsonIgnore]
        private float _emptyMoment;
        [property: JsonIgnore]
        public float EmptyMoment { get { return _emptyMoment; } }

        [field: JsonIgnore]
        private float _loadWeight;
        [property: JsonIgnore]
        public float LoadWeight { get { return _loadWeight; } }

        [field: JsonIgnore]
        private float _loadArm;
        [property: JsonIgnore]
        public float LoadArm { get { return _loadArm; } }

        [field: JsonIgnore]
        private float _loadMoment;
        [property: JsonIgnore]
        public float LoadMoment { get { return _loadMoment; } }

        [property: JsonIgnore]
        public float TotalWeight { get { return _emptyWeight + _loadWeight; } }

        [property: JsonIgnore]
        public float TotalArm { get { return _emptyArm + _loadArm; } }

        [property: JsonIgnore]
        public float TotalMoment { get { return _emptyMoment + _loadMoment; } }

        [field: JsonIgnore]
        private float _cog;
        [property: JsonIgnore]
        public float CoG { get { return _cog; } }

        [field: JsonIgnore]
        private string _dispname = String.Empty;
        public string DisplayName { get { return _dispname; } set { _dispname = value; } }

        [field: JsonIgnore]
        private float _maxgross;
        public float MaxGross { get { return _maxgross; } set { _maxgross = value; } }

        [field: JsonIgnore]
        private string _acimgpath = String.Empty;
        public string AircraftImagePath { get { return _acimgpath; } set { _acimgpath = value; } }

        [field: JsonIgnore]
        private string _chartimgpath = String.Empty;
        public string ChartImagePath { get { return _chartimgpath; } set { _chartimgpath = value; } }

        [field: JsonIgnore]
        private float[] _cgrect = new float[4];
        public float[] CgRectangle { get { return _cgrect; } set { _cgrect = value; } }

        [field: JsonIgnore]
        private UnitSelector _unitSelector = UnitSelector.Unknown;
        public UnitSelector Unit { get { return _unitSelector; } set { _unitSelector = value; } }

        [field: JsonIgnore]
        private ObservableCollection<CoGUnit> _cogs = [];

        public ObservableCollection<CoGUnit> CoGUnits { get { return _cogs; } set { _cogs = value; } }

        [property: JsonIgnore]
        public float CalculatedCoG
        {
            get
            {
                float cog = 0;

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

                    var totwt = _emptyWeight + _loadWeight;
                    var totarm = _emptyArm + _loadArm;
                    var totmoment = _emptyMoment + _loadMoment;

                    cog = totmoment / totarm;

                }

                return cog;
            }
        }

        public Image AircraftImage { get { return new Image { Source = _acimgpath }; } }

        public Image ChartImage { get { return new Image { Source = _chartimgpath }; } }

        public ImageSource AircraftImageSource
        {
            get
            {
                var srcname = $"WeightBalance.Resources.Images.{_acimgpath}";
                return ImageSource.FromResource(srcname, typeof(Aircraft).GetTypeInfo().Assembly);
            }
        }
    }
}