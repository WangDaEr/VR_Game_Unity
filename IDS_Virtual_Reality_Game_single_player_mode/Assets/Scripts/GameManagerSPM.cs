using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerSPM : MonoBehaviour {

    public static GameManagerSPM gm;

    public enum scenes { policeStation, healthCentre, supermarket, outside, safehouse };
    public scenes currentScene = scenes.policeStation;
    public GameObject mainCanvas;
    public GameObject gameOverCanvas;
    public GameObject player;
    public GameObject weapon;
    public GameObject axe;
    public GameObject bat;
    public GameObject zombies;
    public Text healthPointsText;
    public Text energyPointsText;
    public Text woodAmountText;
    public Text interactiveMessage;
    public Text changeSceneMessage;

    private PlayerHealth playerHealth;
    private PlayerMaterial playerMaterial;
    private Information information;
    private float detectRange;
    private bool isSpeedSlow = false;
    private bool isSpeedNormal = true;

	// Use this for initialization
	void Start () {

        //obtain necessary information
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (gm == null) {
            gm = gameObject.GetComponent<GameManagerSPM>();
        }
        if (zombies == null) {
            zombies = GameObject.FindGameObjectWithTag("zombies");
        }
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMaterial = player.GetComponent<PlayerMaterial>();
        detectRange = player.GetComponent<PlayerMove>().detectableRange;
        interactiveMessage.enabled = false;
        changeSceneMessage.enabled = false;
        player.GetComponent<PlayerRoateView>().setGameManager(this);
        checkUpdate();

	}
	
	// Update is called once per frame
	void Update () {

        healthPointsText.text = playerHealth.healthPoints.ToString();
        energyPointsText.text = playerHealth.energyPoints.ToString();
        woodAmountText.text = playerMaterial.woodAmount.ToString();
        detectEnergyChange();
        detectHealthChange();
        zombiesDetection();
		
	}

    /// <summary>
    /// check information after a scene switch such as whether player is equipped with an axe
    /// </summary>
    private void checkUpdate () {
   
        if (GameObject.Find("Information") == null) {
            return;
        }
        information = GameObject.Find("Information").GetComponent<Information>();
        information.player = player;
        information.updateInformation();
        if (information.playerInformation.getEquipedWeapon()){
            equipWeapon();
        }

    }

    /// <summary>
    /// scene switch from inside scene to outside scene
    /// </summary>
    public void changeScene() {

        SceneManager.LoadScene("Outside_Scene", LoadSceneMode.Single);
        information.registerInformation();
        if (information.playerInformation.getEquipedWeapon()) {
            equipWeapon();
        }
        Debug.Log("re.........");

    }

    /// <summary>
    /// scene switch from outside to inside
    /// </summary>
    /// <param name="sceneName"></param>
    public void enterHouse(string sceneName) {

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        information.registerInformation();
        if (information.playerInformation.getEquipedWeapon()){
            equipWeapon();
        }
        Debug.Log("re.........");

    }

    /// <summary>
    /// change the velocity of player according to his energy points.
    /// </summary>
    private void detectEnergyChange() {

        if (!isSpeedSlow && playerHealth.energyPoints < 30) {
            player.GetComponent<PlayerMove>().changePlayerSpeed(0.5F);
            isSpeedSlow = true;
            isSpeedNormal = false;
        } else if (!isSpeedNormal && playerHealth.energyPoints >= 30) {
            player.GetComponent<PlayerMove>().changePlayerSpeed(1F);
            isSpeedSlow = false;
            isSpeedNormal = true;
        }

    }


    /// <summary>
    /// check whether game is failed and prompt relative information
    /// </summary>
    private void detectHealthChange()
    {

        if (playerHealth.healthPoints <= 0)
        {
            player.GetComponent<PlayerMove>().enabled = false;
            player.GetComponent<PlayerRoateView>().enabled = false;
            zombies.SetActive(false);
            mainCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);
        }

    }

    /// <summary>
    /// check whether player is detected by zombies and notify corresponding zombies
    /// </summary>
    private void zombiesDetection(){

        float distance;
        foreach (Transform zombie in zombies.transform) {
            Debug.Log("zombie detection: " + zombie.gameObject.name + " " + detectRange);
            distance = Vector3.Distance(player.transform.position, zombie.position);
            if (distance <= detectRange && zombie.GetComponent<RandomMoveForZombies>().enabled) {
                zombie.GetComponent<RandomMoveForZombies>().enabled = false;
                zombie.GetComponent<Chaser>().enabled = true;
                Debug.Log("chasing player: " + zombie.gameObject.name);
            } else if (distance > detectRange && zombie.GetComponent<Chaser>().enabled) {
                zombie.GetComponent<RandomMoveForZombies>().enabled = true;
                zombie.GetComponent<Chaser>().enabled = false;
            }
        }

    }

    /// <summary>
    /// acquire weapon from police office
    /// </summary>
    public void equipWeapon() {

        Instantiate(axe, player.transform.GetChild(0).Find("Weapon"));

    }

    public void restorePlayerHealth(BounceHealthOrEnergy source) {

        source.restore(player.GetComponent<PlayerHealth>());

    }

}
