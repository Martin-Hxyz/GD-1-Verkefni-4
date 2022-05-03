using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public AudioClip sound;
    public GameObject[] objects;

    private bool _triggered = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        if (_triggered) return;
        var ruby = col.GetComponent<RubyController>();

        _triggered = true;
        
        foreach (var obj in objects)
        {
            obj.SetActive(true);
        }

        ruby.PlaySound(sound);
    }
}