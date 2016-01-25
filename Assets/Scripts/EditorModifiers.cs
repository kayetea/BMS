using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EditorModifiers : MonoBehaviour {

	public Text numString;
	public int numInt = 10; //starting number
	public int increments = 50; //mask incremental number

	public RectTransform fillMask;


	public void ArrowClick(bool up)
	{
		if(up)
		{
			numInt += 1;
			numString.text = numInt.ToString();
			fillMask.sizeDelta = new Vector2(fillMask.sizeDelta.x + increments, fillMask.sizeDelta.y);
		}
		else
		{
			numInt -=1;
			numString.text = numInt.ToString();
			fillMask.sizeDelta = new Vector2(fillMask.sizeDelta.x - increments, fillMask.sizeDelta.y);
		}
	}

	public void CurvilinearSegment (bool curve)
	{
		if(curve)
		{

		}
		else
		{

		}
	}
}
