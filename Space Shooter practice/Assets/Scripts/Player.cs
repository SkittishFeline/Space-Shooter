﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //this variable contains the speed of the player
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]

    private GameObject _laser;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]

    private float _fireRate = 0.5f;

    private float _canFire = 0.5f;
    [SerializeField]

    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;

    // Start is called before the first frame update
    void Start()
    {
        //take the current position of the player = position (0,0,0) (x,y,z)
        //Vector3(x,y,z) in our case Vector3(0,0,0)
        transform.position = new Vector3(0,0,0);
        //store the spawn manager inside. In order to find the spawn we can use the name of the object.
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        //check if the SpawnManager is empty
        if(_spawnManager == null)
        {
            //If SpawnManager is empty
            Debug.LogError("The Spawn Manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        //Time.time + 0_canFire = -1 (When the game starts)
        //Time.time = 1 _canFire + -1 (Right after shooting laser)
        //Time.time = 0.5 _canFire = 0.5
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //accessing the transform component Translate(Vector3(1,0,0))
        //Vector3(1,0,0) 1*fps 0*fps 0*fps
        //Vector391,0,0) 1*1sec 0*sec 0*sec 0*1sec 1m/s
        //Vector3(1,0,0) 1 * 5 * Time.deltaTime 5m/s
        //Vector3(1,0,0) 1 * speed * Time.deltaTime 3.5m/s
        //Vector3(1,0,0) 1 * horizontalInput(-1 - 1) * _speed * Time.deltaTime
        //transform.Translate(Vector3.right *horizontalInput * _speed * Time.deltaTime);
        //Vector3(0,1,0) 1y * verticalInput(-1 - 1) * _speed * time.deltaTime
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        //Vector3(horizontalInput,verticalInput,0) * _speed * Time.deltaTime;
        //Vector3(1,1,0) * _speed * Time.deltaTime;
        //transform.Translate(new Vector3(horizontalInput,verticalInput,0) * _speed * Time.deltaTime);
        Vector3 direction = new Vector3(horizontalInput,verticalInput,0);
        //(checking if hoizontal or vertical) * _speed * Time.deltaTime;
        transform.Translate(direction * _speed * Time.deltaTime);

        // if(transform.position.y >=0)
        // {
        //     //stop the player from going higher in the screen
        //     transform.position = new Vector3(transform.position.x,0,0);
        // }
        // //if the player's y position is smaller or equal to -3.8f
        // else if(transform.position.y <= -3.8f)
        // {
        //     //stops the player from going lower than -3.8f
        //     transform.position = new Vector3(transform.position.x,-3.8f,0);
        // }
        //Vector3(transform.position.xMathf,Clamp(),0)
        transform.position = new Vector3(transform.position.x,Mathf.Clamp(transform.position.y,-3.8f,0),0);



        //check if the player went past 11.3f on the axis (right)
        if(transform.position.x > 11.3f)
        {
            //check if the player went past -11.3f on the axis
            transform.position = new Vector3(-11.3f,transform.position.y,0);
        }

            //check if the player went past -11.3f on the axis (left)
            else if(transform.position.x < -11.3f)
        {
            //check if the player went past -11.3f on the axis
            transform.position = new Vector3(11.3f,transform.position.y,0);
        }
    }

    
    void FireLaser()
    {
        Debug.Log("Space is pressed");
            _canFire = Time.time + _fireRate;
            if(_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position,Quaternion.identity);
            }
            else
            {
                //create a clone on the player's position using the default rotation
                Instantiate(_laser,transform.position + new Vector3(0,1f,0),Quaternion.identity);
            }
            
    }

    public void Damage()
        {
            //lives = lives -1
            //_lives--;
            _lives-= 1;

            if(_lives < 1)
            {
                _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
            }
        }

        public void ActivateTripleShot()
        {
            _isTripleShotActive = true;
            //start coroutine when _IsTripleShotActive is true
            StartCoroutine(TripleShotPowerDownRoutine());
        }

        IEnumerator TripleShotPowerDownRoutine()
        {
            //wait for 5 seconds
            yield return new WaitForSeconds(5f);
            //turn off triple shot
            _isTripleShotActive = false;
        }
}