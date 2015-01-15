﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectibleCleaner : MonoBehaviour
{

    public float cleanInterval = 1.0f;
    private List<GarbageCollector> garbageCollectors = new List<GarbageCollector>();

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("!!!!! Player enter in cleaner !!!!!");
            GarbageCollector collector = other.GetComponent<GarbageCollector>();
            if (collector != null)
            {
                this.garbageCollectors.Add(collector);
                StartCoroutine(clean(collector));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            GarbageCollector collector = other.GetComponent<GarbageCollector>();
            if (collector != null)
            {
                this.garbageCollectors.Remove(collector);
            }
        }
    }

    private IEnumerator clean(GarbageCollector collector)
    {
        while (garbageCollectors.Contains(collector))
        {
            yield return new WaitForSeconds(cleanInterval);
            if (collector.getCollectibleCount() > 0)
            {
                collector.decrease();
            }

        }
    }
}
