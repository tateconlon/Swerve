using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    const float startVel = 6;
    public GameObject sphere;
    Vector3 sphereOffset;
    float vel;
    bool moveUp;
    Vector3 dir;
    int platCount = 0;
    bool dead = false;
    Vector3 respawnPos;
    public AudioSource twinkle;
    public AudioSource woosh;
    // Use this for initialization
    void Start()
    {
        sphereOffset = sphere.transform.localPosition;
        respawnPos = transform.position;
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gm.gameOver && !dead)
        {
            vel = startVel + GameManager.gm.score / 25;
            bool touch = false;
                foreach (Touch t in Input.touches)
                {
                    if (t.phase == TouchPhase.Began)
                    {
                        touch = true;
                        break;
                    }
                }
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || touch))
            {
                woosh.Play();
                GameManager.gm.AddScore(1);
                if (moveUp)
                {
                    dir = new Vector3(vel, 0, 0);
                    moveUp = false;
                }
                else
                {
                    dir = new Vector3(0, 0, vel);
                    moveUp = true;
                }
            }
            transform.position += dir * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Platform")
        {
            Debug.Log(platCount);
            platCount++;
        }
        else if (coll.tag == "Diamond")
        {
            twinkle.Play();
        }
        Debug.Log(coll.tag);
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Platform")
        {
            platCount--;
            Debug.Log(platCount);
            if (platCount <= 0)
            {
                Death();
            }
        }
    }

    void Death()
    {
        GameManager.gm.GameOver();
        dead = true;
        GetComponentInChildren<Rigidbody>().isKinematic = false;
    }

    public void Reset()
    {
        transform.position = respawnPos;
        GetComponentInChildren<Rigidbody>().isKinematic = true;
        sphere.transform.localPosition = sphereOffset;
        dead = false;
        moveUp = false;
        vel = startVel;
        dir = new Vector3(vel, 0, 0);
    }
}
