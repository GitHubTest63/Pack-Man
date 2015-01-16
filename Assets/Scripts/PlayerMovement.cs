using UnityEngine;
using System.Collections;

public abstract class PlayerMovement : MonoBehaviour
{
    protected static Quaternion RIGHT = Quaternion.Euler(90.0f * Vector3.up);
    protected static Quaternion LEFT = Quaternion.Euler(-90.0f * Vector3.up);
    protected static Quaternion UP = Quaternion.Euler(0.0f * Vector3.up);
    protected static Quaternion DOWN = Quaternion.Euler(180.0f * Vector3.up);
    public float respawnDelay = 1.0f;
    private Vector3 startPos;
    public float maxSpeed;
    public float minSpeed;
    public float speed = 3;
    protected bool canMove;
    //protected MapGenerator mapGenerator;
    //protected Vector2 startInput;

    protected virtual void Start()
    {
        //this.mapGenerator = MapGenerator.instance;
        this.startPos = transform.position;
        this.maxSpeed = this.speed;
        this.minSpeed = this.maxSpeed / 5.0f;
    }

    protected virtual void reset()
    {
        this.speed = this.maxSpeed;
        this.transform.position = this.startPos;
        this.canMove = false;
        GarbageCollector collector = GetComponent<GarbageCollector>();
        if (collector != null)
            collector.reset();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        //movement
        Vector3 inc = transform.forward * speed * Time.fixedDeltaTime;

        if (canMove)
            transform.Translate(inc, Space.World);
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
                    this.stopMove();
                //Debug.Log("stopMove");
            }
            else
            {
                //Destroy(collision.collider.gameObject);
                w.destroy();
                //Debug.Log("Collide but don't stop");
            }
            //}
        }
        else if (collision.collider.tag.Equals("Phantom"))
        {
            //this.deathAudio.Play();
            StartCoroutine(this.respawn());
        }
        else if (collision.collider.tag.Equals("Player"))
        {
            if (this.transform.localScale.x < collision.collider.transform.localScale.x)
            {
                //Debug.Log("miam");
                GarbageCollector collector = GetComponent<GarbageCollector>();
                GarbageCollector collectorOther = collision.collider.GetComponent<GarbageCollector>();
                collectorOther.increase(collector.getCollectibleCount());
                StartCoroutine(this.respawn());
            }
        }
    }

    private IEnumerator respawn()
    {
        this.stopMove();
        transform.position = new Vector3(-100, -0, -100);
        yield return new WaitForSeconds(respawnDelay);
        this.reset();
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

    protected void setMove()
    {
        if (!audio.isPlaying)
            audio.Play();
        canMove = true;
    }

    public void stopMove()
    {
        this.canMove = false;
        audio.Stop();
    }
}
