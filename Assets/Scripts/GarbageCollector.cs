using UnityEngine;
using System.Collections;

public class GarbageCollector : MonoBehaviour
{
    private int collectibleCount = 0;
    private static float MIN_SCALE = 0.6f;
    private static float MAX_SCALE = 3f;
    public int scaleTrigger = 10;
    public float scalingEffectDuration = 1.0f;
    public float scalingStep = 0.2f;
    private float scale = MIN_SCALE;
    private PlayerMovement playerMovement;

    private float targetScale;
    private bool needScaleUpdate = false;


    void Start()
    {
        this.targetScale = this.scale;
        this.setScale(this.scale);
        PlayerMovement[] playerMoves = this.GetComponents<PlayerMovement>();
        if (playerMoves == null || playerMoves.Length > 1)
        {
            Debug.LogError("Undefined PlayerMovement");
            this.enabled = false;
            return;
        }
        this.playerMovement = playerMoves[0];
    }

    public void decrease()
    {
        this.decrease(1);
    }

    public void decrease(int amount)
    {
        collectibleCount -= amount;
        if (collectibleCount < 0)
        {
            collectibleCount = 0;
            this.targetScale = MIN_SCALE;

            this.needScaleUpdate = true;
        }
        else
        {
            if (this.collectibleCount % this.scaleTrigger == 0)
            {
                this.targetScale -= this.scalingStep;
                this.needScaleUpdate = true;
            }
        }
    }

    public void increase()
    {
        this.increase(1);
    }

    public void increase(int amount)
    {
        collectibleCount += amount;
        this.update();

    }

    private void update()
    {
        this.checkScale();
        this.playerMovement.speed = PlayerMovement.MAX_SPEED - (((this.targetScale - MIN_SCALE) / (MAX_SCALE - MIN_SCALE)) * (PlayerMovement.MAX_SPEED - PlayerMovement.MIN_SPEED)) + PlayerMovement.MIN_SPEED;
        Debug.Log("Speed = " + this.playerMovement.speed);
    }

    private void checkScale()
    {
        if (this.collectibleCount % this.scaleTrigger == 0)
        {
            this.targetScale += this.scalingStep;
            this.needScaleUpdate = true;
        }
    }

    void Update()
    {
        if (needScaleUpdate)
        {
            if (this.targetScale - this.scale <= 0.01f)
            {
                this.setScale(targetScale);
                this.needScaleUpdate = false;
            }
            else
            {
                this.setScale(Mathf.Lerp(this.scale, this.targetScale, Time.deltaTime));
            }
        }
    }

    private void setScale(float scale)
    {
        this.scale = Mathf.Clamp(scale, MIN_SCALE, MAX_SCALE);
        Debug.Log("Scale = " + this.scale);
        transform.localScale = new Vector3(this.scale, this.scale, this.scale);
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
