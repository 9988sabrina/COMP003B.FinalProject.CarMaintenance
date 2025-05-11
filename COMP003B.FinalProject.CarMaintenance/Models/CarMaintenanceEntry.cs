using System;
using System.ComponentModel.DataAnnotations;

namespace COMP003B.FinalProject.CarMaintenance.Models
{
    public class CarMaintenanceEntry
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ServiceDate { get; set; }

        [Range(0, 10000)]
        public decimal Cost { get; set; }

        public string Notes { get; set; }

        // Foreign Keys
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public int MechanicId { get; set; }
        public int ServiceTypeId { get; set; }

        // Navigation Properties
        public Car? Car { get; set; }
        public Customer? Customer { get; set; }
        public Mechanic? Mechanic { get; set; }
        public ServiceType? ServiceType { get; set; }
    }
}
