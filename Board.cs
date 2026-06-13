class Board
{

    private List<List<Cell>> board = new List<List<Cell>>();


    public Board()
    {
        GenerateBoard();
    }

    public IReadOnlyList<List<Cell>> GameBoard => board;


    private void GenerateBoard()
    {
        for (int i = 0; i < 10; i++)
        {
            board.Add(new List<Cell>());
            for (int j = 0; j < 10; j++)
            {
                Cell cell = new Cell(CellState.EMPTY);
                board[i].Add(cell);
            }
        }
    }

    public void MarkCell(Position position, CellState state)
    {
        var (col, row) = position;
        int column = (int)col;
        Cell cell = board[row][column];
        cell.State = state;
    }

    public void MarkCellHit(Position position)
    {
        var (col, row) = position;
        int column = (int)col;
        Cell cell = board[row][column];
        var isCellHit = cell.State == CellState.PLACED;
        var newCellState = isCellHit ? CellState.HIT : CellState.MISS;
        cell.State = newCellState;
    }

    private bool IsCellOccupied(int col, int row)
    {
        Cell cell = board[row][col];
        return cell.State != CellState.EMPTY;
    }

    private bool IsCellValidHit(int col, int row)
    {
        Cell cell = board[row][col];
        return cell.State == CellState.EMPTY || cell.State == CellState.PLACED;
    }

    private bool IsCellHit(int col, int row)
    {
        Cell cell = board[row][col];
        return cell.State == CellState.PLACED;
    }


    private bool IsInBoardBounds(Position position)
    {
        var (col, row) = position;
        if (col < 0 || row < 0)
        {
            return false;
        }
        int colNum = (int)col;
        return colNum <= board.Count() && row <= board.Count();
    }

    public bool IsValidHit(Position position)
    {
        var (col, row) = position;
        var column = (int)col;

        return IsCellValidHit(column, row);

    }

    public bool IsValidPlacement(Position position, int size, Direction direction)
    {
        for (int i = 0; i < size; i++)
        {
            var (col, row) = GetCoordinates(position, direction, i);
            if (IsCellOccupied(col, row))
            {
                return false;
            }
        }
        return true;
    }

    public List<Cell> PlaceBoat(Position position, int size, Direction direction)
    {
        List<Cell> cells = new List<Cell>();
        for (int i = 0; i < size; i++)
        {
            var (col, row) = GetCoordinates(position, direction, i);
            board[row][col].State = CellState.PLACED;
            cells.Add(board[row][col]);
        }
        return cells;
    }

    private (int, int) GetCoordinates(Position position, Direction direction, int increment)
    {
        var (col, row) = position;
        var column = (int)col;
        var colAsArg = direction == Direction.HORIZONTAL ? column + increment : column;
        var rowAsArg = direction == Direction.HORIZONTAL ? row : row + increment;
        return (colAsArg, rowAsArg);
    }


}
