class View
{
  private readonly string[] columns = {"A", "B", "C", "D", "E", "H", "I", "J", "K", "L"};
 
  public void PrintBoard(List<List<string>> board){
    string boardAsString = BuildBoardAsString(board);
    Console.WriteLine(boardAsString);
  }

  private string BuildBoardAsString(List<List<string>> board){
    string boardAsString = "  |" + String.Join("|", columns) + "|\n";
    for (int row = 0; row < board.Count; row++)
    {
      for (int col = 0; col < board.Count; col++)
      {
        boardAsString += col == 0 ? $"{BuildCurrentRowNumberString(row)}|" : "";
        string currentCell = board[row][col];
        switch(currentCell)
        {
          case "X":
            boardAsString += "X|";
            break;
          default:
            boardAsString += "O|";
            break;
        }
      }
      boardAsString += "\n";
    }
    return boardAsString;
  }

  private string BuildCurrentRowNumberString(int row){
    row++;
    if(row < 10){
      return $"0{row}";
    }
    return row.ToString();
  }

}
