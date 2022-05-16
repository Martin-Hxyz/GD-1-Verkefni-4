using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlatformerPlayerController : MonoBehaviour
{
    public float moveSpeedMultiplier = 3.0f;
    public float jumpMultiplier = 1.0f;
    public float maxHealth = 10.0f;
    public bool hasKey;
    public LayerMask groundMask;
    public GameObject bulletPrefab;

    private Transform m_Transform;
    private Rigidbody2D m_Rigibody;
    private BoxCollider2D m_Collider;
    private Animator m_Animator;

    private float m_MoveHorizontal;
    private Vector2 m_Forward;

    private bool m_GroundCheckEnabled = true;
    private bool m_Grounded = true;
    private bool m_Jumping;

    private float m_Health;

    void Start()
    {
        m_Transform = GetComponent<Transform>();
        m_Collider = GetComponent<BoxCollider2D>();
        m_Rigibody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_Health = maxHealth;
    }

    private void Update()
    {
        m_MoveHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeedMultiplier;

        // Raycast sem tjekkar hvort playerinn standi á eih eða sé í loftinu
        UpdateGrounded();

        if (Input.GetKeyDown(KeyCode.Space) && m_Grounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleInteract();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Shoot();
        }

        UpdateAnimation();

        // tapar ef tu dettur af mappinu
        if (m_Transform.position.y < -25)
        {
            Die();
        }
    }


    private void FixedUpdate()
    {
        m_Rigibody.velocity = new Vector2(m_MoveHorizontal, m_Rigibody.velocity.y);
    }

    private void Shoot()
    {
        Vector3 origin = m_Rigibody.worldCenterOfMass + m_Forward * 0.5f;
        var bullet = Instantiate(bulletPrefab, origin, Quaternion.identity);
        var rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(m_Forward * 1400f);
    }

    private void Jump()
    {
        m_Rigibody.AddForce(Vector2.up * jumpMultiplier, ForceMode2D.Impulse);
    }

    private void UpdateGrounded()
    {
        // raycast til að gá hvort player sé á jörðinni eða í loftinu

        var rayOrigin = m_Collider.bounds.min;
        var rayDirection = Vector2.down;
        var raycast = Physics2D.Raycast(rayOrigin, rayDirection, 0.1f, groundMask);
        m_Grounded = raycast.collider != null;
    }

    private void HandleInteract()
    {
        // raycast til að gá hvort það sé eih fyrir framan ruby
        // interactable er enn og aftur abstract klasi sem er MonoBehavior
        var rayDirection = m_Forward;
        var rayOrigin = m_Transform.position + (Vector3.up * 0.75f);
        var raycast = Physics2D.Raycast(rayOrigin, rayDirection, 0.85f);
        if (raycast.collider == null) return;
        var interactable = raycast.collider.gameObject.GetComponent<Interactable>();
        if (interactable == null) return;
        interactable.Interact(gameObject);
    }

    private void UpdateAnimation()
    {
        // animatorinn notar speed til að ákveða hvort á að sýna idle eða running animation
        m_Animator.SetFloat("Speed", Mathf.Abs(m_MoveHorizontal));

        // Snýr ruby við
        if (m_MoveHorizontal < 0)
        {
            m_Forward = Vector2.left;
            m_Transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (m_MoveHorizontal > 0)
        {
            m_Forward = Vector2.right;
            m_Transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }


    public void ChangeHealth(int amount)
    {
        m_Health = Mathf.Clamp(m_Health + amount, 0, maxHealth);
        GameManager.Instance.UpdateMask(m_Health / maxHealth);

        if (m_Health < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.Instance.GameOver();
        Destroy(gameObject);
    }
}