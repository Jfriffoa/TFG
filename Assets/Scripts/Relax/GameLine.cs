using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Relax {
    public class GameLine : MonoBehaviour {
        int _count;
        public int Count { get => _count; }

        internal void Attach() {
            _count++;
        }

        internal void Detach() {
            _count--;
        }
    }
}