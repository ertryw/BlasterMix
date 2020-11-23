using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCollider : MonoBehaviour
{
    public FloatReference LifesObj;
    public AudioClip end;

    public void HitEnd()
    {
        LifesObj.Variable.Value -= 1;
        SoundMenager.Play(end, SoundMenager.EndDestructor_Volume);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject colliderGameObject = col.gameObject;
        Destroy(colliderGameObject);
        if (colliderGameObject.GetComponent<SnailHandle>())
            HitEnd();
    }
}
