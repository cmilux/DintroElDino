using System.Net;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;     //speed of platforms
    public int startingPoint;       //starting index (platform's position)
    public Transform[] points;      //array of the transform points

    int i;  //index of the array

    private void Start()
    {
        transform.position = points[startingPoint].position;
    }

    private void Update()
    {
        //checking the distance of the platform n point
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;        //increase the index

            if (i == points.Length)     //check if the platform was on the last point after the index increase
            {
                i = 0;      //reset the index
            }
        }

        //moving the platform to the point position with the index "i"
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform, true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null, true);
    }

    private void OnDrawGizmos()
    {
        //debug visualization
        if (points[0] != null && points[1] != null)
        {
            Gizmos.DrawLine(transform.position, points[0].position);
            Gizmos.DrawLine(transform.position, points[1].position);
        }
    }
}
/*
 * 
 * OPCION 1
 * 
public float speed;

int direction = 1;

public Transform platform;
public Transform startPoint;
public Transform endPoint;

// Update is called once per frame
void Update()
{
    Vector2 target = currentMovementTarget();

    platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime);

    float distance = (target - (Vector2)platform.position).magnitude;   

    if (distance <= 0.1)
    {
        direction *= -1;
    }

}

Vector2 currentMovementTarget()
{
    if (direction == 1)
    {
        return startPoint.position;
    }
    else
    {
        return endPoint.position;
    }
}

private void OnDrawGizmos()
{
    //debug visualization
    if (platform != null && startPoint != null && endPoint != null)
    {
        Gizmos.DrawLine(platform.transform.position, startPoint.position);
        Gizmos.DrawLine(platform.transform.position, endPoint.position);
    }
}
*/
