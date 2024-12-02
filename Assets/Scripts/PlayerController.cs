using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento del jugador
    private Animator animator; // Referencia al Animator
    private Rigidbody2D rb; // Referencia al Rigidbody2D
    private SpriteRenderer spriteRenderer; // Para espejar el sprite

    void Start()
    {
        // Obtener referencias al Animator, Rigidbody2D y SpriteRenderer
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Validación para evitar errores
        if (rb == null)
            Debug.LogError("Rigidbody2D no encontrado.");
        if (animator == null)
            Debug.LogError("Animator no encontrado.");
        if (spriteRenderer == null)
            Debug.LogError("SpriteRenderer no encontrado.");
    }

    void Update()
    {
        // Capturar entrada del teclado (WASD o flechas)
        float moveX = Input.GetAxisRaw("Horizontal"); // Movimiento lateral (-1, 0, 1)
        float moveY = Input.GetAxisRaw("Vertical");   // Movimiento vertical (-1, 0, 1)

        // Movimiento del jugador
        Vector2 movement = new Vector2(moveX, moveY).normalized; // Normalizar para evitar velocidad alta en diagonales
        rb.velocity = movement * speed;

        // Actualización de animaciones
        if (moveY > 0) // Mover hacia arriba (W)
        {
            animator.SetTrigger("WalkBack");
            animator.ResetTrigger("WalkFront");
            animator.ResetTrigger("WalkSide");
        }
        else if (moveY < 0) // Mover hacia abajo (S)
        {
            animator.SetTrigger("WalkFront");
            animator.ResetTrigger("WalkBack");
            animator.ResetTrigger("WalkSide");
        }
        else if (moveX != 0) // Movimiento lateral (A/D)
        {
            animator.SetTrigger("WalkSide");
            animator.ResetTrigger("WalkFront");
            animator.ResetTrigger("WalkBack");

            // Espejar el sprite según la dirección
            spriteRenderer.flipX = (moveX < 0); // Espeja a la izquierda si moveX es negativo
        }
        else // Sin movimiento (Idle)
        {
            animator.ResetTrigger("WalkFront");
            animator.ResetTrigger("WalkBack");
            animator.ResetTrigger("WalkSide");
        }
    }
}
