using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] GameUI gameui;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameui.CheckGameState(GameUI.GameState.GameClear);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
