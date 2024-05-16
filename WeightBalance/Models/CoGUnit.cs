using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace WeightBalance.Models
{
    public class CoGUnit : INotifyPropertyChanged
    {
        public string? Station { get; set; }

        [field: JsonIgnore]
        private double _weight = 0;
        public double Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                OnPropertyChanged("Weight");
            }
        }

        [field: JsonIgnore]
        private double _arm = 0;
        public double Arm
        {
            get { return _arm; }
            set
            {
                _arm = value;
                OnPropertyChanged("Arm");
            }
        }

        [field: JsonIgnore]
        public double _moment = 0;
        [property: JsonIgnore]
        public double Moment
        {
            get { return _weight * _arm; }
            set
            {
                _moment = value;
                OnPropertyChanged("Moment");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
