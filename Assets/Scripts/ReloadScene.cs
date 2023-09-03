using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class ReloadScene : MonoBehaviour
    {
        public void Reload()
        {
            BetweenScenesScripts betweenScenesScripts = FindObjectOfType<BetweenScenesScripts>();
            Destroy(betweenScenesScripts.gameObject);
            SceneManager.UnloadSceneAsync("Game");
            SceneManager.LoadScene("Menu");
        }
    }
}