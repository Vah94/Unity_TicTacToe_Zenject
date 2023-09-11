using System;

namespace Common.Utils
{
    [Serializable]
    public class Coordinates
    {
        public int X;
        public int Y;

        public Coordinates()
        {
        }

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Coordinates other)
        {
            return X == other.X
                   && Y == other.Y;
        }

        public override string ToString()
        {
            return $"{X}:{Y}";
        }
    }
}