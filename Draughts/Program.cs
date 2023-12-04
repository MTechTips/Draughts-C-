using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to Checkers!");

        // Initialize the game
        Game checkersGame = new Game();

        // Main game loop
        while (!checkersGame.IsGameOver)
        {
            // Display the current state of the board
            DisplayBoard(checkersGame.Board);

            // Get the current player's move
            Console.WriteLine($"Player {checkersGame.CurrentPlayer}'s turn");
            Console.WriteLine("Enter your move (e.g., A3 to B4): ");
            string move = Console.ReadLine();

            // Process the move
            bool isValidMove = checkersGame.MakeMove(move);

            if (!isValidMove)
            {
                Console.WriteLine("Invalid move. Try again.");
            }
        }

        // Display the winner
        Console.WriteLine($"Player {checkersGame.Winner} wins!");

        Console.WriteLine("Thanks for playing Checkers!");
    }

    static void DisplayBoard(Board board)
    {
        // Implement logic to display the current state of the board
    }
}


