using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using System.IO;
//using System.IO.File;
//using UnityVectorExtension;



public class move : MonoBehaviour
{
	Controller controller;
	Finger index;
	Frame frame;
	GameObject ball;
	int appWidth = 1024;
	int appHeight = 768;
	bool singing = false;
	int score = 100;
	int countBegining= 0;
	public static float appX;
	public static float appY;
	//cemal
	/*
	public static ArrayList coordinates = new ArrayList();
	

	String filePath = @levels.levelname;
    ArrayList pointArray = new ArrayList();
    string[] arr = new string[2];
    foreach((String s in File.ReadAllLines(filePath))
	{
        arr = s.Split(' ');
        pointArray.Add(new Point(Math.Round(float.Parse(arr[0]), 2),
        Math.Round(float.Parse(arr[1]), 2)));
    }
		*/
	//cemal

	// Use this for initialization
	void Start()
	{
		ball = GameObject.Find("ball");
		controller = new Controller();
		index = new Finger();
	}

	// Update is called once per frame
	void Update()
	{
		InteractionBox iBox = controller.Frame().InteractionBox;
		frame = controller.Frame();
		List<Hand> handlist = new List<Hand>();
		for (int h = 0; h < frame.Hands.Count; h++)
		{
			Hand leapHand = frame.Hands[h];
			handlist.Add(leapHand);
		}


			//if (index.IsExtended)

			//leap x y read
		index = frame.Hands[0].Fingers[(int)Finger.FingerType.TYPE_INDEX];
		Vector fingPos = index.StabilizedTipPosition;
		Vector norm = iBox.NormalizePoint(fingPos, false);
		 appX = norm.x * appWidth;
		 appY = -1 * (1 - norm.y) * appHeight;
		//float finalResult = Distance (appX, appY, frames.imageX, frames.imageY);
		float distanceX = appX - frames.imageX;
		float distanceY = appY - frames.imageY;
		if (Mathf.Abs(distanceX) < 50 && Mathf.Abs(distanceY) < 50) {
			appX = frames.imageX;
			appY = frames.imageY;
		}

		Vector3 fPos = new Vector3 (appX, appY);
		Plane objPlane = new Plane (Camera.main.transform.forward * -1, ball.transform.position);
		Ray mRay = Camera.main.ScreenPointToRay (fPos);

		float rayDistance;
		if (objPlane.Raycast (mRay, out rayDistance))
			ball.transform.position = mRay.GetPoint (rayDistance);
		print (fPos);



	}



}