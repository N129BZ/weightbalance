using System.Collections.ObjectModel;
using Microsoft.VisualBasic;
using WeightBalance.Models;

namespace WeightBalance;

public class DefaultData 
{
    public ObservableCollection<Aircraft> aircrafts = new();

    public DefaultData() 
    {
        aircrafts.Add(new Aircraft(()"",))
    }


}