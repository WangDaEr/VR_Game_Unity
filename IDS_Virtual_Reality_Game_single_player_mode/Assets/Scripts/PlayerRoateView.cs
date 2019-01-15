using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoateView : MonoBehaviour {

    public float XaxisSen;
    public float YaxisSen;
    public GameObject RotationSource;

    private bool isCursorLocked = false;
    private bool isSetInteractiveMessage = false;
    private bool isSetChangeSceneMessage = false;
    private float XtotalRotation = 0;
    private float delyBeforeDestroy = 0.0F;
    private LayerMask mask = -1;
    private RaycastHit hit;
    private Transform playerCharacter;
    private Transform playercamera;
    private GameManagerSPM gm;
    private PlayerHealth playerHealth;

    // Use this for initialization
    void Start() {

        isCursorLocked = true;

        playerCharacter = gameObject.transform;
        playercamera = Camera.main.transform;
        playerHealth = gameObject.GetComponent<PlayerHealth>();

    }

    // Update is called once per frame
    void Update() {

        //rotateView();
        detectInteractiveItems();

    }

    public void setGameManager (GameManagerSPM gameManager) {

        gm = gameManager;

    }

    /// <summary>
    /// if current device is computer, pressing "Cancel" button can lose focus on current game.
    /// </summary>
    private void checkCursorLocked(){

        if (Input.GetButtonDown("Cancel")) {
            isCursorLocked = false;
        }

        if (isCursorLocked)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        } else {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }

    
    private void rotateView() {

        Vector3 XaxisRotation = Input.GetAxis("Fire3") * Vector3.right * XaxisSen * Time.deltaTime;
        Vector3 YaxisRotation = Input.GetAxis("Fire2") * Vector3.up * YaxisSen * Time.deltaTime;

        playerCharacter.Rotate(YaxisRotation);

        XtotalRotation += XaxisRotation.x;
        if (XtotalRotation < -90.0F || XtotalRotation > 65.0F) {
            XtotalRotation -= XaxisRotation.x;
            return;
        }
        playercamera.Rotate(XaxisRotation);

    }

    /// <summary>
    /// send notification to interactive items
    /// </summary>
    /// <param name="source"></param>
    private void useInteractiveItems(GameObject source) {

        if (Input.GetButtonDown("Interact")) {
            if (source.GetComponent<BounceHealthOrEnergy>() != null) {
                source.GetComponent<BounceHealthOrEnergy>().restore(playerHealth);
            } else {
                gm.equipWeapon();
                Destroy(source, delyBeforeDestroy);
                GameObject.FindGameObjectWithTag("information").GetComponent<Information>().playerInformation.setEquipedWeapon(true);
                Debug.Log("equip weapon: " + GameObject.FindGameObjectWithTag("information").GetComponent<Information>().playerInformation.getEquipedWeapon());
            }
        }

    }

    private void goToOutside() {

        if (Input.GetButtonDown("Interact")) {
            gm.changeScene();
        }

    }

    /// <summary>
    /// perform scene switch
    /// </summary>
    /// <param name="sceneName"></param>
    private void enterHouses(string sceneName) {

        if (Input.GetButtonDown("Interact")) {
            gm.enterHouse(sceneName);
        }

    }

    /// <summary>
    /// prompt associated messages to screen when player is near an interactive item and looking at it.
    /// </summary>
    private void detectInteractiveItems() {

        GameObject target;
        string name;
        Vector3 fwd = playercamera.TransformDirection(Vector3.forward);

        if (Physics.Raycast(playercamera.position, fwd, out hit, 10.0F, mask)) {
            target = hit.transform.gameObject;
            name = target.transform.parent.gameObject.name;
            Debug.Log("player detect object: " + name);
            if (target.name == "leaveDoor" && gm.changeSceneMessage != null)
            {
                if (!isSetChangeSceneMessage) {
                    gm.changeSceneMessage.enabled = true;
                    isSetChangeSceneMessage = true;
                }
                goToOutside();
            } else if (target.name == "Entry" && gm.changeSceneMessage != null) {
                if (!isSetChangeSceneMessage) {
                    gm.changeSceneMessage.enabled = true;
                    isSetChangeSceneMessage = true;
                }
                enterHouses(hit.transform.parent.gameObject.name);
            } else if (name == "Interactive_Items" && gm.interactiveMessage != null) {
                if (!isSetInteractiveMessage) {
                    gm.interactiveMessage.enabled = true;
                    isSetInteractiveMessage = true;
                }
                useInteractiveItems(target);
            } else if (target.name == "bulb_light") {
                Debug.Log("lights intensity: " + target.GetComponent<Light>().intensity);
            } else if (name == "material") {
                Debug.Log("find tree");
            } else {
                gm.interactiveMessage.enabled = false;
                gm.changeSceneMessage.enabled = false;
                isSetInteractiveMessage = false;
                isSetChangeSceneMessage = false;
            }
        } else {
            gm.interactiveMessage.enabled = false;
            gm.changeSceneMessage.enabled = false;
            isSetInteractiveMessage = false;
            isSetChangeSceneMessage = false;
        }       

    }
}
