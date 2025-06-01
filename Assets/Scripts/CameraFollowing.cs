using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public float followSpeed = 2f;
    public float yOffset = 1.5f;

    private float zPos = -10;

    public Transform target;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3((target.position.x + 5), target.position.y + yOffset, zPos);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
