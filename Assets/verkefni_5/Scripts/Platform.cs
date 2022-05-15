using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool moving = false;
    public float moveSpeedMultiplier = 1f;
    public float moveDistance = 4f;
    public bool vertical = false;

    private Vector3 m_InitialPosition;
    private Transform m_Transform;

    void Start()
    {
        m_InitialPosition = transform.position;
        m_Transform = GetComponent<Transform>();
    }

    void Update()
    {
        // einföld notkun á pingpong til að hreyfa platform fram og til baka með tímanum 
        if (!moving) return;
        float axis = Mathf.PingPong(Time.time * moveSpeedMultiplier, moveDistance);
        var position = m_InitialPosition * 1;

        if (vertical)
        {
            position.y += axis;
        }
        else
        {
            position.x += axis;
        }

        m_Transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // "Festir" við platformið 
        // hann hreyfist semsagt með platforminu 
        if (!col.gameObject.CompareTag("Player")) return;
        col.gameObject.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (!col.rigidbody.CompareTag("Player")) return;
        Debug.Log("Coll exit");
        col.gameObject.transform.SetParent(null);
    }
}