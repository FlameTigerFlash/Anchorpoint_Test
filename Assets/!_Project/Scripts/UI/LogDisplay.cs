using TMPro;
using UnityEngine;

public class LogDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;

    public void Display(string text)
    {
        _text.text = text;
    }

    public void SetActive(bool active)
    {
        if (active)
        {
            _text.color = _activeColor;
        }
        else
        {
            _text.color = _inactiveColor;
        }
    }
}
