using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    public GameObject keyUnlocks;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        var player = col.gameObject.GetComponent<PlatformerPlayerController>();
        player.hasKey = true;
        Destroy(keyUnlocks);
        Destroy(gameObject);
    }
}