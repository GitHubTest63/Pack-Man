using UnityEngine;
using System.Collections;

public class FantomeController : MonoBehaviour
{

    Transform fantomeTransform;
    Rigidbody fantomeRigidbody;
    RaycastHit fantomeHitFront;
    RaycastHit fantomeHitRight;
    RaycastHit fantomeHitLeft;
    bool leftHit = false;
    bool rightHit = false;
    bool frontHit = false;
    public float speed;
    private float rayCastRange = 1f;
    bool isRotate;
    int timerRotate;
    bool checkLeft;
    bool checkRight;

    void Start()
    {
        fantomeTransform = GetComponent<Transform>();
        fantomeRigidbody = GetComponent<Rigidbody>();
        //Time.timeScale = 1f;

    }

    void TurnRight()
    {
        fantomeTransform.rotation *= Quaternion.Euler(0, 90, 0);

    }

    void TurnLeft()
    {
        fantomeTransform.rotation *= Quaternion.Euler(0, -90, 0);

    }

    void Update()
    {


    }

    void FixedUpdate()
    {
        //this.transform.position = new Vector3(this.transform.position.x, 0f, this.transform.position.z);
        fantomeTransform.transform.Translate(0, 0, speed * Time.deltaTime);

        Physics.Raycast(fantomeTransform.position, fantomeTransform.forward, out fantomeHitFront, 0.5f);
        Physics.Raycast(fantomeTransform.position, fantomeTransform.right, out fantomeHitRight, rayCastRange);
        Physics.Raycast(fantomeTransform.position, -fantomeTransform.right, out fantomeHitLeft, rayCastRange);

        //debug
        Debug.DrawRay(fantomeTransform.position, fantomeTransform.forward * 0.5f, Color.red);
        Debug.DrawRay(fantomeTransform.position, fantomeTransform.right, Color.blue);
        Debug.DrawRay(fantomeTransform.position, -fantomeTransform.right, Color.green);



        if (fantomeHitFront.collider)
        {
            float chance = Random.value * 100;
            if (chance > 50)
            {
                TurnRight();
            }
            else
            {
                TurnLeft();
            }
        }

        //
        //		if (fantomeHitRight.collider && fantomeHitRight.collider.gameObject.tag == "Wall") {
        //			rightHit = true;
        //		} else {
        //			rightHit = false;
        //		}
        //		if (fantomeHitLeft.collider && fantomeHitLeft.collider.gameObject.tag == "Wall")
        //		{
        //			leftHit = false;
        //		}else {
        //			leftHit = false;
        //		}
        //		if (fantomeHitFront.collider && fantomeHitFront.collider.gameObject.tag == "Wall")
        //		{
        //			frontHit = false;
        //		} else {
        //			frontHit = false;
        //		}
        //
        //        // si collider devant et gauche mais pas à droite, alors je tourne à droite forcément
        //		if (fantomeHitFront.collider 
        //		    && !fantomeHitRight.collider 
        //		    && fantomeHitLeft.collider 
        //		    && fantomeHitFront.collider.gameObject.tag == "Wall" 
        //		    && fantomeHitLeft.collider.gameObject.tag == "Wall"
        //		    && rightHit == false
        //		    && isRotate == false)
        //        {
        //
        //            TurnRight();
        //        }
        //
        //        // si collider devant et droite mais pas à gauche, alors je tourne à gauche forcément
        //        else if (fantomeHitFront.collider 
        //		         && !fantomeHitLeft.collider 
        //		         && fantomeHitRight.collider
        //		         && fantomeHitFront.collider.gameObject.tag == "Wall" 
        //		         && fantomeHitRight.collider.gameObject.tag == "Wall"
        //		         && leftHit == false
        //		         && isRotate == false)
        //        {
        //
        //            TurnLeft();
        //        }
        //
        //        // si pas de collider à droite et devant et collider à gauche, alors une chance de tourner à droite
        //        else if (!fantomeHitRight.collider 
        //		         && !fantomeHitFront.collider 
        //		         && fantomeHitLeft.collider 
        //		         && fantomeHitLeft.collider.gameObject.tag == "Wall" 
        //		         && rightHit == false
        //		         && frontHit == false
        //		         && isRotate == false)
        //        {
        //
        //            float chance = Random.value * 100;
        //            if (chance > 50)
        //            {
        //                TurnRight();
        //            }
        //        }
        //
        //        // si pas de collider à gauche et devant et collider à droite, alors une chance de tourner à gauche
        //        else if (!fantomeHitLeft.collider 
        //		         && !fantomeHitFront.collider 
        //		         && fantomeHitRight.collider 
        //		         && fantomeHitRight.collider.gameObject.tag == "Wall" 
        //		         && leftHit == false
        //		         && frontHit == false
        //		         && isRotate == false)
        //        {
        //
        //            float chance = Random.value * 100;
        //            if (chance > 50)
        //            {
        //                TurnLeft();
        //            }
        //        }
        //        // si pas de collider à gauche et devant et droite, alors une chance de tourner à gauche ou à droite
        //        else if (!fantomeHitLeft.collider 
        //		         && !fantomeHitFront.collider 
        //		         && !fantomeHitRight.collider 
        //		         && leftHit == false 
        //		         && rightHit == false
        //		         && frontHit == false
        //		         && isRotate == false)
        //        {
        //
        //            float chance = Random.value * 100;
        //            if (chance < 33)
        //            {
        //                TurnRight();
        //            }
        //            else if (chance >= 33 && chance < 66)
        //            {
        //                TurnLeft();
        //            }
        //            else
        //            {
        //                //continuer tout droit
        //            }
        //        }
        //
        //
        //        // si pas de collider à gauche et droite, alors une chance de tourner à gauche ou à droite
        //        else if (!fantomeHitLeft.collider 
        //		         && fantomeHitFront.collider 
        //		         && fantomeHitFront.collider.tag == "Wall"
        //		         && !fantomeHitRight.collider 
        //		         && leftHit == false 
        //		         && rightHit == false
        //		         && isRotate == false)
        //        {
        //            float chance = Random.value * 100;
        //            if (chance > 50)
        //            {
        //                TurnRight();
        //            }
        //            else
        //            {
        //                TurnLeft();
        //            }
        //		} 
        //		else if((fantomeHitLeft.collider && fantomeHitLeft.collider.gameObject.tag == "Wall")
        //		        || (fantomeHitFront.collider && fantomeHitFront.collider.gameObject.tag == "Wall")
        //		        || (fantomeHitRight.collider && fantomeHitRight.collider.gameObject.tag == "Wall"))
        //		{
        //			TurnRight();
        //
        //		}

        //		Debug.Log ("frontHit : " + frontHit);
        //		Debug.Log ("rightHit : " + rightHit);
        //		Debug.Log ("leftHit : " + leftHit);
        //		Debug.Log ("frontColl : " + fantomeHitFront.collider);
        //		Debug.Log ("rightColl : " + fantomeHitRight.collider);
        //		Debug.Log ("leftColl : " + fantomeHitLeft.collider);
        //		Debug.Log (isRotate);
        //		Debug.Log (timerRotate);

    }
}
