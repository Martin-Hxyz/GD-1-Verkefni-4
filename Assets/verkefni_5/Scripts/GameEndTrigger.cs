using UnityEngine;

public class GameEndTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Ending in 5 seconds");
        Invoke(nameof(EndGame), 5f);
    }

    private void EndGame()
    {
        GameManager.Instance.GameOverWin();
    }
}