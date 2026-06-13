class Player
{
    static int playerNumber = 0;
    private Board _board = new Board();
    public int PlayerNumber { get; }
    private List<Ship> ships = new List<Ship>();
    public IReadOnlyList<Ship> Ships => ships;

    public Player()
    {
        playerNumber++;
        PlayerNumber = playerNumber;
        GenerateShips();
    }

    private void GenerateShips()
    {
        int shipNumber = Ship.MaxShipSize;
        for (int i = 1; i <= shipNumber; i++)
        {
            ships.Add(new Ship(i));
        }
    }

    public Board board()
    {
        return _board;
    }

    public override string ToString()
    {
        return $"Player {playerNumber}";
    }

}
