using System;
using System.Collections.Generic;
using System.Linq;
using Common.Utils;
using TicTacToe.Scripts.Board;
using TicTacToe.Scripts.Board.Settings;
using TicTacToe.Scripts.Utils;
using Zenject;

namespace TicTacToe.Scripts.WinLine
{
    public class WinLineManager : IInitializable, IDisposable
    {
        private readonly BoardController _boardController;
        private readonly BoardSizeData _boardSizeData;

        public WinLineManager(
            BoardController boardController,
            BoardSizeData boardSizeData)
        {
            _boardController = boardController;
            _boardSizeData = boardSizeData;
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }

        public BoardItemType GetResult(IBoardItem targetItem)
        {
            var validCoordinates = GetValidCoordinates(targetItem);
            return validCoordinates.Select(
                    coordinates => GetLineWinType(
                        targetItem,
                        coordinates))
                .FirstOrDefault(result => !Equals(result, BoardItemType.None));
        }

        private IEnumerable<Coordinates[]> GetValidCoordinates(IBoardItem item)
        {
            var n = _boardSizeData.Size;
            var validCoordinates = new List<Coordinates[]>();
            var horizontalCoordinates = new Coordinates[n];
            var verticalCoordinates = new Coordinates[n];
            var diagonalCoordinates = new Coordinates[n];
            var reverseDiagonalCoordinates = new Coordinates[n];
            for (var i = 0; i < n; i++)
            {
                horizontalCoordinates[i] = new Coordinates(item.Coordinates.X, i);
                verticalCoordinates[i] = new Coordinates(i, item.Coordinates.Y);
                diagonalCoordinates[i] = new Coordinates(i, i);
                reverseDiagonalCoordinates[i] = new Coordinates(n - 1 - i, 0 + i);
            }

            validCoordinates.Add(horizontalCoordinates);
            validCoordinates.Add(verticalCoordinates);
            if (item.Coordinates.X == item.Coordinates.Y)
            {
                validCoordinates.Add(diagonalCoordinates);
            }

            if (item.Coordinates.X + item.Coordinates.Y == n - 1)
            {
                validCoordinates.Add(reverseDiagonalCoordinates);
            }

            return validCoordinates;
        }

        private BoardItemType GetLineWinType(
            IBoardItem targetItem,
            IReadOnlyList<Coordinates> coordinates)
        {
            var validItems = new IBoardItem[coordinates.Count];
            for (var i = 0; i < validItems.Length; i++)
            {
                validItems[i] = _boardController.GetBoardItem(coordinates[i]);
            }

            var targetType = targetItem.ItemType.Value;
            if (validItems.Any(item => Equals(item.ItemType.Value, BoardItemType.None))
                || validItems.Any(item => !Equals(item.ItemType.Value, targetType)))
            {
                return BoardItemType.None;
            }

            foreach (var item in validItems)
            {
                item.SetWinState(true);
            }

            return targetType;
        }
    }
}