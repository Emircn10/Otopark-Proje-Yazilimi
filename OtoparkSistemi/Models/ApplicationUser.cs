using Microsoft.AspNetCore.Identity;

namespace OtoparkSistemi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? AdSoyad { get; set; }
    }
}
