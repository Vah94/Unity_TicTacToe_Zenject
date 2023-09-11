using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace TicTacToe.Scripts.UI
{
    public class GameEndPanel : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private Button _tryAgainButton;

        private SceneReloadManager _sceneReloadManager;

        [Inject]
        public void Construct(SceneReloadManager sceneReloadManager)
        {
            _sceneReloadManager = sceneReloadManager;
        }

        public void Initialize()
        {
            SetMessage("");
            _tryAgainButton.onClick.RemoveAllListeners();
            _tryAgainButton.onClick.AddListener(
                () => { _sceneReloadManager.ReloadGame(true); });
        }

        public void Dispose()
        {
        }

        public void SetMessage(string message)
        {
            _messageText.text = message;
            gameObject.SetActive(!string.IsNullOrEmpty(message));
        }
    }
}