using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TFG.Relax {
    public class Leaf : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

        [Header("Properties")]
        [Range(0f, 1f)]
        public float probabilityOfChange = 0.6f;
        public Vector2 distanceMargin;
        public Vector2 cooldown;
        public Vector2 possibleAngles = new Vector2(0, 360);

        float _cooldown;
        bool _isDown;
        Rect _bounds;
        float _lastTime;

        // "Attach" to a Line (So we can track the index later)
        GameLine _line;

        // Needed for checking. Is static since all the instances will need the same objects.
        static GraphicRaycaster _raycast;
        static EventSystem _eventSystem;

        void Start() {
            if (!_raycast)
                _raycast = GetComponentInParent<GraphicRaycaster>();
            if (!_eventSystem)
                _eventSystem = GetComponentInParent<EventSystem>();

            _bounds = transform.parent.GetComponent<RectTransform>().rect;
            _lastTime = Time.time;
            _cooldown = RandomUtil.Range(cooldown);
        }

        void Update() {
            if (_isDown) {
                transform.position = Input.mousePosition;
                return;
            }

            var dt = Time.time - _lastTime;

            if (dt >= _cooldown) {
                if (Random.Range(0f, 1f) <= probabilityOfChange) {
                    //Make it fly
                    var pos = transform.localPosition;
                    var dist = RandomUtil.Range(distanceMargin);
                    var angle = RandomUtil.Range(possibleAngles);
                    var dir = Vector3.zero;
                    dir.x = Mathf.Cos(angle  * Mathf.Deg2Rad);
                    dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);

                    // Move and Clamp
                    pos += dir * dist;
                    pos.x = Mathf.Max(pos.x, _bounds.xMin);
                    pos.x = Mathf.Min(pos.x, _bounds.xMax);
                    pos.y = Mathf.Max(pos.y, _bounds.yMin);
                    pos.y = Mathf.Min(pos.y, _bounds.yMax);

                    // Assing new pos
                    transform.localPosition = pos;

                    // Check if we are still on a line
                    var pointer = new PointerEventData(_eventSystem) {
                        position = transform.position
                    };
                    CheckLine(pointer);

                    _lastTime = Time.time;
                    _cooldown = RandomUtil.Range(cooldown);
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData) {
            _isDown = true;
        }

        public void OnPointerUp(PointerEventData eventData) {
            _isDown = false;
            CheckLine(eventData);
        }

        void CheckLine(PointerEventData eventData) {
            List<RaycastResult> results = new List<RaycastResult>();
            _raycast.Raycast(eventData, results);

            foreach(RaycastResult result in results) {
                var line = result.gameObject.GetComponent<GameLine>();

                // We found a line
                if (line != null) {
                    if (_line == null) {
                        line.Attach();
                    } else if (_line != line) {
                        _line.Detach();
                        line.Attach();
                    }
                    _line = line;
                    return;
                }
            }

            // If we are here, we didn't find a line
            if (_line != null) {
                _line.Detach();
                _line = null;
            }
        }
    }
}