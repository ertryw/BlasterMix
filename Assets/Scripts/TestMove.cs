using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    private SpriteRenderer quadRenderer;

    public float scrollSpeed = 0.01f;
    public float x;
    public FloatReference FallSpeed;
    public GameEtaps GE;
    // Start is called before the first frame update

    private void Awake()
    {
        //GE = GetComponent<GameEtaps>();
    }

    void Start()
    {
        quadRenderer = GetComponent<SpriteRenderer>();
    
    }

    void Update()
    {
        if (GE.Stage == stages.play)
        {
            x += FallSpeed.Variable.Value * (-1) * (0.02f);
            scrollSpeed = x;
            //scrollSpeed = FallSpeed.Variable.Value * (-1);
            //scrollSpeed = scrollSpeed + (scrollSpeed * 0.8f);
            //Vector2 textureOffset = new Vector2(Time.time * scrollSpeed, 0);
            Vector2 textureOffset = new Vector2(scrollSpeed, 0);
            quadRenderer.material.SetTextureOffset("_MainTex", textureOffset);
        }
    }
}
