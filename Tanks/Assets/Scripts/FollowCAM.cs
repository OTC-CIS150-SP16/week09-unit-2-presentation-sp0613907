using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCAM : MonoBehaviour {
    static public FollowCAM S;
    public float easing = 0.05f;
    public Vector2 minXY;
    public bool ______________________________;

    public GameObject poi;
    public float camZ;

	void Awake () {
        S = this;
        camZ = this.transform.position.z;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 destination;
        if (poi == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            destination = poi.transform.position;
            if (poi.tag == "Shot")
            {
                //FIXME: need a better way of returning to defualy view
                //       as sometimes this takes to long
                if (poi.GetComponent<Rigidbody>().IsSleeping())
                {
                    poi = null;
                    return;
                }
            }
        }

        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;
        transform.position = destination;
        this.GetComponent<Camera>().orthographicSize = destination.y + 10;
	}
}
