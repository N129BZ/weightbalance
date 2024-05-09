using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeightBalance.Models
{
    public class CoGUnit
    {
        public string? Station { get; set; }
        public float Weight { get; set; }
        public float Arm { get; set; }

        [property: JsonIgnore]
        public float Moment { get { return Weight * Arm; } }
    }
}
