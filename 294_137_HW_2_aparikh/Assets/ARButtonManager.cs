using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARButtonManager : MonoBehaviour
{
    private Camera arCamera;
    private PlaceGameBoard placeGameBoard;
	private int currentPlayer = 0;
	private int[] buttonStatuses = { -1, -1, -1, -1, -1, -1, -1, -1, -1 };
	private bool victory;
	public Text messageText;

	public ARButton A00;
	public ARButton A10;
	public ARButton A20;
	public ARButton A01;
	public ARButton A11;
	public ARButton A21;
	public ARButton A02;
	public ARButton A12;
	public ARButton A22;


	void Start()
    {
        // Here we will grab the camera from the AR Session Origin.
        // This camera acts like any other camera in Unity.
        arCamera = GetComponent<ARSessionOrigin>().camera;
        // We will also need the PlaceGameBoard script to know if
        // the game board exists or not.
        placeGameBoard = GetComponent<PlaceGameBoard>();
		victory = false;
    }

    void Update()
    {
        if (placeGameBoard.Placed() && Input.touchCount > 0)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            // Convert the 2d screen point into a ray.
            Ray ray = arCamera.ScreenPointToRay(touchPosition);
            // Check if this hits an object within 100m of the user.
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                // Check that the object is interactable.
                if (hit.transform.tag == "Interactable")
				{   if (victory == false)
					{
					    int button_num = hit.transform.GetComponent<OnTouch3D>().getButtonNum();
					    int current_status = buttonStatuses[button_num];
					    if (current_status == -1)
					    {
						    hit.transform.GetComponent<OnTouch3D>().setColor(currentPlayer);
						    buttonStatuses[button_num] = currentPlayer;
							checkVictory(button_num);
							currentPlayer = 1 - currentPlayer;
						}
					}
				}
			}
        }
    }

    public void restart()
	{
		messageText.gameObject.SetActive(true);
		messageText.text = "Restarting Game";
		buttonStatuses = new int[]{ -1, -1, -1, -1, -1, -1, -1, -1, -1 };
		currentPlayer = 0;
		victory = false;
		A00.GetComponent<Renderer>().material.color = Color.blue;
		A10.GetComponent<Renderer>().material.color = Color.blue;
		A20.GetComponent<Renderer>().material.color = Color.blue;
		A01.GetComponent<Renderer>().material.color = Color.blue;
		A11.GetComponent<Renderer>().material.color = Color.blue;
		A21.GetComponent<Renderer>().material.color = Color.blue;
		A02.GetComponent<Renderer>().material.color = Color.blue;
		A12.GetComponent<Renderer>().material.color = Color.blue;
		A22.GetComponent<Renderer>().material.color = Color.blue;

	}

	public void checkVictory(int last_placed)
	{
       	int col = last_placed % 3;
		int row = ((int)(last_placed / 3)) * 3;
		//messageText.gameObject.SetActive(true);
		//messageText.text = string.Format(" last {0} col {1} row {2}", last_placed, col, row);

		if (buttonStatuses[col + 0] == currentPlayer && buttonStatuses[col + 3] == currentPlayer && buttonStatuses[col + 6] == currentPlayer)
		{
			victory = true;
			messageText.gameObject.SetActive(true);
			messageText.text = string.Format("Player {0} won", currentPlayer);
			return;
		}
		if (buttonStatuses[row + 0] == currentPlayer && buttonStatuses[row + 1] == currentPlayer && buttonStatuses[row +2] == currentPlayer)
		{
			victory = true;
			messageText.gameObject.SetActive(true);
			messageText.text = string.Format("Player {0} won", currentPlayer);
			return;
		}
		if (last_placed == 0 || last_placed == 4 || last_placed == 8)
		{
            if (buttonStatuses[0] == currentPlayer && buttonStatuses[4] == currentPlayer && buttonStatuses[8] == currentPlayer)
			{
				victory = true;
				messageText.gameObject.SetActive(true);
				messageText.text = string.Format("Player {0} won", currentPlayer);
				return;
			}
		}
		if (last_placed == 2 || last_placed == 4 || last_placed == 6)
		{
			if (buttonStatuses[2] == currentPlayer && buttonStatuses[4] == currentPlayer && buttonStatuses[6] == currentPlayer)
			{
				victory = true;
				messageText.gameObject.SetActive(true);
				messageText.text = string.Format("Player {0} won", currentPlayer);
				return;
			}
		}

		int count = 0;
		foreach (var item in buttonStatuses)
		{
            if (item >= 0)
			{
			    count += item;
			}
		}
        if (count == 9)
		{
			messageText.gameObject.SetActive(true);
			messageText.text = string.Format("Tie!");
			return;
		}
        victory = false;
	}


}