// Form1.cs
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Windows.Forms;

namespace CheckersFormsApp
{
    public enum Player
    {
        x,
        o
    }
    public partial class Form1 : Form
    {
        private int selectedRow = -1;
        private int selectedCol = -1;
        private Player CurrentPlayer = Player.x;
        private const int ButtonSize = 40;
        private const int BoardSize = 8;
        private CheckersGame checkersGame;
        private List<List<string>> Board;

        public Form1()
        {
            InitializeComponent();
            checkersGame = new CheckersGame();
            InitializeBoard();
            InitializeBoardButtons();
            SetFormSize();
        }

        private void InitializeBoard()
        {
            // Initialization of a 2D list representing the game board
            Board = new List<List<string>>();
            for (int i = 0; i < 8; i++)
            {
                List<string> row = new List<string>();
                for (int j = 0; j < 8; j++)
                {
                    row.Add(" ");
                }

                Board.Add(row);
            }

            Board[0][1] = "x";
            Board[0][3] = "x";
            Board[0][5] = "x";
            Board[0][7] = "x";
            Board[1][0] = "x";
            Board[1][2] = "x";
            Board[1][4] = "x";
            Board[1][6] = "x";
            Board[2][1] = "x";
            Board[2][3] = "x";
            Board[2][5] = "x";
            Board[2][7] = "x";
            Board[5][0] = "o";
            Board[5][2] = "o";
            Board[5][4] = "o";
            Board[5][6] = "o";
            Board[6][1] = "o";
            Board[6][3] = "o";
            Board[6][5] = "o";
            Board[6][7] = "o";
            Board[7][0] = "o";
            Board[7][2] = "o";
            Board[7][4] = "o";
            Board[7][6] = "o";
        }

        private void InitializeBoardButtons()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int row = i;
                    int col = j;

                    Button button = new Button();
                    button.Width = 40;
                    button.Height = 40;
                    button.Top = i * 40;
                    button.Left = j * 40;
                    button.Tag = i * BoardSize + j;
                    string symbol = Board[i][j];
                    button.Text = symbol;

                    button.Click += (sender, e) => HandleButtonClick(row, col);
                    Controls.Add(button);
                }
            }
        }

        private void HandleButtonClick(int row, int col)
        {
            // Assuming Board is a List<List<string>> or similar

            if (selectedRow == -1 && selectedCol == -1)
            {
                if (IsValidPieceToSelect(row, col))
                {
                    selectedRow = row;
                    selectedCol = col;
                    Console.WriteLine($"Piece selected: Row {selectedRow}, Col {selectedCol}");
                }
                else
                {
                    MessageBox.Show("Invalid piece to select!", "Error");
                }
            }
            else
            {
                // Second click, perform the move logic
                PerformMove(selectedRow, selectedCol, row, col);

                // Reset selected position for the next move
                selectedRow = -1;
                selectedCol = -1;

                //switch to next player
                SwitchPlayers();
            }
        }

        private bool IsOpponentPiece(string piece1, string piece2)
        {
            // Assuming "X" and "O" represent the two players
            return (piece1 == "x" && piece2 == "o") || (piece1 == "o" && piece2 == "x");
        }

        private bool IsValidPieceToSelect(int row, int col)
        {
            // Check if the position is within the board bounds
            if (row < 0 || row >= BoardSize || col < 0 || col >= BoardSize)
            {
                return false;
            }

            // Check if the position has a non-empty piece that belongs to the current player
            string piece = Board[row][col];

            // Assuming "X" and "O" represent the two players
            if ((piece == "x" && CurrentPlayer == Player.x) || (piece == "o" && CurrentPlayer == Player.o))
            {
                return true;
            }
            if ((piece == "X" && CurrentPlayer == Player.x) || (piece == "O" && CurrentPlayer == Player.o))
            {
                return true;
            }
            return false;
        }
        private bool IsValidMove(int fromRow, int fromCol, int toRow, int toCol, string piece)
        {
            // Check if the destination is within the board bounds
            if (toRow < 0 || toRow >= 8 || toCol < 0 || toCol >= 8)
            {
                return false;
            }
            
            // Check if the destination is empty
            if (Board[toRow][toCol] != " ")
            {
                return false;
            }

            if (Math.Abs(toRow - fromRow) == 1 && Math.Abs(toCol - fromCol) == 1)
            {
                // Regular move is valid
                //checks not backwards
                if (!isKing(piece) && (piece == "x" && toRow - fromRow < 0 || piece == "o" && fromRow - toRow < 0))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            // Check if it's a capture move (jump)
            if (Math.Abs(toRow - fromRow) == 2 && Math.Abs(toCol - fromCol) == 2)
            {
                if (!isKing(piece) && (piece == "x" && toRow - fromRow < 0 || piece == "o" && fromRow - toRow < 0))
                {
                    return false;
                }
                else
                {
                    // Calculate the position of the captured piece
                    int capturedRow = (toRow + fromRow) / 2;
                    int capturedCol = (toCol + fromCol) / 2;

                    // Check if there is an opponent's piece to capture
                    string capturedPiece = Board[capturedRow][capturedCol];
                    if (IsOpponentPiece(capturedPiece, piece))
                    {
                        // Capture move is valid
                        return true;
                    }
                    
                }
                
            }

            return false; // If none of the conditions are met, the move is invalid
        }

        private bool isKing(string piece)
        {
            char character = char.Parse(piece);
            if (char.IsLower(character) == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void PerformMove(int fromRow, int fromCol, int toRow, int toCol)
        {
            string piece = Board[fromRow][fromCol];

            // Check if the move is valid based on your game rules
            if (IsValidMove(fromRow, fromCol, toRow, toCol, piece))
            {
                // Perform the move
                Board[toRow][toCol] = piece;
                Board[fromRow][fromCol] = " "; // Assuming an empty space after moving

                // Check for capturing and additional logic
                if (Math.Abs(toRow - fromRow) == 2 && Math.Abs(toCol - fromCol) == 2)
                {
                    // Capture move: Remove the captured piece
                    int capturedRow = (toRow + fromRow) / 2;
                    int capturedCol = (toCol + fromCol) / 2;
                    Board[capturedRow][capturedCol] = " ";
                }
                if ((piece == "x" && toRow == 8 - 1) || (piece == "o" && toRow == 0))
                {
                    // Convert the piece to a king
                    Board[toRow][toCol] = piece.ToUpper();
                }
                // Update UI based on the game state
                UpdateUI(toRow, toCol, piece);
            }
            else
            {
                // Handle invalid move (e.g., show a message to the player)
                Console.WriteLine("Invalid move!");
            }
        }

        private void UpdateUI(int toRow, int toCol, string piece)
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    Button button = GetButton(i, j);
                    if (button != null)
                    {
                        button.Text = Board[i][j];
                    }
                }
            }
        }
        private Button GetButton(int row, int col)
        {
            foreach (Control control in Controls)
            {
                if (control is Button button && (int)button.Tag == row * 8 + col)
                {
                    return button;
                }
            }
            return null;
        }

        private void SetFormSize()
        {
            int formWidth = 8 * 40;
            int formHeight = 8 * 40;

            MaximizeBox = false;
            ClientSize = new Size(formWidth, formHeight);
            FormBorderStyle = FormBorderStyle.FixedSingle; // Prevent resizing
        }
        private void SwitchPlayers()
        {
            CurrentPlayer = (CurrentPlayer == Player.x) ? Player.o : Player.x;
        }


    }
}
