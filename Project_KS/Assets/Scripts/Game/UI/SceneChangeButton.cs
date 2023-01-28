using UnityEngine;
using UnityEngine.UI;
using Game.Data;

namespace Game.UI
{
    public class SceneChangeButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private SceneName sceneName;

        private void Start()
        {
            button.onClick.AddListener(() => GameManager.Instance.ChangeScene(sceneName));
        }
    }
}