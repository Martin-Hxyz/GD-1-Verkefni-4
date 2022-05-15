using System;
using TMPro;
using UnityEngine;

public class PlatformerJambi : Interactable
{
    public String dialog;
    public Canvas dialogCanvas;
    public TextMeshProUGUI dialogTextBox;

    public override void Interact(GameObject trigger)
    {
        ShowDialog();
        Invoke(nameof(HideDialog), 5f);
    }

    private void ShowDialog()
    {
        dialogCanvas.gameObject.SetActive(true);
        dialogTextBox.text = dialog;
    }

    private void HideDialog()
    {
        dialogCanvas.gameObject.SetActive(false);
    }
}