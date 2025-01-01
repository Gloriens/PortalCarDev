using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTP : MonoBehaviour
{
    public GameObject orangePortal; // Turuncu portal objesi
    public GameObject player;       // Oyuncu objesi
    private Transform orangePortalTransform;
    public LayerMask groundLayer;   // Yere raycast atarken kontrol edilecek layer
    public float rayLength = 10f; // Ray'in uzunluğu

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            orangePortalTransform = orangePortal.GetComponent<Transform>();

            // Raycast pozisyonunu ayarla (portaldan aşağıya doğru)
            Vector3 rayOrigin = orangePortalTransform.position;
            Ray ray = new Ray(rayOrigin, Vector3.down);
            RaycastHit hit;

            // Raycast ile kontrol et
            if (Physics.Raycast(ray, out hit, rayLength, groundLayer))
            {
                // Eğer bir yüzeye çarptıysa, oyuncuyu teleport et
                
                Vector3 playerPos = new Vector3(
                    orangePortalTransform.position.x - 3,
                    player.transform.position.y,
                    orangePortalTransform.position.z
                );
                player.transform.position = playerPos;
            }
            else
            {
                // Eğer altında yol yoksa, oyuncuyu teleport etme
                
            }
        }
    }
}