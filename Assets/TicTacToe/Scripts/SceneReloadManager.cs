using Common.Data.SaveSystem;
using UnityEngine.SceneManagement;

namespace TicTacToe.Scripts
{
    public class SceneReloadManager
    {
        public SceneReloadManager()
        {
        }

        public void ReloadGame(bool resetData = false)
        {
            if (resetData)
            {
                CommonPlayerPrefs.DeleteAll();
            }

            SceneManager.LoadScene(0);
        }
    }
}