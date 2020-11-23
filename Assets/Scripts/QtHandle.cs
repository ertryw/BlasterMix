using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum QTState 
{
    Left, Right , TypeCenter , CenterSlow, CenterLaser
}

public class QtHandle : MonoBehaviour
{

    public FloatReference GameSpeed;
    public FloatReference Scores;
    public FloatReference Lifes;
    public GameObject quitTimeObj;
    public GameObject snail;
    public GameObject snailSpawn;
    public RadialSlider slider;

    [SerializeField] private AudioClip qtON;
    [SerializeField] private AudioClip qtOFF;
    [SerializeField] private AudioClip Correct;
    [SerializeField] private AudioClip INCorrect;
    public float quitTimeEventsTime = 3f;
    public int quitTimeSnailTime = 5;
    public bool LaserOn;

    void Start()
    {
        Application.targetFrameRate = 75;
        Invoke("quitTimeEvent", quitTimeEventsTime);
        Invoke("snailTime", quitTimeSnailTime);
    }

    string keydown;
    private readonly Array keyCodes = Enum.GetValues(typeof(KeyCode));
    public QTState QTState = QTState.Left;
    public int QTStatePrev = 0;
    public string keypush;
    float gameSpeedTemp = 0;


    void Update()
    {
        QtGetKeyToTypeEvent();
        LaserOn = QTState == QTState.CenterLaser ? true : false;

        switch (QTState)
        {
            case QTState.Left:
                SaveSpeed();
                CalcSpeedIncrement();
                break;
            case QTState.Right:
                SaveSpeed();
                CalcSpeedIncrement();
                break;
            case QTState.TypeCenter:

                GameSpeed.Variable.Value = (gameSpeedTemp / 2);

                if (GetKeyTrue())
                {
                    MoveOut(true);
                    Scores.Variable.Value += 50f;
                    SoundMenager.Play(Correct, SoundMenager.QTTypeCorrect_Volume);
                    GameSpeed.Variable.Value = gameSpeedTemp;
                }

                if (slider.isFilled())
                {
                    MoveOut(false);
                    SoundMenager.Play(INCorrect, SoundMenager.QTTypeInCorrect_Volume);
                    GameSpeed.Variable.Value = gameSpeedTemp;
                    Lifes.Variable.Value -= 1;
                }
                break;
            case QTState.CenterSlow:
                GameSpeed.Variable.Value = (gameSpeedTemp / 0.9f);
                QTTCheckIsTimeOut();
                break;
            case QTState.CenterLaser:
                GameSpeed.Variable.Value = (gameSpeedTemp / 1.5f);
                QTTCheckIsTimeOut();

                break;
            default:
                break;

        }
    }

    private void CalcSpeedIncrement()
    {
        if (!LaserOn)
            GameSpeed.Variable.Value += -0.0005733929f * GameSpeed.Variable.Value + 0.0004810179f;

        if (LaserOn)
            GameSpeed.Variable.Value += (-0.0005733929f * GameSpeed.Variable.Value + 0.0004810179f) * 2;

        if (GameSpeed.Variable.Value > 0.8f)
            GameSpeed.Variable.Value += 0.00005f;

        if (GameSpeed.Variable.Value >= 1f)
            GameSpeed.Variable.Value = 1f;
    }

    private void SaveSpeed()
    {
        gameSpeedTemp = GameSpeed.Variable.Value;
    }

    private void QTTCheckIsTimeOut()
    {
        if (slider.isFilled())
        {
            MoveOut(false);
            GameSpeed.Variable.Value = gameSpeedTemp;
        }
    }

    private void QtGetKeyToTypeEvent()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in keyCodes)
            {
                if (Input.GetKey(keyCode))
                {
                    Debug.Log("KeyCode down: " + keyCode);
                    keydown = keyCode.ToString();
                    break;
                }
            }
        }
    }

    public void SetQTEventIN(string _sliderText,int _sliderTime, float _sliderTimeMultiplier, QTState _qtState)
    {
        slider.Settext(_sliderText);
        slider.globalTime = _sliderTime;
        slider.multiplier = _sliderTimeMultiplier;
        qtObjectMoveCenter(0.2f);
        QTState = _qtState;
    }

    private void qtObjectMoveCenter(float time)
    {
        quitTimeObj.LeanMoveLocalX(0, time);
        slider.Reset();
    }

    public void MoveInQTLaser()
    {
        SetQTEventIN("", 1500, 0.3f, QTState.CenterLaser);
    }

    public void MoveInQTSlow()
    {
        SetQTEventIN("", 720, 0.5f, QTState.CenterSlow);
    }

    public void MoveInQTType()
    {
        string[] Alphabet = new string[26]
        { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        keypush = Alphabet[UnityEngine.Random.Range(0, Alphabet.Length)];
        SetQTEventIN(keypush, 360, 1f, QTState.TypeCenter);
        SoundMenager.Play(qtON, SoundMenager.QTTypeCenter_Volume);
    }

    private void MoveOut(bool _playSound)
    {
        if (QTStatePrev == 0)
            DeepMoveOut(800, QTState.Left);

        if (QTStatePrev == 2)
            DeepMoveOut(-800, QTState.Right);

        if (_playSound)
            SoundMenager.Play(qtOFF, SoundMenager.QTTypeOut_Volume);
    }

    public void DeepMoveOut(int position, QTState state)
    {
        quitTimeObj.LeanMoveLocalX(position, 0.3f);
        slider.Reset();
        QTState = state;
    }


    public bool GetKeyTrue()
    {
        if (keydown == keypush)
            return true;
        else
            return false;
    }
    
    public bool QTEventsActive { get => GameObject.FindWithTag("Snail") && GameObject.FindWithTag("SoundLaser"); }

    private void snailTime()
    {

        if (GameStage.stage != Stages.Play)
            return;

        float delay = UnityEngine.Random.Range(quitTimeSnailTime - 2, quitTimeSnailTime + 8);


        if (QTEventsActive && QTState != QTState.TypeCenter)
            return;

        if (GameSpeed.Variable.Value > 0.7f)
            Instantiate(snail, snailSpawn.transform.position, Quaternion.identity);


        Invoke("quitTimeEvent", delay);

    }

    private void quitTimeEvent()
    {

        if (GameStage.stage != Stages.Play)
            return;

        float delay = UnityEngine.Random.Range(quitTimeEventsTime - 0.5f, quitTimeEventsTime + 0.5f);

        if (QTEventsActive)
            return;

        switch (QTState)
        {
            case QTState.Left:
                QTStatePrev = 0;
                MoveInQTType();
                break;
            case QTState.Right:
                QTStatePrev = 2;
                MoveInQTType();
                break;
            default:
                break;
        }

        //Do stuff
        Invoke("quitTimeEvent", delay);
    }


}
