using System.Collections;
using UnityEngine;

public class BounceHealthOrEnergy : MonoBehaviour {

    /// <summary>
    /// different type for different interactive items
    /// </summary>
    public enum selection { increaseHealth, increaseEnergy };
    public selection genre = selection.increaseHealth;
    public int value = 0;

    public void restore(PlayerHealth playerHealth) {

        switch (genre) {
            case selection.increaseHealth:
                playerHealth.restoreHealth(value);
            break;
            case selection.increaseEnergy:
                playerHealth.restoreEnergy(value);
            break;
        }

        Destroy(gameObject);

    }

}
