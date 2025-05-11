using System.ComponentModel.DataAnnotations;

namespace COMP003B.FinalProject.CarMaintenance.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public string? Make { get; set; }

        [Required]
        public string? Model { get; set; }

        [Range(1900, 2100)]
        public int Year { get; set; }

        public ICollection<CarMaintenanceEntry>? CarMaintenanceEntries { get; set; }
    }
}
