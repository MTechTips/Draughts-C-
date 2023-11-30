public class UI
{
    private Game game;

    public UI(Game game)
    {
        this.game = game;
    }

    public void RunGame()
    {
        Console.WriteLine("Welcome to Checkers!");

        while (!game.IsGameOver)
        {
            DisplayBoard();
            Console.WriteLine($"{game.CurrentPlayer.Name}'s turn");

            // Get and process the player's move
            bool validMove = false;
            while (!validMove)
            {
                Console.Write("Enter your move (e.g., A3 to B4): ");
                string move = Console.ReadLine();
                validMove = game.MakeMove(move);

                if (!validMove)
                    Console.WriteLine("Invalid move. Try again.");
            }

            // Check for win conditions
            if (!game.CurrentPlayer.Opponent.HasRemainingPieces())
            {
                game.IsGameOver = true;
            }

            // Switch to the next player
            game.SwitchPlayers();
        }

        DisplayResult();
    }

    private void DisplayBoard()
    {
        Console.Clear();
        game.Board.DisplayBoard();
    }

    private void DisplayResult()
    {
        Console.WriteLine($"Game over! {game.Winner.Name} wins!");
    }
}