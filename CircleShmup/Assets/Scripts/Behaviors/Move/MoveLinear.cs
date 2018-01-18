using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Linear move behavior, move the entity to the target
 * with a given speed
 * @class MoveSeek
 */
public class MoveLinear : Move
{
    public  Vector2 startPoint;
    public  Vector2 endPoint;

    private Rigidbody2D body;
    private Vector2     direction;
    private Vector2     velocity;

    /**
     * Startup method, buffers entity body
     */
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    /**
    * When this behavior is enable,
    * we must recompute the direction
    */
    void OnEnable()
    {
        direction = (endPoint - startPoint).normalized;
        velocity  = new Vector2(direction.x * speed.x, direction.y * speed.y);
    }

    /**
     * Behavior update
     */
    void FixedUpdate()
    {
        body.AddForce(velocity);
    }
}