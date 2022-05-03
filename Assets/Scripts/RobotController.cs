using UnityEngine;

public class RobotController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    public ParticleSystem smokeEffect;
    public AudioClip fixSound;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private AudioSource _audioSource;
    private float _timer;
    private int _direction = 1;
    private bool _broken = true;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _timer = changeTime;
    }

    void Update()
    {
        // ef búið er að laga vélmennið þurfum við ekki að gera meir.
        if (!_broken) return;

        // telur niður frá changeTime niður í 0
        // þegar kemur að núlli snýr vélmennið við og timerinn er resettaður
        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            _direction = -_direction;
            _timer = changeTime;
        }

        // Gá hvort við séum að vinna með lárétta / lóðrétta hreyfingu
        // Setja inn gögn í Move X og Move Y animator breyturnar
        // Þetta er gert til þess að segja animator hvaða átt vélmennið er að fara í.
        if (vertical)
        {
            _animator.SetFloat("Move X", 0);
            _animator.SetFloat("Move Y", _direction);
        }
        else
        {
            _animator.SetFloat("Move X", _direction);
            _animator.SetFloat("Move Y", 0);
        }
    }

    void FixedUpdate()
    {
        if (!_broken) return;
        Vector2 position = _rigidbody.position;
        
        // Reikna út hvert á að færa vélmennið næst
        // FixedDeltaTime er tími síðan seinasta FixedUpdate
        if (vertical)
        {
            position.y += Time.fixedDeltaTime * speed * _direction;
        }
        else
        {
            position.x += Time.fixedDeltaTime * speed * _direction;
        }

        // Færir vélmennið
        _rigidbody.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // við viljum bara keyra þennan kóða ef það er Ruby sem kemur við róbotinn
        // compare tag er besta leiðin til að segja til um hvort það sé verið að ræða um Player
        if (!collision.gameObject.CompareTag("Player")) return;
        var ruby = collision.gameObject.GetComponent<RubyController>();
        ruby.ChangeHealth(-1);
    }

    // Notað vélmennið á að vera lagað
    // Slekkur á öllum andskotanum og spilar fixSound hljóðið
    public void Fix()
    {
        _broken = false;
        _rigidbody.simulated = false;
        _animator.SetTrigger("Fixed");
        smokeEffect.Stop();

        _audioSource.loop = false;
        _audioSource.Stop();
        _audioSource.PlayOneShot(fixSound);
    }

    public bool IsBroken()
    {
        return _broken;
    }
}