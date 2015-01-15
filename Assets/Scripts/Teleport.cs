using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{
    public Transform destination;

    void Start()
    {
        if (destination == null)
        {
            Debug.LogError("Teleporter has  no position to teleport");
            enabled = false;
        }
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        other.transform.position = this.destination.position;
        Debug.Log("Teleport");
    }
}
