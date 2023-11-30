using System;

class Program
{
    static void Main()
    {
        Game checkersGame = new Game();
        UI consoleUI = new UI(checkersGame);

        consoleUI.RunGame();
    }
}
