using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using System.Data;

namespace ClinicManagementSystem.Pages
{
    public class PatientsModel : PageModel
    {
        public List<PatientWithMedicalHistory>? patientWithMedicalHistories { get; set; }
        private readonly DBContext dBContext;

        public PatientsModel(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public IActionResult OnGet(string LogIn)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(LogIn))
                {
                    string decryptLogIn = EncryptorDecryptor.DecryptString(LogIn);
                    if (decryptLogIn == "Success")
                    {
                        Response.Cookies.Append("Authorized", "Yes");
                        patientWithMedicalHistories = GetPatients();
                        return Page();
                    }
                }
                if (!string.IsNullOrEmpty(Request.Cookies["Authorized"]))
                {
                    if (Request.Cookies["Authorized"] == "Yes")
                    {
                        patientWithMedicalHistories = GetPatients();
                        return Page();
                    }
                }
                return Redirect("Index");
            }
            catch (Exception ex)
            {
                Log.Error("Error Loading Patients Page. See Error: " + ex.Message);
                return Redirect("Index");
            }
        }

        public List<PatientWithMedicalHistory> GetPatients()
        {
            try
            {
                List<PatientWithMedicalHistory> patientWithMedicalHistories = new List<PatientWithMedicalHistory>();
                List<Patient> patients = dBContext.Patients.ToList();
                List<MedicalHistory> medicalHistories = dBContext.MedicalHistories.ToList();
                for (int i = 0; i < patients.Count; i++)
                {
                    PatientWithMedicalHistory patient = new PatientWithMedicalHistory
                    {
                        Id = patients[i].Id,
                        Name = patients[i].Name,
                        Address = patients[i].Address,
                        Phone = patients[i].Phone,
                        Age = patients[i].Age,
                        Responded_Doctor = patients[i].Responded_Doctor,
                        procedure_name = medicalHistories[i].procedure_name,
                        Notes = patients[i].Notes
                    };
                    patientWithMedicalHistories.Add(patient);
                }
                return patientWithMedicalHistories;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IActionResult OnPostDeletePatient([FromBody] PatientInformation Json)
        {
            try
            {
                MedicalHistory patientde = dBContext.MedicalHistories.Where(x => x.patient_Id == Json.Id).FirstOrDefault()!;
                dBContext.Remove(patientde);
                dBContext.SaveChanges();
                Medicines patientde1 = dBContext.Medicines.Where(x => x.patient_Id == Json.Id).FirstOrDefault()!;
                dBContext.Remove(patientde1);
                dBContext.SaveChanges();
                Patient patientde2 = dBContext.Patients.Where(x => x.Id == Json.Id).FirstOrDefault()!;
                dBContext.Remove(patientde2);
                dBContext.SaveChanges();
                return StatusCode(200, "Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public IActionResult OnPostEditPatient([FromBody] PatientInformationForEdit Json)
        {
            try
            {
                MedicalHistory patient = dBContext.MedicalHistories.Where(x => x.patient_Id == Json.Id).FirstOrDefault()!;
                Patient dup_patient = dBContext.Patients.Where(x => x.Id == Json.Id).FirstOrDefault()!;
                patient.procedure_name = Json.Procedure;
                dup_patient.Notes = Json.Notes;
                dBContext.SaveChanges();
                return StatusCode(200, "Edited");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public IActionResult OnPostAddPatient([FromBody] PatientInformationForAdd Json)
        {
            try
            {
                Patient patientnumbercheck = dBContext.Patients.Where(x => x.Id == Json.Number).FirstOrDefault()!;
                if (patientnumbercheck == null)
                {
                    Patient patient = new Patient
                    {
                        Id = Json.Number,
                        Name = Json.Name,
                        Phone = Json.Phone,
                        Address = Json.Address!,
                        Age = (int)Json.Age!,
                        Responded_Doctor = Json.Doctor,
                        Notes = Json.Notes
                    };
                    dBContext.Patients.Add(patient);
                    dBContext.SaveChanges();
                    MedicalHistory medicalHistory = new MedicalHistory
                    {
                        Id = Json.Number,
                        patient_Id = Json.Number,
                        procedure_name = Json.Procedure
                    };
                    dBContext.MedicalHistories.Add(medicalHistory);
                    dBContext.SaveChanges();
                    return StatusCode(200, "Added");
                }
                return StatusCode(400, "User was unable to Add");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

    public class PatientInformation
    {
        public int Id { get; set; }
    }

    public class PatientInformationForEdit
    {
        public int Id { get; set; }
        public string? Procedure { get; set; }
        public string? Notes { get; set; }
    }

    public class PatientInformationForAdd
    {
        public int Number { get; set; }
        public string? Name { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
        public int Age { get; set; }
        public string? Doctor { get; set; }
        public string? Procedure { get; set; }
        public string? Notes { get; set; }
    }
}
