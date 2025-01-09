using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player", order = 0)]
public class Player : ScriptableObject
{
    public float Gravity = -9.81f;
    public float LaunchPowerMultiplier = 30;
    public float RotationSpeed = 360f;
    public float ForwardSpeed = 20;
    public float GlideSpeed = 5f;
    public float GlideGravity = -2f;
}
