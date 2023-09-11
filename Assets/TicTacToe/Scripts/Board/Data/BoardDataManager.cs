using System.Linq;
using Common.Data.SaveSystem;
using UnityEngine;

namespace TicTacToe.Scripts.Board.Data
{
    public interface IBoardDataManager
    {
        BoardData BoardData { get; }
        void Save(IBoardItem[] boardItems);
    }

    public class BoardDataManager : IBoardDataManager
    {
        public BoardData BoardData { private set; get; }

        public BoardDataManager()
        {
            Load();
        }

        public void Save(IBoardItem[] boardItems)
        {
            BoardData.BoardItemsData = boardItems.Select(item => item.GetData())
                .ToArray();
            var json = JsonUtility.ToJson(BoardData);

            CommonPlayerPrefs.SetString(nameof(BoardData), json);
        }

        public void Load()
        {
            var json = CommonPlayerPrefs.GetString(nameof(BoardData));
            BoardData = string.IsNullOrEmpty(json) ? new BoardData() : JsonUtility.FromJson<BoardData>(json);
        }
    }
}