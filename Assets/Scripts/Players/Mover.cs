using UnityEngine;
using System.Collections;
using static HealthSystem;


public class Mover : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 5f;

    [Header("Player Settings")]
    [SerializeField] private int playerIndex = 0;

    [Header("Cooldown Settings")]
    [SerializeField] private float cooldownTime = 3f;
    private float cooldownTimer = 0f;
    [SerializeField] private float fireCooldownDuration = 2f; // Default duration for firing cooldown
    [SerializeField] private float postFireCooldownDuration = 1f; // Default duration after stopping firing

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

    private bool isFireEnabled = true;

    private bool isFiring = false;

    private Coroutine fireCooldownCoroutine; // Added coroutine reference

    public HealthBarScript healthBar;
    public HealthSystem healthsystem = new HealthSystem();

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        shield = GetComponent<Shield>(); // Get the Shield script component
        animator = GetComponent<Animator>(); // Get the Animator component
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

            // Trigger the jump animation
            animator.SetBool("isJumping", true);


        }
    }

    public void Fire(bool isFiring)
    {
        this.isFiring = isFiring;

        // Check if the player is grounded before allowing firing
        if (isGrounded && isFireEnabled)
        {
            // Check if the fire button is being pressed
            if (isFiring)
            {
                // Activate the bullet prefab
                gameObject.transform.GetChild(0).gameObject.SetActive(true);

                animator.SetBool("isFiring", true);

                // Start a coroutine to disable firing after the specified duration
                if (fireCooldownCoroutine != null)
                {
                    StopCoroutine(fireCooldownCoroutine);
                }
                fireCooldownCoroutine = StartCoroutine(DisableFireAfterDuration(fireCooldownDuration));
            }
            else
            {
                // Deactivate the bullet prefab when the fire button is released
                gameObject.transform.GetChild(0).gameObject.SetActive(false);

                animator.SetBool("isFiring", false);
            }
        }
    }


    private IEnumerator DisableFireAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        // Deactivate firing after the specified duration
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        animator.SetBool("isFiring", false);

        // Wait for an additional duration before enabling fire again
        yield return new WaitForSeconds(postFireCooldownDuration);

        isFireEnabled = true;
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
        // Check if the player is firing
        if (isFiring && isFireEnabled)
        {
            // If firing animation is playing, freeze the X-axis movement
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        else
        {
            // Player can move if not firing
            moveDirection = new Vector2(inputVector.x, inputVector.y);
            moveDirection.Normalize();

            // Set the velocity only along the Y-axis to allow jumping
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

            // If not firing or fire is disabled, set firing animation to false
            animator.SetBool("isFiring", false);
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

            animator.SetBool("isJumping", false);

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
        if (other.CompareTag("Bullet")) // Updated tag check
        {
            TakeDamage();
            // Destroy the bullet
            Destroy(other.gameObject);

            // Handle bullet destruction and shield interaction
            HandleBulletInteraction();
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
        //currentHealth -= 1; // Deduct 1 health point


        healthsystem.setcurrentHealth();

        animator.SetTrigger("takingDamage");

        // Set the "isDead" parameter to true when the player's health reaches zero
        if (healthsystem.getcurrentHealth() <= 0)
        {
            animator.SetBool(isDead, true);
            transform.position = respawnPoint;
            healthsystem.resethealth();

            // You can add more logic like respawning the player or triggering a game over screen.
        }
    }

    private void HandleBulletInteraction()
    {
        if (shield != null && shield.currentHits > 0)
        {
            // Bullet hit the shield
            shield.HandleBulletHit();
        }
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