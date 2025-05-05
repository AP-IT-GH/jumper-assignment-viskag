using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float speed;
    private Transform spawnerTransform;
    public Vector3 relativePosition;

    private void Start()
    {
        spawnerTransform = transform.parent;
        relativePosition = spawnerTransform.InverseTransformPoint(transform.position);
    }

    private void Update()
    {
        // Move the obstacle in a straight line
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // Set a random speed for the obstacle
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}