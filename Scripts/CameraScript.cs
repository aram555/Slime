using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("Camera Options")]
    public Transform Player;
    public Vector3 Offset;
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Player.position + Offset;
    }
}
