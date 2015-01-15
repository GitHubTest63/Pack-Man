using UnityEngine;
using System.Collections;

public abstract class PlayerMovement : MonoBehaviour
{
    public static float MAX_SPEED;
    public static float MIN_SPEED;
    public float speed = 3;
    public int weight = 1;
    protected bool canMove = true;
    //protected MapGenerator mapGenerator;
    protected Vector2 startInput;

    protected virtual void Start()
    {
        //this.mapGenerator = MapGenerator.instance;
        MAX_SPEED = this.speed;
        MIN_SPEED = MAX_SPEED / 10;
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Wall"))
        {
            /*if (isFront(collision.collider))
            {*/
            WeightDestroyable w = collision.collider.GetComponent<WeightDestroyable>();
            if (w == null || w.weight >= this.transform.localScale.x)
            {
                if (isFront(collision.collider))
                    canMove = false;
                //Debug.Log("stopMove");
            }
            else
            {
                w.destroy();
                //Debug.Log("Collide but don't stop");
            }
            //}
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
