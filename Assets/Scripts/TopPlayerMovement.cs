using UnityEngine;
using System.Collections;

public class TopPlayerMovement : PlayerMovement
{
    protected override void Start()
    {
        base.Start();
        transform.rotation = DOWN;
    }

    protected override void reset()
    {
        base.reset();
        this.transform.rotation = DOWN;
    }

    private void moveRight()
    {
        //Debug.Log("Right");
        transform.rotation = RIGHT;
        setMove();
    }

    private void moveLeft()
    {
        //Debug.Log("Left");
        transform.rotation = LEFT;
        setMove();
    }

    private void moveUp()
    {
        //Debug.Log("Up");
        transform.rotation = UP;
        setMove();
    }

    private void moveDown()
    {
        //Debug.Log("Down");
        transform.rotation = DOWN;
        setMove();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.D))
                moveRight();
            if (Input.GetKeyDown(KeyCode.Q))
                moveLeft();
            if (Input.GetKeyDown(KeyCode.Z))
                moveUp();
            if (Input.GetKeyDown(KeyCode.S))
                moveDown();
        }
    }
}
