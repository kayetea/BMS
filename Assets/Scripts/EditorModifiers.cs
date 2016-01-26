using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EditorModifiers : MonoBehaviour {

	public Text numString;
	public int numInt = 10; //starting number
	public int increments = 50; //mask incremental number
	public string numType;

	public EditorGridManager gridManager;
	public RectTransform blockSeg;
	public RectTransform curveSeg;

	public Sprite curveSegImg;
	public Sprite straightSegImg;

	public void ArrowClick(bool up)
	{
		if(numType == "pressure"){
			if(up)
			{
				numInt += 1;
				curveSeg.sizeDelta = new Vector2(curveSeg.sizeDelta.x, curveSeg.sizeDelta.y + increments);
				blockSeg.sizeDelta = new Vector2(blockSeg.sizeDelta.x, blockSeg.sizeDelta.y + increments);
			}
			else
			{
				numInt -=1;
				curveSeg.sizeDelta = new Vector2(curveSeg.sizeDelta.x, curveSeg.sizeDelta.y - increments);
				blockSeg.sizeDelta = new Vector2(blockSeg.sizeDelta.x, blockSeg.sizeDelta.y - increments);
			}
			numString.text = numInt.ToString();
			gridManager.rowArray[gridManager.currentRow].GetComponent<EditorGridRows>().textColArray[2].text = numInt.ToString();
		}

		else if(numType == "time"){
			if(up)
			{
				numInt += 1;
				curveSeg.sizeDelta = new Vector2(curveSeg.sizeDelta.x + increments, curveSeg.sizeDelta.y);
			}
			else
			{
				numInt -=1;
				curveSeg.sizeDelta = new Vector2(curveSeg.sizeDelta.x - increments, curveSeg.sizeDelta.y);
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
			if(curveSeg.gameObject.GetComponent<Image>().sprite == curveSegImg)
			{
				curveSeg.gameObject.GetComponent<Image>().sprite = straightSegImg;
			}
			else
			{
				curveSeg.gameObject.GetComponent<Image>().sprite = curveSegImg;
			}
		}
	}
}
