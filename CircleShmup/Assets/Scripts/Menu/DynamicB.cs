using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicB : MonoBehaviour
{
    private GameManager manager;

    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (manager.inputs[(int)GameManager.e_input.CANCEL].Length > 3)
            GetComponent<Text>().text = manager.inputs[(int)GameManager.e_input.CANCEL].Substring(0, 3);
        else
            GetComponent<Text>().text = manager.inputs[(int)GameManager.e_input.CANCEL];
    }

    public void UpdateButton()
    {
        if (manager.inputs[(int)GameManager.e_input.CANCEL].Length > 3)
            GetComponent<Text>().text = manager.inputs[(int)GameManager.e_input.CANCEL].Substring(0, 3);
        else
            GetComponent<Text>().text = manager.inputs[(int)GameManager.e_input.CANCEL];
    }
}
