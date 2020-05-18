using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomUtil {
    public static float Range(Vector2 limits)
    {
        return Random.Range(limits.x, limits.y);
    }

    public static T RandomPick<T>(T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
}
