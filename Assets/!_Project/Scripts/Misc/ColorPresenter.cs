using UnityEngine;

public class ColorPresenter : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    [SerializeField] private Color _emptyColor;
    [SerializeField] private Color _fullColor;

    public void Present(float val)
    {
        val = Mathf.Clamp01(val);
        _renderer.material.color = (_emptyColor * (1 - val) + _fullColor * val);
    }
}
