using System;

public class Game
{
    public Board Board { get; private set; }
    public Player CurrentPlayer { get; private set; }
    public bool IsGameOver { get; set; }
    public Player Winner { get; private set; }

    public Game()
    {
        // Initialize the board and players
        Board = new Board();
        Player player1 = new Player("Player 1", PieceColor.Black);
        Player player2 = new Player("Player 2", PieceColor.White);
        player1.Opponent = player2;
        player2.Opponent = player1;

        // Assign the starting player
        CurrentPlayer = player1;
    }

    public void StartGame()
    {
        Console.WriteLine("Welcome to Checkers!");
        while (!IsGameOver)
        {
            DisplayBoard();
            Console.WriteLine($"{CurrentPlayer.Name}'s turn");

            // Get and process the player's move
            bool validMove = false;
            while (!validMove)
            {
                Console.Write("Enter your move (e.g., A3 to B4): ");
                string move = Console.ReadLine();
                validMove = MakeMove(move);

                if (!validMove)
                    Console.WriteLine("Invalid move. Try again.");
            }

            // Check for win conditions
            if (!CurrentPlayer.Opponent.HasRemainingPieces())
            {
                IsGameOver = true;
                Winner = CurrentPlayer;
            }

            // Switch to the next player
            SwitchPlayers();
        }

        DisplayResult();
    }

    private void DisplayBoard()
    {
        Board.DisplayBoard();
    }

    public bool MakeMove(string move)
    {
        // Implement move validation and update board
        // Return true if the move is valid, otherwise false
        return Board.MakeMove(move, CurrentPlayer.Color);
    }

    public void SwitchPlayers()
    {
        CurrentPlayer = CurrentPlayer.Opponent;
    }

    private void DisplayResult()
    {
        Console.WriteLine($"Game over! {Winner.Name} wins!");
    }
}
