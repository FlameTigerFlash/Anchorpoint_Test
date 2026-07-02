using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void Display(int health)
    {
        _text.text = $"HP: {health.ToString()}";
    }
}
