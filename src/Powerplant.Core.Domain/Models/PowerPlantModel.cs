using Powerplant.Core.Domain.Enum;

namespace Powerplant.Core.Domain.Model
{
    public class PowerPlantModel
    {
        public string Name { get; set; }
        public PowerPlantType Type { get; set; }
        public double Efficiency { get; set; }
        public double Pmin { get; set; }
        public double Pmax { get; set; }
        public double CostFuel { get; set; }
        public double CostCo2 { get; set; }
        public double CostGeneratePower { get; set; }
        public double GeneratePower { get; set; }

        public override string ToString()
        {
            return string.Concat("Name: ", Name, " | Type: ", Type, " | Cost: ", CostGeneratePower.ToString());
        }
    }
}