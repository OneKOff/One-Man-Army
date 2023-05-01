using System;
using UnityEngine;

public class BasicCollectible : MonoBehaviour, Interactable
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered interaction radius");
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out PlayerControl pc)) { return; }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ManageCollecting();
        }
    }

    public void ManageCollecting()
    {
        Debug.Log($"Collected {name}");
        Destroy(gameObject);
    }
}
