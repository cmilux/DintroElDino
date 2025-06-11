using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;             //Platforms speed movement

    public int startingPoint;       // Index of the point where the platform starts (set from Inspector)
    int i;                          // Internal index used to track the current target point during movement

    public Transform[] points;      //Array of the transform points where the platform moves

    private void Start()
    {
        transform.position = points[startingPoint].position; // Set platform's initial position
        i = startingPoint;                                   // Initialize internal index to start moving from the starting point
    }

    private void Update()
    {
        //Call this methods every frame
        MoveTowardsCurrentPoint();
        MoveToTheNextPoint();

    }
    void MoveTowardsCurrentPoint()
    {
        //Move platform to the point position with the index "i" at the given speed
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    void MoveToTheNextPoint()
    {
        //Check if platform is close enough to current target point
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;        //Move to next point index (increase)

            if (i == points.Length)     //Check if the platform was on the last point after the index increase
            {
                i = 0;      //Reset the index
            }
        }
    }

    //Sets the player as a child objet when player jumps on it so it moves along
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform, true);
        }
    }

    //Sets the player off from being a child object
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null, true);
        }
    }


    //Debug visualization of the connection points
    private void OnDrawGizmos()
    {
        if (points[0] != null && points[1] != null)
        {
            Gizmos.DrawLine(transform.position, points[0].position);
            Gizmos.DrawLine(transform.position, points[1].position);
        }
    }
}
