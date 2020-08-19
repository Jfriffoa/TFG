using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace TFG.Questionaries {
    public class TestProgress : MonoBehaviour {
        public Transform screensRoot;
        GameObject[] _screens;

        public Transform progressRoot;
        public GameObject progressIcon;
        Image[] _icons;
        public Color done = Color.green;
        public Color actual = Color.red;

        int _actualScreen = -1;

        public UnityEvent onDone;

        void Awake() {
            //Get Screens
            _screens = new GameObject[screensRoot.childCount];
            for (int i = 0; i < screensRoot.childCount; i++) {
                _screens[i] = screensRoot.GetChild(i).gameObject;
                _screens[i].SetActive(false);
            }

            //Get the actual dots
            _icons = new Image[screensRoot.childCount];
            for (int i = 0; i < progressRoot.childCount; i++) {
                _icons[i] = progressRoot.GetChild(i).GetComponent<Image>();
            }

            // Instantiate until dots = screens
            for (int i = progressRoot.childCount; i < screensRoot.childCount; i++) {
                var go = GameObject.Instantiate(progressIcon, progressRoot);
                _icons[i] = go.GetComponent<Image>();
            }

            // Show first screen
            NextScreen();
        }

        public void NextScreen() {
            if (_actualScreen >= 0) {
                _screens[_actualScreen].SetActive(false);
                _icons[_actualScreen].color = done;
            }

            _actualScreen++;

            if (_actualScreen < _screens.Length) {
                _screens[_actualScreen].SetActive(true);
                _icons[_actualScreen].color = actual;
            } else {
                onDone.Invoke();
            }
        }

    }
}