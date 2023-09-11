using System;
using Common.Utils;
using TicTacToe.Scripts.Board.Data;
using TicTacToe.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TicTacToe.Scripts.Board
{
    public interface IBoardItem
    {
        Coordinates Coordinates { get; }
        IUpdatingValue<BoardItemType> ItemType { get; }
        void SetWinState(bool state);

        BoardItemData GetData();
    }

    public class BoardItem :
        MonoBehaviour,
        IBoardItem
    {
        [SerializeField] private TextMeshProUGUI _valueText;
        [SerializeField] private Button _innerButton;

        private readonly UpdatingValue<BoardItemType> _itemType = new UpdatingValue<BoardItemType>
            { Value = BoardItemType.None };

        private IGameStateManager _gameStateManager;
        private BoardStateManager _boardStateManager;

        public IUpdatingValue<BoardItemType> ItemType => _itemType;

        public Coordinates Coordinates { private set; get; }

        [Inject]
        public void Construct(
            IGameStateManager gameStateManager,
            BoardStateManager boardStateManager)
        {
            _gameStateManager = gameStateManager;
            _boardStateManager = boardStateManager;
        }

        public void Initialize(
            Coordinates coordinates,
            BoardItemData boardItemData)
        {
            _itemType.Value = boardItemData.BoardItemType;
            Coordinates = coordinates;
            SetWinState(false);
            _innerButton.onClick.RemoveAllListeners();
            _innerButton.onClick.AddListener(
                () =>
                {
                    if (!Equals(_itemType.Value, BoardItemType.None))
                    {
                        return;
                    }

                    _itemType.Value = _gameStateManager.CurrentUserType.Value;
                    _boardStateManager.OnBoardItemPressedLocal.Invoke(this);
                });
            _itemType.Subscribe(CheckView);
            CheckView();
        }

        public void Dispose()
        {
            _itemType.Unsubscribe(CheckView);
        }


        private void CheckView()
        {
            _valueText.text = _itemType.Value switch
            {
                BoardItemType.None => "",
                BoardItemType.X => "X",
                BoardItemType.O => "O",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void SetWinState(bool state)
        {
            GetComponent<Image>()
                .color = state ? Color.green : Color.white;
        }

        public BoardItemData GetData()
        {
            return new BoardItemData(Coordinates, _itemType.Value);
        }

        public class BoardItemFactory : PlaceholderFactory<BoardItem>
        {
        }
    }
}