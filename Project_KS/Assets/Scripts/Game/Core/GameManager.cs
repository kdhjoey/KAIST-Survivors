using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Data;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance = null;
        [SerializeField] private GameDataLoader gameDataLoader;
        public GameData GameData;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
                Destroy(this.gameObject);

            this.GameData = gameDataLoader.GameData;
        }

        public void ChangeScene(SceneName scene)
        {
            Debug.Log($"Scene Change To : {scene}");
            SceneManager.LoadScene(scene.ToString());
        }

        private void Update()
        {
            Debug.Log(GameData.GetCourse(CourseCode.MAS101).Name);
        }
    }
}