namespace ClinicManagementSystem.Configurations
{
    public class AppSettings
    {
        private static IConfiguration Configuration;

        public static void IntializeConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static string PostGreConnectionString
        {
            get
            {
                if (Configuration["ConnectionStrings:PostgreSQL"] == null || string.IsNullOrWhiteSpace(Configuration["ConnectionStrings:PostgreSQL"]))
                    throw new Exception("Scope not valid");

                return Configuration["ConnectionStrings:PostgreSQL"]!;
            }
        }
    }
}
