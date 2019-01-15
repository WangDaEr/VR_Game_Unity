using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]

public class Chaser : MonoBehaviour {
	
	public float speed = 10.0F;
    public float playerDetectRange;
	public Transform target;
    private Vector3 playerPosition;

	// Use this for initialization
	void Start () {
		// if no target specified, assume the player
		if (target == null) {
			if (GameObject.FindWithTag ("Player")!=null){
				target = GameObject.FindWithTag ("Player").GetComponent<Transform>();
			}
		}
        Debug.Log("target name: " + target.gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {

        if (target == null) {
            return;
        }
        playerPosition = target.position;
        playerPosition.y = transform.position.y;
		transform.LookAt(playerPosition);
		float distance = Vector3.Distance(transform.position,target.position);	
	    transform.position += transform.forward * speed * Time.deltaTime;
        
	}

	public void SetTarget(Transform newTarget) {

		target = newTarget;

	}

    public void restartChasing() {

        gameObject.GetComponent<RandomMoveForZombies>().enabled = false;

    }

    /// <summary>
    /// destroy wood board in safe house when a player is detected
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision){

        Debug.Log("collision name: " + collision.transform.gameObject.name);

        if (collision.transform.gameObject.tag == "woodBoard") {
            Debug.Log("find wood board (zombie)" + collision.transform.gameObject.name);
            gameObject.GetComponent<RandomMoveForZombies>().attackingWoodBoard = true;
            gameObject.GetComponent<RandomMoveForZombies>().enabled = true;
            gameObject.GetComponent<RandomMoveForZombies>().applyDamageToWoodBoard(collision.transform);
        }
        
    }

}
