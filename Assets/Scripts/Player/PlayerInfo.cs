using UnityEngine;

/// <summary>
/// PlayerInfo is used to Organize Transform and speed of players
/// </summary>
public struct PlayerInfo
{
    public Transform playerTransform;
    public float playerSpeed;

    public PlayerInfo(Transform transform, float speed)
    {
        playerTransform = transform;
        playerSpeed = speed;
    }
}