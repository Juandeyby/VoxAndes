using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovementController playerMovementController;
    public PlayerMovementController PlayerMovementController => playerMovementController;
    [SerializeField] private PlayerHealth playerHealth;
    public PlayerHealth PlayerHealth => playerHealth;
}
