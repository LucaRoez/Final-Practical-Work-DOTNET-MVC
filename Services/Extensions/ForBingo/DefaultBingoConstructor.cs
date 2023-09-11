using TrabajoFinal___Bingo_Web_MVC_Service.Models;

namespace TrabajoFinal___Bingo_Web_MVC_Service.Services.Extensions.ForBingo
{
    public class DefaultBingoConstructor : IStructureConstructor
    {
        public BingoViewModel GetNewBingo(int playerNumber)
        {
            string matrix = GetMatrixString(GetMatrix(new int[3, 9], new Random()));
            List<int> m = GetMatrixInt(matrix);
            BingoViewModel bingo = new() { MatrixModelString = matrix, MatrixModel = m, PlayerNumber = playerNumber };

            return bingo;
        }

        private int[,] GetMatrix(int[,] matrix, Random random)
        {
            int maxErasedSquaresPerRow = 4;
            int maxErasedSquaresPerCol = 2;

            List<int> selectedCols = new();
            List<int> discardedRows = new();
            List<int> discardedCols = new();

            int erased = 0;
            while (erased < 12)
            {
                int row_to_erase;
                int col_to_erase;
                if (discardedRows.Count == 0) { row_to_erase = random.Next(0, 3); }
                else { do { row_to_erase = random.Next(0, 3); } while (discardedRows.Contains(row_to_erase)); }
                if (selectedCols.Count == 0) { col_to_erase = random.Next(0, 9); }
                else
                {
                    do { col_to_erase = random.Next(0, 9); } while (!selectedCols.Contains(col_to_erase) || discardedCols.Contains(col_to_erase));
                    selectedCols.Remove(col_to_erase);
                }

                if (matrix[row_to_erase, col_to_erase] == -1) continue;

                int erasedSquaresInRow = CountErasedSquaresInRow(matrix, row_to_erase);
                int erasedSquaresInCol = CountErasedSquaresInCol(matrix, col_to_erase);
                int[] numsPerColumn = CountNumsPerColumn(matrix);

                if (numsPerColumn[col_to_erase] == 1) { discardedCols.Add(col_to_erase); continue; }
                if (erasedSquaresInRow >= maxErasedSquaresPerRow || erasedSquaresInCol >= maxErasedSquaresPerCol)
                {
                    if (erasedSquaresInRow >= maxErasedSquaresPerRow) { discardedRows.Add(row_to_erase); }
                    if (erasedSquaresInCol >= maxErasedSquaresPerCol) { discardedCols.Add(col_to_erase); }
                    continue;
                }

                matrix[row_to_erase, col_to_erase] = -1; erased++;

                numsPerColumn = CountNumsPerColumn(matrix); selectedCols.Clear();
                for (int c = 0; c < 9; c++) { if (numsPerColumn[c] == 3) { selectedCols.Add(c); } };
            }
            return matrix;
        }

        private int CountErasedSquaresInRow(int[,] matrix, int row_to_erase)
        {
            int erasedSquaresInRow = 0;   // counts all erased squares per column throughout the lotted row
            for (int c = 0; c < 9; c++) { if (matrix[row_to_erase, c] == -1) erasedSquaresInRow++; }

            return erasedSquaresInRow;
        }
        private int CountErasedSquaresInCol(int[,] matrix, int col_to_erase)
        {
            int erasedSquaresInCol = 0;   // then does the same for the rows (over the lotted col)
            for (int r = 0; r < 3; r++) { if (matrix[r, col_to_erase] == -1) erasedSquaresInCol++; }

            return erasedSquaresInCol;
        }
        private int[] CountNumsPerColumn(int[,] matrix)
        {
            int[] numsPerColumn = new int[9];   // also counts how many remaining valid selectable squares there're per column
            for (int c = 0; c < 9; c++) { for (int r = 0; r < 3; r++) { if (matrix[r, c] != -1) numsPerColumn[c]++; } }

            return numsPerColumn;
        }
        private string GetMatrixString(int[,] matrix)
        {
            string matrixString = ""; for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 9; c++)
                { if (r == 2 && c == 8) { matrixString += $"{matrix[r, c]}"; } else { matrixString += $"{matrix[r, c]} - "; } }
            }   // with this operation it translates the matrix array into a simple string
            return matrixString;    // sending to archive
        }
        private int[,] GetArrayToMatrixString(List<int> matrix)
        {
            int[,] matrixToString = new int[3, 9];
            for (int i = 0; i < matrix.Count; i++)
            {
                if (i < 9) { matrixToString[0, i] = matrix[i]; }
                else if (i > 8 && i < 18) { matrixToString[1, i - 9] = matrix[i]; }
                else { matrixToString[2, i - 18] = matrix[i]; }
            }

            return matrixToString;
        }
        private string GetChosenNumbers(List<int> matrix)
        {
            string chosenNumbers = "";
            foreach (int number in matrix)
            {
                if (number != -1) { if (chosenNumbers == "") { chosenNumbers = number.ToString(); } else { chosenNumbers += $" - {number}"; } }
            }

            return chosenNumbers;
        }

        public List<int> GetMatrixInt(string matrix)
        {
            string[] numbersString = matrix.Split(" - "); List<int> numbers = new();
            foreach (string numberString in numbersString) { int number = int.Parse(numberString.Trim()); numbers.Add(number); }

            return numbers;
        }

        public List<BingoViewModel> GetBingoModelData(List<BingoViewModel> modelList)
        {
            for (int i = 0; i < modelList.Count; i++)
            {
                modelList[i].PlayerNumber = i + 1;
                modelList[i].MatrixModelString = GetMatrixString(GetArrayToMatrixString(modelList[i].MatrixModel));
                modelList[i].ChosenNumbers = GetChosenNumbers(modelList[i].MatrixModel);
            }
            return modelList;
        }
    }
}
