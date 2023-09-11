using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TicTacToe.Scripts.UI
{
    public class Hud : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private Button _reloadButton;
        private SceneReloadManager _sceneReloadManager;

        [Inject]
        public void Construct(SceneReloadManager sceneReloadManager)
        {
            _sceneReloadManager = sceneReloadManager;
        }

        public void Initialize()
        {
            SetMessage("");
            _reloadButton.onClick.RemoveAllListeners();
            _reloadButton.onClick.AddListener(
                () => { _sceneReloadManager.ReloadGame(true); });
        }

        public void Dispose()
        {
        }

        public void SetMessage(string message)
        {
            _messageText.text = message;
        }
    }
}