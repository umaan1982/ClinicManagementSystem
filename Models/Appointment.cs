using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagementSystem.Models
{
    [Table("Appointment")]
    public class Appointment
    {
        [Key]
        [Column("Id")]
        public virtual int Id { get; set; }
        [Column("Patient_Name")]
        public virtual string? patient_Name { get; set; }
        [Column("Appointment_Date")]
        public virtual DateTime AppointmentDate { get; set; }
    }
}
