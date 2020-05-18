using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform toFollow;

    Vector3 _lastKnownPosition;
    void Start() {
        _lastKnownPosition = toFollow.position;
    }

    void LateUpdate()
    {
        var deltaPos = toFollow.position - _lastKnownPosition;
        transform.position += deltaPos;
        _lastKnownPosition = toFollow.position;
    }
}
