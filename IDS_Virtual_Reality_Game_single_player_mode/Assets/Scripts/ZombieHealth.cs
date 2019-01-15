using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour {

    private float healthPoints = 150.0F;
    private float delayBeforeDestroy = 0.0F;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float getZombieHealth() {

        return healthPoints;

    }

    /// <summary>
    /// receive damage from players
    /// </summary>
    /// <param name="damage"></param>
    public void decreaseHealthPoint(float damage) {

        healthPoints -= damage;
        if (healthPoints <= 0) {
            Destroy(gameObject, delayBeforeDestroy);
            GameObject.FindWithTag("Player").GetComponent<UseWeapon>().weaponCollision = null;
        }
        Debug.Log("applying damage to" + gameObject.name + " HP: " + healthPoints);

    }
}
