class Game
{

    private Board board = new Board();
    private View view = new View();
    private Player? _playerOne;
    private Player? _playerTwo;


    public void Run()
    {
        Console.WriteLine("BOARD");
        view.PrintBoard(board.GameBoard);
        StartGame();
    }

    public void StartGame()
    {
        _playerOne = new Player();
        _playerTwo = new Player();
        PlaceBoats(_playerOne);
        PlaceBoats(_playerTwo);
    }

    private void PlaceBoats(Player player)
    {
        view.PrintBoardForPlacement(player.board().GameBoard);
        var ships = player.Ships;
        foreach (var ship in ships)
        {
            PlaceBoat(player, ship);
            view.PrintBoardForPlacement(player.board().GameBoard);
        }
        ;
    }

    private void PlaceBoat(Player player, Ship ship)
    {
        bool isPlacementSuccess = false;
        Position position = new Position();
        Direction direction = Direction.VERTICAL;
        while (!isPlacementSuccess)
        {
            position = view.PromptForGuess($"Player {player.ToString()} place your boat");
            direction = view.PromptForDirection();
            isPlacementSuccess = player.board().IsValidPlacement(position, ship.GetSize(), direction);
            if (!isPlacementSuccess)
            {
                view.Print("Invalid Placement!\n Try Again!");

            }
        }
        List<Cell> cells = player.board().PlaceBoat(position, ship.GetSize(), direction);
        ship.PlaceShip(cells);
    }

    private bool isValidPlacement()
    {
        return true;
    }

    private void MarkCell()
    {

    }

}
