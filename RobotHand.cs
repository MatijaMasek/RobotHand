using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotHand : MonoBehaviour
{   
    [SerializeField] GameObject[] joints;

    Quaternion targetRotation0, targetRotation1;

    [SerializeField] InputField inputField;
    [SerializeField] Text commandInfo;

    string command;

    int jointNumber;
    char rotation;
    int degrees;

    void Start()
    {
        targetRotation0 = joints[0].transform.rotation;
        targetRotation1 = joints[1].transform.rotation;
    }

    void Update()
    {
        switch (jointNumber)
        {
            case 0:
                joints[jointNumber].transform.rotation = Quaternion.Lerp(joints[jointNumber].transform.rotation, targetRotation0, 10 * Time.deltaTime);
                break;
            case 1:
                joints[jointNumber].transform.rotation = Quaternion.Lerp(joints[jointNumber].transform.rotation, targetRotation1, 10 * Time.deltaTime);
                break;
        }
    }

    public void GetInput()
    {
        inputField = GameObject.Find("InputField").GetComponent<InputField>();

        command = inputField.text;

        jointNumber = int.Parse(command[0].ToString());

        rotation = command[1];

        //Getting degrees

        switch (command.Length)
        {
            case 3:
                degrees = int.Parse(command[2].ToString());
                break;
            case 4:
                degrees = int.Parse(command[2].ToString()) * 10 + int.Parse(command[3].ToString());
                break;
            case 5:
                degrees = int.Parse(command[2].ToString()) * 100 + int.Parse(command[3].ToString()) * 10 + int.Parse(command[4].ToString());
                break;
            default:
                commandInfo.text = "Invalid input";
                break;
        }

        Debug.Log(jointNumber + "/" + rotation + "/" + degrees);

        switch (jointNumber)
        {
            case 0:
                if(degrees >= 0 && degrees <= 180)
                {
                    targetRotation0 = Quaternion.AngleAxis(degrees, Vector3.up);
                }
                else
                {
                    commandInfo.text = "Degrees for 0R can be 0-180";
                }               
                break;
            case 1:
                targetRotation1 = Quaternion.AngleAxis(degrees, Vector3.up);
                break;
            default:
                commandInfo.text = "Invalid input";
                break;
        }

        inputField.text = "";
    }
}
