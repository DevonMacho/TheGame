using UnityEngine;

public class configure_gui : MonoBehaviour {
	private Rect charStats = new Rect(0, Screen.width/5, Screen.width/2, Screen.width/4);
	private Rect miniMap = new Rect(0,0,Screen.width/5,Screen.width/5);
	public Texture clb; // command line background
	void DoMyWindow(int windowID) {
		GUI.DragWindow(new Rect(0,0,Screen.width,Screen.height));
	}

	void Start () {
	
	}

	void Update () {
		charStats.height = Screen.height / 4;
		charStats.width = Screen.width/3;
		miniMap.height = Screen.width / 5;
		miniMap.width = Screen.width / 5;
	}
	private void OnGUI(){
		GUI.DrawTexture (new Rect(0,Screen.height - (Screen.height * 256 / 768), Screen.width, Screen.height * 256 / 768), clb, ScaleMode.StretchToFill, true, 10.0F);
		charStats = GUI.Window(0, charStats, DoMyWindow, "Char Stats");
		miniMap = GUI.Window(1, miniMap, DoMyWindow, "MiniMap");
		if (GUI.Button (new Rect (15, Screen.height - 3*(Screen.height/10), Screen.width / 5, Screen.height/15), "save")) {
			Debug.Log("minimap " + miniMap.position + " charstats " + charStats.position);
				}
		if (GUI.Button (new Rect (15, Screen.height - (Screen.height/10), Screen.width / 5, Screen.height/15), "back")) {
			Debug.Log("Back pressed");
		}
		if (GUI.Button (new Rect (15, Screen.height - 2*(Screen.height/10), Screen.width /5, Screen.height/15), "reset")) {
			miniMap.x = 0;
			miniMap.y = 0;
			charStats.x = 0;
			charStats.y = 150;
		}
		if(miniMap.x > Screen.width- miniMap.width) // border collision start
		{
			miniMap.x = Screen.width- miniMap.width -1;
		}
		if(miniMap.x < 0)
		{
			miniMap.x = 0+1;
		}
		if(miniMap.y > 2*(Screen.height/3)- miniMap.height)
		{
			miniMap.y = 2*(Screen.height/3)- miniMap.height -1;
		}
		if(miniMap.y < 0)
		{
			miniMap.y = 0+1;
		}
		if(charStats.x > Screen.width- charStats.width)
		{
			charStats.x = Screen.width- charStats.width -1;
		}
		if(charStats.x < 0)
		{
			charStats.x = 0+1;
		}
		if(charStats.y > 2*(Screen.height/3)- charStats.height)
		{
			charStats.y = 2*(Screen.height/3)- charStats.height -1;
		}
		if(charStats.y < 0)
		{
			charStats.y = 0+1;
		} // border collision end

		}


}
