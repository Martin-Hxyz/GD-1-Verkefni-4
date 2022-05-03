using UnityEngine;

public class DamageZone : MonoBehaviour
{
    // keyrir við "árekstur" -- triggerar hindra ekki hreyfingu
    // munur á enter og stay er að stay keyrir aftur og aftur svo lengi sem object heldur sér innan collidersins
    void OnTriggerStay2D(Collider2D other)
    {
        // er þetta ruby?
        if (other.CompareTag("Player"))
        {
            // minnka health um 1
            var controller = other.GetComponent<RubyController>();
            controller.ChangeHealth(-1);
        }
    }
}