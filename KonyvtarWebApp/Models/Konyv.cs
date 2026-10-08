using System.ComponentModel.DataAnnotations;

namespace KonyvtarWebApp.Models
{
    public class Konyv
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A cím megadása kötelező!")]
        [MaxLength(150, ErrorMessage = "A cím maximum 150 karakter lehet!")]
        public string Cim { get; set; }
        public int KiadasEve { get; set; }

        [Required]
        public int SzerzoId { get; set; }

        // Navigation property to the Szerzo class
        public Szerzo Szerzo { get; set; }
    }
}
