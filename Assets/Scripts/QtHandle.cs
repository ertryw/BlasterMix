using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QtHandle : MonoBehaviour
{

    public FloatReference GameSpeed;
    public FloatReference Scores;
    public FloatReference Lifes;
    public GameObject quitTimeObj;
    public GameObject snail;
    public GameObject snailSpawn;
    public RadialSlider slider;

    public AudioClip qtON;
    public AudioClip qtOFF;
    public AudioClip Correct;
    public AudioClip INCorrect;
    public float quitTimeEventsTime = 3f;
    public int quitTimeSnailTime = 5;
    public bool LaserOn;
    public GameEtaps GE;

    private void Awake()
    {
        GE = GetComponent<GameEtaps>();
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        StartCoroutine("quitTime");
        StartCoroutine("snailTime");
    }

    string keydown;
    private readonly Array keyCodes = Enum.GetValues(typeof(KeyCode));


    float gameSpeedTemp = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in keyCodes)
            {
                if (Input.GetKey(keyCode))
                {
                    Debug.Log("KeyCode down: " + keyCode);
                    keydown = keyCode.ToString();
                    if (keydown != keypush)
                    {
                        //SoundMenager.Play(INCorrect, 0.8f);
                    }
                        break;
                }
            }
        }


        //GameSpeed.Variable.Value = 0.3f + (Scores.Variable.Value / 3000f);

        if (QTState == 1 || QTState == 4 || QTState == 6)
        {
            if (QTState == 1)
                GameSpeed.Variable.Value = (gameSpeedTemp / 2);
            if (QTState == 4)
                GameSpeed.Variable.Value = (gameSpeedTemp / 1.5f);
            if (QTState == 6)
                GameSpeed.Variable.Value = (gameSpeedTemp / 0.9f);
        }
        else
        {
            gameSpeedTemp = GameSpeed.Variable.Value;
            // GameSpeed.Variable.Value = 0.3f + (Scores.Variable.Value / 3000f);
            //GameSpeed.Variable.Value += 0.0001582261f * Mathf.Pow(GameSpeed.Variable.Value, -2.488733f);
            if (!LaserOn)
                GameSpeed.Variable.Value += -0.0005733929f * GameSpeed.Variable.Value + 0.0004810179f;

            if (LaserOn)
                GameSpeed.Variable.Value += (-0.0005733929f * GameSpeed.Variable.Value + 0.0004810179f)*2;

            if (GameSpeed.Variable.Value > 0.8f)
                GameSpeed.Variable.Value += 0.00005f;

            //    0.00001f;

            if (GameSpeed.Variable.Value >= 1f)
            {
                GameSpeed.Variable.Value = 1f;
            }
        }


        if (QTState == 1 && GetKeyTrue())
        {
            if (QTStatePrev == 0)
                MoveOutQTRight();
            
             if (QTStatePrev == 2)
                 MoveOutQTLeft();

            Scores.Variable.Value += 50f;
            SoundMenager.Play(Correct,0.8f);

            GameSpeed.Variable.Value = gameSpeedTemp;
        }
        
        if (QTState == 1 && slider.isFilled())
        {
            SoundMenager.Play(INCorrect, 0.6f);

            if (QTStatePrev == 0)
                MoveOutQTRightSlow();

            if (QTStatePrev == 2)
                MoveOutQTLeftSlow();
            
            GameSpeed.Variable.Value = gameSpeedTemp;
            Lifes.Variable.Value -= 1;
        }

        if ((QTState == 4 || QTState == 6) && slider.isFilled())
        {

            if (QTStatePrev == 0)
                MoveOutQTRightSlow();

            if (QTStatePrev == 2)
                MoveOutQTLeftSlow();

            LaserOn = false;
            GameSpeed.Variable.Value = gameSpeedTemp;
        }

    }

    public int QTState = 0;
    public int QTStatePrev = 0;
    public string keypush;
    public void MoveInLaser()
    {
        slider.Settext("");
        quitTimeObj.LeanMoveLocalX(0, 0.2f);
        slider.globalTime = 1500;
        slider.multiplier = 0.3f;
        slider.Reset();
        LaserOn = true;
        QTState = 6;
    }

    public void MoveInSlow()
    {
        slider.Settext("");
        quitTimeObj.LeanMoveLocalX(0, 0.2f);
        slider.globalTime = 720;
        slider.multiplier = 0.5f;
        slider.Reset();
        QTState = 4;
    }
    public void MoveOutQTRightSlow()
    {
        quitTimeObj.LeanMoveLocalX(800, 0.3f);
        slider.Reset();
        QTState = 2;
    }
    public void MoveOutQTLeftSlow()
    {
        quitTimeObj.LeanMoveLocalX(-800, 0.3f);
        slider.Reset();
        QTState = 0;
    }

    public void MoveInQT()
    {
        keypush = Alphabet[UnityEngine.Random.Range(0, Alphabet.Length)];
        slider.Settext(keypush);
        slider.globalTime = 360;
        slider.multiplier = 1f;
        quitTimeObj.LeanMoveLocalX(0, 0.2f);
        slider.Reset();
        SoundMenager.Play(qtON, 0.8f);
        QTState = 1;
    }

    public void MoveOutQTRight()
    {
        quitTimeObj.LeanMoveLocalX(800, 0.3f);
        slider.Reset();
        SoundMenager.Play(qtOFF, 0.8f);
        QTState = 2;
    }
    public void MoveOutQTLeft()
    {
        quitTimeObj.LeanMoveLocalX(-800, 0.3f);
        slider.Reset();
        SoundMenager.Play(qtOFF, 0.8f);
        QTState = 0;
    }
    string[] Alphabet = new string[26]
    { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

    public bool GetKeyTrue()
    {
        if (keydown == keypush)
            return true;
        else
            return false;
    }


    IEnumerator snailTime()
    {

        while (true)
        {
            if (GE.Stage == stages.play)
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(quitTimeSnailTime - 2, quitTimeSnailTime + 8));

                if (!GameObject.FindWithTag("Snail") && QTState != 1 && !GameObject.FindWithTag("SoundLaser") && GE.Stage == stages.play)
                {

                    if (GameSpeed.Variable.Value > 0.7f)
                    {
                        Instantiate(snail, snailSpawn.transform.position, Quaternion.identity);
                    }
                }
            }

            yield return 0;



        }
    }


   IEnumerator quitTime()
    {

        while (true)
        {

            if (GE.Stage == stages.play)
            {

                yield return new WaitForSeconds(UnityEngine.Random.Range(quitTimeEventsTime - 0.5f, quitTimeEventsTime) + 0.5f);


                if (!GameObject.FindWithTag("Snail") && !GameObject.FindWithTag("SoundLaser") && GE.Stage == stages.play)
                {
                    if (QTState == 0)
                    {
                        QTStatePrev = 0;
                        MoveInQT();
                    }

                    if (QTState == 2)
                    {
                        QTStatePrev = 2;
                        MoveInQT();

                    }
                }
            }


            yield return 0;
        }
    }
}
