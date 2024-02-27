using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerStype
    {
        Cube,
        Rocket,
        SpaceShip,
    }

    public PlayerStype playerStype;
    [SerializeField] PlayerStype playerStartSpaceShip;

    [SerializeField] Rigidbody2D rbCure;
    [SerializeField] GameObject playerCubeMesh;

    //[SerializeField] Rigidbody2D rbRocket;

    [SerializeField] GameObject playerSpaceShipMesh;


    [SerializeField] GameObject playerRocketMesh;

    [SerializeField] bool isGrounded;

    [SerializeField] float jumpForce = 10f;

    [SerializeField] Transform startLocation;

    float moveSpeed = 5f;
    [SerializeField] float moveSpeedNormal = 5f;

    [SerializeField] float moveSpeedBooted = 10f;

    [SerializeField] float turnSpeed = 10f;
    [SerializeField] float speedBootCount = 1f;

    [SerializeField] float thrustForce = 5f;
    [SerializeField] float rotationSpeed = 2f;


    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
        playerStype = playerStartSpaceShip;

    }
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = moveSpeedNormal;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStype == PlayerStype.Rocket)
        {
            Vector2 velocity = rbCure.velocity;
            if (velocity != Vector2.zero)
            {
                float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

    }

    private void FixedUpdate()
    {
        //Movement();
        switch (playerStype)
        {
            case PlayerStype.Cube:
                Movement3();

                break;
            case PlayerStype.Rocket:
                Movement();
                break;

            case PlayerStype.SpaceShip:
                Movement2();

                break;
        }
    }

    void PlayerInput()
    {

    }

    //Rocket Move Style 1
    void Movement()
    {
        rbCure.velocity = new Vector2(moveSpeed, Input.GetAxisRaw("Vertical") * turnSpeed) * Time.deltaTime * 100;
    }

    //Rocket Move Style 2
    void Movement2()
    {
        Vector2 movement = new Vector2(moveSpeed, rbCure.velocity.y);
        rbCure.velocity = movement;

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Jump");
            if (rbCure.velocity.y <= 10)
            {
                rbCure.AddForce(transform.up * thrustForce);
            }
        }
    }

    //Cube movement
    void Movement3()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, .7f, LayerMask.GetMask("Ground"));

        Vector2 movement = new Vector2(moveSpeed, rbCure.velocity.y);
        rbCure.velocity = movement;

        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Jump");
            //rb.AddForce(transform.up * thrustForce);

            rbCure.velocity = new Vector2(rbCure.velocity.x, jumpForce);
        }
    }

    public void ChangeSpaceShip(PlayerStype newStype)
    {
        moveSpeed = moveSpeedNormal;

        playerCubeMesh.gameObject.SetActive(false);
        playerRocketMesh.gameObject.SetActive(false);
        playerSpaceShipMesh.gameObject.SetActive(false);
        
        playerStype = newStype;

        switch (newStype)
        {
            case PlayerStype.Cube:
                playerStype = newStype;
                rbCure.gravityScale = 2;
                rbCure.velocity = Vector2.zero;

                playerCubeMesh.gameObject.SetActive(true);
                break;

            case PlayerStype.Rocket:
                rbCure.velocity = Vector2.zero;
                rbCure.gravityScale = 0;

                playerRocketMesh.gameObject.SetActive(true);

                break;

            case PlayerStype.SpaceShip:
                rbCure.velocity = Vector2.zero;
                rbCure.gravityScale = 1;

                playerSpaceShipMesh.gameObject.SetActive(true);
                break;
        }

    }


    public void ReStart()
    {
        ChangeSpaceShip(playerStartSpaceShip);
        transform.position = startLocation.position;

    }

    public void BootSpeed()
    {
        moveSpeed = moveSpeedBooted;
        StartCoroutine(SpeedBootTimer());
    }

    public void HitJumpBox()
    {
        rbCure.velocity = Vector3.zero;
        rbCure.velocity = Vector3.up *10;
    }

    IEnumerator SpeedBootTimer()
    {
        Debug.Log("Boot Speed");
        yield return new WaitForSeconds(speedBootCount);
        moveSpeed = moveSpeedNormal;
    }


}
