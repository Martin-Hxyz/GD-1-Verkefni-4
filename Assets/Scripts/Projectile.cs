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
        if(transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }        
    }

    // laga robotta sem projectile hittir
    private void OnCollisionEnter2D(Collision2D other)
    {
        var robot = other.collider.GetComponent<RobotController>();
        
        if (robot != null)
        {
            robot.Fix();
        }
        
        Destroy(gameObject);
    }
}