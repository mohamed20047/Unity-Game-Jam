using UnityEngine;
using System;

namespace Iterations.Events
{
    [CreateAssetMenu(menuName = "Unity Game Jam/Events/Void Event Channel")]
    public class VoidEventChannelSO : ScriptableObject
    {
        public event Action OnEventRaised;

        public void RaiseEvent()
        {
            OnEventRaised?.Invoke();
        }
    }
}