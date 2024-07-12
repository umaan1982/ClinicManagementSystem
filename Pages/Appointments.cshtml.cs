using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace ClinicManagementSystem.Pages
{
    public class AppointmentsModel : PageModel
    {
        public List<Appointment> appointsments { get; set; }
        private readonly DBContext dBContext;

        public AppointmentsModel(DBContext dBContext)
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
                        appointsments = dBContext.appointments.ToList();
                        return Page();
                    }
                }
                
            }
            catch (Exception ex)
            {
                Log.Error("Error Loading Main Page. See Error: " + ex.Message);
            }
            return Redirect("Index");
        }

        public IActionResult OnPostDeleteAppointment([FromBody] AppointmentDelete json)
        {
            try
            {
                Appointment appointment = dBContext.appointments.Where(x => x.Id == json.id).FirstOrDefault()!;
                dBContext.Remove(appointment);
                dBContext.SaveChanges();
                return StatusCode(200, "Added");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public IActionResult OnPostAddAppointment([FromBody] AppointmentAdd json)
        {
            try
            {
                Appointment appointment = dBContext.appointments.Where(x => x.Id == json.id).FirstOrDefault()!;
                if(appointment == null)
                {
                    Appointment new_appointment = new Appointment
                    {
                        Id = json.id,
                        patient_Name = json.patientname,
                        AppointmentDate = json.appointmentdate.ToUniversalTime()
                    };
                    dBContext.Add(new_appointment);
                    dBContext.SaveChanges();
                    return StatusCode(200, "Added");
                }
                return StatusCode(400, "Not added");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

    public class AppointmentDelete
    {
        public int id { get; set; }
    }

    public class AppointmentAdd
    {
        public int id { get; set; }
        public string? patientname { get; set; }
        public DateTime appointmentdate { get; set; }
    }
}
