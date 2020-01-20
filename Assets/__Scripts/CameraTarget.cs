using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject cam;
    [SerializeField] Vector3 cameraOffset;
    [Space] 
    [SerializeField] float smoothing;
    [Space]
    [SerializeField] bool drawGizmo;
    

    void FixedUpdate()
    {
        // move towards player and snap camera to position + offset.
        GoToPlayer();
        cam.transform.position = transform.position + cameraOffset;
    }



    /// <summary>
    /// Moves object towards the player.
    /// </summary>
    void GoToPlayer()
    {
        float u = Mathf.Lerp(transform.position.y, player.position.y, smoothing);
        transform.position = new Vector3( transform.position.x, u, transform.position.z);
    }





    // draw red sphere at position for better readability in player
    void OnDrawGizmos()
    {
        if (!drawGizmo)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);

    }
}
