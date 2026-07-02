using TMPro;
using UnityEngine;

public class LogDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void Display(string text)
    {
        _text.text = text;
    }
}
