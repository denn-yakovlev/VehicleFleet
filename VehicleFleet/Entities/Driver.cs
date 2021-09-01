using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleFleet.Entities
{
    public class Driver
    {
        public int Id { get; init; }
            
        [MinLength(5)]
        public string FullName { get; init; }
    }
}