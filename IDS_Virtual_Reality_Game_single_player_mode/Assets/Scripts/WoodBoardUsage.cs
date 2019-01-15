using UnityEngine;
using UnityEngine.UI;

public class WoodBoardUsage : MonoBehaviour {

    public GameObject player;
    public GameObject theOtherHole;
    public Text messageForBoard;
    public int currentActiveBoard = 0;
    public bool isSetMessage = false;
    private bool holeFixed = false;
    private bool isMessageActive = false;
    private int ActivedChildCount;
    private Vector3 pivot;
    private Vector3 playerPosition;

	// Use this for initialization
	void Start () {

        pivot = transform.GetChild(0).position;
        playerPosition = player.transform.position;
        pivot.y = 0.0F;
        playerPosition.y = 0.0F;

	}
	
	// Update is called once per frame
	void Update () {
        playerPosition = player.transform.position;
        playerPosition.y = 0;
        checkPlayerDirection();
	}

    /// <summary>
    /// checking whether player is around and looking at the hole on the wall
    /// </summary>
    private void checkPlayerDirection () {

        Vector3 temp = player.transform.forward;
        temp.y = 0;
        float distance_XZ = Mathf.Pow((playerPosition.x - pivot.x), 2.0F) + Mathf.Pow((playerPosition.z - pivot.z), 2.0F);
        float angle = Vector3.Angle((pivot - playerPosition), temp);
        if (distance_XZ <= 100.0F && angle <= 30.0F) {           
            isMessageActive = true;
            theOtherHole.GetComponent<WoodBoardUsage>().isSetMessage = true;
            attachingWoodBoard();            
        } else {
            if (theOtherHole.GetComponent<WoodBoardUsage>().isSetMessage) {
                theOtherHole.GetComponent<WoodBoardUsage>().isSetMessage = false;
            }
            isMessageActive = false;
        }
        displayMessage();

    }

    /// <summary>
    /// prompt for fixing the hole
    /// </summary>
    private void displayMessage() {

        if (isSetMessage) {
            return;
        } else if (holeFixed && theOtherHole.GetComponent<WoodBoardUsage>().isSetMessage) {
            messageForBoard.enabled = false;
            return;
        }

        if (isMessageActive && !messageForBoard.enabled) {
            messageForBoard.enabled = true;
        } else if (!isMessageActive && messageForBoard.enabled) {
            messageForBoard.enabled = false;
        }

    }

    /// <summary>
    /// attach wood board to the hole
    /// </summary>
    private void attachingWoodBoard () {

        int count = 0;
        if (Input.GetButtonDown("Interact") && player.GetComponent<PlayerMaterial>().woodAmount > 0) {
            foreach (Transform board in transform) {
                if (!board.gameObject.activeSelf) {
                    board.gameObject.SetActive(true);
                    holeFixed = (count == 4);
                    break;
                }
                count += 1;
            }
            currentActiveBoard = count;
            player.GetComponent<PlayerMaterial>().woodAmount -= 1;
        }

    }
}
