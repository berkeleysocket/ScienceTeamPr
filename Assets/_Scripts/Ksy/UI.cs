using System;
using UnityEngine;

namespace KSY.UI
{
    public abstract class UI : MonoBehaviour
    {
        public event Action OnEnabled;
        public event Action OnDisabled;

        private void OnEnable()
        {
            OnEnabled?.Invoke();
        }
        private void OnDisable()
        {
            OnDisabled?.Invoke();
        }
    }
}
