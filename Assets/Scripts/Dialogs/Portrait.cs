using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace TFG.Dialog {
    public class Portait : MonoBehaviour {

        Animator _animator;
        
        // Start is called before the first frame update
        void Start() {
            _animator = GetComponentInChildren<Animator>();
        }

        internal void ApplyAttributes(string[] attributes) {
            //TODO: Update animations in order based on the keys
            // - Enter
            // - Exit
            // - Focus (On)
            // - Background (Off)

            // Even more: Expressions?
        }
    }
}