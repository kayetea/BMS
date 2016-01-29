using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EditorGridManager: MonoBehaviour {

	public GameObject[] rowArray = new GameObject[7];
	public int currentRow = 0;

	public Text endPressureText;
	public Text timeText;
	public Text flowRateText;
	public Text printIntText;
	

	// Use this for initialization
	void Start () {
		int i = 0;
		foreach (Transform child in this.transform) {
			//reset each text element
			rowArray[i] = child.gameObject;
			i++;
		}
	}

	public void SetStart(){
		EditorGridRows temp;
		temp = rowArray[currentRow].GetComponent<EditorGridRows>();

		temp.textColArray[0].text = (currentRow + 1).ToString();
		temp.textColArray[1].text = "1.00";
		temp.textColArray[2].text = "2.00";
		temp.textColArray[3].text = "True";
		temp.textColArray[4].text = "10";
		temp.textColArray[5].text = "90";
		temp.textColArray[6].text = "1";
	}

	public void NewRow(){
		currentRow += 1;

		EditorGridRows temp;
		temp = rowArray[currentRow].GetComponent<EditorGridRows>();

		temp.textColArray[0].text = (currentRow + 1).ToString();
		temp.textColArray[1].text = rowArray[currentRow-1].GetComponent<EditorGridRows>().textColArray[2].text; //start pressure is last row's end pressure
		temp.textColArray[2].text = endPressureText.text;
		temp.textColArray[3].text = "False";
		temp.textColArray[4].text = "10"; //always starts at 10
		temp.textColArray[5].text = flowRateText.text;
		temp.textColArray[6].text = printIntText.text;

		NewSegment();
		
	}

	public void NewSegment(){
		if (currentRow > 0)
		{
			GameObject segContainer = GameObject.Find ("Canvas/Editor/Grid-Img/SegmentContainer");

			//add new block, set as child of segmentcontainer
			GameObject block = Instantiate(Resources.Load ("BlockSegment-Panel") as GameObject);
			block.transform.SetParent(segContainer.transform, false);

			//temp variables
			RectTransform blockRect = block.GetComponent<RectTransform>(); 
			int childCount = segContainer.transform.childCount;
			RectTransform prevChildRect = segContainer.transform.GetChild(childCount-2).GetComponent<RectTransform>();


			blockRect.sizeDelta = new Vector2 (blockRect.sizeDelta.x, prevChildRect.sizeDelta.y);
			Vector2 tempPos = new Vector2(prevChildRect.localPosition.x + prevChildRect.sizeDelta.x, prevChildRect.localPosition.y); 
			blockRect.localPosition = tempPos;

			EditorModifiers[] editorModScripts = FindObjectsOfType(typeof(EditorModifiers)) as EditorModifiers[];
			foreach (EditorModifiers editorModScript in editorModScripts)
			{
				editorModScript.NewSegment(blockRect);
			}

			//turn on airbreak option
			if(currentRow == 1)
			{
				GameObject.Find ("Canvas/Editor/RightInfo-Panel/AirBreak-Btn").GetComponent<Button>().interactable = true;
			}

		}
	}
}
