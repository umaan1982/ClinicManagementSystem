using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagementSystem.Models
{
    [Table("Patient")]
    public class Patient
    {
        [Key]
        [Column("Id")]
        public virtual int Id { get; set; }
        [Column("Name")]
        public virtual string? Name { get; set; }
        [Column("Phone")]
        public virtual string Phone { get; set; }
        [Column("Address")]
        public virtual string? Address { get; set; }
        [Column("Age")]
        public virtual int Age { get; set; }
        [Column("Responded_Doctor")]
        public virtual string? Responded_Doctor { get; set; }
        [Column("Notes")]
        public virtual string? Notes { get; set; }
    }
}
