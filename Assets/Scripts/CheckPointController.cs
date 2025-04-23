using System;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public event Action<CheckPointController> OnPlayerPass;
    public bool Passed { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        OnPlayerPass?.Invoke(this);
    }

    /// <summary>
    /// Sets <see cref="Passed"/> to <paramref name="state"/>
    /// </summary>
    public void SetPassed(bool state)
    {
        Passed = state;
    }
}
