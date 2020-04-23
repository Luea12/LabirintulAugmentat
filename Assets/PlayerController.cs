using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{

    private MazeCell currentCell;
    private MazeDirection currentDirection;

    private MazeCell dest;
    private Vector3 destPosition;
    private bool isMoving = false;

    public void SetLocation(MazeCell cell)
    {
        currentCell = cell;
        Vector3 temp = cell.transform.position;
        temp.y += 0.5f;
        transform.position = temp;
    }

    private void Look(MazeDirection direction)
    {
        transform.localRotation = direction.ToRotation();
        currentDirection = direction;
    }

    private int NumberOfNeighbours(MazeCell cell)
    {

        int neighbours = 0;
        foreach (MazeDirection dir in Enum.GetValues(typeof(MazeDirection)))
        {
            MazeCellEdge edge = cell.GetEdge(dir);
            if (edge is MazePassage)
            {
                neighbours++;
            }
        }
        return neighbours;
    }

    public void Move(MazeDirection direction)
    {
        isMoving = true;

        Look(direction);

        MazeCellEdge edge = currentCell.GetEdge(direction);
        dest = currentCell;

        bool canContinue = edge is MazePassage; // dead end
        bool shouldContinue = true; // junction

        while (canContinue && shouldContinue)
        {
            canContinue = false;
            shouldContinue = false;
            edge = dest.GetEdge(direction);
            if (edge is MazePassage)
            {
                dest = edge.otherCell;
                shouldContinue = NumberOfNeighbours(dest) <= 2;
                canContinue = true;
            }
        }
        currentCell = dest;
        destPosition = dest.transform.position;
        destPosition.y += 0.5f;
    }

    private void Start()
    {
        // start = transform.localPosition;
    }

    private void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move(MazeDirection.North);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move(MazeDirection.East);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move(MazeDirection.South);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move(MazeDirection.West);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, destPosition, 5.0f * Time.deltaTime);
            isMoving = !(transform.position == destPosition);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            GameManager.CoinPickup.Invoke();
        }

        if(other.gameObject.tag == "Teleport")
        {
            GameManager.instance.RestartGame();
        }
    }
}
