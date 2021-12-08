using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrigger : MonoBehaviour
{
    private int trigger = 0;
    private GameScript gameScript;

    private void Awake()
    {
        gameScript = GameObject.FindObjectOfType<GameScript>();
    }

    void OnTriggerEnter(Collider other)
    {
            gameScript.EndGame();
    }
}