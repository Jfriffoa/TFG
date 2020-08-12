using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace TFG.SimonSays {
    public class SimonController : MonoBehaviour {

        [Header("Sequence")]
        [Tooltip("How long should the buttons light up when showing the sequence")]
        public float blinkingSeconds = 3f;
        [Tooltip("How much should we wait before light up the next button in the sequence")]
        public float interBlinking = 0.2f;

        [Header("Win/Lose")]
        [Tooltip("How long should be the buttons light up when wrong")]
        public float wrongSeconds = 1f;
        [Tooltip("How much should we wait (after a wrong step) before showing the sequence again")]
        public float wrongWait = 1f;
        public Color wrongColor = Color.red;
        public Color winColor = Color.green;

        [Header("Callbacks")]
        public UnityEvent onWin;

        SimonButton[] _buttons;
        int[] _sequence;
        int _sequenceIndex;
        int _stepIndex;

        bool _showing = false;

        // Start is called before the first frame update
        void Start() {
            _buttons = GetComponentsInChildren<SimonButton>();

            for (int i = 0; i < _buttons.Length; i++) {
                var index = i;
                _buttons[i].onPressed += delegate {
                    CheckStep(index);
                };
            }
        }

        public void StartGame(int sequenceLength = 5) {
            if (sequenceLength < 1) {
                Debug.LogWarning("The sequence must be a positive integer greater than 0");
                return;
            }

            // Generate Sequence
            _sequenceIndex = 0;
            _sequence = new int[sequenceLength];
            for (int i = 0; i < sequenceLength; i++) {
                _sequence[i] = Random.Range(0, _buttons.Length);
            }

            // Show the sequence
            NextSequence();
        }

        public void RepeatSequence() {
            if (!_showing)
                StartCoroutine(ShowSequence());
        }

        void CheckStep(int index) {
            // Check step
            if (_sequence[_stepIndex] == index) {
                //Correct Step
                _stepIndex++;

                // If we did all the steps, go to next sequence.
                if (_stepIndex >= _sequenceIndex) {
                    NextSequence();
                }
            } else {
                //We Lose
                Debug.Log("WRONG");
                _stepIndex = 0;
                StartCoroutine(WrongStep());
            }
        }

        void NextSequence() {
            // Check if we won
            if (_sequenceIndex >= _sequence.Length) {
                Debug.Log("WON");
                //Disable buttons
                transform.BroadcastMessage("Win", winColor);
                onWin.Invoke();
                return;
            }

            //Restart the parameters and show the new sequence
            _sequenceIndex++;
            _stepIndex = 0;
            StartCoroutine(ShowSequence());
        }

        IEnumerator WrongStep() {
            _showing = true;

            for (int i = 0; i < _buttons.Length; i++) {
                _buttons[i].SetButtonEnabled(false);
                _buttons[i].Blink(wrongSeconds, wrongColor);
            }

            yield return new WaitForSeconds(wrongSeconds + wrongWait);
            StartCoroutine(ShowSequence());
        }

        IEnumerator ShowSequence() {
            // If the actual index is greater than the total sequence size. Stop coroutine
            if (_sequenceIndex > _sequence.Length)
                yield break;


            // Disable the buttons while we show the sequence
            transform.BroadcastMessage("SetButtonEnabled", false);
            _showing = true;

            // Show the sequence
            for (int i = 0; i < _sequenceIndex; i++) {
                _buttons[_sequence[i]].Blink(blinkingSeconds);
                yield return new WaitForSeconds(blinkingSeconds + interBlinking);
            }

            // Reactive the buttons
            transform.BroadcastMessage("SetButtonEnabled", true);
            _showing = false;
        }
    }
}