using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Delays the position of an object
 * @class Delayer
 */
public class Delayer : MonoBehaviour
{
    public  Transform     player;
    private List<Vector2> positions = new List<Vector2>();

    public  float delay;
    private float timer;

    /**
     * Updates the delayer, buffers positions
     */
    public void Update()
    {
        positions.Add(player.position);

        if(timer <= delay)
        {
            timer += Time.deltaTime;
        }
        else
        {
            transform.position = positions[0];
            positions.RemoveAt(0);
        }
    }
}
