using System;
using UnityEngine;

public class MummyDor : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private GameObject[] doorFragments;
    
    [SerializeField] private AudioClip doorBreakSound;
    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = GameSingleton.Instance.AudioManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mummy"))
        {
            foreach (var fragment in doorFragments)
            {
                var rb = fragment.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(Vector3.forward * -20f, ForceMode.Impulse);
                }
                var doorFragment = fragment.GetComponent<DoorFragment>();
                if (doorFragment != null)
                {
                    doorFragment.Init();
                }
            }
            boxCollider.enabled = false;
            
            if (doorBreakSound != null) {
                _audioManager.PlaySFX(doorBreakSound);
            } else {
                Debug.LogWarning("Door break sound not assigned.");
            }
        }
    }
}
