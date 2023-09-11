using System;
using System.Linq;
using TicTacToe.Scripts.Board;
using TicTacToe.Scripts.UI;
using TicTacToe.Scripts.Utils;
using TicTacToe.Scripts.WinLine;
using Zenject;

namespace TicTacToe.Scripts.Game
{
    public class GameManager : IInitializable, IDisposable
    {
        private GameStateManager _gameStateManager;
        private IBoardStateManager _boardStateManager;
        private BoardController _boardController;
        private WinLineManager _winLineManager;
        private Hud _hud;
        private GameEndPanel _gameEndPanel;

        [Inject]
        public void Construct(
            GameStateManager gameStateManager,
            IBoardStateManager boardStateManager,
            BoardController boardController,
            WinLineManager winLineManager,
            Hud hud,
            GameEndPanel gameEndPanel)
        {
            _gameStateManager = gameStateManager;
            _boardStateManager = boardStateManager;
            _boardController = boardController;
            _winLineManager = winLineManager;
            _hud = hud;
            _gameEndPanel = gameEndPanel;
        }

        public void Initialize()
        {
            CheckResult();
            _boardStateManager.OnBoardItemPressed.Subscribe(OnBoardItemPressed);
        }


        public void Dispose()
        {
            _hud.Dispose();
            _boardStateManager.OnBoardItemPressed.Unsubscribe(OnBoardItemPressed);
        }


        private void OnBoardItemPressed(IBoardItem boardItem)
        {
            _gameStateManager.ChangeType();
            CheckResult(boardItem);
        }

        private void CheckResult(IBoardItem targetItem = null)
        {
            var result = targetItem == null
                ? BoardItemType.None
                : _winLineManager.GetResult(targetItem);
            if (!Equals(result, BoardItemType.None))
            {
                _hud.SetMessage($"Congratulations {result.ToString()}");
                _gameEndPanel.SetMessage($"{result.ToString()} is win!");
            }
            else
            {
                if (CheckDeadlock(_boardController.AllBoardItems))
                {
                    _hud.SetMessage("!!!");
                    _gameEndPanel.SetMessage("Deadlock");
                }
                else
                {
                    _hud.SetMessage($"{_gameStateManager.CurrentUserType.Value} turn");
                }
            }
        }

        private bool CheckDeadlock(IBoardItem[] boardItems)
        {
            return !boardItems.Any(item => Equals(item.ItemType.Value, BoardItemType.None));
        }
    }
}