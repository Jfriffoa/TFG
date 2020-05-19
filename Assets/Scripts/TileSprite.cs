using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSprite : MonoBehaviour
{
    //TODO: Make it for any direction
    //public enum Tiling { None, X, Y }

    //public Tiling mode;
    public GameObject spriteToTile;

    List<GameObject> _objects = new List<GameObject>();
    int _actualIndex;
    Rect _actualRect = new Rect();
    Rect _cameraRect = new Rect();

    Vector3[] _initialPos;

    // Add more tiles
    // TODO: Make generic por N tiles
    void Start() {
        _objects.Add(spriteToTile);
        Vector3 newPos = spriteToTile.transform.position;
        newPos.x += spriteToTile.GetComponent<Renderer>().bounds.extents.x * 2;

        var secondTile = Instantiate(spriteToTile, newPos, spriteToTile.transform.rotation, spriteToTile.transform.parent);
        _objects.Add(secondTile);

        _actualIndex = 0;
        UpdateRect();

        //Save Initial Pos (in case of reset)
        _initialPos = new Vector3[_objects.Count];
        for (int i = 0; i < _objects.Count; i++) {
            _initialPos[i] = _objects[i].transform.position;
        }

        //Camera Rect
        var cam = Camera.main;
        _cameraRect.width = cam.orthographicSize * 2 * Screen.width / Screen.height;
        _cameraRect.height = cam.orthographicSize;
        _cameraRect.center = cam.transform.position;

    }

    void Update() {
        if (OutOfScreen()) {
            _objects[_actualIndex].transform.Translate(Vector3.right * _actualRect.size.x * _objects.Count);
            _actualIndex = (_actualIndex + 1) % _objects.Count;
            UpdateRect();
        }
    }

    void UpdateRect() {
        var bounds = _objects[_actualIndex].GetComponent<Renderer>().bounds;

        _actualRect.xMin = (bounds.center - bounds.extents).x;
        _actualRect.yMin = (bounds.center - bounds.extents).y;
        _actualRect.xMax = (bounds.center + bounds.extents).x;
        _actualRect.yMax = (bounds.center + bounds.extents).y;
    }

    // Left
    bool OutOfScreen() {
        _cameraRect.center = Camera.main.transform.position;

        return _actualRect.xMin < _cameraRect.x && _actualRect.xMax < _cameraRect.x; 

        //var topLeft = new Vector2(_actualRect.xMin, _actualRect.yMin);
        //var topRight = new Vector2(_actualRect.xMax, _actualRect.yMin);
        //var bottomLeft = new Vector2(_actualRect.xMin, _actualRect.yMax);
        //var bottomRight = new Vector2(_actualRect.xMax, _actualRect.yMax);

        //return !(_cameraRect.Contains(topLeft) ||
        //        _cameraRect.Contains(topRight) ||
        //        _cameraRect.Contains(bottomLeft) ||
        //        _cameraRect.Contains(bottomRight));
    }

    public void ResetPos() {
        for (int i = 0; i < _objects.Count; i++) {
            _objects[i].transform.position = _initialPos[i];
        }

        _actualIndex = 0;
        UpdateRect();
    }
}
