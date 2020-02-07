using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;
//using System.Media;

public class frames : MonoBehaviour 
{

	private Vector3 startpos;
	private Vector3 endpos;
	private Vector3 pos;
	private int count = 1;	
	public float speed = 1;
	public bool move2 = true;
	float ElapsedTime = 0;
    	float FinishTime = 60f; 
	public static float imageX;
	public static float imageY;
	string line1;
	string line2;
	StreamReader theReader = new StreamReader(levels.levelname, Encoding.Default);
	List<Vector3> posList = new List<Vector3>();
	int countBegining=0;
	int score = 100;

	// Use this for initialization
	void Start () 
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 240;
		line1 = theReader.ReadLine ();
		while(line1 != null) 
		{
			string[] entries1 = line1.Split(' ');
			pos = new Vector3(float.Parse(entries1[0])*1024/1500, (-1*float.Parse(entries1[1])+1000)*768/1000);
			posList.Add(pos);
			line1 = theReader.ReadLine();	
			line1 = theReader.ReadLine();		
		}
		transform.position = posList[0];
		pos = posList[posList.Count - 1];
	}
	void Draw()
	{

			// file x y coordinate
			if (move2) {
			
				endpos = posList [count];
				float dist = Vector3.Distance (transform.position, endpos);
				imageX = transform.position.x;
				imageY = transform.position.y;
				if ((move.appX == imageX && move.appY == imageY) || countBegining == 1) 
				{
					Vector3 velocity = Vector3.zero;
					float smoothTime = 0.3F;
					transform.position = Vector3.MoveTowards (transform.position, endpos, dist);  
					if (transform.position == pos)
						move2 = false;
					count += 1;
					score = calculateScore (move.appX, move.appY, frames.imageX, frames.imageY, score);
					countBegining = 1;
				}
			}

	}

	// Update is called once per frame
	void Update ()
	{

		Draw ();

	
	}
	public static int calculateScore(float x1, float y1,float x2, float y2 , int score)
	{

		var finalResult = CalculateDistance(x1, y1, x2, y2);

		//calculating score - we need to add popups in here.

		if (x1 != x2 && y1 != y2)
		{
			if (finalResult < 20)// popup good.
			{
				score = score + 5;
			}
			else if (finalResult > 20 && finalResult < 30) // popup you are getting far from the line!
			{
				score = score - 5;
			}
			else if (finalResult > 30 && finalResult < 40) // popup you are getting far from the line!!
			{
				score = score - 10;
			}
			else if (finalResult > 40 && finalResult < 50) // popup you are far from the line!!!
			{
				score = score - 15;
			}
			else // popup you are too far from the line!!!
			{
				score = score - 20;
			}

		}
		else
		{
			score = score + 10;
		}

		return score;

	}
	public static float CalculateDistance(float x1, float x2, float y1, float y2)
	{

		float temp1 = Mathf.Pow((x2 - x1), 2);
		float	temp2 = Mathf.Pow((y2 - y1), 2);
		float result = Mathf.Sqrt(temp1 + temp2);

		return result;
	}
}
