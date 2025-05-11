using System.ComponentModel.DataAnnotations;

namespace COMP003B.FinalProject.CarMaintenance.Models
{
    public class ServiceType
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public ICollection<CarMaintenanceEntry>? CarMaintenanceEntries { get; set; }
    }
}
