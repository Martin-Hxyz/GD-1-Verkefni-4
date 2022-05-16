using System;
using UnityEngine;

public class PlatformerBullet : MonoBehaviour
{
    private float m_LifeTime;
    private Rigidbody2D m_Rigidbody;
    private Vector2 m_Direction;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        m_LifeTime += Time.deltaTime;
        if (m_LifeTime > 2.5f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Eagle")) return;
        Destroy(col.gameObject);
        Destroy(gameObject);
    }
}