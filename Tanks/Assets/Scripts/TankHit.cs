using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHit : MonoBehaviour {
    static public bool hit = false;
    static public bool tankDestroyed = false;
    public int health = 20;
    public int damage = 5;

    void OnTriggerEnter(Collider other)
    {
        print("yo");
        if (other.gameObject.tag == "Shot")
        {
            health = health - damage;
            print(health);
            if(health <= 0)
            {
                Destroy(this.transform.parent.gameObject);
                tankDestroyed = true;
            }
        }
    }
}
