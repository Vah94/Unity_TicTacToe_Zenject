using Common.Utils;

namespace TicTacToe.Scripts.Board
{
    public interface IBoardStateManager
    {
        ISafeAction<IBoardItem> OnBoardItemPressed { get; }
    }

    public class BoardStateManager : IBoardStateManager
    {
        public ISafeAction<IBoardItem> OnBoardItemPressed => OnBoardItemPressedLocal;
        public readonly SafeAction<IBoardItem> OnBoardItemPressedLocal = new SafeAction<IBoardItem>();
    }
}