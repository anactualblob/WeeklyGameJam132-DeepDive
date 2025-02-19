﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class Diver : MonoBehaviour
{

    static Diver _S;
    static Diver S
    {
        get
        {
            if (_S != null) return _S;
            Debug.LogError("Diver.cs : trying to get Diver Sigleton instance but it has not been assigned.");
            return null;
        }
        set
        {
            if (_S != null)
            {
                Debug.LogError("Diver.cs : trying to set Diver Sigleton instance again.");
                return;
            }
            _S = value;
        }
    }

    [SerializeField] float diveAngle = 45;
    [SerializeField] float diveForce = 1;
    [SerializeField] float waterDragMultiplier = 1;
    [SerializeField] float maxVelocityDown = 1;
    [SerializeField] float maxVelocitySide = 1;

    [Header("Depth")]
    [SerializeField] DepthMeter depthMeter;
    [Tooltip("How many meters in a unit. Used for depth calculation.")]
    [SerializeField] float metersPerUnit;
    [SerializeField] float maxDepth;
    [Space]
    [SerializeField] GameObject groundPrefab;
    
    Vector2 velocity;
    Vector2 acceleration;
    
    float depth = 0;

    static public float DEPTH
    {
        get { return S.depth; }
        private set { S.depth = value; }
    }

    SpriteRenderer spriteRenderer;
    Animator animator;

    //[Header("Particle Systems")]
    //[SerializeField] ParticleSystem diveParticles;


    float leftBound;
    float rightBound;

    private void Awake()
    {
        S = this;

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {

        StateManager.StartGame();


        velocity = Vector2.zero;
        acceleration = Vector2.zero;

        //setup depth meter
        depthMeter.maxDepth = maxDepth;
        depthMeter.depth = depth;

        // find the edges of the screen
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = screenAspect * halfHeight;

        // leftbound and rightbound are the left and right borders of the camera
        leftBound = -halfWidth;
        rightBound = halfWidth;


        // spawn ground at max depth
        Instantiate(groundPrefab, Vector3.up * (-3.5f + (maxDepth / metersPerUnit)), Quaternion.identity);
    }

    
    void Update()
    {
        if (StateManager.STATE != StateManager.State.inGame)
        {
            return;
        }


        if (DEPTH <= maxDepth)
        {
            StateManager.GameWon();
        }


        // clamp vertical velocity
        if (Mathf.Abs(velocity.y) > maxVelocityDown)
        {
            velocity.y = Mathf.Sign(velocity.y) * maxVelocityDown;
        }

        //clamp horizontal velocity
        if (Mathf.Abs(velocity.x) > maxVelocitySide)
        {
            velocity.x = Mathf.Sign(velocity.x) * maxVelocitySide;
        }


        // if velocity is greater than a value, scale it down by waterDragMultiplier (must be < 0)
        // this simulates water drag
        if (velocity.magnitude > 0.0001)
        {
            velocity.Scale(new Vector2(waterDragMultiplier, waterDragMultiplier));
        }
        else
        {
            velocity = Vector2.zero;
        }

        
        // update velocity with acceleration
        velocity += acceleration;

        // if the diver would go out of the screen with this velocity, reduce it
        if ( (transform.position + (Vector3)velocity).x > rightBound)
        {
            velocity.x = 0;
        } 
        else if ( (transform.position + (Vector3)velocity).x < leftBound)
        {
            velocity.x = 0;
        }

        // resolve position and reset acceleration;
        transform.Translate( velocity );
        acceleration = Vector2.zero;


        // update depth & depth meter
        depth = transform.position.y * metersPerUnit;
        depthMeter.depth = depth;
    }


    /// <summary>
    /// Makes the player dive left.
    /// <para>
    /// Adds to the player's acceleration Vector2.down, oriented according to diveAngle, scaled by Time.deltaTime and diveForce.
    /// </para>
    /// </summary>
    public void DiveLeft()
    {
        acceleration += (Vector2)(Quaternion.AngleAxis(diveAngle, Vector3.back) * Vector2.down * diveForce * Time.deltaTime);

        // make sprite face left
        spriteRenderer.flipX = false;

        // start diving animation
        animator.SetTrigger("Diving");

        //spend oxygen
        Oxygen.SpendOxygenDive();

        //// start diveParticles
        //diveParticles.Play();
    }

    /// <summary>
    /// Makes the player dive right.
    /// <para>
    /// Adds to the player's acceleration Vector2.down, oriented according to -diveAngle, scaled by Time.deltaTime and diveForce.
    /// </para>
    /// </summary>
    public void DiveRight()
    {
        acceleration += (Vector2)(Quaternion.AngleAxis(-diveAngle, Vector3.back) * Vector2.down * diveForce * Time.deltaTime);

        // make sprite face right
        spriteRenderer.flipX = true;

        // start diving animation
        animator.SetTrigger("Diving");

        //spend oxygen
        Oxygen.SpendOxygenDive();

        //// start diveParticles
        //diveParticles.Play();
    }


    /// <summary>
    /// Stops all momentum and makes the player move up and towards the horizontal center of the screen.
    /// <para>
    /// Called when player collides with an enemy.
    /// </para>
    /// </summary>
    public void EscapeEnemy()
    {

        // stop momentum
        velocity = Vector2.zero;
        acceleration = Vector2.zero;

        // go up and towards x=0
        // this calculation ensures that we will rotate clockwise if we're on the left of the screen and counter-clockwise if we're on the right
        // dividing diveAngle by 2 to go more up than to the side
        float angle = -diveAngle/2 * Mathf.Sign(transform.position.x);
        acceleration = Quaternion.AngleAxis( angle, Vector3.back )  *  Vector2.up * diveForce * Time.deltaTime;

        // determine flipped
        // flip if the player is to the left ( sprite faces left by default and must face the center of the screen)
        spriteRenderer.flipX = Mathf.Sign(transform.position.x) < 0;

        //Start animation
        animator.SetTrigger("Escaping");

        // consume oxygen
        Oxygen.SpendOxygenEscape();


    }
}
