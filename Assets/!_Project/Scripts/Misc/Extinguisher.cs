using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    public void Extinguish(GameObject obj)
    {
        if (obj.TryGetComponent<ExtinguishableObject>(out var fire))
        {
            fire.OnExtinguish();
        }
    }
}
