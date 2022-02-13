namespace Powerplant.Core.Domain.Model.View
{
    public class PowerPlantView
    {
        public string Name { get; set; }
        public double P { get; set; }

        public override string ToString()
        {
            return string.Concat("Name: ", Name, " | " ,"Power: ", P);
        }
    }
}