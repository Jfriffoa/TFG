using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Questionaries {

    public class Question : MonoBehaviour {
        public int id;
        internal int value;

        public void SetValue(int val) {
            value = val;
        }
    }
}