namespace TrabajoFinal___Bingo_Web_MVC_Service.Services.Extensions.ForBingo
{
    public class DrawLottery : IChanceMechanism
    {
        private readonly Random _random;
        public DrawLottery(Random random)
        {
            _random = random;
        }

        public string GetNewDrawLot() => _random.Next(1, 91).ToString();

        public string GetNewDrawLot(string lottery)
        {
            List<string> lotteryList = lottery.Split(" - ").ToList();
            List<int> lotteryListInt = new();
            foreach (string lottedNum in lotteryList)
            {
                if (lottedNum != "") { int lottedNumInt = int.Parse(lottedNum); lotteryListInt.Add(lottedNumInt); }
            }

            int lottedNumber;
            do { lottedNumber = _random.Next(1, 91); }
            while (lotteryListInt.IndexOf(lottedNumber) >= 0);

            return lottedNumber.ToString();
        }
    }
}
