﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private Camera camera;
    private GameManager manager;

    private float shake = 0;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    private Vector3 previousCam;

    private void Start()
    {
        camera = Camera.main;
        previousCam = camera.transform.localPosition;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        if ((shake > 0) && (manager.gameManagerState != GameManager.EGameState.GamePaused))
        {
            Vector3 random = Random.insideUnitSphere * shakeAmount;
            camera.transform.localPosition = new Vector3(random.x, random.y, previousCam.z);

            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            camera.transform.localPosition = previousCam;
            shake = 0;
        }
    }

    public void Shake(float shakeTime)
    {
        shake = shakeTime;
    }
}