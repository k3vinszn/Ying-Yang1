using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private GameObject currentBullet;
    private bool isBulletActive = false; // Variable to check if a bullet is currently active

    public float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    public bool isFacingRight = true;

    void Update()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }

        // Set the position of the shield (bullet) in front of the player if a bullet is active
        if (currentBullet != null)
        {
            Vector3 bulletPosition = firePoint.position;
            bulletPosition.x += horizontal * 0.5f; // Adjust the offset based on the player's movement
            currentBullet.transform.position = bulletPosition;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed && !isBulletActive) // Check if the fire button is pressed and no bullet is active
        {
            currentBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            isBulletActive = true; // Set the flag to indicate that a bullet is active
        }
        else if (context.canceled && currentBullet != null) // Check if the fire button is released and there's a bullet active
        {
            Destroy(currentBullet);
            isBulletActive = false; // Reset the flag when the bullet is destroyed
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
