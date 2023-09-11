using System.ComponentModel.DataAnnotations;

namespace TrabajoFinal___Bingo_Web_MVC_Service.Data.Entities
{
    public class Bingo : EntityBase
    {
        [Required]
        public int PlayerNumber { get; set; } = 0;
        [Required]
        public string Matrix { get; set; } = "";
        [Required]
        public string Numbers { get; set; } = "";
        [Required]
        public string Lottery { get; set; } = "";
        [Required]
        public int Hits { get; set; } = 0;
        [Required]
        public string WinningNumbers { get; set; } = "";
        [Required]
        public bool IsWinner { get; set; } = false;
    }
}
