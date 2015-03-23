using UnityEngine;
using System.Collections;

public class BackgroundFlash : MonoBehaviour {

	// Use this for initialization
    Animator anim;
	void Start () {
        anim = GetComponent<Animator>();
        anim.speed = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.gm.score > 45)
        {
            anim.speed = GameManager.gm.score / 75f;
            anim.SetBool("flash", true);
        }
        else
        {
            anim.speed = 0;
            anim.SetBool("flash", false);
        }
	}
}
