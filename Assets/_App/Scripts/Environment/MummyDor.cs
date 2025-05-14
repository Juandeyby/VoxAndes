using System;
using UnityEngine;

public class MummyDor : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private GameObject[] doorFragments;

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
            }
            boxCollider.enabled = false;
        }
    }
}
