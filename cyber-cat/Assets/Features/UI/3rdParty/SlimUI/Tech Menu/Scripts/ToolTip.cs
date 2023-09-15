using UnityEngine;

public class ToolTip : MonoBehaviour
 {
	 public RectTransform toolTip;

	 void Update(){
		 toolTip.anchoredPosition = Input.mousePosition;
	 }
 }