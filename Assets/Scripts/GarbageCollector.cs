using UnityEngine;
using System.Collections;

public class GarbageCollector : MonoBehaviour
{
    private int collectibleCount = 0;
    private static float MIN_SCALE = 0.7f;
    private static float MAX_SCALE = 2;
    private static int MAX_COLLECTIBLE = 5;

    void Start()
    {
        this.updateScale();
    }

    public void decrease()
    {
        this.decrease(1);
    }

    public void decrease(int amount)
    {
        collectibleCount -= amount;
        if (collectibleCount < 0)
            collectibleCount = 0;
        this.updateScale();
    }

    public void increase()
    {
        this.increase(1);
    }

    public void increase(int amount)
    {
        collectibleCount += amount;
        this.updateScale();

    }

    private void updateScale()
    {
        float scale = ((float)this.collectibleCount / MAX_COLLECTIBLE) * (MAX_SCALE - MIN_SCALE) + MIN_SCALE;
        Debug.Log("Scale = " + scale);
        transform.FindChild("avatar").localScale = new Vector3(scale, scale, scale);
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

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 30), "Collectibles : " + this.collectibleCount);
    }
}
