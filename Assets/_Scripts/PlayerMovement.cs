using System;
using UnityEngine;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private AudioSource audioSource;
    private Vector2 movement;

    [Header("Configurações")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float speed = 2f;
    private float hitCooldown = 1f;
    private float lastHitTime = -10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (gameManager == null)
        {
            Debug.LogError("ERRO: Arraste o GameManager para o Player no Inspector!");
        }
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        movement = new Vector2(moveHorizontal, moveVertical).normalized;

        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        if (animator == null) return;

        if (movement.magnitude > 0.1f)
        {
            animator.SetFloat("MoveX", Mathf.Abs(movement.x));
            animator.SetFloat("MoveY", movement.y);

            if (movement.x < -0.1f) spriteRenderer.flipX = true;
            else if (movement.x > 0.1f) spriteRenderer.flipX = false;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime * speed);
    }

    // MANTEMOS este para o Coletável (que deve continuar como Is Trigger)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coletavel"))
        {
            if (audioSource != null) audioSource.Play();
            if (gameManager != null) gameManager.AddTime(5f);
            
            ReposicionarColetavel(collision.gameObject);
        }
    }

    // ADICIONAMOS este para o Inimigo (que agora é sólido para não fundir)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            // Sistema de Cooldown para não perder todo o tempo em 1 segundo
            if (Time.time > lastHitTime + hitCooldown)
            {
                if (audioSource != null) audioSource.Play();
                
                if (gameManager != null) 
                {
                    gameManager.RemoveTime(5f); 
                    Debug.Log("Bateu no monstro! Tempo reduzido.");
                }
                
                Vector2 direcaoDano = (transform.position - collision.transform.position).normalized;
                rb.AddForce(direcaoDano * 10f, ForceMode2D.Impulse); 

                lastHitTime = Time.time;

            }
        }
    }

    void ReposicionarColetavel(GameObject coletavel)
    {
        float novoX = UnityEngine.Random.Range(-6.2f, 6.2f);
        float novoY = UnityEngine.Random.Range(-3.2f, 3.2f);
        coletavel.transform.position = new Vector2(novoX, novoY);
    }

}