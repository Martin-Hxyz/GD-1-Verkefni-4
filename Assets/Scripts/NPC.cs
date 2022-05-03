using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject dialogBox;
    public float displayTime = 4.0f;
    private float _timerDisplay;

    void Start()
    {
        dialogBox.SetActive(false);
        _timerDisplay = -1.0f;
    }

    void Update()
    {
        if (_timerDisplay >= 0)
        {
            // telja niður þangað til þarf að fela dialog box aftur
            _timerDisplay -= Time.deltaTime;
            if (_timerDisplay < 0)
            {
                dialogBox.SetActive(false);
            }
        }
    }

    // sýnir dialogið í ákveðinn tíma
    public void DisplayDialog()
    {
        _timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }
}