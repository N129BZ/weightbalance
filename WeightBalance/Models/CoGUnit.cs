using System.Text.Json.Serialization;


namespace WeightBalance.Models
{
    public class CoGUnit 
    {
        [field: JsonIgnore]
        private string _station = String.Empty;
        public string Station
        {
            get { return _station; }
            set { _station = value; }
        }

        [field: JsonIgnore]
        private float _weight = 0;
        public float Weight 
        {
            get { return _weight; } 
            set
            {
                _weight = value;
            }
        }

        [field: JsonIgnore]
        private float _arm = 0;
        public float Arm
        {
            get { return _arm; }
            set
            {
                _arm = value;
            }
        }

        [property: JsonIgnore]
        public float Moment 
        {
            get 
            { 
                return _weight * _arm;
            } 
        }
    }
}
