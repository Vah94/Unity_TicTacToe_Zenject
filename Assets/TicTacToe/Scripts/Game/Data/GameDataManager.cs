using Common.Data.SaveSystem;
using TicTacToe.Scripts.Utils;
using UnityEngine;

namespace TicTacToe.Scripts.Game.Data

{
    public interface IGameDataManager
    {
        GameData GameData { get; }
        void Save(BoardItemType boardItemType);
    }

    public class GameDataManager : IGameDataManager
    {
        public GameData GameData { private set; get; }

        public GameDataManager()
        {
            Load();
        }

        public void Save(BoardItemType boardItemType)
        {
            GameData.BoardItemType = boardItemType;
            var json = JsonUtility.ToJson(GameData);

            CommonPlayerPrefs.SetString(nameof(GameData), json);
        }

        public void Load()
        {
            var json = CommonPlayerPrefs.GetString(nameof(GameData));
            GameData = string.IsNullOrEmpty(json) ? new GameData() : JsonUtility.FromJson<GameData>(json);
        }
    }
}