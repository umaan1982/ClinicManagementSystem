using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace ClinicManagementSystem.Pages
{
    public class MedicinesModel : PageModel
    {
        public List<PatientWithMedicines>? patientWithMedicines{ get; set; }
        private readonly DBContext dBContext;
        public MedicinesModel(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public IActionResult OnGet()
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Cookies["Authorized"]))
                {
                    if (Request.Cookies["Authorized"] == "Yes")
                    {
                        patientWithMedicines = GetPatientsWithMedicines();
                        return Page();
                    }
                }
                return Redirect("Index");
            }
            catch (Exception ex)
            {
                Log.Error("Error Loading Medicines Page. See Error: " + ex.Message);
                return Redirect("Index");
            }
        }

        public List<PatientWithMedicines> GetPatientsWithMedicines()
        {
            try
            {
                List<PatientWithMedicines> patientWithMedicines = new List<PatientWithMedicines>();
                List<Patient> patients = dBContext.Patients.ToList();
                List<Medicines> medicines = dBContext.Medicines.ToList();
                foreach(Patient patient in patients)
                {
                    foreach(Medicines medicine in medicines)
                    {
                        if(medicine.patient_Id == patient.Id)
                        {
                            PatientWithMedicines patientWithMedicines1 = new PatientWithMedicines
                            {
                                Id = medicine.Id,
                                PatientId = patient.Id,
                                PatientName = patient.Name,
                                Quantity = medicine.Quantity,
                                Date_Given = medicine.Date_Given,
                                TimePeriod = medicine.TimePeriod,
                                MedicineName = medicine.Name
                            };
                            patientWithMedicines.Add(patientWithMedicines1);
                        }
                    }
                }
                return patientWithMedicines;
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to get Patients With Medicines.Reason : " + ex.Message);
            }
        }


        public IActionResult OnPostAddMedicine([FromBody]MedicineAdd json)
        {
            try
            {
                Patient patient = dBContext.Patients.Where(x => x.Name == json.patientName).FirstOrDefault()!;
                Medicines medicine = new Medicines
                {
                    Id = json.Id,
                    patient_Id = patient.Id,
                    Name = json.medicineName,
                    Quantity = json.quantity,
                    Date_Given = json.givenAt.ToUniversalTime(),
                    TimePeriod = json.timePeriod.ToUniversalTime()
                };
                dBContext.Medicines.Add(medicine);
                dBContext.SaveChanges();
                return StatusCode(200, "Added");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public IActionResult OnPostDeleteMedicine([FromBody]MedicineDelete json)
        {
            try
            {
                //Medicines medicine = dBContext.Medicines.Where(x => x.Id == json.Id).FirstOrDefault()!;
                //dBContext.Remove(medicine);
                //dBContext.SaveChanges();
                return StatusCode(200, "Successful");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }

    public class MedicineAdd
    {
        public int Id { get; set; }
        public string? patientName { get; set; }
        public string? medicineName { get; set; }
        public int quantity { get; set; }
        public DateTime givenAt { get; set; }
        public DateTime timePeriod { get; set; }
    }

    public class MedicineDelete
    {
        public int Id { get; set; }
    }

}
