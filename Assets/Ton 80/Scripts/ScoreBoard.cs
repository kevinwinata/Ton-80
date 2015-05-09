using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ScoreBoard : MonoBehaviour
{
	public int[] scores;
	public int maxDart;
	public int curDart;
	public Vector3[] dartPositions;
	public int remaining;
	public int prevRemaining;
	public bool isBust;
	public Font font;
	private int w = Screen.width;
	private int h = Screen.height;

	void Start() 
	{
		maxDart = 3;
		curDart = 0;
		remaining = 501;
		dartPositions = new Vector3[maxDart];
		scores = new int[maxDart];
		isBust = false;
	}

	void Update ()
	{

	}

	void OnGUI ()
	{
		GUI.skin.font = font;
		GUI.skin.label.fontSize = 30;
		int sum = 0;
		for(int i = 0; i < curDart; i++)
		{
			GUI.Label(new Rect(w/10.7f,h/7+(i*30),w,h),scores[i].ToString());
			sum += scores[i];
		}
		
		if(isBust) GUI.Label(new Rect(w/2,h/2,w,h),"BUST");
		GUI.Label(new Rect(w/5.0f,h/7+30,w,h),sum.ToString());
		GUI.Label(new Rect(w/19.5f,h/6+(maxDart*30),w,h),
			"Remaining : "+remaining.ToString());
	}

	public int countScores(Vector3 position)
	{
		int result = 0;
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
		if (radius >= 0 && radius < 0.140) 
		{
			multiplier = 2;
			score = 25;
		}
		else if (radius >= 0.140 && radius < 0.250) 
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
		}

		result = score*multiplier;
		
		remaining -= result;

		if(remaining < 0 || (remaining == 0 && multiplier != 2)) 
		{
			isBust = true;
			remaining = prevRemaining;
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
		prevRemaining = remaining;
		
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
