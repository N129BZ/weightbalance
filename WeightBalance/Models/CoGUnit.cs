using System.ComponentModel;
using System.Text.Json.Serialization;

namespace WeightBalance.Models;

public class CoGUnit : INotifyPropertyChanged
{
    public string? Station { get; set; }

    [field: JsonIgnore]
    private double weight = 0;
    public double Weight
    {
        get { return weight; }
        set
        {
            weight = value;
            RaisePropertyChanged(nameof(Weight));
            RaisePropertyChanged(nameof(Moment));
        }
    }

    [field: JsonIgnore]
    private double arm = 0;
    public double Arm
    {
        get { return arm; }
        set
        {
            arm = value;
            RaisePropertyChanged(nameof(Arm));
            RaisePropertyChanged(nameof(Moment));
        }
    }

    [property: JsonIgnore]
    public double Moment { get { return weight * arm; } }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void RaisePropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
