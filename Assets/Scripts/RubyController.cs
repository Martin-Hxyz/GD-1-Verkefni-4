using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class RubyController : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public GameObject projectilePrefab;
    public AudioClip hitSound;

    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private AudioSource _audioSource;
    private float _horizontal;
    private float _vertical;
    private Vector2 _lookDirection = new Vector2(1, 0);

    // segir til hversu lengi ruby a ad vera odrepanlegur
    // ekki tengt við framerate bara algengt að kalla þetta invincibility-frames eða i-frames
    // ef talan er meira en 0, þá er ruby ódrepanlegur
    // talan er hækkuð þegar ruby meiðir sig
    // talan er minnkuð í FixedUpdate 
    private int invincibilityFrames = 0;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        health = maxHealth;
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        HandleAnimation();

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            var hit = Physics2D.Raycast(_rigidbody.position + Vector2.up * 0.2f, _lookDirection, 1.5f,
                LayerMask.GetMask("NPC"));
            if (hit.collider == null) return;
            
            var npc = hit.collider.GetComponent<NPC>();
            if (npc == null) return;
            
            npc.DisplayDialog();
        }
    }

    void FixedUpdate()
    {
        HandleMovement();

        // ef iframes eru fleiri en 0 minnka iframes um einn
        if (invincibilityFrames > 0)
        {
            invincibilityFrames--;
        }
    }

    private void HandleMovement()
    {
        Vector2 position = _rigidbody.position;
        position.x += 3.0f * _horizontal * Time.deltaTime;
        position.y += 3.0f * _vertical * Time.deltaTime;
        _rigidbody.MovePosition(position);
    }

    // uppfærir Look X, Look Y og Speed í animator 
    // notað til ap uppfæra t.d. hvaða átt Ruby horfir í
    private void HandleAnimation()
    {
        Vector2 move = new Vector2(_horizontal, _vertical);

        // ef annaðhvort move.x eða move.y eru EKKI sirka 0 þá uppfærum við lookDirection
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            _lookDirection.Set(move.x, move.y);
            _lookDirection.Normalize();
        }

        // animatorinn notar þessar breytur
        _animator.SetFloat("Look X", _lookDirection.x);
        _animator.SetFloat("Look Y", _lookDirection.y);
        _animator.SetFloat("Speed", move.magnitude);
    }

    // breyta lífinu hjá ruby
    public void ChangeHealth(int change)
    {
        if (invincibilityFrames > 0) return;

        if (change < 0)
        {
            // triggerar hit animationið
            _animator.SetTrigger("Hit");
            
            // 45 updeit ódrepanlegur
            invincibilityFrames = 45;
            
            // spila hitSound hljóðið
            PlaySound(hitSound);
        }

        // clamp segir talan ´health + change´ verður að vera innan 0 til maxHealth
        health = Math.Clamp(health + change, 0, maxHealth);

        // ef health er minna en 0
        if (health <= 0)
        {
            // loada game over scene
            SceneManager.LoadScene(2);
        }
        
        // resize-a healthbar maskið
        HealthBar.Instance.SetValue(health / (float)maxHealth);
    }

    public void Launch()
    {
        // býr til nýtt game object eftir 'projectilePrefab' prefabbinu
        var projectileObject =
            Instantiate(projectilePrefab, _rigidbody.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(_lookDirection, 300);
        _animator.SetTrigger("Launch");
        PlaySound(projectile.launchSound);
    }
    
    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}