using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlikeringForBulb : MonoBehaviour {

    private float timeForOn;
    private float timeForOff;
    private float counter = 0.0F;
    private bool onTimeFinished = false;
    private bool offTimeFinished = false;
    private bool isOn = true;
    private bool isOff = false;

	// Use this for initialization
	void Start () {

        createRandom();
		
	}
	
	// Update is called once per frame
	void Update () {

        remain();

	}

    private void createRandom() {

        timeForOn = 1.5F * Random.Range(0.5F, 1.0F);
        timeForOff = 1.5F * Random.Range(0.5F, 1.0F);

    }

    /// <summary>
    /// switch the light randomly based on random number to simulate flikering
    /// </summary>
    private void remain() {

        if (!onTimeFinished) {
            if (counter < timeForOn) {
                if (!isOn) {
                    gameObject.GetComponent<Light>().intensity = 3;
                    isOn = true;
                }
                counter += Time.deltaTime;
            } else {
                isOn = false;
                onTimeFinished = true;
                counter = 0.0F;
            }
        } else if (!offTimeFinished) {
            if (counter < timeForOff) {
                if (!isOff) {
                    gameObject.GetComponent<Light>().intensity = 0;
                    isOff = true;
                }
                counter += Time.deltaTime;
            } else {
                isOff = false;
                offTimeFinished = true;
                counter = 0.0F;
            }
        } else {
            onTimeFinished = false;
            offTimeFinished = false;
            createRandom();
        }

    }
}
