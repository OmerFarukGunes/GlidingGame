using System;
using UnityEngine;
public class InputManager : Singleton<InputManager>
{
    public static event Action<Vector3> OnTouchStart;
    public static event Action<Vector3> OnTouchMove;
    public static event Action<Vector3> OnTouchEnd;
    public override void Initialize()
    {
        base.Initialize();
    }
    private void Update()
    {
        UpdateInput();
    }
    private void UpdateInput()
    {
        if (Application.isEditor)
        {
            HandeMouseInput();
        }
        else if (Input.touchCount == 1)
        {
            HandleTouchInput();
        }
    }
    private void HandeMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleTouch(Input.mousePosition, TouchPhase.Began);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            HandleTouch(Input.mousePosition, TouchPhase.Ended);
        }
        else if (Input.GetMouseButton(0))
        {
            HandleTouch(Input.mousePosition, TouchPhase.Moved);
        }
    }
    private void HandleTouchInput()
    {
        HandleTouch(Input.touches[0].position, Input.touches[0].phase);
    }
    private void HandleTouch(Vector2 pos, TouchPhase phase)
    {
        switch (phase)
        {   
            case TouchPhase.Began:
                TouchStart(pos);
                break;
            case TouchPhase.Moved:
                TouchMove(pos);
                break;
            case TouchPhase.Ended:
                TouchEnd(pos);
                break;
        }
    }
    private void TouchStart(Vector3 position)
    {
        OnTouchStart?.Invoke(position);
    }
    private void TouchEnd(Vector3 position)
    {
        OnTouchEnd?.Invoke(position);
    }
    private void TouchMove(Vector3 position)
    {
        OnTouchMove?.Invoke(position);
    }
}