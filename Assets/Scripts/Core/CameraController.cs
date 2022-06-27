using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Room camera
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    //Follow player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;

    //Camera bounds
    [SerializeField] private float maxY;

    private float minY;
    private float lookAhead;

    private void Start()
    {
        minY = transform.position.y;
    }

    private void Update()
    {
        //Room camera
        // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);

        //Follow player
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(player.position.y,minY, maxY), transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        print("here");
        currentPosX = _newRoom.position.x;
    }
}
