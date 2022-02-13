namespace Powerplant.Core.Domain.Model.Input
{
    public class PowerPlantInput
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double? Efficiency { get; set; }
        public double? Pmin { get; set; }
        public double? Pmax { get; set; }
    }
}