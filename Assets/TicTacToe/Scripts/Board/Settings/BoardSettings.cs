using System;

namespace TicTacToe.Scripts.Board.Settings
{
    [Serializable]
    public class BoardSettings
    {
        public BoardSizeData BoardSizeData;
        public BoardItem BoardItemPrefab;
    }

    [Serializable]
    public class BoardSizeData
    {
        public int Size = 3;
    }
}