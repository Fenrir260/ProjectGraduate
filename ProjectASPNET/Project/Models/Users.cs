using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Login { get ; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
    }
}
