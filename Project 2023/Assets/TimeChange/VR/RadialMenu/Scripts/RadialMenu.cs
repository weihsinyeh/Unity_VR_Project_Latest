using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
    [Header("Scene")]
    public Transform selectionTransform;
    public Transform cursorTransform;

    [Header("Events")]
    public RadialSection top = null;
    public RadialSection right = null;
    public RadialSection bottom = null;
    public RadialSection left = null;

    [Header("parameters")]
    public int index;

    private Vector2 touchPosition = Vector2.zero;
    private List<RadialSection> radialSections = null;
    private RadialSection hightlightSection = null;

    private readonly float degreeIncrement = 90.0f;

    private void Awake()
    {
        CreateAndSetUp();
    }

    private void CreateAndSetUp()
    {
        radialSections = new List<RadialSection>()
        {
            top,
            right,
            bottom,
            left
        };

        foreach (RadialSection section in radialSections)
        {
            section.iconRenderer.sprite = section.icon;
        }

    }


    private void Start()
    {
        Show(false);
    }


    public void Show(bool value)
    {
        gameObject.SetActive(value);
    }

    private void Update()
    {
            Vector2 dir = Vector2.zero + touchPosition;
            float rotation = GetDegree(dir);

            SetCursorPos();
            SetSelectionRotation(rotation);
            SetSelectedEvent(rotation);

    }

    private float GetDegree(Vector2 dir)
    {
        float value = Mathf.Atan2(dir.x, dir.y);
        value *= Mathf.Rad2Deg;

        if (value < 0)
        {
            value += 360.0f;
        }

        return value;
    }

    private void SetCursorPos()
    {
        cursorTransform.localPosition = touchPosition;
    }

    private void SetSelectionRotation(float newRotation)
    {
        float snappedRotation = SnapRotation(newRotation);
        selectionTransform.localEulerAngles = new Vector3(0, 0, -snappedRotation);
    }
    private float SnapRotation(float Rotation)
    {
        return GetNearestIncrement(Rotation) * degreeIncrement;
    }
    private int GetNearestIncrement(float Rotation)
    {
        return Mathf.RoundToInt(Rotation / degreeIncrement);
    }

    private void SetSelectedEvent(float currentRotation)
    {
        index = GetNearestIncrement(currentRotation);

        if (index == 4)
            index = 0;

        hightlightSection = radialSections[index];
    }
    public void SetTouchPos(Vector2 newValue)
    {
        touchPosition = newValue;
    }

    public void ActiveHighlightedSection()
    {
        hightlightSection.onPress.Invoke();
    }
}
