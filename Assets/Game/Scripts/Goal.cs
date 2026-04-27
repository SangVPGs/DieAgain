using UnityEngine;
using System;

public class Goal : MonoBehaviour
{
    public static Action OnPlayerReachGoal;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        OnPlayerReachGoal?.Invoke();
    }
}