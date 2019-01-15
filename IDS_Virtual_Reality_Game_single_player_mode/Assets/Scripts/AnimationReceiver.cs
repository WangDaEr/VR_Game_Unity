using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationReceiver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// delay the animation when corresponding button pressed before animation finished
    /// </summary>
    public void animationCompleted() {

        gameObject.transform.parent.parent.GetComponent<UseWeapon>().waitForNextAttack();

    }
}
