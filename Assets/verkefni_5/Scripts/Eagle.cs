using UnityEngine;

public class Eagle : MonoBehaviour
{
    private Vector3 m_InitialPosition;
    private Transform m_Transform;

    void Start()
    {
        m_Transform = transform;
        m_InitialPosition = m_Transform.position;
    }

    void Update()
    {
        var position = m_InitialPosition * 1;
        position.y += Mathf.PingPong(Time.time, 3f);
        m_Transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        var player = col.gameObject.GetComponent<PlatformerPlayerController>();
        player.ChangeHealth(-1);
    }
}