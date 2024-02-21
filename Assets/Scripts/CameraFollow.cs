using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset; //offset for the camera from the center of the player
    public float damping; //how fast to catch up with the player

    private Transform playerTarget;
    private Vector3 velocity = Vector3.zero;
    PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        if(player != null)
        {
            playerTarget = player.transform;
        }
    }
    private void Update()
    {
        if (playerTarget != null)
        {
            if (playerTarget.position != null && transform.position != null)
            {
                Vector3 targetPosition = playerTarget.position + offset;
                targetPosition.z = transform.position.z;
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, damping); // to make the camera t follow smoothly
            }         
        }

    }
}
