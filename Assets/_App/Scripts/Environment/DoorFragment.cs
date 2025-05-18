using System;
using System.Collections;
using UnityEngine;

public class DoorFragment : MonoBehaviour
{
    Rigidbody _rb;
    MeshCollider _meshCollider;

    private void Awake()
    {
        _meshCollider = GetComponent<MeshCollider>();
        _rb = GetComponent<Rigidbody>();
    }

    public void Init()
    {
        StartCoroutine(DoorFragmentCoroutine());
    }

    private IEnumerator DoorFragmentCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _meshCollider.enabled = false;
        _rb.isKinematic = true;
    }
}
