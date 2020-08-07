using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Runner {
    public class Collectable : MonoBehaviour {
        public int score = 1;

        private void LateUpdate() {
            if (transform.position.x < RunnerManager.Instance.player.transform.position.x) {
                Destroy(gameObject, 2f);
            }
        }

        void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.CompareTag("Player")) {
                RunnerManager.Instance.AddScore(score);
                Destroy(gameObject);
            }
        }
    }
}