using UnityEngine;
using System.Collections;

public class FantomeController : MonoBehaviour
{

    Transform fantomeTransform;
    Rigidbody fantomeRigidbody;
    RaycastHit fantomeHitFront;
    RaycastHit fantomeHitRight;
    RaycastHit fantomeHitLeft;
    bool leftHit = true;
    bool rightHit = true;
    public float speed;
    private float rayCastRange = 2f;

    void Start()
    {
        fantomeTransform = GetComponent<Transform>();
        fantomeRigidbody = GetComponent<Rigidbody>();
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

        fantomeTransform.transform.Translate(0, 0, speed * Time.deltaTime);

        Physics.Raycast(fantomeTransform.position, fantomeTransform.forward, out fantomeHitFront, this.rayCastRange);
        Physics.Raycast(fantomeTransform.position - fantomeTransform.forward * 0.5f, fantomeTransform.right, out fantomeHitRight, this.rayCastRange);
        Physics.Raycast(fantomeTransform.position - fantomeTransform.forward * 0.5f, -fantomeTransform.right, out fantomeHitLeft, this.rayCastRange);

        //debug
        //Debug.DrawRay(fantomeTransform.position, 0.5f * fantomeTransform.forward, Color.red);
        //Debug.DrawRay(fantomeTransform.position - fantomeTransform.forward * 0.5f, fantomeTransform.right, Color.blue);
        //Debug.DrawRay(fantomeTransform.position - fantomeTransform.forward * 0.5f, -fantomeTransform.right, Color.green);


        // si collider devant et gauche mais pas à droite, alors je tourne à droite forcément
        if (fantomeHitFront.collider && !fantomeHitRight.collider && fantomeHitLeft.collider)
        {

            TurnRight();
            leftHit = true;
            rightHit = true;
            //Debug.Log("droite");
        }

        // si collider devant et droite mais pas à gauche, alors je tourne à gauche forcément
        else if (fantomeHitFront.collider && !fantomeHitLeft.collider && fantomeHitRight.collider)
        {

            TurnLeft();
            leftHit = true;
            rightHit = true;
            //Debug.Log("left");
        }

        // si pas de collider à droite et devant et collider à gauche, alors une chance de tourner à droite
        else if (!fantomeHitRight.collider && !fantomeHitFront.collider && fantomeHitLeft.collider && rightHit == false)
        {

            float chance = Random.value * 100;
            if (chance > 50)
            {
                TurnRight();
            }
            leftHit = true;
            rightHit = true;
            //Debug.Log("droite chance : " + Mathf.Floor(chance));
        }

        // si pas de collider à gauche et devant et collider à droite, alors une chance de tourner à gauche
        else if (!fantomeHitLeft.collider && !fantomeHitFront.collider && fantomeHitRight.collider && leftHit == false)
        {

            float chance = Random.value * 100;
            if (chance > 50)
            {
                TurnLeft();
            }
            leftHit = true;
            rightHit = true;
            //Debug.Log("gauche chance : " + Mathf.Floor(chance));
        }
        // si pas de collider à gauche et devant et droite, alors une chance de tourner à gauche ou à droite
        else if (!fantomeHitLeft.collider && !fantomeHitFront.collider && !fantomeHitRight.collider && leftHit == false && rightHit == false)
        {

            float chance = Random.value * 100;
            if (chance > 33)
            {
                TurnRight();
            }
            else if (chance >= 33 && chance < 66)
            {
                TurnLeft();
            }
            else
            {
                //continuer tout droit
            }
            leftHit = true;
            rightHit = true;
            //Debug.Log("both chance : " + Mathf.Floor(chance));
        }


        // si pas de collider à gauche et droite, alors une chance de tourner à gauche ou à droite
        else if (!fantomeHitLeft.collider && fantomeHitFront.collider && !fantomeHitRight.collider && leftHit == false && rightHit == false)
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
            leftHit = true;
            rightHit = true;
            //Debug.Log("both chance : " + Mathf.Floor(chance));
        }
        if (fantomeHitRight.collider)
        {
            rightHit = false;
        }
        if (fantomeHitLeft.collider)
        {
            leftHit = false;
        }
    }
}
