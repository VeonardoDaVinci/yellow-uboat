using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RotationController : MonoBehaviour
{
    public float animationRampKey = 0f;
    private float animationRampMultiplier = 5f;
    [SerializeField] private AnimationCurve rotationRamp;

    public bool isRotationActive = false;
    [SerializeField] private bool isUsingRotationRamp = false;

    private float rotationDagrees = 360f;
    [SerializeField] private float rotationInterval = 1f;
    [SerializeField] private Vector3 rotationAxis = Vector3.left;
    [SerializeField] private GameObject gameObjectToRotate;
    public void RotateObject(GameObject rotationObject, float dagrees, float time)
    {
        if (time == 0) { return; }
        Vector3 rotationAngles = new Vector3(dagrees * Time.deltaTime / time, dagrees * Time.deltaTime / time, dagrees * Time.deltaTime / time);
        rotationObject.transform.Rotate(Vector3.Scale(rotationAngles,rotationAxis));
    }

    private void Update()
    {
        animationRampKey = Mathf.Clamp01(animationRampKey);
        if (isUsingRotationRamp)
        {
            rotationInterval = rotationRamp.Evaluate(animationRampKey) * animationRampMultiplier;
        }
        if(isRotationActive)
        {
            RotateObject(gameObjectToRotate, rotationDagrees, rotationInterval);
        }
    }
}
