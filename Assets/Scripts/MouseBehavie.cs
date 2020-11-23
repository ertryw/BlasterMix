using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MouseBehavie : MonoBehaviour
{

    public GameObject img;
    public GameObject img2;
    public Vector2 offset;
    public AudioClip laser;
    public AudioClip laserlow;
    public AudioClip[] boom;
    public AudioClip slow;
    public Material mat;
    public Text ScoresTxt;
    public GameObject boomLaser;
    public FloatReference Combo;
    public Text ComboTxt;
    public int ComboToLaser = 50;
    public FloatReference Scores;
    QtHandle qt;
    public UnityEngine.Experimental.Rendering.Universal.Light2D PlayerLight;
    public GameObject laserLight;
    // Start is called before the first frame update
    void Start()
    {
        MouseCoursor.Instance.UIImageClick = img;
        MouseCoursor.Instance.mat = mat;
        MouseCoursor.Instance.GetUIImageProps();
        qt = GetComponent<QtHandle>();
    }


    bool laserSound = false;
    void Small()
    {
        ComboTxt.gameObject.LeanScale(new Vector3(1, 1, 1), 0.2f);
    }
    GameObject Laser_Sound;
    // Update is called once per frame
    void Update()
    {
        MouseCoursor.Instance.Offset = offset;


        if (Combo.Variable.Value > 0)
        {
            ComboTxt.text = "Combo x " + Combo.Variable.Value;
        }
        else
        {
            ComboTxt.text = "";
        }

        if ((Combo.Variable.Value % ComboToLaser == 0) && Combo.Variable.Value> 0)
        {
            ComboTxt.color = Color.red;
            StartCoroutine("laserTime");
        } 
        else
        {
            if (!qt.LaserOn)
            {
                ComboTxt.color = Color.yellow;
                StopCoroutine("laserTime");
            }
        }


        if (qt.LaserOn)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            TagetHit(hit, true);
            Vector3 mosueposWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           // laserLight.SetActive(true);
            laserLight.transform.position = new Vector3(mosueposWorld.x - 0.1f, mosueposWorld.y - 0.1f, 10);

            if (laserSound == false)
            {
                Laser_Sound = SoundMenager.PlayLoop(laserlow, 0.8f);
                laserSound = true;
            }
        }
        else
        {

            if (laserSound == true)
            {
                Destroy(Laser_Sound);
                laserSound = false;
            }
            // laserLight.SetActive(false);
            MouseCoursor.Instance.UIImageClick = img;
             MouseCoursor.Instance.GetUIImageProps();
  

            if (Input.GetMouseButtonDown(0))
            {
                SoundMenager.Play(laser, 0.3f);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

                TagetHit(hit, false);
            }
        }
    }
    IEnumerator laserTime()
    {

        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            Vector3 mosueposWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(boomLaser, new Vector3(mosueposWorld.x - 0.1f, mosueposWorld.y - 0.1f, 1), Quaternion.identity);

        }
    }
    

    private void TagetHit(RaycastHit2D hit,bool laser)
    {


        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            ShieldBehavior sh = hit.collider.gameObject.GetComponent<ShieldBehavior>();
            if (sh != null)
            {
                ScoresTxt.color = (Color.cyan + sh.GlobalLight.color) / 2;

                Combo.Variable.Value += 1;


                if (Combo.Variable.Value % ComboToLaser == 0)
                {
                    ComboTxt.gameObject.LeanScale(new Vector3(3, 3, 3), 0.2f).setOnComplete(Small);
                    qt.MoveInQTLaser();
                    MouseCoursor.Instance.UIImageClick = img2;
                    MouseCoursor.Instance.GetUIImageProps();
                }
                else
                {


                    ComboTxt.gameObject.LeanScale(new Vector3(2, 2, 2), 0.2f).setOnComplete(Small);
                }


                sh.ShBoom();
                Scores.Variable.Value += sh.points * 2;
                ScoresTxt.text = Scores.Variable.Value.ToString();
                SoundMenager.Play(boom[Random.Range(0, boom.Length)], 0.6f);

            }
            else if (hit.collider.gameObject.GetComponent<SnailHandle>() != null)
            {
                SnailHandle snail = hit.collider.gameObject.GetComponent<SnailHandle>();
                snail.SnailBoom();
                SoundMenager.Play(slow, 0.5f);
                qt.MoveInQTSlow();
            }
        }
        else
        {
            if(!laser)
                Combo.Variable.Value = 0;
        }
    }
}
