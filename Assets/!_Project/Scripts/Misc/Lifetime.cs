using System.Collections;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    [SerializeField] private float _lifetime = 5f;

    private void Start()
    {
        StartCoroutine(LifetimeCoroutine(_lifetime));
    }

    private IEnumerator LifetimeCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
