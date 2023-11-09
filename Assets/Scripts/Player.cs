using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private string inputNameHorizontal;
    [SerializeField] private string inputNameVertical;

    [SerializeField] private Color color;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float inputHorizontal;
    private float inputVertical;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;
    }

    private void Update()
    {
        inputHorizontal = Input.GetAxisRaw(inputNameHorizontal);
        inputVertical = Input.GetAxisRaw(inputNameVertical);
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(inputHorizontal, inputVertical).normalized;
        rb.velocity = movement * speed * Time.fixedDeltaTime;
    }
}
