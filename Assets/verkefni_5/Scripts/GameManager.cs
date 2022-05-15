using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Mask healthMask;
    public GameObject gameOver;
    public Image fadeOut;
    private float m_MaskFullWidth;

    private void Start()
    {
        Instance = this;
        m_MaskFullWidth = healthMask.rectTransform.rect.width;
    }

    public void UpdateMask(float ratio)
    {
        healthMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, m_MaskFullWidth * ratio);
    }

    public void GameOver()
    {
        // sýnir game over textann
        gameOver.SetActive(true);
        
        // loadar game over senunni eftir 2.8s
        Invoke(nameof(GameOver_LoadScene), 2.8f);
    }

    private void GameOver_LoadScene()
    {
        SceneManager.LoadScene(2);
    }

    public void GameOverWin()
    {
        // loadar win senunni eftir 2.8s
        Invoke(nameof(GameOverWin_LoadScene), 2.8f);
    }


    private void GameOverWin_LoadScene()
    {
        SceneManager.LoadScene(3);
    }

    private void FadeTick()
    {
        var color = fadeOut.color;
        color.a += (100f * 0.05f);
        fadeOut.color = color;
    }
}