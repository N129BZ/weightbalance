using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace WeightBalance
{
    public enum UnitSelector
    {
        Imperial = 0,
        Metric = 1
    }

    public class Aircraft
    {
        [field: JsonIgnore]
        private Image? _acimage;

        [property: JsonIgnore]
        public Image? AcImage
        {
            get
            {
                if (_acimage == null)
                {
                    _acimage = new Image { Source = ImagePath };
                }
                return _acimage;
            }
        }

        [field: JsonIgnore]
        private Image? _chartimage;

        [property: JsonIgnore]
        public Image? ChartImage
        {
            get
            {
                if (_chartimage == null)
                {
                    _chartimage = new Image { Source = ChartImagePath, Scale = .75 };
                }
                return _chartimage;
            }
        }
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? ImagePath { get; set; }
        public string? ChartImagePath { get; set; }
        public float[]? CgRectangle { get; set; }
        public UnitSelector Unit { get; set; }
        public ObservableCollection<CoGUnit>? CoGUnits { get; set; }

    }

    public class CoGUnit
    {
        public string? Name { get; set; }
        public float Weight { get; set; }
        public float Arm { get; set; }

        [property: JsonIgnore]
        public float Moment { get { return Weight * Arm; } }
    }
}