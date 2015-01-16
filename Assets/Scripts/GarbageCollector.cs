using UnityEngine;
using System.Collections;

public class GarbageCollector : MonoBehaviour
{
    private int collectibleCount = 0;
    private int score = 0;
    public float minScale = 0.6f;
    public float maxScale = 3f;
    public int scaleTrigger = 10;
    public float scalingEffectDuration = 1.0f;
    public float scalingStep = 0.2f;
    private float scale;
    private PlayerMovement playerMovement;

    private float targetScale;
    private bool needScaleUpdate = false;

    GameObject emitter;

    public void startDropEffect()
    {
        emitter = Instantiate(Resources.Load("Prefabs/Pill_drop")) as GameObject;
        emitter.transform.position = transform.position;
    }

    public void stopDropEffect()
    {
        if (emitter != null)
            Destroy(emitter);
    }


    void Start()
    {
        PlayerMovement[] playerMoves = this.GetComponents<PlayerMovement>();
        if (playerMoves == null || playerMoves.Length > 1)
        {
            Debug.LogError("Undefined PlayerMovement");
            this.enabled = false;
            return;
        }
        this.playerMovement = playerMoves[0];
        this.reset();
    }

    public void reset()
    {
        this.collectibleCount = 0;
        this.scale = this.minScale;
        this.targetScale = this.scale;
        this.setScale(this.scale);
        this.updateSpeed();
    }

    public void decrease()
    {
        this.decrease(1);
    }

    public void decrease(int amount)
    {
        this.score += amount;
        collectibleCount -= amount;
        if (collectibleCount <= 0)
        {
            collectibleCount = 0;
            if (emitter != null)
            {
                stopDropEffect();
            }
        }
        this.update(false);
    }

    public void increase()
    {
        this.increase(1);
    }

    public void increase(int amount)
    {
        collectibleCount += amount;
        this.update(true);

    }

    private void update(bool inc)
    {
        this.checkScale(inc);
        this.updateSpeed();
    }

    private void updateSpeed()
    {
        this.playerMovement.speed = Mathf.Max(this.playerMovement.minSpeed, this.playerMovement.maxSpeed - (((this.targetScale - this.minScale) / (this.maxScale - this.minScale)) * (this.playerMovement.maxSpeed - this.playerMovement.minSpeed)));
        //Debug.Log("Speed = " + this.playerMovement.speed);
    }

    private void checkScale(bool inc)
    {
        if (this.collectibleCount == 0)
        {
            this.setTargetScale(this.minScale);
        }
        else if (this.collectibleCount % this.scaleTrigger == 0)
        {
            if (inc)
            {
                this.setTargetScale(this.targetScale += this.scalingStep);
            }
            else
            {
                this.setTargetScale(this.targetScale -= this.scalingStep);
            }
        }
    }

    private void setTargetScale(float scale)
    {
        this.targetScale = Mathf.Clamp(scale, this.minScale, this.maxScale);
        //Debug.Log("TargetScale = " + this.targetScale);
        this.needScaleUpdate = true;
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
        this.scale = Mathf.Clamp(scale, this.minScale, this.maxScale);
        //Debug.Log("CurrentScale = " + this.scale);
        transform.localScale = new Vector3(this.scale, this.scale, this.scale);
    }

    public int getCollectibleCount()
    {
        return this.collectibleCount;
    }

    public int getScore()
    {
        return this.score;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Collectible"))
        {
            increase();
            PillGenerator.instance.decreasePillOnScreen();
            Destroy(other.gameObject);
            GameObject emitter = Instantiate(Resources.Load("Prefabs/Pill_collect")) as GameObject;
            emitter.transform.position = other.transform.position;
            ParticleSystem emitterSystem = emitter.GetComponent<ParticleSystem>();
            Destroy(emitter, emitterSystem.duration + emitterSystem.startLifetime);
        }
    }
}
