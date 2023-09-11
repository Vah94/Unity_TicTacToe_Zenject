using System;
using System.Linq;
using Common.Utils;
using TicTacToe.Scripts.Board.Data;
using TicTacToe.Scripts.Board.Settings;
using TicTacToe.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace TicTacToe.Scripts.Board
{
    public class BoardController : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] private RectTransform _container;
        private BoardItem[,] _boardItems;


        public IBoardItem[] AllBoardItems => _boardItems.Cast<IBoardItem>()
            .ToArray();

        private IBoardDataManager _boardDataManager;
        private IBoardStateManager _boardStateManager;
        private BoardSizeData _boardSizeData;
        private BoardItem.BoardItemFactory _boardItemFactory;

        [Inject]
        public void Construct(
            IBoardDataManager boardDataManager,
            IBoardStateManager boardStateManager,
            BoardSizeData boardSizeData,
            BoardItem.BoardItemFactory boardItemFactory)
        {
            _boardDataManager = boardDataManager;
            _boardStateManager = boardStateManager;
            _boardSizeData = boardSizeData;
            _boardItemFactory = boardItemFactory;
        }

        public void Initialize()
        {
            _boardItems = new BoardItem[_boardSizeData.Size, _boardSizeData.Size];
            for (var i = 0; i < _boardItems.GetLength(0); i++)
            {
                for (var j = 0; j < _boardItems.GetLength(1); j++)
                {
                    var item = _boardItemFactory.Create();
                    item.transform.SetParent(_container);
                    _boardItems[i, j] = item;
                    var coordinate = new Coordinates(i, j);
                    var itemData
                        = _boardDataManager.BoardData.BoardItemsData.FirstOrDefault(
                              itemData => itemData.Coordinates.Equals(coordinate))
                          ?? new BoardItemData(coordinate, BoardItemType.None);

                    item.Initialize(coordinate, itemData);
                }
            }

            _boardStateManager.OnBoardItemPressed.Subscribe(OnBoardItemPressed);
        }

        public void Dispose()
        {
            foreach (var item in _boardItems)
            {
                item.Dispose();
            }

            _boardStateManager.OnBoardItemPressed.Unsubscribe(OnBoardItemPressed);
        }

        private void OnBoardItemPressed(IBoardItem boardItem)
        {
            _boardDataManager.Save(AllBoardItems);
        }

        public IBoardItem GetBoardItem(Coordinates coordinates)
        {
            if (coordinates.X > _boardItems.GetLength(0)
                || coordinates.Y > _boardItems.GetLength(1))
            {
                return null;
            }

            return _boardItems[coordinates.X, coordinates.Y];
        }
    }
}