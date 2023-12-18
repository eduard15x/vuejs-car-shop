using System.ComponentModel.DataAnnotations;

namespace server.Dtos.Car
{
    public class AddNewCarDto
    {
        [Required]
        public string CarName { get; set; }
        [Required]
        public string CarModel { get; set; }
        [Required]
        public int CarYear { get; set; }
        [Required]
        public int CarPrice { get; set; }
        [Required]
        public string Engine { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
