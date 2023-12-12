using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float maxDistance = 5f; // Set your desired max distance here
    public float zoomSpeed = 5f; // Adjust the zoom speed as needed

    private CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (virtualCamera == null)
        {
            Debug.LogError("CinemachineVirtualCamera component not found!");
            return;
        }

        // You may need to adjust this based on your scene and players
        if (player1 == null || player2 == null)
        {
            Debug.LogError("Player Transforms not assigned!");
            return;
        }
    }

    void Update()
    {
        float distance = Vector2.Distance(player1.position, player2.position);

        // Adjust orthographic size based on player distance
        float targetOrthoSize = Mathf.Clamp(distance, 0f, maxDistance);
        virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, targetOrthoSize, Time.deltaTime * zoomSpeed);
    }
}