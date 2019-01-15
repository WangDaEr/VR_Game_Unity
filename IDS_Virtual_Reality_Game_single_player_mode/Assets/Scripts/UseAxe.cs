using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAxe : MonoBehaviour {

    public float damage = 25.0F;

    private UseWeapon useWeaponObject;
    private GameObject attackedObject;

	// Use this for initialization and attach to its parent for animation
	void Start () {

        useWeaponObject = GameObject.FindWithTag("Player").GetComponent<UseWeapon>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision){

        Debug.Log("axe object: " + collision == null);
        useWeaponObject.weaponCollision = collision;

    }
}
