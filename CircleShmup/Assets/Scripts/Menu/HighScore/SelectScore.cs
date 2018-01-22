using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SelectScore : ASelect
{
    private GameManager manager;
    private Scrollbar   scrollbar;
    private Text        scoreText;
    private Text        firstScore;
    private Text        secondScore;
    private Text        thirdScore;

    private new void Start()
    {
        base.Start();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scrollbar = GameObject.Find("Scrollbar").GetComponent<Scrollbar>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

        firstScore = GameObject.Find("FirstPlace").GetComponent<Text>();
        secondScore = GameObject.Find("SecondPlace").GetComponent<Text>();
        thirdScore = GameObject.Find("ThirdPlace").GetComponent<Text>();

        string fullBoard = "";

        firstScore.text = getPos(1) + " " + manager.scoreboard[0].name + " " + getScore(manager.scoreboard[0].score) + "\n";
        secondScore.text = getPos(2) + " " + manager.scoreboard[1].name + " " + getScore(manager.scoreboard[1].score) + "\n";
        thirdScore.text = getPos(3) + " " + manager.scoreboard[2].name + " " + getScore(manager.scoreboard[2].score) + "\n";
        for (int i = 3; i < manager.scoreboard.Length; i++)
        {
            if (i < 99)
                fullBoard += getPos(i + 1) + " " + manager.scoreboard[i].name + " " +  getScore(manager.scoreboard[i].score) + "\n";
        }
        scoreText.text = fullBoard;
    }

    string  getPos(int nb)
    {
        if (nb < 10)
            return ("0" + nb.ToString() + "_");
        return (nb.ToString() + "_");

    }

    string getScore(float nb)
    {
        if (nb > 99999)
        {
            nb /= 1000;
            return string.Format("{0:N3}", nb);
        }
        else if (nb > 9999)
        {
            nb /= 1000;
            return ("0" + string.Format("{0:N2}", nb));
        }
        else if (nb > 999)
        {
            nb /= 1000;
            return ("00" + string.Format("{0:N1}", nb));
        }
        else
        {
            if (nb > 99)
             return ("000." + nb.ToString());
            else if (nb > 9)
                return ("000.0" + nb.ToString());
            else
                return ("000.00" + nb.ToString());
        }
    }

    private void Update()
    {
        float translation = Input.GetAxisRaw("Vertical");

        if (manager.GetKeyDown(GameManager.e_input.CANCEL) || Input.GetKeyDown("joystick button 1"))
        {
            AkSoundEngine.PostEvent("Main_Menu_UI_Back", music);
            StartCoroutine(LoadYourAsyncScene("Menu/MainMenu"));
        }

        if (manager.GetKeyDown(GameManager.e_input.DOWN) || (translation < -0.8))
        {
            scrollbar.value -= 0.01f;
        }
        else if (manager.GetKeyDown(GameManager.e_input.UP) || (translation > 0.8))
        {
            scrollbar.value += 0.01f;
        }
    }
}
