using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour {

    public GameObject platformPrefab;
    public GameObject diamondPrefab;
    public GameObject startPlatform;
    public Transform newSpawnPoint;

    Vector3 startPlatformPos;
    Vector3 respawnPoint;
    Vector3 xOffset = new Vector3(2, 0, 0);
    Vector3 zOffset = new Vector3(0, 0, 2);
    Queue<GameObject> q = new Queue<GameObject>();

    public int maxPlatforms = 15;
    float startDelayTime = 4;
    float runningDelayTime;
	// Use this for initialization
	void Start () {
        startPlatformPos = startPlatform.transform.position;
        respawnPoint = newSpawnPoint.position;
        Random.seed = (int)System.DateTime.Now.Ticks;
        for (int i = 0; i < maxPlatforms; i++)
        {
            q.Enqueue((GameObject)Instantiate(platformPrefab));
        }
        Reset();
	}

    // Update is called once per frame
    void Update()
    {
        runningDelayTime -= Time.deltaTime;
        if (!(q.Peek().GetComponent<Renderer>().isVisible) && runningDelayTime <= 0)
        {
            Recycle();
        }
	}

    void Recycle()
    {
        GameObject p1 = q.Dequeue();
        p1.GetComponent<Rigidbody>().isKinematic = true;
        if ((newSpawnPoint.position.z - newSpawnPoint.position.x >= 11.99 || Random.value < 0.5) && !(newSpawnPoint.position.x - newSpawnPoint.position.z >= 11.99))
        {
            newSpawnPoint.position += xOffset;
        }
        else
        {
            newSpawnPoint.position += zOffset;
        }
        if (Random.value < 0.1)
        {
            p1.GetComponent<PlatformBehaviour>().SpawnDiamond();
        }
        else {
            p1.GetComponent<PlatformBehaviour>().DestroyDiamond();
        }

        p1.transform.position = newSpawnPoint.position;
        q.Enqueue(p1);
    }

    public void Reset()
    {
        startPlatform.transform.position = startPlatformPos;
        startPlatform.GetComponent<Rigidbody>().isKinematic = true;
        newSpawnPoint.position = respawnPoint;
        runningDelayTime = startDelayTime;
        for (int i = 0; i < q.Count; i++)
        {
            Recycle();
        }
    }
}
