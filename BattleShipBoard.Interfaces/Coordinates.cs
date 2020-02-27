using System;
using System.Collections.Generic;

namespace BattleShipBoard.Interfaces
{
    public class Coordinates
    {
        private static readonly List<string> Letters = new List<string>{"a", "b", "c", "d", "e", "f", "g", "h", "i", "j"};

        public Coordinates(int x, int y)
        {
            if (x < 0 || x > 9)
            {
                throw new ArgumentException($"Incorrect coordinates ({x}{y}). X coordinate is out of range.");
            }

            if (y < 0 || y > 9)
            {
                throw new ArgumentException($"Incorrect coordinates ({x}{y}). Y coordinate is out of range.");
            }

            X = x;
            Y = y;
        }

        public Coordinates(char x, int y)
        {
            if (!Letters.Contains(x.ToString().ToLower()))
            {
                throw new ArgumentException($"Incorrect coordinates ({x}{y}). X coordinate is out of range.");
            }

            if (y < 1 || y > 10)
            {
                throw new ArgumentException($"Incorrect coordinates ({x}{y}). Y coordinate is out of range.");
            }

            X = Letters.IndexOf(x.ToString().ToLower());
            Y = y-1;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"{Letters[X].ToUpper()}{Y+1}";
        }
    }
}