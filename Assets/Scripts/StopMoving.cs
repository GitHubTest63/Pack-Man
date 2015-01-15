using UnityEngine;
using System.Collections;

public class StopMoving : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("Player enter stopMoving");
            PlayerMovement[] playerMovement = other.GetComponents<PlayerMovement>();
            GarbageCollector collector = other.GetComponent<GarbageCollector>();
            if (playerMovement != null && collector != null && collector.getCollectibleCount() > 0)
            {
                playerMovement[0].stopMove();
            }
            else
            {
                Debug.Log("Don't stop move");
            }
        }
    }
}
