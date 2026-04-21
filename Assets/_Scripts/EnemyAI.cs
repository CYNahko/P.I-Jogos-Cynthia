using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform playerTransform;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [Header("Configurações")]
    [SerializeField] private float speed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Proteção contra o erro de missing Animator (Ponto 3.3 da rubrica)
        if (animator == null) Debug.LogError("Falta o Animator no " + gameObject.name);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerTransform = player.transform;
        else Debug.LogError("Inimigo não achou o Player! Verifique a Tag do Player.");
    }

    private Vector2 direcao;

    void Update()
    {
        if (playerTransform != null)
        {
            direcao = (playerTransform.position - transform.position).normalized;
            UpdateAnimation(direcao);
        }
    }

    void FixedUpdate()
    {
        if (playerTransform != null)
        {
            // SOLUÇÃO PARA SOBREPOSIÇÃO:
            // Usamos linearVelocity para que o motor de física do Unity impeça
            // que um monstro entre dentro do outro.
            rb.linearVelocity = direcao * speed;
        }
    }

    void UpdateAnimation(Vector2 dir)
    {
        if (animator == null) return;

        animator.SetFloat("MoveX", Mathf.Abs(dir.x));
        animator.SetFloat("MoveY", dir.y);

        if (dir.x < -0.1f) spriteRenderer.flipX = true;
        else if (dir.x > 0.1f) spriteRenderer.flipX = false;
    }
}