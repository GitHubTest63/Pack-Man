using UnityEngine;
using System.Collections;

public class Destroyable : MonoBehaviour
{
    public float delay;

    public Destroyable()
    {

    }

    public Destroyable(float delay)
    {
        this.delay = delay;
    }

    public void destroy()
    {
        onDestroy();
        Destroy(this.gameObject, delay);
    }

    protected virtual void onDestroy()
    {
        //Debug.Log("onDestroy"); 
    }
}
