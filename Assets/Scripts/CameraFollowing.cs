using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    private float followSpeed = 2f;                  //Follow player with camera speed
    private float yOffset = 1.5f;                      //Vertical offset fot the camera's position
    private float xForwardOffset = 7f;               //How far ahead of the player the camera should look
    private float yTriggerHeight = 5f;               //Height threshold to move camera vertically when jumping
    private float jumpCameraYOffset = 6f;            //Extra Y offset when player jumps high

    private readonly float zPos = -10;              //Fixed z-pos for the cam

    public Transform target;                        //Player's transform

    private PlayerController playerController;      //PlayerController script
   
    private void Start()
    {
        //Gets the playerController script
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        //Calls this method every frame
        CameraPosition();
    }

    public void CameraPosition()
    {
        //Base camera position a bit ahead of player on x-axis and with y-axis offset
        Vector3 newPos = new((target.position.x + xForwardOffset), yOffset, zPos);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);

        //If player jumps or is high on the y-axis, move the camera higher
        if (target.transform.position.y >= yTriggerHeight)
        {
            Vector3 offScreenPos = new((target.position.x + xForwardOffset), jumpCameraYOffset + yOffset, zPos);
            transform.position = Vector3.Slerp(transform.position, offScreenPos, followSpeed * Time.deltaTime);
        }

        //If player moves to the left, update camera to stay centered
        if (playerController.horizontalInput >= 0)
        {
            Vector3 backwardsPos = new(target.position.x, yOffset, zPos);
            transform.position = Vector3.Slerp(transform.position, backwardsPos, followSpeed * Time.deltaTime);
        }

    }
}
