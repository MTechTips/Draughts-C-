public enum PieceType
{
    Regular,
    King
}

public class Piece
{
    public PieceColor Color { get; set; }
    public PieceType Type { get; set; }

    // Additional properties for king status
    public bool IsKing => Type == PieceType.King;

    public Piece(PieceColor color, PieceType type = PieceType.Regular)
    {
        Color = color;
        Type = type;
    }

    // Method to promote a regular piece to a king
    public void PromoteToKing()
    {
        if (Type == PieceType.Regular)
        {
            Type = PieceType.King;
            // You might want to add additional logic for king promotion
        }
    }

    // Method to check if a piece can capture an opponent at a given destination
    public bool CanCapture(int toRow, int toCol, Piece[,] board)
    {
        // Implement your logic to check if the piece can capture an opponent
        // You might want to check the diagonal position and the presence of an opponent piece
        // Also, consider the rules for regular pieces and kings
        // For simplicity, allow any capture for now
        return true;
    }

    // Other methods and properties specific to the Piece class can be added as needed
}