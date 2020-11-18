using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeedTest : MonoBehaviour
{
    // Start is called before the first frame update

    Animator m_Animator;
    //Value from the slider, and it converts to speed level
    float m_MySliderValue = 0.3f;
    public FloatReference FallSpeed;

    void Start()
    {
        //Get the animator, attached to the GameObject you are intending to animate.
        m_Animator = gameObject.GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        float temp = Mathf.Lerp(m_Animator.speed, FallSpeed.Variable.Value, 4f);
        m_Animator.speed = temp;
    }
}

