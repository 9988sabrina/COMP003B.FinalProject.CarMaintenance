using System.ComponentModel.DataAnnotations;

namespace COMP003B.FinalProject.CarMaintenance.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string? FullName { get; set; }

        [Phone]
        public string? Phone { get; set; }

        public ICollection<CarMaintenanceEntry>? CarMaintenanceEntries { get; set; }
    }
}
