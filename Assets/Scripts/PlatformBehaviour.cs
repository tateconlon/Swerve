using UnityEngine;
using System.Collections;

public class PlatformBehaviour : MonoBehaviour
{

    public GameObject diamondPrefab;
    GameObject d;
    Animator anim;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.score >= 15)
        {
            anim.SetBool("flash", true);
            anim.speed = GameManager.gm.score / 50f;
            if (GameManager.gm.score >= 50)
            {
                anim.SetBool("super", true);
            }
            else
            {
                anim.SetBool("super", false);
            }
        }
        else
        {
            anim.SetBool("flash", false);
            anim.speed = 1;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Player")
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    public void SpawnDiamond()
    {
        d = (GameObject)Instantiate(diamondPrefab, transform.position + new Vector3(0f, 2.33f, 0f), Quaternion.identity);
        d.transform.parent = transform;
    }

    public void DestroyDiamond()
    {
        if (d != null)
            Destroy(d);
    }
}
