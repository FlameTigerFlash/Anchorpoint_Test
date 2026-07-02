using TMPro;
using UnityEngine;

public class EnergyDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void Display(float energy)
    {
        _text.text = $"Energy: {((int)energy).ToString()}";
    }
}
