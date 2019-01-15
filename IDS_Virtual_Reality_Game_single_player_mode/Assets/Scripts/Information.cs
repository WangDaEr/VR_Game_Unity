using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Information : MonoBehaviour {

    public GameObject player;
    public PlayerInformation playerInformation;
    public ZombieInformation zombieInformation;
    public string lockName = "";

	// Use this for initialization
	void Awake () {
		
        DontDestroyOnLoad(this);
        if (FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);
        }
        playerInformation = gameObject.GetComponent<PlayerInformation>();
        zombieInformation = gameObject.GetComponent<ZombieInformation>();
        playerInformation.informationObject = this;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// load information from last scene when switch scenes
    /// </summary>
    public void registerInformation() {

        if (player == null) {
            Debug.LogError("player not set (information)");
        } else {
            playerInformation.registerInformation();
        }

    }

    /// <summary>
    /// record information updated in current before switch scenes
    /// </summary>
    public void updateInformation () {

        if (player == null) {
            Debug.LogError("player not set (information)");
        } else {
            playerInformation.updateInformation();
        }

    }
}
