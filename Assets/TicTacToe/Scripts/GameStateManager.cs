using System;
using Common.Utils;
using TicTacToe.Scripts.Game.Data;
using TicTacToe.Scripts.Utils;
using Zenject;
using Random = UnityEngine.Random;

namespace TicTacToe.Scripts
{
    public interface IGameStateManager
    {
        IUpdatingValue<BoardItemType> CurrentUserType { get; }
    }

    public class GameStateManager : IGameStateManager, IInitializable, IDisposable
    {
        private readonly UpdatingValue<BoardItemType> _currentUserType = new UpdatingValue<BoardItemType>();
        private IGameDataManager _gameDataManager;
        public IUpdatingValue<BoardItemType> CurrentUserType => _currentUserType;

        [Inject]
        public void Construct(IGameDataManager gameDataManager)
        {
            _gameDataManager = gameDataManager;
        }

        public void Initialize()
        {
            var savedState = _gameDataManager.GameData.BoardItemType;
            _currentUserType.Value = Equals(savedState, BoardItemType.None)
                ? Random.Range(0f, 1f) > 0.5f ? BoardItemType.X : BoardItemType.O
                : savedState;
        }

        public void Dispose()
        {
        }

        public void ChangeType()
        {
            _currentUserType.Value = !Equals(_currentUserType.Value, BoardItemType.X)
                ? BoardItemType.X
                : BoardItemType.O;

            _gameDataManager.Save(_currentUserType.Value);
        }
    }
}