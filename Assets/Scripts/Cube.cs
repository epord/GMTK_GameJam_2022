using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    enum MoveDirection { UP, RIGHT, DOWN, LEFT }

    public float speed = 3.0f;

    // From center of cube
    private readonly Vector3 anchorUp = new Vector3(1, -1, 0);
    private readonly Vector3 anchorRight = new Vector3(0, -1, -1);
    private readonly Vector3 anchorDown = new Vector3(-1, -1, 0);
    private readonly Vector3 anchorLeft = new Vector3(0, -1, 1);

    private readonly Vector3 xAxis = new Vector3(1, 0, 0);
    private readonly Vector3 yAxis = new Vector3(0, 1, 0);
    private readonly Vector3 zAxis = new Vector3(0, 0, 1);

    private bool isRotating = false;

    private void Update()
    {
        if (!isRotating)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Vector3 anchor = transform.position + new Vector3(1, -1, 0);
                Vector3 axis = -zAxis;
                StartCoroutine(this.Move(anchor, axis));
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Vector3 anchor = transform.position + new Vector3(0, -1, -1);
                Vector3 axis = -xAxis;
                StartCoroutine(this.Move(anchor, axis));
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Vector3 anchor = transform.position + new Vector3(-1, -1, 0);
                Vector3 axis = zAxis;
                StartCoroutine(this.Move(anchor, axis));
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                Vector3 anchor = transform.position + new Vector3(0, -1, 1);
                Vector3 axis = xAxis;
                StartCoroutine(this.Move(anchor, axis));
            }
        }
    }

    private IEnumerator Move(Vector3 anchor, Vector3 axis)
    {
        this.isRotating = true;

        for (int i = 0; i < (90 / speed); i++)
        {
            transform.RotateAround(anchor, axis, speed);
            //yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(0.01f);
        }

        this.RoundTransform();

        this.isRotating = false;

    }

    private void RoundTransform()
    {
        transform.position = new Vector3(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y),
            Mathf.RoundToInt(transform.position.z));
        transform.rotation = Quaternion.Euler(
            Mathf.RoundToInt(transform.eulerAngles.x),
            Mathf.RoundToInt(transform.eulerAngles.y),
            Mathf.RoundToInt(transform.eulerAngles.z));
    }
}
