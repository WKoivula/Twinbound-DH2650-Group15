using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Vector3 target;

    private Vector3 lastPosition;
    public Vector3 Velocity { get; private set; }

    void Start()
    {
        target = pointB.position;
        lastPosition = transform.position;
    }

    void Update()
    {
        // Move toward the current target
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // If the platform reaches the target, switch
        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            target = (target == pointA.position) ? pointB.position : pointA.position;
        }

        // Calculate velocity
        Velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }
}