using UnityEngine;

public class IdleFloat : MonoBehaviour
{
    public float amplitude = 0.5f; // Maximum height the player moves up or down
    public float speed = 2.0f; // Speed of the up and down motion

    private Vector3 initialPosition; // Store the initial position to keep the movement relative

    void Start()
    {
        // Record the initial position of the player
        initialPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new Y position using a sine wave for smooth up and down motion
        float newY = initialPosition.y + Mathf.Sin(Time.time * speed) * amplitude;

        // Apply the new position to the player, keeping X and Z the same
        transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }
}
