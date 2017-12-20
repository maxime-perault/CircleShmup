using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manages the core player weapon : a circle of spheres
 * @class PlayerSphereController
 */
public class PlayerSphereController : MonoBehaviour
{
    [SerializeField] public GameObject       spherePrefab;
    [SerializeField] public List<GameObject> spheres;
    
    [SerializeField] public float invertDelay       = 0.5f;
    [SerializeField] public float invertTimer       = 0.0f;
    [SerializeField] public float sphereDelay       = 0.5f;
    [SerializeField] public float sphereTimer       = 0.0f;
    [SerializeField] public float radius            = 5.0f;
    [SerializeField] public float minRadius         = 5.0f;
    [SerializeField] public float maxRadius         = 25.0f;
    [SerializeField] public float radiusGrowSpeed   = 5.0f;
    [SerializeField] public float radiusCrunchSpeed = 10.0f;
    [SerializeField] public float rotationSpeed     = 10.0f;

    [SerializeField] public int   startSphereCount  = 3;


    // private IEnumerator coroutine;



    /**
     * Adds to the weapon the desired number of spheres
     */
    void Start()
    {
        AddSphere(startSphereCount);
    }

    /**
     * TODO
     */
    void Update()
    {
        // Normalizes radius to increases rotation speed depending the radius size
        // The larger the radius is, faster is the rotation speed
        float nRadius = 0.6f + radius / maxRadius;
        transform.Rotate(new Vector3(0.0f, 0.0f, rotationSpeed * nRadius) * Time.deltaTime);

        if (sphereTimer > 0.0f)
        {
            sphereTimer -= Time.deltaTime;
            if (sphereTimer < 0.0f)
            {
                sphereTimer = 0.0f;
            }
        }

        if (invertTimer > 0.0f)
        {
            invertTimer -= Time.deltaTime;
            if (invertTimer < 0.0f)
            {
                invertTimer = 0.0f;
            }
        }
    }

    /**
     * TODO
     */
    public void InvertRotation()
    {
        if (invertTimer == 0.0f)
        {
            rotationSpeed *= -1.0f;
            invertTimer = invertDelay;
        }
    }

    /**
     * TODO
     */
    public void AddSphere(int count)
    {
        for (int n = 0; n < count; ++n)
        { AddSphere(false); }
    }

    /**
     * TODO
     */
    public void IncreaseRadius()
    {
        if (radius + Time.deltaTime * radiusGrowSpeed <= maxRadius)
        {
            radius += Time.deltaTime * radiusGrowSpeed;
            ComputeSpherePosition();
        }
    }

    /**
      * TODO
      */
    public void DecreaseRadius()
    {
        if (radius - Time.deltaTime * radiusCrunchSpeed >= minRadius)
        {
            radius -= Time.deltaTime * radiusCrunchSpeed;
            ComputeSpherePosition();
        }
    }

    /**
     * TODO
     */
    public void AddSphere(bool wait = true)
    {
        if (sphereTimer == 0.0f || !wait)
        {
            GameObject sphere = Instantiate(spherePrefab, transform);
            spheres.Add(sphere);

            sphereTimer = sphereDelay;
            ComputeSpherePosition();
        }
    }

    /**
     * TODO
     */
    public void RemoveSphere()
    {
        int count = spheres.Count;
        if (count >= 1)
        {
            Destroy(spheres[count - 1]);
            spheres.RemoveAt(spheres.Count - 1);
        }

        ComputeSpherePosition();
    }

    /**
     * TODO
     */
    private void ComputeSpherePosition()
    {
        int sphereCount = spheres.Count;
        float alpha = (360.0f * (Mathf.PI / 180.0f)) / sphereCount;

        for (int nSphere = 0; nSphere < sphereCount; ++nSphere)
        {
            float x = Mathf.Cos(alpha * nSphere) * radius;
            float y = Mathf.Sin(alpha * nSphere) * radius;

            spheres[nSphere].transform.localPosition = new Vector3(x, y, 0.0f);
        }
    }
}
