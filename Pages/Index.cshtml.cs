using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClinicManagementSystem.Security;
using Serilog;

namespace ClinicManagementSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DBContext dBContext;

        public IndexModel(ILogger<IndexModel> logger, DBContext DBContext)
        {
            _logger = logger;
            dBContext = DBContext;
        }

        private static List<Users> users { get; set; }

        public void OnGet()
        {
            try
            {
                Response.Cookies.Append("Authorized", "No");
                users = dBContext.Users.ToList();
            }
            catch(Exception ex)
            {
                Log.Error("Error Loading Main Page. See Error: " + ex.Message);
            }
        }

        public IActionResult OnPostSignIn([FromBody]UserDetails Json)
        {
            try
            {
                bool flag = false;
                foreach(Users user in users)
                {
                    if(user.Username == Json.Username && user.Password == Json.Password)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    string SuccessLogIn = EncryptorDecryptor.EncryptString("Success");
                    return StatusCode(200, SuccessLogIn);
                }
                return StatusCode(400, "Bad login");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

    public class UserDetails
    {
        public string? Username { get; set; }
        public string? Password { get; set; }   
    }
}