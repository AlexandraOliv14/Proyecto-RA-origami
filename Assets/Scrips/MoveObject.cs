using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{

    public Transform targetObject; // Objeto a escalar
    private Vector2 previousDistance;
    public float scaleSpeed = 0.1f;
    private Vector2 previousVector;
    private float previousRotation;
    public float rotationSpeed = 0.1f; // Velocidad de rotación

    private Vector2 previousTouch1, previousTouch2;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                previousTouch1 = touch1.position;
                previousTouch2 = touch2.position;
            }
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                Vector2 currentTouch1 = touch1.position;
                Vector2 currentTouch2 = touch2.position;

                // Escalar si la distancia entre los toques cambió significativamente
                float previousDistance = Vector2.Distance(previousTouch1, previousTouch2);
                float currentDistance = Vector2.Distance(currentTouch1, currentTouch2);
                if (Mathf.Abs(currentDistance - previousDistance) > scaleSpeed)
                {
                    ScaleObject(previousDistance, currentDistance);
                }

                // Rotar si el ángulo entre los toques cambió significativamente
                float previousAngle = Mathf.Atan2(previousTouch2.y - previousTouch1.y, previousTouch2.x - previousTouch1.x);
                float currentAngle = Mathf.Atan2(currentTouch2.y - currentTouch1.y, currentTouch2.x - currentTouch1.x);
                if (Mathf.Abs(Mathf.DeltaAngle(Mathf.Rad2Deg * previousAngle, Mathf.Rad2Deg * currentAngle)) > rotationSpeed)
                {
                    RotateObject(previousAngle, currentAngle);
                }

                previousTouch1 = currentTouch1;
                previousTouch2 = currentTouch2;
            }
        }
    }

    private void ScaleObject(float previousDistance, float currentDistance)
    {
        float scaleFactor = (currentDistance - previousDistance) * Time.deltaTime * scaleSpeed;
        targetObject.localScale += Vector3.one * scaleFactor;
    }

    private void RotateObject(float previousAngle, float currentAngle)
    {
        float angleDelta = Mathf.DeltaAngle(Mathf.Rad2Deg * previousAngle, Mathf.Rad2Deg * currentAngle);
        targetObject.Rotate(Vector3.forward, angleDelta * rotationSpeed, Space.World);
    }
    void rotar()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            Vector2 currentVector = touch2.position - touch1.position;
            float currentRotation = Mathf.Atan2(currentVector.y, currentVector.x) * Mathf.Rad2Deg;

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                previousVector = currentVector;
                previousRotation = currentRotation;
            }
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                float rotationDelta = currentRotation - previousRotation;
                targetObject.Rotate(Vector3.forward, -rotationDelta, Space.World);

                previousVector = currentVector;
                previousRotation = currentRotation;
            }
        }
    }
    void escalar()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                previousDistance = touch2.position - touch1.position;
            }
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                Vector2 currentDistance = touch2.position - touch1.position;
                Vector2 difference = currentDistance - previousDistance;

                float scaleFactor = difference.magnitude * Time.deltaTime * scaleSpeed;

                if (Vector2.Dot(difference.normalized, previousDistance.normalized) > 0)
                {
                    // Aumentar tamaño
                    targetObject.localScale += Vector3.one * scaleFactor;
                }
                else
                {
                    // Disminuir tamaño
                    targetObject.localScale -= Vector3.one * scaleFactor;
                }

                previousDistance = currentDistance;
            }
        }
    }

}
