using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Score actor
 * @class Score
 */
public class Score : MonoBehaviour
{
    public int iScore;
    public string sScore;
    public float upTime;
    public float upSpeed;
    public float rotationSpeed;

    public Sprite addSprite;
    public Sprite subSprite;

    public List<Sprite> numbers = new List<Sprite>();
    public List<GameObject> placeholder = new List<GameObject>();

    /**
     * States of the score
     */
    private enum EScoreState
    {
        Idle,
        MovingUp,
        Rotating
    }

    private float timer;
    private EScoreState state;

    /**
     * Sets the internal score
     * @param score the score to display
     */
    public void SetScore(int score)
    {
        iScore = score;
        sScore = iScore.ToString();

        if (score > 0)
        {
            placeholder[0].GetComponent<SpriteRenderer>().sprite = addSprite;
        }
        else
        {
            placeholder[0].GetComponent<SpriteRenderer>().sprite = subSprite;
        }

        int charCount = sScore.Length;
        for (int nChar = 0; nChar < charCount; ++nChar)
        {
            placeholder[nChar + 1].GetComponent<SpriteRenderer>().sprite = numbers[(int)char.GetNumericValue(sScore[nChar])];
        }

        // Enables renderers
        int enableCount = charCount + 1;
        for (int nEnable = 0; nEnable < enableCount; ++nEnable)
        {
            placeholder[nEnable].SetActive(true);
        }

        timer = 0.0f;
        state = EScoreState.MovingUp;
    }

    /**
     * Called when the object is instanciated
     */
    public void Start()
    {
        // None
    }

    /**
     * Called once each update
     */
    public void Update()
    {
        switch (state)
        {
            case EScoreState.MovingUp: MovingUp(); break;
            case EScoreState.Rotating: Rotating(); break;
            default: break;
        }
    }

    /**
     * TODO
     */
    private void MovingUp()
    {
        timer += Time.deltaTime;
        transform.Translate(new Vector3(0.0f, upSpeed, 0.0f) * Time.deltaTime);

        if (timer >= upTime)
        {
            state = EScoreState.Rotating;
            timer = 0.0f;
        }
    }

    /**
     * TODO
     */
    private void Rotating()
    {
        timer += Time.deltaTime;

        bool allRotated = true;
        int spriteCount = sScore.Length + 1;

        for (int nSprite = 0; nSprite < spriteCount; ++nSprite)
        {
            if (timer > 0.0f + nSprite * 0.1f)
            {
                // Default value = 0.025f
                float p = (timer - (0.0f + nSprite * 0.1f)) / 0.25f;

                if (p > (90.0f * (Mathf.PI / 180.0f)))
                {
                    p = 90.0f * (Mathf.PI / 180.0f);
                }
                else
                {
                    allRotated = false;
                }

                // Computing the letter rotation
                float XRotation = p / (Mathf.PI / 180.0f);
                placeholder[nSprite].transform.rotation = Quaternion.Euler(XRotation, 0.0f, 0.0f);
            }
        }

        if(allRotated)
        {
            Destroy(this.gameObject);
        }
    }
}