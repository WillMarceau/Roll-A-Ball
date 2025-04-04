using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    
    public float jumpForce;
    private bool extraJump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent <Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        extraJump = true;
    }

    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 16)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    private void Update()
    {
        if (isGrounded()) {
            extraJump = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded()) {
            jump();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isGrounded()) {
            if (extraJump) {
                extraJump = false;
                jump();
            }
        }
    }
    private void FixedUpdate()
    {
        //vert = jump();
        Vector3 movement = new Vector3 (movementX, 0, movementY);
        rb.AddForce(movement * speed);
        //jump()
        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded()) {
            //rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            //jumps--;
           // jump();
       // }
    }

    private bool isGrounded()
    {
        // move the position lower 
        //return Physics.Raycast(transform.position + Vector3.down * 0.4f, Vector3.down, 0.2f);
        return Physics.Raycast(transform.position, Vector3.down, 0.6f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // destroy player
            Destroy(gameObject);
            // update win text
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }

    void jump() 
    {
        // apply force
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        
    }
    
}
