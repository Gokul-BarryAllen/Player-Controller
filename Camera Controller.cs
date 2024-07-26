using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private float direction = 0f;
    public float offset;
    public float offsetSmoothing;
    private Vector3 playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            playerPosition = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
            direction = Input.GetAxis("Vertical");
            if(direction > 0f)
            {
                playerPosition = new Vector3(playerPosition.x, playerPosition.y + offset, playerPosition.z);
            }        
            if(direction < 0f)
            {
                playerPosition = new Vector3(playerPosition.x, playerPosition.y - offset, playerPosition.z);
            }

            transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }
}
