using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public float followSpeed = 2f;
    public float yOffset = 2f;

    private readonly float zPos = -10;

    public Transform target;

    public PlayerController playerController;
    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraPosition();
    }

    public void CameraPosition()
    {
        Vector3 newPos = new((target.position.x + 7), 0 + yOffset, zPos);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);

        if (target.transform.position.y >= 5)
        {
            Vector3 offScreenPos = new((target.position.x + 7), 6 + yOffset, zPos);
            transform.position = Vector3.Slerp(transform.position, offScreenPos, followSpeed * Time.deltaTime);
        }

        if (playerController.horizontalInput >= -1)
        {
            Vector3 backwardsPos = new(target.position.x, 0 + yOffset, zPos);
            transform.position = Vector3.Slerp(transform.position, backwardsPos, followSpeed * Time.deltaTime);
        }

    }
}
