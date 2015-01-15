using UnityEngine;
using System.Collections;

public class WeightDestroyable : Destroyable {


    public int weight = 1;

    protected override void onDestroy()
    {
        base.onDestroy();
        Debug.Log(tag + " will be destroyed");
    }
}
