using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace TFG.Runner {
    public class RunnerManager : MonoBehaviour {
        // Singleton
        static RunnerManager _instance;
        public static RunnerManager Instance { get => _instance; }

        [Header("Game Objects")]
        public RunnerController player;
        public ObstacleSpawner spawner;
        public TileSprite floor;
        public Transform goal;

        [Header("UI")]
        public GameObject gameOverScreen;
        public Text scoreText;
        public Slider progressBar;
        public Image darkness;
        float _initialAlpha;
        int _score = 0;

        [Header("Callback")]
        public UnityEvent onEnd;

        void Awake() {
            if (_instance != null) {
                Debug.LogWarning("There's another instance of the Manager. Deleting this one");
                Destroy(gameObject);
            } else {
                _instance = this;
            }
        }

        void Start() {
            StartGame();

            if (darkness)
                _initialAlpha = darkness.color.a;
        }

        void Update() {
            // Update Progress
            var progress = player.transform.position.x / goal.position.x;
            progressBar.value = progress;

            if (darkness) {
                var c = darkness.color;
                var easeOutT = Mathf.Sin((1 - progress) * Mathf.PI * .5f);
                c.a = Mathf.Lerp(0, _initialAlpha, easeOutT);
                darkness.color = c;
            }

            if (progress >= 1)
                GameOver();
        }

        public void AddScore(int score) {
            _score += score;
            //scoreText.text = "Score: " + _score;
        }

        public void HitPlayer() {
            player.TakeDamage();
        }

        public void GameOver() {
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
            onEnd.Invoke();
        }

        void StartGame() {
            _score = 0;
            AddScore(0);
            Time.timeScale = 1;
            gameOverScreen.SetActive(false);
        }

        public void ResetGame() {
            //player.ResetPos();
            //spawner.Clean();
            //floor.ResetPos();
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}