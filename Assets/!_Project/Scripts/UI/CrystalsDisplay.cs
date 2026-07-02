using TMPro;
using UnityEngine;

public class CrystalsDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private int _requiredAmount = 3;

    public void SetRequiredAmount(int value)
    {
        _requiredAmount = value;
    }

    public void Display(int amount)
    {
        _text.text = $"Crystals: {amount.ToString()} / {_requiredAmount.ToString()}";
    }
}
