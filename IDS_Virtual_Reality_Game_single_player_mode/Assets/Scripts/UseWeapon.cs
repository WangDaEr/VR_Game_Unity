using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseWeapon : MonoBehaviour {

    public float damage = 25.0F;
    public Animator weaponAnimator;
    public Collision weaponCollision;

    private bool usingWeapon = false;
    private bool canAcquireWood = true; // set to false;
    private string weaponName = "axe"; 
    

	// Use this for initialization
	void Start () {
        weaponCollision = new Collision();
	}
	
	// Update is called once per frame
	void Update () {

        if (weaponAnimator.GetCurrentAnimatorStateInfo(0).IsName("Use_Weapon")) {
            Debug.Log("animation state");
        }

	}

    public void equipWeapon(bool acquireWood) {

        canAcquireWood = acquireWood;

    }

    public bool getUsingWeapon() {

        return usingWeapon;

    } 

    /// <summary>
    /// apply damage to zombies or obtain woods
    /// </summary>
    public void attack() {

        weaponAnimator.SetBool("attackOnce", true);
        usingWeapon = true;
        if (weaponCollision != null && usingWeapon ) {
            if (weaponCollision.gameObject.tag == "zombie") {
                weaponCollision.gameObject.GetComponent<ZombieHealth>().decreaseHealthPoint(damage);
            } else if (canAcquireWood && weaponCollision.gameObject.tag == "tree") {
                Debug.Log("obtaining woods");
                gameObject.GetComponent<PlayerMaterial>().acquireWood(1);
                weaponCollision.gameObject.GetComponent<Tree>().acquireWood(1);
            }
        }
    }

    /// <summary>
    /// wait and reject input from "attack" button until animation is finished
    /// </summary>
    public void waitForNextAttack() {

        weaponAnimator.SetBool("attackOnce", false);
        usingWeapon = false;
        Debug.Log("wait for next attack...");

    }

}
