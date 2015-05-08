using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ScoreBoard : MonoBehaviour
{
	public int[] scores;
	public int maxDart;
	public int curDart;
	public Vector3[] dartPositions;
	public int commentIdx;
	public Font font;

	void Start() 
	{
		maxDart = 3;
		curDart = 0;
		dartPositions = new Vector3[maxDart];
		scores = new int[maxDart];
	}

	void Update ()
	{

	}

	void OnGUI ()
	{
		GUI.skin.font = font;
		GUI.skin.label.fontSize = 20;
		int sum = 0;
		for(int i = 0; i < curDart; i++)
		{
			GUI.Label(new Rect(55,50+(i*20),Screen.width,Screen.height),scores[i].ToString());
			sum += scores[i];
		}
		GUI.Label(new Rect(30,65+(maxDart*20),Screen.width,Screen.height),"Total : "+sum.ToString());
	}

	public int countScores(Vector3 position)
	{
		int result = 0;
		if (transform.position.y > 2) 
		{
			commentIdx = 1;
		}
		else if (transform.position.x < -5) 
		{
			commentIdx = 2;
		}
		else if (transform.position.y < -3) 
		{
			commentIdx = 3;
		}
		else if (transform.position.z < 1) 
		{
			commentIdx = 4;
		}
		else
		{
			Vector3 v = position;
			double radius = Mathf.Sqrt(v.x*v.x + v.y*v.y);
			double angle = Mathf.Atan2(v.y, v.x);

			int score = 0;
			if 		(angle <= Mathf.PI/10*1.5 && angle > Mathf.PI/10*0.5)
				score = 13;
			else if (angle <= Mathf.PI/10*2.5 && angle > Mathf.PI/10*1.5)
				score = 4;
			else if (angle <= Mathf.PI/10*3.5 && angle > Mathf.PI/10*2.5)
				score = 18;
			else if (angle <= Mathf.PI/10*4.5 && angle > Mathf.PI/10*3.5)
				score = 1;
			else if (angle <= Mathf.PI/10*5.5 && angle > Mathf.PI/10*4.5)
				score = 20;
			else if (angle <= Mathf.PI/10*6.5 && angle > Mathf.PI/10*5.5)
				score = 5;
			else if (angle <= Mathf.PI/10*7.5 && angle > Mathf.PI/10*6.5)
				score = 12;
			else if (angle <= Mathf.PI/10*8.5 && angle > Mathf.PI/10*7.5)
				score = 9;
			else if (angle <= Mathf.PI/10*9.5 && angle > Mathf.PI/10*8.5)
				score = 14;
			else if (angle >= -Mathf.PI/10*1.5 && angle < -Mathf.PI/10*0.5)
				score = 10;
			else if (angle >= -Mathf.PI/10*2.5 && angle < -Mathf.PI/10*1.5)
				score = 15;
			else if (angle >= -Mathf.PI/10*3.5 && angle < -Mathf.PI/10*2.5)
				score = 2;
			else if (angle >= -Mathf.PI/10*4.5 && angle < -Mathf.PI/10*3.5)
				score = 17;
			else if (angle >= -Mathf.PI/10*5.5 && angle < -Mathf.PI/10*4.5)
				score = 3;
			else if (angle >= -Mathf.PI/10*6.5 && angle < -Mathf.PI/10*5.5)
				score = 19;
			else if (angle >= -Mathf.PI/10*7.5 && angle < -Mathf.PI/10*6.5)
				score = 7;
			else if (angle >= -Mathf.PI/10*8.5 && angle < -Mathf.PI/10*7.5)
				score = 16;
			else if (angle >= -Mathf.PI/10*9.5 && angle < -Mathf.PI/10*8.5)
				score = 8;
			else if (angle <= Mathf.PI/10*0.5 || angle > -Mathf.PI/10*0.5)
				score = 6;
			else if (angle <= Mathf.PI/10*9.5 || angle > -Mathf.PI/10*9.5)
				score = 11;

			int multiplier = 1;
			if (radius >= 0 && radius < 0.240) 
			{
				multiplier = 2;
				score = 25;
			}
			else if (radius >= 0.240 && radius < 0.350) 
			{
				multiplier = 1;
				score = 25;
			}
			else if (radius > 0.765 && radius < 0.855) 
			{
				multiplier = 3;
			}
			else if (radius > 1.090 && radius < 1.180) 
			{
				multiplier = 2;
			}
			else if (radius >= 1.160) 
			{
				multiplier = 0;
				commentIdx = 0;
			}

			result = score*multiplier;

			if (result == 0) 
				commentIdx = 6;
			else if (result < 10) 
				commentIdx = 5;
		}
		return result;
	}

	public void addDart(Vector3 position) 
	{
		if(position.z > 3.90) 
		{
			GetComponent<AudioSource>().Play();

			GameObject hl = GameObject.Find("Hitlight");
			hl.transform.position = new Vector3(position.x,position.y,hl.transform.position.z);

			Light light = (Light)hl.GetComponent<Light>();
			light.DOIntensity(2,0.3f).OnComplete(delegate(){ light.DOIntensity(0,0.3f); });
		}
		dartPositions[curDart] = position;
		scores[curDart] = countScores(position);
		curDart++;
	}

	public void emptyBoard()
	{
		dartPositions = new Vector3[maxDart];
		scores = new int[maxDart];
		curDart = 0;
		
		GameObject[] stuckDarts;
        stuckDarts = GameObject.FindGameObjectsWithTag("Stuck");
        foreach (GameObject dart in stuckDarts) 
        {
        	GameObject.Destroy(dart);
        }
	}

	public bool isBoardFull() 
	{
		return (curDart == maxDart);
	}
}
