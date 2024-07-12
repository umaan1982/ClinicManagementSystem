namespace ClinicManagementSystem.Models
{
    public class PatientWithMedicines
    {

        public virtual int Id { get; set; }
        public virtual string? PatientName { get; set; }
        public int? PatientId { get; set; }
        public virtual string? MedicineName { get; set; }
        public virtual int Quantity { get; set; }
        public virtual DateTime Date_Given { get; set; }
        public virtual DateTime TimePeriod { get; set; }
    }
}

