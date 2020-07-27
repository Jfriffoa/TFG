using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace TFG.Stress {
    public class PingPongAlpha : MonoBehaviour {
        public Image alpha;
        public Vector2 minMax;
        public float time;

        bool _forward;
        float _lastTime;
        Color _baseColor;

        void Start() {
            _lastTime = Time.time;
            _forward = true;

            if (alpha == null)
                alpha.GetComponent<Image>();
            _baseColor = alpha.color;
        }

        void Update() {
            var t = (Time.time - _lastTime) / time;
            if (!_forward)
                t = 1 - t;
            _baseColor.a = Mathf.Lerp(minMax.x, minMax.y, t);
            alpha.color = _baseColor;

            if (t >= 1) {
                _forward = !_forward;
                _lastTime = Time.time;
            }
        }
    }
}