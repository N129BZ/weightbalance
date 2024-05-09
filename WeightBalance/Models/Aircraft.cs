using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace WeightBalance.Models
{


    public class Aircraft
    {
        public enum UnitSelector
        {
            Imperial = 0,
            Metric = 1
        }

        public string? Name { get; set; }

        [field: JsonIgnore]
        private float _emptyWeight;
        [property: JsonIgnore]
        public float EmptyWeight
        {
            get
            {
                return _emptyWeight;
            }
        }

        [field: JsonIgnore]
        private float _emptyArm;
        [property: JsonIgnore]
        public float EmptyArm
        {
            get
            {
                return _emptyArm;
            }
        }

        [field: JsonIgnore]
        private float _emptyMoment;
        [property: JsonIgnore]
        public float EmptyMoment
        {
            get
            {
                return _emptyMoment;
            }
        }

        public string? DisplayName { get; set; }
        public float MaxGross { get; set; }
        public string? ImagePath { get; set; }
        public string? ChartImagePath { get; set; }
        public float[]? CgRectangle { get; set; }
        public UnitSelector Unit { get; set; }

        [field: JsonIgnore]
        private ObservableCollection<CoGUnit> _cogs = [];

        public ObservableCollection<CoGUnit> CoGUnits
        {
            get { return _cogs; }
            set
            {
                _cogs = value;
                SetEmptyState();
            }
        }

        private void SetEmptyState()
        {
            if (CoGUnits != null)
            {
                foreach (var item in CoGUnits)
                {
                    if (item.Station == "Nose Wheel" ||
                        item.Station == "Tail Wheel" ||
                        item.Station == "Left Main" ||
                        item.Station == "Right Main")
                    {
                        _emptyWeight += item.Weight;
                        _emptyArm += item.Arm;
                        _emptyMoment += item.Weight * item.Arm;
                    }
                }
            }
        }

        [property: JsonIgnore]
        public Image AcImage { get { return new Image { Source = ImagePath }; } }
        [property: JsonIgnore]
        public Image Spacer { get { return new Image { Source = "chartspacer.png" }; } }
        [property: JsonIgnore]
        public Image ChartImage { get { return new Image { Source = ChartImagePath }; } }

    }
}