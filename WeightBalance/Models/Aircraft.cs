using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json.Serialization;

namespace WeightBalance.Models;

public class Aircraft
{

    public ObservableCollection<CoGUnit> CoGUnits { get; set; } = []; 

    [field: JsonIgnore]
    public long id;
    public long ID { get { return id; } set { id = value; } }

    [field: JsonIgnore]
    private string name = string.Empty;
    public string Name { get { return name; } set { name = value; } }

    [field: JsonIgnore]
    private double maxGross;
    public double MaxGross { get { return maxGross; } set { maxGross = value; } }

    [field: JsonIgnore]
    private double minGross;
    public double MinGross { get { return minGross; } set { minGross = value; } }

    [field: JsonIgnore]
    private double minCg;
    public double MinCg { get { return minCg; } set { minCg = value; } }

    [field: JsonIgnore]
    private double maxCg;
    public double MaxCg { get { return maxCg; } set { maxCg = value; } }

    [field: JsonIgnore]
    private double emptyWeight;
    [property: JsonIgnore]
    public double EmptyWeight { get { return emptyWeight; } set { emptyWeight = value; } }

    [field: JsonIgnore]
    private double emptyArm;
    [property: JsonIgnore]
    public double EmptyArm { get { return emptyArm; } set { emptyArm = value; } }

    [field: JsonIgnore]
    private double emptyMoment;
    [property: JsonIgnore]
    public double EmptyMoment { get { return emptyMoment; } set { emptyMoment = value; } }

    [field: JsonIgnore]
    private double loadWeight;
    [property: JsonIgnore]
    public double LoadWeight { get { return loadWeight; } set { loadWeight = value; } }

    [field: JsonIgnore]
    private double loadArm;
    [property: JsonIgnore]
    public double LoadArm { get { return loadArm; } set { loadArm = value; } }

    [field: JsonIgnore]
    private double loadMoment;
    [property: JsonIgnore]
    public double LoadMoment { get { return loadMoment; } set { loadMoment = value; } }

    [field: JsonIgnore]
    private double totalWeight = 0;
    [property: JsonIgnore]
    public double TotalWeight { get { return totalWeight; } }

    [field: JsonIgnore]
    private double totalArm = 0;
    [property: JsonIgnore]
    public double TotalArm { get { return totalArm; } }

    [field: JsonIgnore]
    private double totalMoment = 0;
    [property: JsonIgnore]
    public double TotalMoment { get { return totalMoment; } }

    [field: JsonIgnore]
    private string displayName = String.Empty;
    public string DisplayName { get { return displayName; } set { displayName = value; } }

    [field: JsonIgnore]
    private string acImagePath = String.Empty;
    public string AircraftImagePath { get { return acImagePath; } set { acImagePath = value; } }

    [field: JsonIgnore]
    private string chartImagePath = String.Empty;
    public string ChartImagePath { get { return chartImagePath; } set { chartImagePath = value; } }

    [field: JsonIgnore]
    private double[] cgRectangleCoordinates = new double[4];
    public double[] CgRectCoordinates { get { return cgRectangleCoordinates; } set { cgRectangleCoordinates = value; } }

    [property: JsonIgnore]
    public Rect CgRectangle
    {
        get
        {
            Rect rect = new()
            {
                X = cgRectangleCoordinates[0],
                Y = cgRectangleCoordinates[1],
                Width = cgRectangleCoordinates[2],
                Height = cgRectangleCoordinates[3]
            };
            return rect;
        }
    }

    [field: JsonIgnore]
    private bool isDefault = false;
    public bool IsDefault { get { return isDefault; } set { isDefault = value; } }

    [field: JsonIgnore]
    private double cog = 0;

    [property: JsonIgnore]
    public double CoG { get { CalculateCg(); return cog; } }

    public void CalculateCg()
    {
        double cog = 0;
        loadWeight = 0;
        loadArm = 0;
        loadMoment = 0;
        emptyWeight = 0;
        emptyArm = 0;
        emptyMoment = 0;

        if (CoGUnits != null)
        {
            foreach (var item in CoGUnits)
            {
                if (item.Station == "NoseWheel" ||
                    item.Station == "TailWheel" ||
                    item.Station == "LeftMain" ||
                    item.Station == "RightMain")
                {
                    emptyWeight += item.Weight;
                    emptyArm += item.Arm;
                    emptyMoment += item.Weight * item.Arm;
                }
                else
                {
                    loadWeight += item.Weight;
                    loadArm += item.Arm;
                    loadMoment += (item.Weight * item.Arm);
                }
            }

            totalWeight = emptyWeight + loadWeight;
            totalArm = emptyArm + loadArm;
            totalMoment = emptyMoment + loadMoment;

            cog = totalMoment / totalWeight;

            isWithinRange = cog >= minCg && cog <= MaxCg;
            isWithinWeight = totalWeight <= maxGross;
        }

        if (cog > 0)
            this.cog = Math.Round(cog, 2);
        else
            this.cog = 0;
    }

    [field: JsonIgnore]
    private bool isWithinRange = false;
    [property: JsonIgnore]
    public bool IsWithinRange { get { return isWithinRange; } }

    [field: JsonIgnore]
    private bool isWithinWeight = false;
    [property: JsonIgnore]
    public bool IsWithinWeight { get { return isWithinWeight; } }

    [property: JsonIgnore]
    public string AircraftResourcePath {  get { return $"WeightBalance.Resources.Images.{acImagePath}"; } }
    
    [property: JsonIgnore]
    public string ChartResourcePath { get { return $"WeightBalance.Resources.Images.{chartImagePath}"; } }

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