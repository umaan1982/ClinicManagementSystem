using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ClinicManagementSystem.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        [Column("Id")]
        public virtual int Id { get; set; }
        [Column("username")]
        public virtual string? Username { get; set; }
        [Column("password")]
        public virtual string? Password { get; set; }
        [Column("Role")]
        public virtual string? Role { get; set; }
    }
}
