class Game {

  private Board board = new Board();
  private View view = new View();


  public void Run(){
    Console.WriteLine("BOARD");
    view.PrintBoard(board.ToString()); 
  }

}
