using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float moveSpeed = 3.0f;
    public float detectableRange = 30.0f;
    public float sensitivityX = 2f;
    public float sensitivityY = 2f;

    private CharacterController controller;
    private bool weaponAvailable = true;
    private float speed;
    private bool equipedWeapon = true;
    private bool isDown = false;

	// Use this for initialization
	void Start () {

        controller = gameObject.GetComponent<CharacterController>();
        speed = moveSpeed;

	}
	
	// Update is called once per frame
	void Update () {

        ReceiveMoves();
        ReceiveAttack();

    }

    /// <summary>
    /// apply translation to player character according to the input of player
    /// </summary>
    private void ReceiveMoves() {

        Vector3 movementZ = new Vector3(0, 0, 0);
        Vector3 movementX = new Vector3(0, 0, 0);

        /*
        if (Input.GetButton("Vertical")) {
            movementZ = Input.GetAxis("Vertical") * Vector3.forward * speed * Time.deltaTime;            
        }
        if (Input.GetButton("Horizontal")) {
            movementX = Input.GetAxis("Horizontal") * Vector3.right * speed * Time.deltaTime;
        }
        */

        Vector3 forward = new Vector3(gameObject.transform.forward.x, 0.0F, gameObject.transform.forward.z);
        Vector3 right = new Vector3(gameObject.transform.right.x, 0.0F, gameObject.transform.right.z);
        

        movementZ = Input.GetAxis("Vertical") * forward * speed * Time.deltaTime;
        movementX = Input.GetAxis("Horizontal") * right * speed * Time.deltaTime * 0.7F;
        //Vector3 totalMove = transform.TransformDirection(movementZ + movementX);
        Vector3 totalMove = movementZ + movementX;

        //Debug.Log("forward: " + movementZ + ", right: " + movementX);
        controller.Move(totalMove);  

        if (Input.GetButtonDown("GetDown")) {
                if (!isDown)
                {
                    controller.Move(new Vector3(0.0F, -4.0F, 0.0F));
                    isDown = true;
                } else {
                    controller.Move(new Vector3(0.0F, 4.0F, 0.0F));
                    isDown = false;
                }
        }

    }

    /// <summary>
    /// start "attack" animation and apply damage to objects it collides with
    /// </summary>
    private void ReceiveAttack() {

        if (equipedWeapon && Input.GetButtonDown("Fire1")
            && !gameObject.GetComponent<UseWeapon>().getUsingWeapon()
            && !gameObject.GetComponent<UseWeapon>().weaponAnimator.GetCurrentAnimatorStateInfo(0).IsName("Use_Weapon")) {
            Debug.Log("using weapon");
            gameObject.GetComponent<UseWeapon>().attack();
        }

    }

    public void changePlayerSpeed(float changedValue) {

        speed = changedValue * moveSpeed;

    }

    /// <summary>
    /// avoid unexpected translation or stuck in the wall
    /// </summary>
    public void attackedByZombie() {

        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        transform.Translate(Vector3.back * 3.0F, Space.Self);
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

    }

}