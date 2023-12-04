using System;

public enum PieceColor
{
    None,
    Black,
    White
}

public class Piece
{
    public PieceColor Color { get; set; }
    // You might want to include additional properties for king status, etc.
}

public class Board
{
    public Piece[,] Pieces { get; private set; }

    public Board()
    {
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        Pieces = new Piece[8, 8];

        // Initialize pieces for player 1 (Black)
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 8; col += 2)
            {
                if (row % 2 == 0)
                    Pieces[row, col + 1] = new Piece { Color = PieceColor.Black };
                else
                    Pieces[row, col] = new Piece { Color = PieceColor.Black };
            }
        }

        // Initialize pieces for player 2 (White)
        for (int row = 5; row < 8; row++)
        {
            for (int col = 0; col < 8; col += 2)
            {
                if (row % 2 == 0)
                    Pieces[row, col + 1] = new Piece { Color = PieceColor.White };
                else
                    Pieces[row, col] = new Piece { Color = PieceColor.White };
            }
        }
    }

    public void DisplayBoard()
    {
        Console.Clear();
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                if (Pieces[row, col] == null)
                    Console.Write(" - ");
                else
                    Console.Write(Pieces[row, col].Color == PieceColor.Black ? " B " : " W ");
            }
            Console.WriteLine();
        }
    }

    public void MovePiece(string from, string to)
    {
        // Convert string coordinates to array indices
        int fromRow = 8 - int.Parse(from[1].ToString());
        int fromCol = (int)from[0] - 'A';

        int toRow = 8 - int.Parse(to[1].ToString());
        int toCol = (int)to[0] - 'A';

        // Validate the move
        if (IsValidMove(fromRow, fromCol, toRow, toCol))
        {
            // Perform the move
            Pieces[toRow, toCol] = Pieces[fromRow, fromCol];
            Pieces[fromRow, fromCol] = null;
        }
    }

    private bool IsValidMove(int fromRow, int fromCol, int toRow, int toCol)
    {
        // Implement your move validation logic here
        // For simplicity, allow any move for now
        return true;
    }

    internal bool MakeMove(string move, PieceColor color)
    {
        throw new NotImplementedException();
    }
}
