using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace TFG.Runner {
    public class RunnerManager : MonoBehaviour {
        static RunnerManager _instance;
        public static RunnerManager Instance {
            get {
                return _instance;
            }
        }

        [Header("Game Objects")]
        public RunnerController player;
        public ObstacleSpawner spawner;
        public TileSprite floor;

        [Header("UI")]
        public GameObject gameOverScreen;
        public Text scoreText;
        int _score = 0;

        void Awake() {
            _instance = this;
        }

        void Start() {
            StartGame();
        }

        public void AddScore(int score) {
            _score += score;
            scoreText.text = "Score: " + _score;
        }

        public void GameOver() {
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
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