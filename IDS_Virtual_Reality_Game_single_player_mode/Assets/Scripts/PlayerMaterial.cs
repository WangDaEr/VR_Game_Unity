using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterial : MonoBehaviour {

    public int woodAmount = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void acquireWood (int amount) {

        woodAmount += amount;

    }

    public void useWood (int amount) {

        woodAmount -= amount;

    }
}
