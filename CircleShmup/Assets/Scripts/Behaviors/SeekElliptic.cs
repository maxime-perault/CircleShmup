using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * 
 * @class SeekElliptic
 */
[System.Serializable]
public class SeekElliptic : Seek
{
    [SerializeField] public Vector2    axis;
    [SerializeField] public float      alpha;
    [SerializeField] public float      smoothConstant;
    [SerializeField] public float      axisReductionCoefficient;
    [SerializeField] public float      minSpeed;
    [SerializeField] public float      maxSpeed;

    private enum SeekState
    {
        SeekRadius,
        SeekTarget
    }

    private bool        canSeekRadius;
    public float        timer;
    private int counter;
    private float       seekRadiusTimer;
    public float        speed;
    private Rigidbody2D body;
    private SeekState   state;
    private Vector2     radiusTarget;
    private Vector2     bufferedTarget;
    private Vector2     velocity;
    private float       startDist;

    /**
     * 
     */
    public void Init()
    {
        bufferedTarget = new Vector2(target.position.x, target.position.y);
        body = instance.GetComponent<Rigidbody2D>();

        // Computing axis
        Vector2 position  = body.transform.position;
        Vector2 targetPos = target.transform.position;

        float distance = (position - targetPos).magnitude;

        axis.x = distance;
        axis.y = distance;

        startDist = distance;

        ComputeApproachAngle();
        seekRadiusTimer = 0.0f;
        timer = 0.0f;
        minSpeed /= target.lossyScale.x;
        maxSpeed /= target.lossyScale.x;
        counter = 0;
        state = SeekState.SeekTarget;
        canSeekRadius = true;
    }

    /**
     * Update the behavior
     * @param dt The elapsed time
     */
    public void Update(float dt)
    {
        timer += Time.deltaTime;
        speed = Mathf.Lerp(minSpeed, maxSpeed, timer);

        if(!canSeekRadius)
        {
            seekRadiusTimer += dt;
            if(seekRadiusTimer >= 2.0f)
            {
                canSeekRadius = true;
                seekRadiusTimer = 0.0f;
                Debug.Log("Resets");
            }
        }

        Vector2 diff = bufferedTarget - new Vector2(target.position.x, target.position.y);
        if(diff.magnitude >= 2.0f && canSeekRadius)
        {
            // The target has moved, must re-seek the current radius
            state = SeekState.SeekRadius;
            ComputeApproachAngle();
            canSeekRadius = false;
            Debug.Log("Change to Seek radius");
            bufferedTarget = new Vector2(target.position.x, target.position.y);
        }

        if(state == SeekState.SeekRadius)
        {
            // Radius target
            radiusTarget.x = bufferedTarget.x + axis.x * Mathf.Cos(alpha);
            radiusTarget.y = bufferedTarget.y + axis.y * Mathf.Sin(alpha);
            
            Vector2 position  = new Vector2(body.transform.position.x, body.transform.position.y);
            Vector2 direction = radiusTarget - position;
            Vector2 velocity  = direction.normalized * speed;

            body.AddForce(velocity);

            if((radiusTarget - position).magnitude < 1.0f)
            {
                state = SeekState.SeekTarget;
                Debug.Log("Change to Seek target");
            }
        }
        else
        {
            if(counter == 0)
            {
                // Computing next ellipse position
                float X = bufferedTarget.x + (axis.x * Mathf.Cos(alpha));
                float Y = bufferedTarget.y + (axis.y * Mathf.Sin(alpha));

                Vector2 nextPosition = new Vector2(X, Y);

                // Direction
                Vector2 direction = nextPosition - body.position;
                velocity = direction.normalized * speed;
               
            }
            //counter++;
            //if(counter == 2)
            //{
            //    counter = 0;
            //}

            // Coef angular speed;
            Vector2 position  = body.transform.position;
            Vector2 targetPos = bufferedTarget;

            float currentDir = (position - targetPos).magnitude;
            float coeff = 1.0f / (currentDir / startDist);

            // Apply force
            body.AddForce(velocity);
            alpha += Time.deltaTime * coeff;

            // Reducing axis
            /* if(axis.x >= 1.0f)*/ axis.x -= dt * (1.0f / axisReductionCoefficient);
            /* if(axis.y >= 1.0f)*/ axis.y -= dt * (1.0f / axisReductionCoefficient);
        }
    }

    /**
     * 
     */
    private void ComputeApproachAngle()
    {
        // Computing start angle
        alpha = Mathf.Atan2(body.position.y - bufferedTarget.y, body.position.x - bufferedTarget.x);

        if (alpha > 0)
        {
            alpha += smoothConstant;
        }
        else
        {
            alpha = Mathf.PI + (Mathf.PI + alpha);
            alpha += smoothConstant;
        }
    }
}
