using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{

    public Information informationObject;
    private bool equipedWeapon = false;
    private int healthPoint = 100;
    private int energyPoint = 100;
    private int woodAmount = 0;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void registerInformation () {

        healthPoint = informationObject.player.GetComponent<PlayerHealth>().healthPoints;
        energyPoint = informationObject.player.GetComponent<PlayerHealth>().energyPoints;
        woodAmount = informationObject.player.GetComponent<PlayerMaterial>().woodAmount;

        Debug.Log("re" + healthPoint + " " + energyPoint + " " + woodAmount);

    }

    public void updateInformation () {
        
        informationObject.player.GetComponent<PlayerHealth>().healthPoints = healthPoint;
        informationObject.player.GetComponent<PlayerHealth>().energyPoints = energyPoint;
        informationObject.player.GetComponent<PlayerMaterial>().woodAmount = woodAmount;

        Debug.Log("up" + healthPoint + " " + energyPoint + " " + woodAmount);

    }

    public void setEquipedWeapon (bool source) {

        equipedWeapon = source;

    }

    public bool getEquipedWeapon () {

        return equipedWeapon;


    }
}
