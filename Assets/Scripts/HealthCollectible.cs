using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public int healing = 1;
    public AudioClip collectedClip;

    // gefur ruby meira líf þegar hún labbar á þetta
    private void OnTriggerEnter2D(Collider2D other)
    {
        // bera saman 'other' og gá hvort það sé ruby
        if (!other.CompareTag("Player")) return;
        var controller = other.GetComponent<RubyController>();

        // bara framkvæma ef ruby er ekki með fullt health
        if (controller.health < controller.maxHealth)
        {
            controller.ChangeHealth(healing);
            Destroy(gameObject);
            controller.PlaySound(collectedClip);
        }
    }
}