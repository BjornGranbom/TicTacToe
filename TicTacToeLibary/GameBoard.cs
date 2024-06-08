

using System.Drawing;

namespace TicTacToeLibary
{
    public class GameBoard
    {
        private readonly string[][] _status;
        private Random _random;

        public GameBoard()
        {
            _status = new string[3][];
            for (int i = 0; i < 3; i++)
            {
                _status[i] = new string[3];
            }
            _random= new Random((int)DateTime.Now.Ticks);
        }

        public string CheckForWinner()
        {
            for (int i=0;i<3;i++)
            {
                if (_status[i][0] != "?" && _status[i][0] == _status[i][1] && _status[i][0] == _status[i][2])
                {
                    return _status[i][0];
                }
                if (_status[0][i] != "?" && _status[0][i] == _status[1][i] && _status[0][i] == _status[2][i])
                {
                    return _status[0][i];
                }
            }
            if (_status[0][0] != "?" && _status[0][0] == _status[1][1] && _status[0][0] == _status[2][2]) 
            {
                return _status[0][0];
            }
            if (_status[0][2] != "?" && _status[0][2] == _status[1][1] && _status[0][2] == _status[2][0]) 
            {
                return _status[0][2];
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_status[i][j] == "?") return "";
                }
            }
            return "?";//Draw
        }

        public Point ComputerTurn()
        {
            var points= new List<Point>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_status[i][j] == "?")
                    {
                        points.Add(new Point(i, j));
                    }
                }
            }
            var pos = _random.Next(points.Count);
            Point point = points[pos];
            _status[point.X][point.Y] = "X";
            return point;
        }

        public void SetStatus(int i, int j, string text)
        {
            _status[i][j] = text;
        }
    }
}
