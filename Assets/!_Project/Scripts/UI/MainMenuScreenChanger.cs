using UnityEngine;

public class MainMenuScreenChanger : MonoBehaviour
{
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private Canvas _rulesCanvas;

    public void ToMainCanvas()
    {
        _mainCanvas.enabled = true;
        _rulesCanvas.enabled = false;
    }

    public void ToRulesCanvas()
    {
        _mainCanvas.enabled = false;
        _rulesCanvas.enabled = true;
    }
}
