class Board 
{

  private List<List<string>> board;


  public Board(){
    GenerateBoard();
  }

  private void GenerateBoard(){
    List<List<string>> newBoard = new List<List<string>>();
    for (int i = 0; i < 10; i++){
     newBoard.Add(new List<string>());
     for (int j = 0; j < 10; j++){
      newBoard[i].Add("");
     }
    }
   board = newBoard;
  }

  public List<List<string>> ToString(){
    return board;
  }

}
