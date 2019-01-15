using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

    public int woodAmount = 10;
    public float delayBeforeDestroy = 0.0F;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void acquireWood(int amount) {

        woodAmount -= amount;
        checkTreeIsDown();

    }

    /// <summary>
    /// destroy a tree gameObject when its wood are all extracted
    /// </summary>
    public void checkTreeIsDown () {

        if (woodAmount <= 0) {
            Debug.Log("destroy tree: " + gameObject.name);
            Destroy(gameObject, delayBeforeDestroy); //change to stump maybe;
            GameObject.FindWithTag("Player").GetComponent<UseWeapon>().weaponCollision = null;
        }

    }
}
