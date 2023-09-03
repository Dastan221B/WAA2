using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ArrowState
{
	On,
	Off
}
public class ArrowScript : MonoBehaviour, IPointerDownHandler
{
	[SerializeField] private TMP_Dropdown _dropdown;
	[SerializeField] private ArrowScript _arrow1;
	[SerializeField] private ArrowScript _arrow2;
	public ArrowState arrowState = ArrowState.Off;

	private void ChangeValue(int v)
    {
		this.transform.localScale = new Vector3(1, 1, 1);
		arrowState = ArrowState.Off;
		Debug.Log("Changed");
	}

    public void OnPointerDown(PointerEventData eventData)
	{

		Debug.Log("is work");
		if(arrowState == ArrowState.Off)
        {
			this.transform.localScale = new Vector3(-1, 1, 1);
			arrowState = ArrowState.On;
		}else
		if(arrowState == ArrowState.On)
        {
			this.transform.localScale = new Vector3(1, 1, 1);
			arrowState = ArrowState.Off;
		}
	}

	public bool GetDropDownList()
	{
		foreach (Transform g in _dropdown.transform.GetComponentsInChildren<Transform>())
		{
			//Debug.Log(g.name);
			if (g.name == "Dropdown List")
				return true;
		}
		return false;
	}

    private void Update()
    {
        if (!GetDropDownList())
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            arrowState = ArrowState.Off;
        }
        else
        {
			this.transform.localScale = new Vector3(-1, 1, 1);
			arrowState = ArrowState.On;
		}
    }
}
