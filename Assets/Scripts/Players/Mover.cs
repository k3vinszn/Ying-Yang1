using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed = 3f;

    [SerializeField]
    private float JumpForce = 5f;

    [SerializeField]
    private int playerIndex = 0;

    [SerializeField]
    private int health = 10;

    private Rigidbody2D rb;

    public float cooldownTime = 3f; // Cooldown time in seconds
    private float cooldownTimer = 0f; // Timer to keep track of cooldown

    private Vector2 moveDirection = Vector2.zero;
    private Vector2 inputVector = Vector2.zero;

    private bool isGrounded = true;

    public GameObject bulletPrefab;
    public Transform firePoint;
    private GameObject currentBullet;
    private bool isBulletActive = false;
    private Vector3 respawnPoint;
    public GameObject fallDetector;

    private bool isFacingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;

    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    public void Fire(bool isFiring)
    {
        if (cooldownTimer > 0)
        {
            // If cooldown is active, decrement the timer and return without firing
            cooldownTimer -= Time.deltaTime;
            return;
        }

        if (isFiring && !isBulletActive)
        {
            currentBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            isBulletActive = true;
        }
        else if (!isFiring && currentBullet != null)
        {
            Destroy(currentBullet);
            isBulletActive = false;
            StartCooldown(); // Start the cooldown when the bullet is destroyed
        }
    }

    void StartCooldown()
    {
        cooldownTimer = cooldownTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && currentBullet != null)
        {
            // If the bullet collides with an object with the "Bullet" tag, start cooldown
            StartCooldown();
            Destroy(currentBullet);
            isBulletActive = false;
        }
    }

    void Update()
    {
        moveDirection = new Vector2(inputVector.x, inputVector.y);
        moveDirection.Normalize();

        rb.velocity = new Vector2(moveDirection.x * MoveSpeed, rb.velocity.y);

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);


        // Flip the player and the fire position if moving left
        if (moveDirection.x < 0 && isFacingRight)
        {
            Flip();
        }
        // Flip back if moving right
        else if (moveDirection.x > 0 && !isFacingRight)
        {
            Flip();
        }

        // Set the position of the bullet to the fire point's position if a bullet is active
        if (currentBullet != null)
        {
            currentBullet.transform.position = firePoint.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.layer == LayerMask.NameToLayer("PlayerBranco") && collision.gameObject.layer == LayerMask.NameToLayer("PlataformaPreta"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        else if (gameObject.layer == LayerMask.NameToLayer("PlayerBranco") && collision.gameObject.layer == LayerMask.NameToLayer("ParedePreta"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        else if (gameObject.layer == LayerMask.NameToLayer("PlayerPreto") && collision.gameObject.layer == LayerMask.NameToLayer("PlataformaBranca"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>()); 
        }
        else if (gameObject.layer == LayerMask.NameToLayer("PlayerPreto") && collision.gameObject.layer == LayerMask.NameToLayer("ParedeBranca"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        else if (gameObject.layer == LayerMask.NameToLayer("PlayerPreto") && collision.gameObject.layer == LayerMask.NameToLayer("PlayerBranco"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.collider.CompareTag("Key"))
        {
            KeyCollectible key = collision.collider.GetComponent<KeyCollectible>();

            if (key != null)
            {
                key.CollectKey();

                // Assuming the final door is tagged as "FinalDoor"
                GameObject finalDoor = GameObject.FindGameObjectWithTag("FinalDoor");

                if (finalDoor != null)
                {
                    FinalDoorBehavior finalDoorBehavior = finalDoor.GetComponent<FinalDoorBehavior>();

                    if (finalDoorBehavior != null)
                    {
                        finalDoorBehavior.keysCollected++;
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet")) // Updated tag check
        {
            TakeDamage(); // Call the TakeDamage method when hit by a bullet
            Destroy(other.gameObject); // Destroy the bullet
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (other.gameObject.CompareTag("Fall Detector"))
        {
            transform.position = respawnPoint;
        }
        else if (other.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
        }

    }

    public void TakeDamage() // Change to public
    {
        health -= 1; // Deduct 1 health point
        Debug.Log("Player Health: " + health);

        if (health <= 0)
        {
            // Perform any actions when the player's health reaches zero (e.g., respawn logic)
            Debug.Log("Player is defeated!");
            // You can add more logic like respawning the player or triggering a game over screen.
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        // Flip the player's sprite
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;

        // Flip the firePoint
        Vector3 firePointScale = firePoint.localScale;
        firePointScale.x *= -1f;
        firePoint.localScale = firePointScale;
    }
}