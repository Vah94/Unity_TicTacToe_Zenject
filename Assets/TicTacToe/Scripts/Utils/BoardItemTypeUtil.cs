using System;

namespace TicTacToe.Scripts.Utils
{
    public enum BoardItemType
    {
        None,
        X,
        O
    }

    public static class BoardItemTypeUtil
    {
        public static BoardItemType[] All => (BoardItemType[])Enum.GetValues(typeof(BoardItemType));
    }
}