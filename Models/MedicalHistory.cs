using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagementSystem.Models
{
    [Table("MedicalHistory")]
    public class MedicalHistory
    {
        [Key]
        [Column("Id")]
        public virtual int Id { get; set; }
        [ForeignKey("Patient")]
        [Column("Patient_Id")]
        public virtual int patient_Id { get; set; }
        [Column("Procedure_Name")]
        public virtual string? procedure_name { get; set; }
    }
}
