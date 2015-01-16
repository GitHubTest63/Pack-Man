using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{
    public Vector3 mirrorAxis;

    void OnTriggerEnter(Collider other)
    {
        Vector3 pos = other.transform.position;
        if (mirrorAxis.x == 0)
            pos.x *= -1;
        if (mirrorAxis.y == 0)
            pos.y *= -1;
        if (mirrorAxis.z == 0)
            pos.z *= -1;

        pos += transform.forward * (other.transform.localScale.x + 1) * 0.5f;
        other.transform.position = pos;
        Debug.Log("Teleport " + other.tag);
    }
}
