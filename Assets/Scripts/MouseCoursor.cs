using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCoursor : Singleton<MouseCoursor>
{
    // (Optional) Prevent non-singleton constructor use.
    protected MouseCoursor() { }
    public Texture PickTexture;
    public GameObject UIImageClick;
    public float PickTextureResolution = 1;
    public Material mat;
    private Rect rect;
    public Vector2 Offset;

    void Start()
    {
       
    }

    public void StartMethod()
    {
        GameObject gobj = GameObject.Find("Global");
    }

    // rysowanie textury z UIImage //
    void OnGUI()
    {
        if (Event.current.type.Equals(EventType.Repaint))
        {
            if (PickTexture != null)
            {
                Vector2 ImagePos = new Vector2(Input.mousePosition.x - (PickTexture.width / 2), Screen.height - Input.mousePosition.y - (PickTexture.height / 2));
                Graphics.DrawTexture(new Rect(ImagePos.x + Offset.x, ImagePos.y + Offset.y, rect.width * PickTextureResolution, rect.height * PickTextureResolution), PickTexture,mat);
                UnityEngine.Cursor.visible = false;
            }
        }
    }
    void Update()
    {
        if (UIImageClick == null)
        {
            ClearMouseTexture();
        }

    }

    public void ClearMouseTexture()
    {
        UnityEngine.Cursor.visible = true;
        PickTexture = null;
    }

    // Pobieranie tekstury, wymirów i przypisanych obiektów 
    // z naciśnietego obrazka // 
    public void GetUIImageProps()
    {
        if (UIImageClick != null)
        {
            Debug.Log("test");
            PickTexture = UIImageClick.GetComponent<UnityEngine.UI.Image>().mainTexture;
       
            rect = UIImageClick.GetComponent<RectTransform>().rect;
        }
    }

}
