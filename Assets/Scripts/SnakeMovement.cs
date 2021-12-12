using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    private List<GameObject> parts = new List<GameObject>();

    [SerializeField] private GameObject head;

    [SerializeField] private GameObject body;

    [SerializeField] float speed = 3f;

    string currentInput;
    string currentDir;
    
    Transform partTransform;

    // Directions
    Vector3 leftDir;
    Vector3 rightDir;
    Vector3 upDir;
    Vector3 downDir;

    Vector3 newDir;

    Vector3 newRotation;

    bool hasToRotate;

    void Start()
    {
        //Reset all variables when starting
        currentInput = "up";
        currentDir = "up";
        newDir = transform.position;     
        hasToRotate = false;
        this.parts.Add(this.head);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        MoveHead(head);//Move the head, this makes the rest of the body move
    }

    private void MovePart(Transform targetPos, int index)
    {
        if (parts.Count >= (index+1))//If there are parts to move
        {         
            GameObject part = parts[index];
            partTransform = part.transform;//Transform the next snake part must reach
            part.transform.Rotate(targetPos.rotation.eulerAngles);

            //Move the snake part
            Vector3 behind = -targetPos.forward;
            part.transform.position = Vector3.MoveTowards(part.transform.position, targetPos.position + behind, Time.deltaTime * speed);
            MovePart(partTransform, (index+1));
        }       
    }

    private void MoveHead(GameObject part)
    {
        partTransform = part.transform;//Transform the next snake part must reach

        leftDir = new Vector3(part.transform.position.x - 1, part.transform.position.y, part.transform.position.z);
        rightDir = new Vector3(part.transform.position.x + 1, part.transform.position.y, part.transform.position.z);
        upDir = new Vector3(part.transform.position.x, part.transform.position.y, part.transform.position.z + 1);
        downDir = new Vector3(part.transform.position.x, part.transform.position.y, part.transform.position.z - 1);

        switch (currentInput.ToLower())
        {
            case "up"://Move to this direction only if the current one isnt the opposite
                if (!currentDir.Equals("down"))
                {
                    newDir = upDir;
                    switch (currentDir.ToLower())
                    {
                        case "left":
                            newRotation = new Vector3(0, 90, 0); //Rotate 90 to north
                            hasToRotate = true;//It has to rotate only if it can change direction
                            //Create a direction change point only it can change directon
                            break;

                        case "right":
                            newRotation = new Vector3(0, -90, 0); //Rotate -90 to north
                            hasToRotate = true;
                            break;
                    }
                }
                else
                {
                    newDir = downDir;
                }
                break;

            case "down":
                if (!currentDir.Equals("up"))
                {
                    newDir = downDir;
                    switch (currentDir.ToLower())
                    {
                        case "left":
                            newRotation = new Vector3(0, -90, 0); //Rotate -90 to south
                            hasToRotate = true;
                            break;

                        case "right":
                            newRotation = new Vector3(0, 90, 0); //Rotate 90 to south
                            hasToRotate = true;
                            break;
                    }
                }
                else
                {
                    newDir = upDir;
                }
                break;

            case "left":
                if (!currentDir.Equals("right"))
                {
                    newDir = leftDir;
                    switch (currentDir.ToLower())
                    {
                        case "up":
                            newRotation = new Vector3(0, -90, 0); //Rotate -90 to west
                            hasToRotate = true;
                            break;

                        case "down":
                            newRotation = new Vector3(0, 90, 0); //Rotate 90 to west
                            hasToRotate = true;
                            break;
                    }
                }
                else
                {
                    newDir = rightDir;
                }
                break;

            case "right":
                if (!currentDir.Equals("left"))
                {
                    newDir = rightDir;
                    switch (currentDir.ToLower())
                    {
                        case "up":
                            newRotation = new Vector3(0, 90, 0); //Rotate 90 to east
                            hasToRotate = true;
                            break;

                        case "down":
                            newRotation = new Vector3(0, -90, 0); //Rotate -90 to east
                            hasToRotate = true;
                            break;
                    }
                }
                else
                {
                    newDir = leftDir;
                }
                break;

            default:
                break;
        }

        if (hasToRotate && !currentDir.Equals(currentInput))//Rotate only if a direction change has happened
        {
            hasToRotate = false;
            part.transform.Rotate(newRotation, Space.World);
            currentDir = currentInput;
        }

        //Move the snake part
        part.transform.position = Vector3.MoveTowards(part.transform.position, newDir, Time.deltaTime * speed);

        //Call the recursive function to move the next snake part      
        MovePart(partTransform, 1);
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentInput = "up";
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentInput = "down";
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentInput = "left";
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentInput = "right";
        }      
    }

    public void AddJoint(GameObject jointPrefab)
    {
        GameObject previous = parts[parts.Count - 1];
        Vector3 behind = -previous.transform.forward;
        GameObject newJoint = Instantiate(jointPrefab, previous.transform.position + behind, previous.transform.rotation, this.body.transform);
        this.parts.Add(newJoint);
    }

}
