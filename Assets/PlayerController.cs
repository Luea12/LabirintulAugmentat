using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{

	private MazeCell currentCell;
    private MazeDirection currentDirection;

    private MazeCell dest;

    private bool isMoving = false;

	public void SetLocation (MazeCell cell) {
		currentCell = cell;
        Vector3 temp = cell.transform.localPosition;
	    transform.localPosition = cell.transform.position;
	}

    private void Look (MazeDirection direction) {
		transform.localRotation = direction.ToRotation();
        currentDirection = direction;
	}

    private int NumberOfNeighbours (MazeCell cell)
    {

        int neighbours = 0;
        foreach (MazeDirection dir in Enum.GetValues(typeof(MazeDirection)))
        {
            MazeCellEdge edge = cell.GetEdge(dir);
            if(edge is MazePassage){
                neighbours++;
            }
        }
        return neighbours;
    }

	public void Move (MazeDirection direction)
    {
        isMoving = true;

        Look(direction);

    	MazeCellEdge edge = currentCell.GetEdge(direction);
        dest = currentCell;

        bool canContinue = edge is MazePassage; // dead end
        bool shouldContinue = true; // junction

        while(canContinue && shouldContinue)
        {
            canContinue = false;
            shouldContinue = false;
            edge = dest.GetEdge(direction);
            if (edge is MazePassage) {
                dest = edge.otherCell;
                shouldContinue = NumberOfNeighbours(dest)<=2;
                print(NumberOfNeighbours(dest));
                canContinue = true;
            }
        }
        currentCell = dest;
	}

    private void Start () {
        // start = transform.localPosition;
    }

	private void Update () {
        if(!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                Move(MazeDirection.North);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                Move(MazeDirection.East);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                Move(MazeDirection.South);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                Move(MazeDirection.West);
            }
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.position, dest.transform.position, 5.0f * Time.deltaTime);
            isMoving = !(transform.position == dest.transform.position);
        }
	}
}
