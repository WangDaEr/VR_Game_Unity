using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoveForZombies : MonoBehaviour {

    public float speed = 20.0f;
    public float walkingRangeRadius = 5.0f;
    public bool isDetected = false;
    public bool attackingWoodBoard = false;
    public bool isActive;
    public float playerDetectRange;

    private GameObject player;
    private bool setPlayer = false;
    private bool isChasing = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        move();
		
	}

    public void setPlayerAsTarget(GameObject target) {

        player = target;

    }

    public void debugg() {

        Debug.Log("isActive = " + isDetected + " " + gameObject.name);

    }

    private void move () {

        if (attackingWoodBoard) {
            return;
        }
        transform.position += transform.forward * speed * Time.deltaTime;

    }

    /// <summary>
    /// destory wood boards when a zombie collides with it while chasing after player in safe house.
    /// </summary>
    /// <param name="target"></param>
    public void applyDamageToWoodBoard(Transform target) {

        float count = 0.0F;
        Transform boardCollection = target.parent;
        int attachedBoards = boardCollection.gameObject.GetComponent<WoodBoardUsage>().currentActiveBoard;

        gameObject.GetComponent<Chaser>().enabled = false;
        while (attachedBoards > 0) {
            Debug.Log("attacking woodb (zombie)");
            if (count < 1.0F) {
                count += Time.deltaTime;
            } else {
                boardCollection.GetChild(attachedBoards).gameObject.SetActive(false);
                count = 0.0F;
                attachedBoards -= 1;
            }
        }
        attackingWoodBoard = false;
        gameObject.GetComponent<Chaser>().enabled = true;
        gameObject.GetComponent<Chaser>().restartChasing();
        Debug.Log("finish attacking (zombie)");

    }

    /// <summary>
    /// resume moving
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision){

        if (collision.gameObject.tag == "Player"){
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            isActive = true;
        }

    }

    /// <summary>
    /// avoid unexpected translation
    /// </summary>
    /// <param name="newCollision"></param>
    private void OnCollisionEnter (Collision newCollision) {

        string tagName = newCollision.gameObject.tag;
        float sensitivity = 15.0F;
        float yRotation = 0.0F;

        if (tagName != "Player" && tagName != "floors") { 
            gameObject.transform.eulerAngles += new Vector3(0.0f, 90.0f, 0.0f);
        } else if (tagName == "Player") {
            InteractWithPlayer(newCollision);
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            isActive = false;
        }
        
    }

    /// <summary>
    /// apply damage to player
    /// </summary>
    /// <param name="target"></param>
    void InteractWithPlayer(Collision target) {

        float thrust = 50.0F;
        player = target.gameObject;
        setPlayer = true;
        player.GetComponent<PlayerHealth>().getAttacked(15);
        player.GetComponent<PlayerMove>().attackedByZombie();

        Debug.Log("add force to player");

    }
}
