using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAnimationRootMotion : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;

    public bool isRootMotionApplied = true;
    
    private void OnAnimatorMove()
    {
        if (isRootMotionApplied)
        {
            MoveRootMotion();
        }
    }

    private void MoveRootMotion()
    {
        var newPosition = transform.position + animator.deltaPosition;
        rb.MovePosition(newPosition);
        var newRotation = transform.rotation * animator.deltaRotation;
        rb.MoveRotation(newRotation);
    }
}
