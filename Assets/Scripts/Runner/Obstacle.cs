using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Runner {
    public class Obstacle : MonoBehaviour {
        public int score = 1;
        public Color inactiveColor = Color.white;
        bool _dead;

        void LateUpdate() {
            if (_dead)
                return;

            if (transform.position.x < RunnerManager.Instance.player.transform.position.x) {
                RunnerManager.Instance.AddScore(score);
                _dead = true;
                Destroy(gameObject, 2f);
            }
        }

        void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.CompareTag("Player")) {
                RunnerManager.Instance.HitPlayer();
                //Destroy(gameObject);

                // Deactive the object
                GetComponent<SpriteRenderer>().color = inactiveColor;
                var colliders = GetComponents<Collider2D>();
                foreach (var collider in colliders) {
                    collider.enabled = false;
                }
            }
        }
    }
}