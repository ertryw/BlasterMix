using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ShieldBehavior : MonoBehaviour
{

    Rigidbody2D rb;
    public FloatReference FallSpeed;
    public float multiplier;
    public Light2D GlobalLight;
    public GameObject Boom;
    public GameObject Number;
    SpriteRenderer number_sprite;
    Light2D BoomLight;
    public Sprite[] numbers;
    public int points;
    // Start is called before the first frame update
    private void Awake()
    {
        points = Random.Range(0, 9);
        GlobalLight.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.0f, 1f);
        BoomLight = Boom.GetComponentInChildren<Light2D>();
        BoomLight.color = GlobalLight.color;
        number_sprite = Number.GetComponentInChildren<SpriteRenderer>();
        number_sprite.sprite = numbers[points];
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, FallSpeed.Variable.Value * multiplier);
    }

    public void ShBoom()
    {
    
      GameObject boom =  Instantiate(Boom, transform.position, Quaternion.identity);
      Destroy(gameObject);
      //boom.transform.parent = transform;
    }
}
