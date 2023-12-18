﻿using System.ComponentModel.DataAnnotations;

namespace server.Dtos.Car
{
    public class GetSingleCarDto
    {
        [Required]
        public int Id { get; set; }
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
    }
}
