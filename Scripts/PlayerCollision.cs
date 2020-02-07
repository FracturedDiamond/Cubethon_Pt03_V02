
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    public PlayerMovement movement;                     // Reference to our player cube

    void OnCollisionEnter (Collision collisionInfo)     // Function to check for collisions
    {
        if (collisionInfo.collider.tag == "Obstacle")   // Check if player collides with an obstacle
        {
            movement.enabled = false;                   // Disable players movement
            FindObjectOfType<GameManager>().EndGame();  // Call GameManager's "EndGame()" function
        }
    }

}
