using System.Collections;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private float x;

    private float y;
    private Vector3 rotateValue;

    void Update()
    {
        y = Input.GetAxis("MouseX");

        x = Input.GetAxis("MouseY");

        Debug.Log(x + ":" + y);

        rotateValue = new Vecotr3(x, y * -1, 0);

        transform.eulerangles =
        transform.eulerangles - rotateValue;
    }
}
