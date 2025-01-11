using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player", order = 0)]
public class RocketmanData : ScriptableObject
{
    public float Gravity = -9.81f;
    public float GlideGravity = -2f;

    public float LaunchPowerMultiplier = 30;
    public float JumperPower = 20;

    public float BounceDamping = 0.8f;
    public float MinBounceSpeed = 0.5f;

    public float ForwardSpeedMultiplier = 2;
    public float GlideForwardSpeed = 10;

    public float RotationSpeed = 360f;
    public float FallRotationSpeed = 20;

    public float GlideSwipeSpeed = 5f;
}
