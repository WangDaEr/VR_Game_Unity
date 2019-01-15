using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    [Tooltip("Different characters have different health points")]
    public int healthPoints = 100;
    [Tooltip("Different characters have different energy points")]
    public int energyPoints = 100;
    public Animation deadAnimation;
    public bool isAlive = true;
    public bool isLosingHealth = true;
    public bool isLosingEnergy = true;

    private float counter = 0.0F;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        DecreaseHealthAndEnergy();
		
	}

    public void restoreHealth(int value) {

        healthPoints += value;

    }

    public void restoreEnergy(int value)
    {

        energyPoints += value;

    }

    void DecreaseHealthAndEnergy() {

        counter += Time.deltaTime;
        if (counter < 1.0F){
            return;
        } else {
            energyPoints -= 1;
            counter = 0.0F;
        }

    }

    public void getAttacked(int damageValue) {

        healthPoints -= damageValue;

    }

}
