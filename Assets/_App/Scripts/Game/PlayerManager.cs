using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Player player;
    public Player Player => player;
    
    [SerializeField] private PlayerSpawnPoint playerSpawnPoint;
    public PlayerSpawnPoint PlayerSpawnPoint => playerSpawnPoint;
}