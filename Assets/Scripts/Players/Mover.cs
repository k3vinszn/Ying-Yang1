using UnityEngine;
using System.Collections;


public class Mover : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 5f;

    [Header("Player Settings")]
    [SerializeField] private int playerIndex = 0;
    [SerializeField] private int health = 10;

    [Header("Cooldown Settings")]
    [SerializeField] private float cooldownTime = 3f;
    private float cooldownTimer = 0f;

    [Header("Components")]
    private Rigidbody2D rb;
    private Shield shield;

    [Header("Input and Direction")]
    private Vector2 moveDirection = Vector2.zero;
    private Vector2 inputVector = Vector2.zero;

    [Header("Bullet Settings")]
    private GameObject currentBullet;
    private bool isBulletActive = false;

    [Header("Ground Check")]
    private bool isGrounded = true;

    [Header("Respawn and Checkpoints")]
    private Vector3 respawnPoint;

    [Header("Facing Direction")]
    private bool isFacingRight = true;

    [Header("Fall Detection")]
    public GameObject fallDetector;

    [Header("Animation")]
    private Animator animator;
    private bool isRunning = false;
    private static readonly int isDead = Animator.StringToHash("isDead"); // Added Animator parameter hash
    private static readonly int isJumping = Animator.StringToHash("isJumping"); // Added Animator parameter hash

    [Header("Player Life")]
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int currentHealth;

    private bool isFireEnabled = true;
    private Coroutine fireCooldownCoroutine; // Added coroutine reference

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        shield = GetComponent<Shield>(); // Get the Shield script component
        animator = GetComponent<Animator>(); // Get the Animator component
        currentHealth = maxHealth;
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
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;

            // Set the "isJumping" parameter to true in the Animator
            animator.SetBool(isJumping, true);
        }
    }

    public void Fire(bool isFiring)
    {
        if (isFireEnabled)
        {
            // Check if the fire button is being pressed
            if (isFiring)
            {
                // Activate the bullet prefab
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                // Deactivate the bullet prefab when the fire button is released
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && currentBullet != null)
        {
            Destroy(currentBullet);
            isBulletActive = false;
        }
    }

    void Update()
    {
        moveDirection = new Vector2(inputVector.x, inputVector.y);
        moveDirection.Normalize();

        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

        // Set the isRunning boolean based on whether the player is moving or not
        isRunning = Mathf.Abs(moveDirection.x) > 0.1f;

        // Update the animator parameter for the run animation
        animator.SetBool("isRunning", isRunning);

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

        if (shield != null && shield.currentHits <= 0)
        {
            DisableFire();
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

            // Set the "isJumping" parameter to false in the Animator
            animator.SetBool(isJumping, false);
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
        if (other.gameObject.CompareTag("Bullet")) // Updated tag check
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

    public void TakeDamage()
    {
        currentHealth -= 1; // Deduct 1 health point
        Debug.Log("Player Health: " + currentHealth);

        // Set the "isDead" parameter to true when the player's health reaches zero
        if (currentHealth <= 0)
        {
            animator.SetBool(isDead, true);
            Debug.Log("Player is defeated!");
            // You can add more logic like respawning the player or triggering a game over screen.
        }
    }

    public void DisableFire()
    {
        isFireEnabled = false;
        if (fireCooldownCoroutine != null)
        {
            StopCoroutine(fireCooldownCoroutine);
        }
        fireCooldownCoroutine = StartCoroutine(EnableFireAfterCooldown());
    }

    private IEnumerator EnableFireAfterCooldown()
    {
        float waitTime = 3f; // Store the wait time in a local variable
        yield return new WaitForSeconds(waitTime);

        isFireEnabled = true;
        shield.ResetHits(); // Reset shield hits when re-enabling fire
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        // Flip the player's sprite
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}