using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using TMPro;

namespace TFG.Stress {
    public class BadWord : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
        [Header("Words")]
        public TextAsset wordsFile;
        public TextMeshProUGUI text; 

        static string[] _words;
        static int _lastIdx;

        [Header("Movement")]
        public Vector2 speedRange = new Vector2(180, 300);

        float _speed;
        Vector3 _delta = Vector3.zero;
        bool _isDown = false;
        Rect _bounds;

        // Raycast to check zones
        GraphicRaycaster _raycast;

        void Awake() {
            LoadFile();
        }

        void Start() {
            _raycast = GetComponentInParent<GraphicRaycaster>();
            _bounds = transform.parent.GetComponent<RectTransform>().rect;
            _delta.x = (Random.Range(0f, 1f) >= 0.5f) ? 1 : -1;
            _delta.y = (Random.Range(0f, 1f) >= 0.5f) ? 1 : -1;
            _speed = RandomUtil.Range(speedRange);

            // Set new Word different than the last one spawned
            (text.text, _lastIdx) = RandomUtil.RandomPickDifferent(_words, _lastIdx);
        }

        void Update() {
            // Add 2% per second
            StressManager.Instance.AddStress(0.02f * Time.deltaTime);

            if (_isDown) {      // Move with the mouse
                transform.position = Input.mousePosition;
            } else {            // Move by its own
                var deltaPos = _delta * _speed * Time.deltaTime;
                transform.localPosition += deltaPos;

                //Check bounds
                if (transform.localPosition.x < _bounds.xMin || transform.localPosition.x > _bounds.xMax)
                    _delta.x *= -1;
                if (transform.localPosition.y < _bounds.yMin || transform.localPosition.y > _bounds.yMax)
                    _delta.y *= -1;
            }
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData) {
            _isDown = true;
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData) {
            _isDown = false;
            CheckZone(eventData);
        }

        void CheckZone(PointerEventData eventData) {
            var results = new List<RaycastResult>();
            _raycast.Raycast(eventData, results);

            foreach (RaycastResult result in results) {
                if (result.gameObject.CompareTag("StressZone")) {
                    //We are still in the stress zone
                    return;
                }
            }

            //We are out of the zoned
            Destroy(gameObject);
        }

        void LoadFile() {
            if (_words != null || wordsFile == null)
                return;

            if (string.IsNullOrEmpty(wordsFile.text))
                return;

            // Read File
            var w = new List<string>();
            using (var reader = new StringReader(wordsFile.text)) {
                string line;
                while((line = reader.ReadLine()) != null) {
                    if (!string.IsNullOrEmpty(line))
                        w.Add(line);
                }
            }

            _words = w.ToArray();
        }
    }
}