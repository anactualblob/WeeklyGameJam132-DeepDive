using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TiledParallaxTexture : MonoBehaviour
{
    [Tooltip("0 is static, 1 is the same speed as the camera")]
    [Range(0,1)]
    [SerializeField] float parallaxSpeed;



    float height;
    float cameraTop;
    float prevCameraTop;

    bool hasSpawnedNext = false;

    SpriteRenderer spriteRenderer;



    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        height = spriteRenderer.bounds.size.y;

        hasSpawnedNext = false;


        // initialize camera top et prev camera top to the same value to prevent movement on first frame
        cameraTop = Camera.main.transform.position.y + Camera.main.orthographicSize;
        prevCameraTop = cameraTop;

    }


    void Update()
    {

        // compute new location for the top of the camera
        cameraTop = Camera.main.transform.position.y + Camera.main.orthographicSize;


        // translate the object vertically the same amount as the camera multiplied by parallax speed
        // speed = 1 => same speed as the camera
        // speed = 0 => does not move
        transform.Translate(Vector3.down * (prevCameraTop - cameraTop) * parallaxSpeed);
        prevCameraTop = cameraTop;



        // if the bottom of the camera reaches the bottom of this object, spawn a new one below
        if (cameraTop - Camera.main.orthographicSize * 2 <= transform.position.y - height/2 && !hasSpawnedNext)
        {

            Vector3 pos = new Vector3(transform.position.x, transform.position.y - height, transform.position.z);

            Debug.Log(pos);

            Instantiate(gameObject, pos , Quaternion.identity);

            hasSpawnedNext = true;
        }



        // Destroy gameobject if it leaves the screen
        // WARNING : the player could technically go back up to where this object has been destroyed and see a gap in the tiling
        // might fix that at some point but low priority
        if (cameraTop < transform.position.y - height)
        {
            Destroy(gameObject);
        }
        


    }


}
