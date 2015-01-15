using UnityEngine;
using System.Collections;

public abstract class PlayerMovement : MonoBehaviour
{

    public float speed = 3;
    public int weight = 1;
    protected bool canMove = true;
    protected MapGenerator mapGenerator;
    protected Vector2 startInput;

    void Start()
    {
        this.mapGenerator = MapGenerator.instance;
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (isFront(collision.collider))
        {
            WeightDestroyable w = collision.collider.GetComponent<WeightDestroyable>();
            if (w == null || w.weight >= this.transform.localScale.x)
            {
                Debug.Log("target w (" + w.weight + ") >= myScale (" + this.transform.localScale.x + ")");
                canMove = false;
                //Debug.Log("stopMove");
            }
            else
            {
                w.destroy();
                //Debug.Log("Collide but don't stop");
            }
        }
    }

    private bool isFront(Collider collider)
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, transform.forward, out hit, transform.localScale.z))
        {
            if (hit.collider == collider)
            {
                //Debug.Log("is in front me");
                return true;
            }
        }
        return false;
    }

    public void stopMove()
    {
        this.canMove = false;
    }
}
