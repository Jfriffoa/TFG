using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TFG.Jigsaw {
    public class Piece : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

        // Piece Stuff
        RectTransform _transform;
        LayoutElement _layout;
        bool _isDown;
        int _index;

        // Bigger when clicked
        Vector2 _originalSize;
        Vector2 _onClickSize;

        // Raycast to check the board
        GraphicRaycaster _raycast;

        // Start is called before the first frame update
        void Start() {
            _transform = GetComponent<RectTransform>();
            _layout = GetComponent<LayoutElement>();
            _raycast = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<GraphicRaycaster>();
            Invoke("GetSize", 0.51f);
        }

        void GetSize() {
            _originalSize = _transform.sizeDelta;
        }

        // Update is called once per frame
        void Update() {
            if (_isDown) {
                _transform.position = Input.mousePosition;
            }
        }

        internal void SetIndex() {
            _index = transform.GetSiblingIndex();
        }

        internal void SetClickSize(Vector2 size) {
            _onClickSize = size;
        }

        public void OnPointerDown(PointerEventData eventData) {
            _isDown = true;
            _transform.sizeDelta = _onClickSize;
        }

        public void OnPointerUp(PointerEventData eventData) {
            _isDown = false;
            _transform.sizeDelta = _originalSize;
            CheckBoard(eventData);
        }

        void CheckBoard(PointerEventData eventData) {
            List<RaycastResult> results = new List<RaycastResult>();
            _raycast.Raycast(eventData, results);

            foreach (RaycastResult result in results) {
                var board = result.gameObject.GetComponent<BoardPiece>();

                if (board != null) {
                    if (board._index == _index) {   // We are in the right spot
                        board.SetPiece(GetComponent<Image>().sprite);
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}