using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class RoboBossController : MonoBehaviour
{
    public int health = 8;
    public GameObject projectilePrefab;
    public AudioClip hitSound;

    private Rigidbody2D _rigidbody;
    private AudioSource _audioSource;
    
    private int _iFrames;
    private float _shotTimer;
    private int _shot = 0;
    private Vector2 _projectileOffset = new Vector2(0, 0.3f);
    private bool _broken = true;


    private float _moveTimer = 0.0f;
    private Random _random = new Random();
    private Vector2[] _directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _iFrames = 0;
    }

    private void Update()
    {
        _shotTimer -= Time.deltaTime;

        if (_shotTimer <= 0)
        {
            Shoot(8);
            _shotTimer = 2.6f;
        }
    }

    private void FixedUpdate()
    {
        _iFrames--;
        _moveTimer -= Time.fixedDeltaTime;

        if (_moveTimer <= 0)
        {
            Move();
            _moveTimer = 1.3f;
        }
    }

    private void Move()
    {
        var directionIndex = _random.Next(_directions.Length);
        var direction = _directions[directionIndex];
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(direction * 90);
    }

    private void Shoot(int projectiles)
    {
        for (int i = 0; i < projectiles; i++)
        {
            var angle = 360 / projectiles * i;
            var angleVec = DegreeToVector2(angle);

            var bossPosition = transform.position;
            var projectilePosition = new Vector3(bossPosition.x + _projectileOffset.x,
                bossPosition.y + _projectileOffset.y, bossPosition.z);

            projectilePosition.x += angleVec.x * 0.1f;
            projectilePosition.y += angleVec.y * 0.1f;

            var projectile = Instantiate(projectilePrefab, projectilePosition, Quaternion.identity);
            var rb = projectile.GetComponent<Rigidbody2D>();

            rb.AddForce(angleVec * 120f);
        }
    }

    public void OnHit()
    {
        if (_iFrames > 0) return;
        health--;
        _iFrames = 45;
        _audioSource.PlayOneShot(hitSound);
        
        if (health <= 0)
        {
            Fix();
        }
        
    }

    public void Fix()
    {
        SceneManager.LoadScene(3);
    }

    private Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    private Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
}