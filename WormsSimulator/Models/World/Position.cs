using System;
using System.Drawing;

namespace CS_lab.Models
{
    public class Position
    {
        private readonly int _x;
        private readonly int _y;
        public int X { get => _x; }

        public int Y { get => _y; }

        public Position(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public Position DirectedPosition(Direction direction)
        {
            var newX = _x;
            var newY = _y;

            switch (direction)
            {
                case Direction.Up:
                    newY++;
                    break;
                case Direction.Down:
                    newY--;
                    break;
                case Direction.Left:
                    newX--;
                    break;
                case Direction.Right:
                    newX++;
                    break;
                case Direction.Nothing:
                    break;
            }

            var newPosition = new Position(newX, newY);

            return newPosition;
        }
        
        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !(GetType() == obj.GetType()))
            {
                return false;
            }
            else {
                Position p = (Position) obj;
                return (_x == p._x) && (_y == p._y);
            }
        }

        public override int GetHashCode()
        {
            return (_x << 2) ^ _y;
        }

        public override string ToString()
        {
            return $"Position({_x}, {_y})";
        }
    }
}