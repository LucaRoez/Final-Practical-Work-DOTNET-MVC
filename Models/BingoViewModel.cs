using System.ComponentModel.DataAnnotations;

namespace TrabajoFinal___Bingo_Web_MVC_Service.Models
{
    public class BingoViewModel : BaseViewModel
    {
        public int PlayerNumber { get; set; } = 0;
        public string MatrixModelString { get; set; } = "";
        [Required]
        public List<int> MatrixModel { get; set; } = new();
        public string ChosenNumbers { get; set; } = "";
        public string CurrentLottery { get; set; } = "";
        public int CurrentHits { get; set; } = 0;
        public string CurrentWinningNumbers { get; set; } = "";
        public bool IsWinner { get; set; } = false;
    }
}
