class View
{
    private readonly Column[] columns = { Column.A, Column.B, Column.C, Column.D, Column.E, Column.F, Column.G, Column.H, Column.I, Column.J };

    public void PrintBoard(IReadOnlyList<List<Cell>> board)
    {
        string boardAsString = BuildBoardAsString(board, GetCurrentCell);
        Console.WriteLine(boardAsString);
    }

    public void PrintBoardForPlacement(IReadOnlyList<List<Cell>> board)
    {
        string boardAsString = BuildBoardAsString(board, GetCurrentCellForPlacement);
        Console.WriteLine(boardAsString);
    }

    public void Print(String msg)
    {
        Console.WriteLine(msg);
    }

    private string BuildBoardAsString(IReadOnlyList<List<Cell>> board, Func<CellState, string> mapper)
    {
        string boardAsString = "  |" + String.Join("|", columns) + "|\n";
        for (int row = 0; row < board.Count; row++)
        {
            for (int col = 0; col < board.Count; col++)
            {
                boardAsString += col == 0 ? $"{BuildCurrentRowNumberString(row)}|" : "";
                Cell currentCell = board[row][col];
                boardAsString += mapper(currentCell.State);
            }
            boardAsString += "\n";
        }
        return boardAsString;
    }

    private string BuildCurrentRowNumberString(int row)
    {
        row++;
        if (row < 10)
        {
            return $"0{row}";
        }
        return row.ToString();
    }

    private string GetCurrentCell(CellState state) =>
      state switch
      {
          CellState.HIT => "X|",
          CellState.EMPTY => "O|",
          CellState.MISS => "-|",
          CellState.PLACED => "O|",
          _ => throw new ArgumentOutOfRangeException()
      };

    private string GetCurrentCellForPlacement(CellState state) =>
      state switch
      {
          CellState.PLACED => "S|",
          _ => GetCurrentCell(state)
      };

    public Position PromptForGuess(string order)
    {
        Position? result = null;
        Console.WriteLine(order);
        while (result is null)
        {
            string? promptForGuess = Console.ReadLine();
            try
            {
                result = GuessInBound(promptForGuess);
            }
            catch
            {
                Console.WriteLine("Wrong input!\nGuess again!\n");
            }
        }
        Position position = result.Value;
        return position;
    }

    public Direction PromptForDirection()
    {
        Console.WriteLine("Type H for horizontal or V for vertical direction.");
        string result = "";
        while (result == "")
        {
            try
            {
                string? input = Console.ReadLine();
                if (input is null)
                {
                    throw new ArgumentException("Only H or V is allowd");
                }
                string upperInput = input.ToUpper();
                if (upperInput != "V" && upperInput != "H")
                {
                    throw new ArgumentException("Only H or V is allowd");
                }
                result = upperInput;

            }
            catch
            {
                Console.WriteLine("Type H for horizontal or V for vertical direction.");
            }
        }
        Direction returnValue = result switch
        {
            "H" => Direction.HORIZONTAL,
            _ => Direction.VERTICAL

        };
        return returnValue;
    }

    private Position GuessInBound(string? guess)
    {
        if (guess is null)
        {
            throw new ArgumentException("Guess must be defined!");
        }
        if (guess.Length > 3)
        {
            throw new ArgumentException("Column must be between A and J!\nRow must be between 1 and 10!");
        }
        string[] chunks = [guess[..1], guess[1..]];
        Column col = MapStringToColumn(chunks[0].ToUpper());
        int row = Int32.Parse(chunks[1]);
        if (row > 10 || row < 1)
        {
            throw new ArgumentException("Row must be between 1 and 10!");
        }
        Position position = new Position(col, row - 1);
        return position;
    }

    public Column MapStringToColumn(string input) =>
      input switch
      {
          "A" => Column.A,
          "B" => Column.B,
          "C" => Column.C,
          "D" => Column.D,
          "E" => Column.E,
          "F" => Column.F,
          "G" => Column.G,
          "H" => Column.H,
          "I" => Column.I,
          "J" => Column.J,
          _ => throw new ArgumentException("Column must be between A and J!"),
      };

}
