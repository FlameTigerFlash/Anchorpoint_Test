using UnityEngine;
using UnityEngine.UI;

public class HaulingCrystalDisplay : MonoBehaviour
{
    [SerializeField] private Image _image;

    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;

    public void Display(bool active)
    {
        if (active)
        {
            _image.color = _activeColor;
        }
        else
        {
            _image.color = _inactiveColor;
        }    
    }
}
