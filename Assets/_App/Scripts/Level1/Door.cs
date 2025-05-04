using UnityEngine;
using UnityEngine.Serialization;

namespace _App.Scripts.Level1
{
    public class Door : MonoBehaviour
    {
        private static readonly int Open = Animator.StringToHash("Open");
        [SerializeField] private Animator animator;
        [SerializeField] private int totalCount = 2;
        private int _currentCount;
        private bool _isOpen;
        
        public void AddActivation()
        {
            _currentCount++;
            if (_currentCount >= totalCount)
            {
                OpenDoor();
            }
        }

        private void OpenDoor()
        {
            if (_isOpen) return;
            animator.SetTrigger(Open);
            _isOpen = true;
        }
    }
}