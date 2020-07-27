using System.Collections;
using UnityEngine;

namespace TFG {
    public class SpawnOnUI : MonoBehaviour {
        public float timeToSpawn;
        public float initialDelay;
        public GameObject prefab;
        public RectTransform spawnRect;

        bool _isActive = true;

        void Start() {
            Invoke("StartSpawn", initialDelay);
        }

        void StartSpawn() {
            StartCoroutine(Spawning());
        }

        IEnumerator Spawning() {
            while (_isActive) {
                Spawn();
                yield return new WaitForSeconds(timeToSpawn);
            }
        }

        void Spawn() {
            var rect = spawnRect.rect;

            //TODO: Tomar en cuenta el tamaño del prefab al spawnear
            var x = Random.Range(rect.xMin, rect.xMax);
            var y = Random.Range(rect.yMin, rect.yMax);

            var go = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity, spawnRect);
            go.transform.localPosition = new Vector2(x, y);
        }

        public void StopSpawning() {
            _isActive = false;
        }
    }
}