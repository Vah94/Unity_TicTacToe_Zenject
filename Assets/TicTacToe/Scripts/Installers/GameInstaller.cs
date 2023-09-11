using TicTacToe.Scripts.Board;
using TicTacToe.Scripts.Board.Data;
using TicTacToe.Scripts.Game;
using TicTacToe.Scripts.Game.Data;
using TicTacToe.Scripts.UI;
using TicTacToe.Scripts.WinLine;
using UnityEngine;
using Zenject;

namespace TicTacToe.Scripts.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Inject] private BoardItem _boardItemPrefab;

        [SerializeField] private BoardController _boardController;
        [SerializeField] private Hud _hud;
        [SerializeField] private GameEndPanel _gameEndPanel;

        public override void InstallBindings()
        {
            Container.Bind<SceneReloadManager>()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<GameDataManager>()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<BoardDataManager>()
                .AsSingle();
            Container.BindFactory<BoardItem, BoardItem.BoardItemFactory>()
                .FromComponentInNewPrefab(_boardItemPrefab);
            Container.BindInterfacesAndSelfTo<GameStateManager>()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<BoardStateManager>()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<BoardController>()
                .FromInstance(_boardController)
                .AsSingle();
            Container.BindInterfacesAndSelfTo<WinLineManager>()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<Hud>()
                .FromInstance(_hud)
                .AsSingle();
            Container.BindInterfacesAndSelfTo<GameEndPanel>()
                .FromInstance(_gameEndPanel)
                .AsSingle();
            Container.BindInterfacesTo<GameManager>()
                .AsSingle()
                .NonLazy();
        }
    }
}