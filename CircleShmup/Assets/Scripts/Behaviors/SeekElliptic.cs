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
    [SerializeField] public float      speed;

    private Rigidbody2D body;

    /**
     * 
     */
    public void Init()
    {
        body = instance.GetComponent<Rigidbody2D>();
    }

    /**
     * TODO
     */
    public void Update(float dt)
    {
        // Computing next ellipse position
        float X = target.position.x + (axis.x * Mathf.Cos(alpha));
        float Y = target.position.y + (axis.y * Mathf.Sin(alpha));

        Vector2 nextPosition = new Vector2(X, Y);

        // Direction
        Vector2 direction = nextPosition - body.position;

        // Apply force
        body.AddForce(direction.normalized * speed);
        alpha += Time.deltaTime;

        // Reducing axis
        axis.x -= dt * 1.0f;
        axis.y -= dt * 1.0f;
    }
}
