using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    private float _followSpeed = 2f;              //Follow player camera speed
    private float _yOffset = 1.5f;                  //Vertical offset for the cam pos

    private readonly float _zPos = -10;          //Fixed z-pos of cam

    public Transform target;                    //Player's transform reference

    private PlayerController _playerController;  //Gets the PlayerController script
   
    private void Start()
    {
        //Gets the PlayerController script
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        //Calls every frame
        CameraPosition();
    }

    public void CameraPosition()
    {
        //Base camera position a bit ahead of the player on x-axis, and with y-axis offset
        Vector3 newPos = new((target.position.x + 7), _yOffset, _zPos);
        transform.position = Vector3.Slerp(transform.position, newPos, _followSpeed * Time.deltaTime);

        //If the player jumps or is high on the Y axis, move camera higher
        if (target.transform.position.y >= 5)
        {
            Vector3 offScreenPos = new((target.position.x + 7), 6 + _yOffset, _zPos);
            transform.position = Vector3.Slerp(transform.position, offScreenPos, _followSpeed * Time.deltaTime);
        }

        //If the player is moving to the left, update camera to stay centered
        if (_playerController.horizontalInput >= -1)
        {
            Vector3 backwardsPos = new(target.position.x, _yOffset, _zPos);
            transform.position = Vector3.Slerp(transform.position, backwardsPos, _followSpeed * Time.deltaTime);
        }

    }
}