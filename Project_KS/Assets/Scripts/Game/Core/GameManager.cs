using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Data;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [HideInInspector]
        public static GameManager Instance = null;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
                Destroy(this.gameObject);
        }

        public void ChangeScene(SceneName scene)
        {
            Debug.Log($"Scene Change To : {scene}");
            SceneManager.LoadScene(scene.ToString());
        }
    }
}