class Game
{

    private Board board = new Board();
    private View view = new View();
    private Player? _playerOne;
    private Player? _playerTwo;


    public void Run()
    {
        view.Print("Game Begins...");
        StartGame();
    }

    public void StartGame()
    {
        _playerOne = new Player();
        _playerTwo = new Player();
        PlaceBoats(_playerOne);
        PlaceBoats(_playerTwo);
        RunGame(_playerOne, _playerTwo);
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

    private void RunGame(Player playerOne, Player playerTwo)
    {
        bool hasWinner = false;
        Player currentActivePlayer = playerOne;
        Player currentDefendingPlayer = playerTwo;

        while (!hasWinner)
        {
            RunTurn(currentActivePlayer, currentDefendingPlayer);
            hasWinner = currentDefendingPlayer.Ships.All(ship => ship.HasSunk());

            if (!hasWinner)
            {
                Player nextActive = currentDefendingPlayer;
                currentDefendingPlayer = currentActivePlayer;
                currentActivePlayer = nextActive;

            }
        }

        view.Print($"{currentActivePlayer} you won!");

    }

    private void RunTurn(Player guessingPlayer, Player defendingPlayer)
    {
        view.Print($"Player {guessingPlayer.ToString()}. Start your turn!");
        view.PrintBoard(defendingPlayer.board().GameBoard);
        bool isPlacementSuccess = false;
        Position position = new Position();
        while (!isPlacementSuccess)
        {
            position = view.PromptForGuess($"Player {guessingPlayer.ToString()} provide your guess.");
            isPlacementSuccess = defendingPlayer.board().IsValidHit(position);
            if (!isPlacementSuccess)
            {
                view.Print("Invalid Guess!\n Try Again!");

            }
        }
        defendingPlayer.board().MarkCellHit(position);
        view.PrintBoard(defendingPlayer.board().GameBoard);
    }

    private bool isValidPlacement()
    {
        return true;
    }

    private void MarkCell()
    {

    }

}
