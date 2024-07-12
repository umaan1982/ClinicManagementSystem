namespace ClinicManagementSystem.Models
{
    public class PatientWithMedicalHistory
    {
        public virtual int Id { get; set; }
        public virtual string? Name { get; set; }
        public virtual string Phone { get; set; }
        public virtual string? Address { get; set; }
        public virtual int Age { get; set; }
        public virtual string? Responded_Doctor { get; set; }
        public virtual string? procedure_name { get; set; }
        public virtual string? Notes { get; set; }
    }
}
