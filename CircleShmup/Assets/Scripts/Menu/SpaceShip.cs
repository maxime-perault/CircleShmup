using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{
    public GameObject   SelectionScript;
    public GameObject[] buttons;
    public GameObject   ProjectilePrefab;
    public int          nb_buttons = 5;
    public Canvas       CanvasMenu;

    private float       translation = 0;
    private int         actual_button = 0;
    private SelectMenu  MenuClass;

    //SpaceShip
    private Vector2     pos;
    private bool        isFiring = false;
    private bool        isMoving = false;

    //Shots
    private GameObject  bullet;
    private float       Velocity;
    private float       bulletSpawn;
    private float       ShootingRange;

    void Start ()
    {
        pos.x = GetComponent<RectTransform>().localPosition.x;
        pos.y = GetComponent<RectTransform>().localPosition.y;
        MenuClass = SelectionScript.GetComponent<SelectMenu>();
        Velocity = Screen.width / 4 * 10;
        bulletSpawn = transform.GetComponent<RectTransform>().rect.width / 2;
        ShootingRange = Screen.width - 
            (buttons[actual_button].transform.GetComponent<RectTransform>().anchoredPosition.x +
            buttons[actual_button].transform.GetComponent<RectTransform>().rect.width +
            -transform.GetComponent<RectTransform>().anchoredPosition.x +
            transform.GetComponent<RectTransform>().rect.width);
        ShootingRange /= CanvasMenu.scaleFactor;
    }
	
    void ChangeButton(int y)
    {
        buttons[actual_button].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        buttons[actual_button].GetComponentInChildren<Text>().color = new Color32(0 ,0, 0, 255);
        actual_button -= y;
        pos.y = buttons[actual_button].transform.localPosition.y;
        GetComponent<RectTransform>().localPosition = new Vector3(pos.x, pos.y, 0);
        isMoving = true;
        buttons[actual_button].GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        buttons[actual_button].GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
    }

	void Update ()
    {
        translation = Input.GetAxisRaw("Vertical");

        /*
        ** SpaceShip shots
        */
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey("joystick button 0"))
            && !isFiring)
        {
            isFiring = true;
            bullet = (GameObject)Instantiate(
                ProjectilePrefab,
                transform);
            //The spaceship is rotated by 90° on Z
            bullet.transform.localPosition = new Vector3(0, bulletSpawn, 0);
        } 
        if (isFiring && bullet != null)
        {

            bullet.transform.Translate(new Vector3(0, 1) * Time.deltaTime * Velocity);
            if (bullet.transform.localPosition.y > ShootingRange)
            {
                DestroyObject(bullet);
                MenuClass.Select(actual_button);
                isFiring = false;
            }
        }

        /*
        ** Wait until the stick return before moving again
        */
        if ((isMoving == true) && (translation == 0))
            isMoving = false;

        /*
        ** Move the ship across the buttons
        */
        if (isMoving == false)
        {
            if ((translation < 0) && (actual_button < (nb_buttons - 1)))
            {
                ChangeButton(-1);
                
            }
            else if ((translation > 0) && (actual_button > 0))
            {
                ChangeButton(1);
            }
        }
    }
}
