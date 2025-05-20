using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    public Transform SpawnPoint => spawnPoint;
}