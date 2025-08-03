using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CorrectPosition : MonoBehaviour
{
    public bool isCorrectPosition = false;
    GameObject player;
    public float speed = 0.1f; // Speed at which the player moves to the correct position
    public PuzzleManager puzzleManager;

    private void Start()
    {
        puzzleManager.nextSceneUi.SetActive(false);
    }

    private void Update()
    {
        MoveToSolution();
    }



    private void MoveToSolution()
    {
        if (isCorrectPosition)
        {
            // If the player is in the correct position, you can add any additional logic here
            Debug.Log("Player is in the correct position.");
            player.transform.position = Vector3.MoveTowards(player.transform.position,transform.position,speed);
            //player.transform.forward = Vector3.MoveTowards(player.transform.forward ,transform.forward,speed*0.1f);

            //manage win condition
            //show ui
            puzzleManager.nextSceneUi.SetActive(true);
            //lock cursor and hide it
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("correct position");
            isCorrectPosition = true;

            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("false position");
            isCorrectPosition = false;
        }
    }

}
