using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARButton : MonoBehaviour, OnTouch3D
{
	public int button_num;
	public GameObject button;

    public void Start()
	{
		button.GetComponent<Renderer>().material.color = Color.blue;
	}
	public int getButtonNum()
	{
		return button_num;
	}
    public void setColor(int player)
	{
		if (player == 0)
		{
			button.GetComponent<Renderer>().material.color = Color.red;
		} else 
		{
			button.GetComponent<Renderer>().material.color = Color.green;
		}
	}

}