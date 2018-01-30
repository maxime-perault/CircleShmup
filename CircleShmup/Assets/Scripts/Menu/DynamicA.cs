using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicA : MonoBehaviour
{
    private GameManager manager;
    private static bool first = false;

    void Start ()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        if (first == false)
        {
            first = true;
            return;
        }
        if (manager.inputs[(int)GameManager.e_input.ACCEPT].ToString().Length > 3)
            GetComponent<Text>().text = manager.inputs[(int)GameManager.e_input.ACCEPT].ToString().Substring(0, 3);
        else
            GetComponent<Text>().text = manager.inputs[(int)GameManager.e_input.ACCEPT].ToString();
    }

    public void UpdateButton()
    {
        if (manager.inputs[(int)GameManager.e_input.ACCEPT].ToString().Length > 3)
            GetComponent<Text>().text = manager.inputs[(int)GameManager.e_input.ACCEPT].ToString().Substring(0, 3);
        else
            GetComponent<Text>().text = manager.inputs[(int)GameManager.e_input.ACCEPT].ToString();
    }
}
