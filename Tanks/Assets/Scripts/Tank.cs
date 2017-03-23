using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {
    public GameObject prefabShot;
    public float velocityMult = 4f;
    public bool __________________________;

    public GameObject lp; // launchPoint
    public Vector3 lPos;
    public GameObject shot;
    public bool aimingMode;

    void Awake()
    {
        Transform lpTrans = transform.Find("LaunchPoint");
        lp = lpTrans.gameObject;
        lp.SetActive(false);
        lPos = lpTrans.position;
    }
    void OnMouseEnter()
    {
        //print("Tank:OnMouseEnter()");
        lp.SetActive(true);
    }

    void OnMouseExit()
    {
        //print("Tank:OnMouseExit()");
        lp.SetActive(false);
    }

    void OnMouseDown()
    {
        aimingMode = true;
        shot = Instantiate(prefabShot) as GameObject;
        shot.transform.position = lPos;
        shot.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update()
    {
        if (!aimingMode) return;
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 mouseDelta = mousePos3D - lPos;
        float maxMag = this.GetComponent<SphereCollider>().radius;
        if(mouseDelta.magnitude > maxMag)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMag;
        }
        Vector3 shotPos = lPos + mouseDelta;
        shot.transform.position = shotPos;

        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            shot.GetComponent<Rigidbody>().isKinematic = false;
            shot.GetComponent<Rigidbody>().velocity = -mouseDelta * velocityMult;
            FollowCAM.S.poi = shot;
            shot = null;
            GameManager.ShotFired();
        }
    }
}
