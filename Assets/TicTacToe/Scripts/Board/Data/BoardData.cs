using System;

namespace TicTacToe.Scripts.Board.Data
{
    [Serializable]
    public class BoardData
    {
        public BoardItemData[] BoardItemsData = Array.Empty<BoardItemData>();

        public BoardData()
        {
        }
    }
}