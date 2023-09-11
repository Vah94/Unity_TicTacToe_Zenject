using System;
using Common.Utils;
using TicTacToe.Scripts.Utils;

namespace TicTacToe.Scripts.Board.Data
{
    [Serializable]
    public class BoardItemData
    {
        public Coordinates Coordinates;
        public BoardItemType BoardItemType;

        public BoardItemData(
            Coordinates coordinates,
            BoardItemType itemTypeValue)
        {
            Coordinates = coordinates;
            BoardItemType = itemTypeValue;
        }

        public override string ToString()
        {
            return $"{Coordinates} => {BoardItemType}";
        }
    }
}