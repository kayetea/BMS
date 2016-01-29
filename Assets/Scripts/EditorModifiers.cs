using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EditorModifiers : MonoBehaviour {

	public Text numString;
	public int numInt = 10; //starting number
	public int increments = 50; //mask incremental number
	public string numType;

	public EditorGridManager gridManager;
	public RectTransform currentSegment; //assign the curve as the starting segment in the editor

	public Sprite curveSegImg;
	public Sprite straightSegImg;

	public void ArrowClick(bool up)
	{
		if(numType == "pressure"){
			if(gridManager.currentRow == 0){
				if(up)
				{
					numInt += 1;
					currentSegment.sizeDelta = new Vector2(currentSegment.sizeDelta.x, currentSegment.sizeDelta.y + increments);
				}
				else
				{
					numInt -=1;
					currentSegment.sizeDelta = new Vector2(currentSegment.sizeDelta.x, currentSegment.sizeDelta.y - increments);
				}
				numString.text = numInt.ToString();
				gridManager.rowArray[gridManager.currentRow].GetComponent<EditorGridRows>().textColArray[2].text = numInt.ToString();
			}
			else{ //convert from rectangle segment to curved
				//currentSegment.localScale = new Vector3(-currentSegment.localScale.x, currentSegment.localScale.y, currentSegment.localScale.z);
				//currentSegment.localPosition = new Vector2(currentSegment.localPosition.x + currentSegment.sizeDelta.x, currentSegment.localPosition.y);
				currentSegment.gameObject.GetComponent<Image>().sprite = straightSegImg;
			}
		}

		else if(numType == "time"){
			if(up)
			{
				numInt += 1;
				currentSegment.sizeDelta = new Vector2(currentSegment.sizeDelta.x + increments, currentSegment.sizeDelta.y);
			}
			else
			{
				numInt -=1;
				currentSegment.sizeDelta = new Vector2(currentSegment.sizeDelta.x - increments, currentSegment.sizeDelta.y);
			}
			numString.text = numInt.ToString();
			gridManager.rowArray[gridManager.currentRow].GetComponent<EditorGridRows>().textColArray[4].text = numInt.ToString();
		}
		else if(numType == "flow"){
			if(up)
			{
				numInt += 1;
			}
			else
			{
				numInt -=1;
			}
			numString.text = numInt.ToString();
			gridManager.rowArray[gridManager.currentRow].GetComponent<EditorGridRows>().textColArray[5].text = numInt.ToString();
		}
		else if(numType == "printint"){
			if(up)
			{
				numInt += 1;
			}
			else
			{
				numInt -=1;
			}
			numString.text = numInt.ToString();
			gridManager.rowArray[gridManager.currentRow].GetComponent<EditorGridRows>().textColArray[6].text = numInt.ToString();
		}
	}

	public void CurvilinearSegment ()
	{
		if(gridManager.currentRow == 0){
			if(currentSegment.gameObject.GetComponent<Image>().sprite == curveSegImg)
			{
				currentSegment.gameObject.GetComponent<Image>().sprite = straightSegImg;
			}
			else
			{
				currentSegment.gameObject.GetComponent<Image>().sprite = curveSegImg;
			}
		}
	}

	public void AirBreakClick()
	{
		if(currentSegment.GetComponent<Image>().color != Color.blue)
		{
			currentSegment.GetComponent<Image>().color = Color.blue;
		}
		else
		{
			currentSegment.GetComponent<Image>().color = Color.green;
		}
	}

	public void NewSegment(RectTransform newSeg)
	{
		currentSegment = newSeg;
	}
}
