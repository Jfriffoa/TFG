using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject toSpawn;
    public Vector2 timeToSpawn;
    public float spawnOffset;
    public Vector2 ySpawn;

    public float[] ySpawnList;

    bool _spawning = true;
    Camera _cam;

    void Start() {
        _cam = Camera.main;
        Invoke("StartSpawn", 1f);
    }

    void StartSpawn() {
        StartCoroutine(Spawn());
    }

    public void Clean() {
        for (int i = transform.childCount - 1; i >= 0; i--) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    IEnumerator Spawn() {
        while (_spawning) {
            var horz = _cam.orthographicSize * Screen.width / Screen.height;
            var spawnPos = _cam.transform.position;

            spawnPos.x += horz + spawnOffset;
            //spawnPos.y = RandomUtil.Range(ySpawn);
            spawnPos.y = RandomUtil.RandomPick(ySpawnList);
            spawnPos.z = transform.position.z;
            Instantiate(toSpawn, spawnPos, Quaternion.identity, transform);
            yield return new WaitForSeconds(RandomUtil.Range(timeToSpawn));
        }
    }
}
