using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCollider : MonoBehaviour
{
    public FloatReference LifesObj;
    public AudioClip end;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitEnd()
    {
        LifesObj.Variable.Value -= 1;
        SoundMenager.Play(end, 0.6f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(col.gameObject);
        if (!col.gameObject.GetComponent<SnailHandle>())
            HitEnd();
    }
}
