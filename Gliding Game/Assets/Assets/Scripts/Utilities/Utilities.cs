using UnityEngine;
public static class Utilities
{
    public static void Open(this CanvasGroup canvas)
    {
        canvas.alpha = 1;
        canvas.blocksRaycasts = true;
        canvas.interactable = true;
    }
    public static void Close(this CanvasGroup canvas)
    {
        canvas.alpha = 0;
        canvas.blocksRaycasts = false;
        canvas.interactable = false;
    }
    public static void MoveForward(this Transform transform, Vector3 velocity)
    {
        Vector3 forwardDirection = transform.forward;
        Vector3 Velocity = Vector3.zero;
        Velocity += forwardDirection * velocity.z;
        Velocity.y = velocity.y;
        transform.position += Velocity * Time.deltaTime;
    }
}