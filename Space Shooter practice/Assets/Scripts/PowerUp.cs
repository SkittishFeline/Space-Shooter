using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    //ID for powerups
    //0 = Triple shot
    //1 = Speed
    //2 = Shield
    [SerializeField]
    private int _powerupID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if(transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
        {
            //if the other object has a tag of Player
            if(other.tag == "Player")
            {
                //Create an instance of the Player script
                Player player = other.GetComponent<Player>();
                //if the player is found
                if(player != null)
                {
                    //player.ActivateTripleShot();

                    switch(_powerupID)
                    {
                        case 0:
                            player.ActivateTripleShot();
                            break;
                        case 1:
                            player.SpeedBoostActive();
                            Debug.Log("Collected Speed Boost");
                            break;
                        case 2:
                            player.ActiveateShield();
                            Debug.Log("Shields Collected");
                            break;
                        default:
                            Debug.Log("Default Value");
                            break;
                    }
                }
                //then destroy the powerup
                Destroy(this.gameObject);
            }
        }
}
