using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //translate laser up
        //speed variable of 8

        //transform.Translate(Vector3.up * _speed * Time.deltaTime)
        transform.Translate(new Vector3(0,1,0) * 8 * Time.deltaTime);

        if(transform.position.y > 8f)
        {
            Destroy(gameObject);
        }
    }
}
