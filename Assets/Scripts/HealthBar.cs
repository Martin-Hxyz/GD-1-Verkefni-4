using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{ 
    // singleton pattern svo auðvelt sé að nota SetValue fallið hvar sem er
    public static HealthBar Instance { get; private set; }
    
    public Mask mask;
    private float _originalSize;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _originalSize = mask.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {			
        // breytir stærð á mask til að fela eða sýna meira af bláa health barnum
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _originalSize * value);
    }
}