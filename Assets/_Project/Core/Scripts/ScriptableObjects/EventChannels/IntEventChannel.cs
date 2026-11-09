using UnityEngine;
using System;

namespace Iterations.Events
{
    [CreateAssetMenu(menuName = "Unity Game Jam/Events/Int Event Channel")]
    public class IntEventChannelSO : ScriptableObject
    {
        public event Action<int> OnEventRaised;

        public void RaiseEvent(int value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
}