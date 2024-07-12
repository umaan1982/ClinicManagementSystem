using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagementSystem.Models
{
    [Table("Medicines")]
    public class Medicines
    {
        [Key]
        [Column("Id")]
        public virtual int Id { get; set; }
        [ForeignKey("Patient")]
        [Column("Patient_Id")]
        public virtual int patient_Id { get; set; }
        [Column("Name")]
        public virtual string? Name { get; set; }
        [Column("Quantity")]
        public virtual int Quantity { get; set; }
        [Column("DateGiven")]
        public virtual DateTime Date_Given { get; set; }
        [Column("TimePeriod")]
        public virtual DateTime TimePeriod { get; set; }
    }
}
