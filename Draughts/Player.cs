public class Player
{
    public string Name { get; }
    public PieceColor Color { get; }
    public Player Opponent { get; set; }
    public List<Piece> Pieces { get; }

    public Player(string name, PieceColor color)
    {
        Name = name;
        Color = color;
        Pieces = new List<Piece>();
    }

    // Add a piece to the player's collection
    public void AddPiece(Piece piece)
    {
        Pieces.Add(piece);
    }

    // Remove a piece from the player's collection
    public void RemovePiece(Piece piece)
    {
        Pieces.Remove(piece);
    }

    // Check if the player has any remaining pieces
    public bool HasRemainingPieces()
    {
        return Pieces.Count > 0;
    }

    // Other methods and properties specific to the Player class can be added as needed
}