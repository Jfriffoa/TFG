using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG {
    public class Parallax : MonoBehaviour {
        public float effect;
        public bool local;

        float _lastCamX;
        Camera _cam;

        void Start() {
            _cam = Camera.main;
            _lastCamX = (local) ? _cam.transform.position.x : _cam.transform.localPosition.x;
        }

        // Update is called once per frame
        void LateUpdate() {
            float camX = (local) ? _cam.transform.localPosition.x : _cam.transform.position.x;
            float dist = (camX - _lastCamX) * effect;

            if (local)
                transform.localPosition += Vector3.right * dist;
            else
                transform.position += Vector3.right * dist;

            _lastCamX = camX;
        }
    }
}