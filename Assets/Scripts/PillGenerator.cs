using UnityEngine;
using System.Collections;

public class PillGenerator : MonoBehaviour
{

    public GameObject pill;

    public float xMin;				// coordonées de l'ecran de jeu
    public float xMax;
    public float zMin;
    public float zMax;
    float timerNextPill;			// timer de gestion des pills (secondes)
    public float timerSpawn; 		// au bout de combien de temps une pill spawn (secondes)
    public int maxPillsOnScreen;
    int pillsOnScreen;
    float pillCoodinates_X;
    float pillCoodinates_Z;
    Vector3 pillCoordinates;
    RaycastHit hitPill;
    private static string[] LAYERS = new string[] { "Default", "PillBlocker" };

    void Start()
    {
        pillCoodinates_X = (int)(Random.Range(0, (xMax - xMin) + 1) + xMin);
        pillCoodinates_Z = (int)(Random.Range(0, (zMax - zMin) + 1) + zMin);
    }

    void CheckPillPosition()
    {

        pillCoodinates_X = (int)(Random.Range(0, (xMax - xMin) + 1) + xMin);
        pillCoodinates_Z = (int)(Random.Range(0, (zMax - zMin) + 1) + zMin);
        pillCoordinates = new Vector3(pillCoodinates_X, 0.5f, pillCoodinates_Z);

        Physics.Raycast(pillCoordinates + new Vector3(0, 50, 0), -Vector3.up, out hitPill, LayerMask.GetMask(LAYERS));
        //Debug.DrawRay(pillCoordinates, -Vector3.up, Color.red);

        if (hitPill.collider == null)
        {
            SpawnPill(pill);
        }
        else
        {
            CheckPillPosition();
        }
    }

    void SpawnPill(GameObject _pill)
    {
        GameObject clone = Instantiate(_pill, pillCoordinates, Quaternion.identity) as GameObject;
        pillsOnScreen++;
    }


    void Update()
    {
        timerNextPill += Time.deltaTime;
        //Debug.Log (timerNextPill);
        if (timerNextPill >= timerSpawn && pillsOnScreen < maxPillsOnScreen)
        {
            timerNextPill = 0;
            CheckPillPosition();
        }
    }
}
