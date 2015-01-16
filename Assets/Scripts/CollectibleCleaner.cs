using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectibleCleaner : MonoBehaviour
{

    public AudioClip dropPillSound;
    private Animator anim;
    private bool opened;

    public float cleanInterval = 1.0f;
    private List<GarbageCollector> garbageCollectors = new List<GarbageCollector>();

    void Start()
    {
        this.anim = GetComponentInChildren<Animator>();
    }

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
                collector.stopDropEffect();
                if (!opened && collector.getCollectibleCount() > 0)
                {
                    opened = true;
                    this.anim.SetBool("opened", true);
                }
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
                collector.stopDropEffect();
            }
            if (this.garbageCollectors.Count == 0 && anim != null)
            {
                anim.SetBool("opened", false);
                this.opened = false;
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
                audio.Play();
            }

        }
    }
}
