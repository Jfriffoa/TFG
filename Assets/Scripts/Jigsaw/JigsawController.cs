using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace TFG.Jigsaw {
    public class JigsawController : MonoBehaviour {

        public RectTransform board;
        public RectTransform pieces;

        [Space(10)]
        public UnityEvent onWin;

        int _piecesDone;

        // Start is called before the first frame update
        void Start() {
            _piecesDone = 0;
            
            //Prepare Pieces
            pieces.BroadcastMessage("SetIndex");
            pieces.BroadcastMessage("SetClickSize", board.GetComponent<GridLayoutGroup>().cellSize);

            //Shuffle them
            for (int i = 0; i < 3; i++) {                       // Shuffle 3 times
                for (int j = 0; j < pieces.childCount; j++) {   // Shuffle each child
                    pieces.GetChild(j).SetSiblingIndex(Random.Range(0, pieces.childCount));
                }
            }

            //Let the layout group prepare the childs before ignoring them
            Invoke("ChildIgnoreLayout", 0.01f);
        }

        void ChildIgnoreLayout() {
            for (int i = 0; i < pieces.childCount; i++) {
                pieces.GetChild(i).GetComponent<LayoutElement>().ignoreLayout = true;
            }
        }

        internal void PieceSet() {
            _piecesDone++;

            if (_piecesDone == board.childCount) {
                // WIN
                Debug.Log("Jigsaw Completed", gameObject);
                onWin.Invoke();
            }
        }
    }
}