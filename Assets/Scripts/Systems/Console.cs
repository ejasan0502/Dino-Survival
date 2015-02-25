using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Console : MonoBehaviour {

	public bool showGUI = false;
	public float scrollSpeed = 10.0f;

	[HideInInspector] public List<string> logs = new List<string>();
	[HideInInspector] public Vector2 scrollPosition = Vector2.zero;
	private Vector2 prevMousePos = Vector2.zero;

	private static object _lock = new object();
	private static Console _instance;
	public static Console instance {
		get {
			if ( _instance == null ) {
				lock(_lock){
					GameObject c = GameObject.Find("Console");
					if ( c == null ){
						GameObject o = new GameObject("Console");
						_instance = o.AddComponent<Console>();
					} else {
						_instance = c.GetComponent<Console>();
					}
				}
			}
			return _instance;
		}
	}

	void Awake(){
		DontDestroyOnLoad(this);
	}

	void Update(){
		if ( !showGUI ) return;

		#if UNITY_EDITOR || UNITY_STANDALONE_WIN
		if ( Input.GetMouseButtonDown(0) ){
			prevMousePos = Input.mousePosition;
		}
		if ( Input.GetMouseButton(0) ){
			scrollPosition += new Vector2(0,(Input.mousePosition.y-prevMousePos.y)*Time.deltaTime*scrollSpeed);
		}
		#elif UNITY_ANDROID || UNITY_IPHONE
		if ( Input.touchCount > 0 ){
			Touch touch = Input.touches[0];
			if ( touch.phase == TouchPhase.Began ){
				prevMousePos = Input.mousePosition;
			}
			if ( touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved ){
				scrollPosition += new Vector2(0,(Input.mousePosition.y-prevMousePos.y)*Time.deltaTime*scrollSpeed);
			}
		}
		#endif
	}

	void OnGUI(){
		if ( !showGUI ) {
			if ( GUI.Button(new Rect(Screen.width-Screen.height*0.1f,0,Screen.height*0.1f,Screen.height*0.1f),"Console") ){
				showGUI = true;
			}
			return;
		} else {
			if ( GUI.Button(new Rect(Screen.width-Screen.height*0.1f,0,Screen.height*0.1f,Screen.height*0.1f),"Exit") ){
				showGUI = false;
			}
		}

		GUI.depth = -100;
		GUI.Box(new Rect(0,0,Screen.width,Screen.height),"");
		GUI.skin.horizontalScrollbar = GUIStyle.none;
		scrollPosition = GUI.BeginScrollView(new Rect(0,0,Screen.width,Screen.height),
											 scrollPosition,
											 new Rect(0,0,Screen.width,Screen.height*0.05f*logs.Count));

		for (int i = 0; i < logs.Count; i++){
			GUI.Label(new Rect(Screen.width*0.01f,Screen.height*0.05f*i,Screen.width,Screen.height*0.05f),logs[i]);
		}

		GUI.EndScrollView();
	}

	public static void Log(string s){
		Debug.Log(s);
		instance.logs.Add("Log " + instance.logs.Count + ": " +s);
		instance.scrollPosition.y += Screen.height*0.05f;
	}
	public static void Log(string text, string className, string methodName){
		string s = className + "." + methodName + " - " + text;
		Debug.Log(s);
		instance.logs.Add("Log " + instance.logs.Count + ": " +s);
		instance.scrollPosition.y += Screen.height*0.05f;
	}
}