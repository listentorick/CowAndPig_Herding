using UnityEngine;
using System.Collections;

public class ToggleButton : MonoBehaviour, IButton {
	
	
	public Texture2D activeTexture;
	public Texture2D inactiveTexture;
	public Rect position = new Rect(10,10,112,121);
	public bool isActive = false;
	public bool isHover = false;
	public GUISkin skin;
	private Texture2D currentTexture;
	private float alpha = 0.75f;
	
	// Use this for initialization
	void Start () {
		currentTexture = inactiveTexture;
	}
		
	private void OnGUI(){
		Event e = Event.current;
		GUI.skin = skin;
		GUI.color = new Color(1,1,1,alpha);
		
		//Input.mousePosition uses screen space coordinates, which are inverted from GUI coordinates.
		//Don't use anything from Input in OnGUI, use Event.current instead, like Event.current.mousePosition.
		
		if(position.Contains( Event.current.mousePosition)){
			isHover = true;
			currentTexture = activeTexture;
		} else {
			isHover = true;
			currentTexture = inactiveTexture; 
		}
		
		
		
		if(GUI.Button(position,currentTexture)){
		
			isActive = !isActive;
			if(isActive) {
				currentTexture = activeTexture;
				alpha = 1f;
			} else {
				currentTexture = inactiveTexture; 
				alpha = 0.75f;
			}
			
			OnClick();
		} 
	
	}

	private void OnClick(){
		Click(this);
	}
	
	public event ClickHandler Click;
	public delegate void ClickHandler(IButton sender);
	
}
