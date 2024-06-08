using System.Drawing.Printing;
using TicTacToeLibary;

namespace TicTacToeApplication
{
    public partial class TicTacToeApplication : Form
    {
        private Button[][]? _gameButtons;
        private GameBoard _gameBoard;

        public TicTacToeApplication()
        {
            InitializeComponent();
            _gameButtons = null;
            CreateNewGame();
        }


        private void CreateNewGame()
        {
            if (_gameButtons != null)
            {
                foreach (var buttonRow in _gameButtons)
                {
                    foreach (var button in buttonRow)
                    {
                        button.Visible = false;
                        Controls.Remove(button);
                    }
                }
            }
            _gameButtons = new Button[3][];
            for (int i = 0; i < _gameButtons.Length; i++)
            {
                _gameButtons[i] = new Button[3];
                for (int j = 0; j < _gameButtons[i].Length; j++)
                {
                    _gameButtons[i][j] = new Button();
                }
            }
            for (int i = 0; i < _gameButtons.Length; i++)
            {
                for (int j = 0; j < _gameButtons[i].Length; j++)
                {
                    var button = _gameButtons[i][j];
                    button.Text = "?";
                    button.Visible = true;
                    button.Enabled = true;
                    SetButtonSize(button, i, j);
                    button.Click += ButtonClick;
                    button.BackColor = Color.White;
                    Controls.Add(button);
                }
            }
        }

        private void SetButtonSize(Button button, int i, int j)
        {
            int startPosRow = 25;
            int margin = 5;
            int diffRow = (this.Width - 2 * startPosRow - 2 * margin) / 3;
            int startPosColumn = 50;
            int diffColumn = (this.Height - startPosColumn - startPosRow - 20 - 2 * margin) / 3;
            button.Size = new Size(width: diffRow - margin, height: diffColumn - margin);
            button.Location = new Point(x: startPosRow + i * diffRow, y: startPosColumn + j * diffColumn);
            if (button.Width < button.Height)
            {
                button.Font = new Font("Segoe UI", button.Width / 2 - 10, FontStyle.Regular, GraphicsUnit.Point, 0);
            }
            else
            {
                button.Font = new Font("Segoe UI", button.Height / 2 - 10, FontStyle.Regular, GraphicsUnit.Point, 0);
            }
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.Text = "O";
            button.Enabled = false;

            CreateGameBoard();
            bool winner=CheckForWinner();
            if (!winner)
            {
                ComputerTurn();
            }
        }

        private void CreateGameBoard()
        {
            _gameBoard = new GameBoard();
            for (int i = 0; i < _gameButtons.Length; i++)
            {
                for (int j = 0; j < _gameButtons[i].Length; j++)
                {
                    _gameBoard.SetStatus(i,j, _gameButtons[i][j].Text);
                }
            }
        }

        private bool CheckForWinner()
        {
            string winner = _gameBoard.CheckForWinner();
            switch (winner)
            {
                case "X":
                    MessageBox.Show("Computer wins","WINNER");
                    CreateNewGame();
                    return true;
                case "O":
                    MessageBox.Show("Player wins", "WINNER");
                    CreateNewGame();
                    return true;
                case "?":
                    MessageBox.Show("It's a draw","DRAW");
                    CreateNewGame();
                    return true;
                default:
                    return false;
            }
        }

        private void ComputerTurn()
        {
            Point point = _gameBoard.ComputerTurn();
            var button = _gameButtons[point.X][point.Y];
            button.Text = "X";
            button.Enabled = false;
            CheckForWinner();
        }

        private void TicTacToeApplication_ResizeEnd(object sender, EventArgs e)
        {
            if (_gameButtons!=null)
            {
                for (int i = 0; i < _gameButtons.Length; i++)
                {
                    for (int j = 0; j < _gameButtons[i].Length; j++)
                    {
                        SetButtonSize(_gameButtons[i][j], i, j);
                    }
                }
            }
        }
    }
}
