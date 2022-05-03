using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public AudioClip launchSound;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // beitir afli á projectileið
    public void Launch(Vector2 direction, float force)
    {
        _rigidbody.AddForce(direction * force);
    }

    // eyða projectileinu ef það er farið of langt
    private void Update()
    {
        if (transform.position.magnitude > 500.0f)
        {
            Destroy(gameObject);
        }
    }

    // laga robotta sem projectile hittir
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player")) return;
        
        
        var robot = other.collider.GetComponent<RobotController>();
        var roboBoss = other.collider.GetComponent<RoboBossController>();
        
        if (robot != null)
        {
            robot.Fix();
        }

        if (roboBoss != null)
        {
            roboBoss.OnHit();
        }
        
        Destroy(gameObject);
    }
}