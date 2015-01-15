using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraShake : MonoBehaviour
{

    public bool shake;
    private Camera camera;
    private Vector3 startPosition;
    private static Vector3 offset = new Vector3();
    public float MAX_RANGE = 0.1f;

    void Start()
    {
        this.camera = GetComponent<Camera>();
        if (camera == null)
        {
            this.camera = Camera.main;
        }
        this.startPosition = this.camera.transform.position;
    }

    public void shakeFor(float duration)
    {
        StartCoroutine(internalShake(duration));
    }

    private IEnumerator internalShake(float duration)
    {
        this.shake = true;
        yield return new WaitForSeconds(duration);
        this.shake = false;
    }

    void Update()
    {
        if (shake)
        {
            offset.x = Random.Range(-MAX_RANGE, MAX_RANGE);
            offset.y = Random.Range(-MAX_RANGE, MAX_RANGE);
            offset.z = Random.Range(-MAX_RANGE, MAX_RANGE);
            this.camera.transform.position = this.startPosition + offset;
        }
        else
        {
            this.camera.transform.position = this.startPosition;
        }
    }
}
