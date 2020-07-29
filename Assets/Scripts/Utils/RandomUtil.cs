using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG {
    public static class RandomUtil {
        public static float Range(Vector2 limits) {
            return Random.Range(limits.x, limits.y);
        }

        public static T RandomPick<T>(T[] array) {
            return array[Random.Range(0, array.Length)];
        }

        public static (T, int) RandomPickDifferent<T>(T[] array, int last) {
            if (array.Length < 2) {
                Debug.LogWarning("Array is size 1, can't pick a different element");
                return (array[0], 0);
            }

            int idx = last;
            while (idx == last) {
                idx = Random.Range(0, array.Length);
            }
            return (array[idx], idx);
        }
    }
}
