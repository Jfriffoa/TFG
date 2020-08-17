using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace TFG {
    public class PickRandomImage : MonoBehaviour {
        [Tooltip("Image to replace. If is not configured, the script will try to search one on this gameobject.")]
        public bool startRandom;
        public Image target;
        public Sprite[] sprites;
        
        // Start is called before the first frame update
        void Start() {
            if (target == null) {
                target = GetComponent<Image>();
            }

            if (startRandom) {
                PickRandom();
            }
        }

        public void PickRandom() {
            target.sprite = RandomUtil.RandomPick(sprites);
        }
    }
}