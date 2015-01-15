using UnityEngine;
using System.Collections;

public class GarbageCollector : MonoBehaviour
{
    private int collectibleCount = 0;

    public void decrease()
    {
        this.decrease(1);
    }

    public void decrease(int amount)
    {
        collectibleCount -= amount;
        if (collectibleCount < 0)
            collectibleCount = 0;
    }

    public void increase()
    {
        this.increase(1);
    }

    public void increase(int amount)
    {
        collectibleCount += amount;
    }

    public int getCollectibleCount()
    {
        return this.collectibleCount;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Collectible"))
        {
            increase();
            Destroy(other.gameObject);
        }
    }
}
