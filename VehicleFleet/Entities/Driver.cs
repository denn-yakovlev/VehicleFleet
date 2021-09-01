using System.ComponentModel.DataAnnotations;

namespace VehicleFleet.Entities
{
    public class Driver
    {
        [MinLength(5)]
        public string FullName { get; }

        public Driver(string fullName)
        {
            FullName = fullName;
        }
    }
}