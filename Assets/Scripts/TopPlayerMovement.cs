using UnityEngine;
using System.Collections;

public class TopPlayerMovement : PlayerMovement
{
    private static Quaternion RIGHT = Quaternion.Euler(90.0f * Vector3.up);
    private static Quaternion LEFT = Quaternion.Euler(-90.0f * Vector3.up);
    private static Quaternion UP = Quaternion.Euler(0.0f * Vector3.up);
    private static Quaternion DOWN = Quaternion.Euler(180.0f * Vector3.up);

    void Start()
    {
        transform.rotation = DOWN;
    }

    // Update is called once per frame
    void Update()
    {
        Touch[] touches = Input.touches;
        if (touches.Length > 0)
        {
            for (int i = 0; i < touches.Length; i++)
            {
                if (touches[i].phase == TouchPhase.Began)
                {
                    startInput = touches[i].position;
                }
                else if (touches[i].phase == TouchPhase.Ended)
                {
                    Vector2 diff = startInput - touches[i].position;
                    if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
                    {
                        if (diff.x > 0)
                        {
                            Debug.Log("Right");
                            transform.rotation = RIGHT;
                        }
                        else if (diff.x < 0)
                        {
                            Debug.Log("Left");
                            transform.rotation = LEFT;
                        }
                    }
                    else
                    {
                        if (diff.y > 0)
                        {
                            Debug.Log("Top");
                            transform.rotation = UP;
                        }
                        else if (diff.y < 0)
                        {
                            Debug.Log("Bottom");
                            transform.rotation = DOWN;
                        }
                    }
                    //if (touches[i].position.y > Screen.height * 0.5f)
                    //{
                    //Debug.Log("TopView");
                    //if (touches[i].deltaPosition.x > 0)
                    //{
                    //    Debug.Log("Right");
                    //    transform.rotation = RIGHT;
                    //}
                    //else if (touches[i].deltaPosition.x < 0)
                    //{
                    //    Debug.Log("Left");
                    //    transform.rotation = LEFT;
                    //}

                    //if (touches[i].deltaPosition.y > 0)
                    //{
                    //    Debug.Log("Top");
                    //    transform.rotation = TOP;
                    //}
                    //else if (touches[i].deltaPosition.y < 0)
                    //{
                    //    Debug.Log("Bottom");
                    //    transform.rotation = BOTTOM;
                    //}
                }
                //else
                //{
                //    Debug.Log("BottomView");
                //}
                //}
            }
        }
        //transform.Translate(transform.forward * speed * Time.deltaTime);
    }
}
