using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace TFG.Jigsaw {
    public class BoardPiece : MonoBehaviour {

        internal int _index;
        Image _image;
        JigsawController _controller;

        // Start is called before the first frame update
        void Start() {
            _index = transform.GetSiblingIndex();
            _image = GetComponent<Image>();
        }

        void SetController(JigsawController controller) {
            _controller = controller;
        }

        internal void SetPiece(Sprite sprite) {
            _image.sprite = sprite;
            _image.color = Color.white;
            _controller.PieceSet();
        }
    }
}