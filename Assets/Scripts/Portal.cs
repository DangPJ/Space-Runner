using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] PlayerController.PlayerStype PlayerNextType;
    [SerializeField] Transform nextLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel()
    {
        
        //SceneManager.LoadScene(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Hit Portal");
            //NextLevel();

            collision.GetComponentInParent<PlayerController>().ChangeSpaceShip(PlayerNextType);
            collision.GetComponentInParent<PlayerController>().gameObject.transform.position = nextLocation.position;

        }
    }
}
