using UnityEngine;

public class RoboBossBullet : MonoBehaviour
{
    private float _lifetime = 3.6f;

    private void Update()
    {
        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        var ruby = col.GetComponent<RubyController>();
        ruby.ChangeHealth(-1);
    }
}