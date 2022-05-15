using System;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        //bara meiða player
        if (!col.gameObject.CompareTag("Player")) return;

        Debug.Log("Spike ");

        var player = col.gameObject.GetComponent<PlatformerPlayerController>();
        player.ChangeHealth(-1);
    }
}