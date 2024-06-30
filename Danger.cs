using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Danger : MonoBehaviour
{
    public GameObject teleportTarget; //The variable for the teleport position
    public CharacterController Player; //The variable for the teleporting player

    private void OnTriggerEnter(Collider collider) //Trigger for teleportation
    {
        if (collider.tag == "Player")
        {
            Debug.Log("Danger");
            //Player.transform.position = teleportTarget.transform.position;
            Manager.Lives(-1);
            Manager.lifeLost = true;
            SceneManager.LoadScene("Level01 Scene");
        }
            //Trigger makes one position equal to another
    }
}
