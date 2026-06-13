class Ship(int size)
{
    public static readonly int MaxShipSize = 4;
    private List<Cell> tiles = new List<Cell>();

    public void PlaceShip(List<Cell> cells)
    {
        if (cells.Count() != size)
        {

            throw new ArgumentException("Ship size does not match!");
        }
        for (int i = 0; i < size; i++)
        {
            tiles.Add(cells[i]);
        }
    }

    public bool HasSunk()
    {
        return tiles.All(tile => tile.State == CellState.HIT);
    }

    public bool isShipPlaced()
    {
        return tiles.Count() == size;
    }

    public int GetSize()
    {
        return size;
    }
}
