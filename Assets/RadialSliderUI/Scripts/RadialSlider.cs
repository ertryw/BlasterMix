using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class RadialSlider: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	public int globalTime = 360;
	public float multiplier = 1f;
	void Start()
	{
		StartCoroutine("TrackPointer");
	}
	// Called when the pointer enters our GUI component.
	// Start tracking the mouse
	public void OnPointerEnter( PointerEventData eventData )
	{
		//StartCoroutine( "TrackPointer" );            
	}
	
	// Called when the pointer exits our GUI component.
	// Stop tracking the mouse
	public void OnPointerExit( PointerEventData eventData )
	{
		//StopCoroutine( "TrackPointer" );
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		//isPointerDown= true;
		//Debug.Log("mousedown");
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		//isPointerDown= false;
		//Debug.Log("mousedown");
	}




	// mainloop
	IEnumerator TrackPointer()
	{
		var ray = GetComponentInParent<GraphicRaycaster>();
		var input = FindObjectOfType<StandaloneInputModule>();

		var text = GetComponentInChildren<Text>();
		
		if( ray != null && input != null )
		{
			while( Application.isPlaying )
			{


			GetComponent<Image>().fillAmount += Time.deltaTime * multiplier;
				var temperatureT = Mathf.InverseLerp(
								globalTime,
								0,
								int.Parse(text.text));


				text.text = (globalTime - ((int)(GetComponent<Image>().fillAmount * globalTime))).ToString();

				GetComponent<Image>().color = Color.Lerp(Color.green, Color.red, temperatureT);

				yield return new WaitForSeconds(Time.deltaTime);
			}        
		}
		else
			UnityEngine.Debug.LogWarning( "Could not find GraphicRaycaster and/or StandaloneInputModule" );        
	}
	public bool isFilled()
	{
		return GetComponent<Image>().fillAmount == 1 ? true : false;
	}

	public void Reset()
	{
		var text = GetComponentInChildren<Text>();
		text.text = (globalTime).ToString();
		GetComponent<Image>().fillAmount = 0;
	}

	string[] Alphabet = new string[26]
	{ "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

	public Text KeyClickTxt;

	public void Settext(string txt)
	{
		KeyClickTxt.text = txt;
	}


}
