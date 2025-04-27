using UnityEngine;

namespace _App.Scripts.Level1
{
    public class Door : MonoBehaviour
    {
        private static readonly int Open = Animator.StringToHash("Open");
        [SerializeField] private Animator animator;
        
        public void OpenDoor()
        {
            animator.SetTrigger(Open);
        }
    }
}