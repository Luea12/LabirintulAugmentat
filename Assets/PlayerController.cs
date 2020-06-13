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

    private ProfileData profile;

    public void SetLocation(MazeCell cell)
    {
        currentCell = cell;
        Vector3 temp = cell.transform.position;
        transform.position = temp;
        transform.localScale = Vector3.one*5f;
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
    }

    private void Start()
    {
        profile = ProfileData.Load();
    }

    public void SetPlayerModel(GameObject model)
    {
        GameObject instance = Instantiate(model);
        instance.transform.SetParent(this.transform);
        instance.transform.position = new Vector3(0,0.5f,0);
    }

    private void FixedUpdate()
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
            transform.position = Vector3.MoveTowards(transform.position, destPosition, 1f* Time.deltaTime);
            isMoving = !(transform.position == destPosition);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            //GameManager.CoinPickup.Invoke();
            GameEvents.current.CoinPickup();
        }

        if(other.gameObject.tag == "Teleport")
        {
            GameManager.instance.currentLevel++;

            if(GameManager.instance.currentLevel <= GameManager.instance.currentDifficulty.numberOfLevels)
            {
                profile.UpdateCoins(GameManager.instance.currentNumberOfCoins);
                GameEvents.current.LevelUp();
                GameManager.instance.RestartGame();
            }
            else
            {
                profile.UpdateCoins(GameManager.instance.currentNumberOfCoins);
                GameEvents.current.GameWin();
            }
        }
    }
}
