using UnityEngine;
using UnityEngine.SceneManagement;

namespace TFG {
    public class SceneChanger : MonoBehaviour {
        public bool multiplesScenes;

        [Scene]
        public string sceneName;

        [Scene]
        public string[] scenes;

        int _sceneIndex;

        public void LoadScene() {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadScene(int i) {
            SceneManager.LoadScene(scenes[i]);
        }

        public void LoadSceneIndex() {
            LoadScene(_sceneIndex);
        }

        public void SetSceneIndex(int i) {
            _sceneIndex = i;
        }
    }
}