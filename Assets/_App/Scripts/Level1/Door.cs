using UnityEngine;

namespace _App.Scripts.Level1
{
    public class Door : MonoBehaviour
    {
        private static readonly int Open = Animator.StringToHash("Open");
        [SerializeField] private Animator animator;
        
        private int _activatedCount;
        private bool _isOpen;
        
        public void AddActivation()
        {
            _activatedCount++;
            if (_activatedCount == 2)
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