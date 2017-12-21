using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manages the core player weapon : a circle of spheres
 * @class PlayerSphereController
 */
public class PlayerSphereController : MonoBehaviour
{
    public List<GameObject> spheres;
    public GameObject       spherePrefab;

    public int   startSphereCount  = 3;
    public float reverseDelay      = 0.5f;
    public float sphereDelay       = 0.5f;
    public float radius            = 5.0f;
    public float minRadius         = 5.0f;
    public float maxRadius         = 25.0f;
    public float radiusGrowSpeed   = 5.0f;
    public float radiusCrunchSpeed = 10.0f;
    public float rotationSpeed     = 10.0f;

    public bool  canReverse        = true;
    public bool  canAddSphere      = true;

    private IEnumerator addSphereCooldownCoroutine;
    private IEnumerator reverseRotationCooldownCoroutine;

    /**
    * Adds to the weapon the desired number of spheres
    */
    void Start()
    {
        // Creates the two coroutine timers
        addSphereCooldownCoroutine       = WairForAddSphereCooldown();
        reverseRotationCooldownCoroutine = WairForInversionCooldown();

        StartCoroutine(addSphereCooldownCoroutine);
        StartCoroutine(reverseRotationCooldownCoroutine);

        AddSphere(startSphereCount);
    }

    /**
     * If the player add a sphere, this coroutine
     * will be make this action available again after
     * the desired cooldown
     */
    private IEnumerator WairForAddSphereCooldown()
    {
        while(true)
        {
            if(!canAddSphere)
            {
                yield return new WaitForSeconds(sphereDelay);
                canAddSphere = true;
            }

            yield return new WaitForSeconds(0.016f);
        }
    }

    /**
     * If the player reverse the rotation, this coroutine
     * will be make this action available again after
     * the desired cooldown
     */
    private IEnumerator WairForInversionCooldown()
    {
        while (true)
        {
            if (!canReverse)
            {
                yield return new WaitForSeconds(reverseDelay);
                canReverse = true;
            }

            yield return new WaitForSeconds(0.016f);
        }
    }

    /**
     * Updates the sphere controller, rotates all spheres
     */
    void Update()
    {
        // Normalizes radius to increases rotation speed depending the radius size
        // The larger the radius is, faster is the rotation speed (subject to later modifications)
        float nRadius = 0.6f + radius / maxRadius;
        transform.Rotate(new Vector3(0.0f, 0.0f, rotationSpeed * nRadius) * Time.deltaTime);
    }

    /**
     * Reverses the rotation direction
     */
    public void ReverseRotation()
    {
        if (canReverse)
        {
            canReverse = false;
            rotationSpeed *= -1.0f;
        }
    }

    /**
     * Adds a sphere to the sphere controller
     * @param count The count of sphere to add
     */
    public void AddSphere(int count)
    {
        for (int n = 0; n < count; ++n)
        {
            AddSphere(false);
        }
    }

    /**
     * Increase the radius size over time
     * Recomputes the sphere positions
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
     * Decreases the radius size over time
     * Recomputes the sphere positions
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
     * Instantiates a sphere game object and adds it
     * to the sphere controller. Recomputes sphere positions
     */
    public void AddSphere(bool wait = true)
    {
        if (canAddSphere || !wait)
        {
            spheres.Add(Instantiate(spherePrefab, transform));

            canAddSphere = false;
            ComputeSpherePosition();
        }
    }

    /**
     * Removes the last added sphere of the sphere controller
     * Destroy the game object. Recomputes sphere positions
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
     * Computes all sphere positions placing them
     * to an equal distance from each other.
     * Picks equal angles by dividing the total angle
     * by the count of sphere and places them by
     * computing their position on the trigonometric circle
     * multiplied by the radius of the sphere controller.
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
