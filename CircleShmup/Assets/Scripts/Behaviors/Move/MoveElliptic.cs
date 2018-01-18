using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Elliptic move behavior, move the target with
 * an elliptic trajectory at x RPM
 * @class MoveElliptic
 */
public class MoveElliptic : Move
{
    public float   rpm;
    public Vector2 axis;
    public float   clockwise;

    private float       alpha;
    private Vector2     velocity;
    private Rigidbody2D body;

    // TMP
    private bool        updated;

    /**
     * Startup method, buffers entity body
     */
    void Start()
    {
        updated = false;
        body = GetComponent<Rigidbody2D>();
    }

    /**
     * When this behavior is enable,
     * we must recompute the alpha
     */
    void OnEnable()
    {
        // Computing start angle
        ComputeAproachAngle();
    }

    /**
     * Behavior update
     */
    void FixedUpdate()
    {
        if(!updated)
        {
            updated = true;
            ComputeAproachAngle();
        }

        // RPM Update
        speed.x = axis.x * 6.0f * rpm;
        speed.y = axis.y * 6.0f * rpm;

        float X = axis.x * Mathf.Cos(alpha);
        float Y = axis.y * Mathf.Sin(alpha);

        Vector2 currentPosition = body.position;
        Vector2 nextPosition    = new Vector2(X, Y);

        // Direction
        Vector2 direction = (nextPosition - currentPosition).normalized;
        velocity = new Vector2(direction.x * speed.x, direction.y * speed.y);

        body.AddForce(velocity);

        // Computing next alpha
        alpha += Time.fixedDeltaTime * rpm * clockwise;
    }

    /**
     * Computes the current position of the object on
     * the trigonometric circle
     */
    public void ComputeAproachAngle()
    {
        alpha = Mathf.Atan2(transform.position.y, transform.position.x);

        if (alpha < 0)
        {
            alpha = Mathf.PI + (Mathf.PI + alpha);
        }
    }
}