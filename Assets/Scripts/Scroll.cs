using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {

    public Transform player;
    float xOffset;
    float yOffset;
    float zOffset;
    Vector3 newPos;
	// Use this for initialization
	void Start () {
        xOffset = transform.position.x;
        yOffset = transform.position.y;
        zOffset = transform.position.z;
        newPos = new Vector3();
	}
	
	// Update is called once per frame
	void Update () {
        float dist = Vector2.Dot(new Vector2(1, 1).normalized, new Vector2(player.transform.position.x, player.transform.position.z));  //Find distance
        newPos.x = (dist / Mathf.Sqrt(2)) + xOffset;    //distance from center / sqrt(2) because of 45deg angle
        newPos.y = yOffset;
        newPos.z = (dist / Mathf.Sqrt(2)) + zOffset;
        transform.position = newPos;
	}
}
