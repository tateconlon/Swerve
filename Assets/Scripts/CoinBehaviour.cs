using UnityEngine;
using System.Collections;

public class CoinBehaviour : MonoBehaviour {

	// Use this for initialization
    float rotateSpeed = 360/5;
    float bobHeight = 0.33f;
    float bobSpeed = 2f;
    float bobOffset;
    bool goingUp = false;
    public Animator anim;
	void Start () {
        bobOffset = transform.position.y;
        anim.speed = 0;
	}
	
	// Update is called once per frame
	void Update () {
        anim.speed = GameManager.gm.score / 25f;
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        if (transform.position.y > bobHeight + bobOffset)
        {
            goingUp = false;
        }
        else if (transform.position.y <= bobOffset)
        {
            goingUp = true;
        }

        if (goingUp)
        {
            transform.position = transform.position + new Vector3(0, bobHeight, 0) * bobSpeed * Time.deltaTime;
        }
        else
        {
            transform.position = transform.position - new Vector3(0, bobHeight, 0) * bobSpeed * Time.deltaTime;
        }
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            Debug.Log("Player");
            GameManager.gm.AddScore(2);
            Destroy(gameObject);
        }
    }
}
