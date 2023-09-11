using TicTacToe.Scripts.Board.Settings;
using UnityEngine;
using Zenject;

namespace TicTacToe.Scripts.Installers
{
    [CreateAssetMenu(menuName = nameof(TicTacToe) + "/" + nameof(GameSettingsInstaller))]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public BoardSettings Board;


        public override void InstallBindings()
        {
            Container.BindInstance(Board.BoardSizeData);
            Container.BindInstance(Board.BoardItemPrefab);
        }
    }
}