using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    bool left;
    bool right;
    public float speed;
    public Material redMat;
    public Material whiteMat;

    private float rotationValue;
    public float rotationSpeed;
    public float maxRotValue;
    public float minRotValue;

    public GameCenter gameCenter;
    public EnemyScript enemyScript;
    public CameraController cameraController;

    private Animator _anim;

    public Transform PlayerT;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        
    }
    void Update()
    {
        
        if (Input.touchCount > 0)
        {
            gameCenter.CloseHAD(); //it closed the Hold&Drag UI

            Touch finger = Input.GetTouch(0);

            if(finger.deltaPosition.x > 0)
            {
                right = true;
                left = false;
            }

            if (finger.deltaPosition.x < -0)
            {
                right = false;
                left = true;
            }

            if (right == true && !_anim.GetBool("isDead") && !_anim.GetBool("isWin"))
            {          
                transform.position = Vector3.Lerp(transform.position, new Vector3(4, transform.position.y, transform.position.z), 1 * Time.deltaTime);
                RotatePlayer();
            }


            if (left == true && !_anim.GetBool("isDead") && !_anim.GetBool("isWin"))
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(0f, transform.position.y, transform.position.z), 1 * Time.deltaTime);
                RotatePlayer();
            }
      
        }

        if (PlayerT.transform.position.y < 0)
        {
            _anim.SetBool("isDead", true);
            gameCenter.EnableGameOver();
            gameCenter.EnableRestart2();
        }

        if (!_anim.GetBool("isDead") && !_anim.GetBool("isWin"))
        {
            
        }

        if (Input.touchCount > 0 && !_anim.GetBool("isDead"))
        {
            _anim.SetBool("isRunning", true);
            
        } 

    }

    void FixedUpdate()
    {
        if (_anim.GetBool("isRunning") && !_anim.GetBool("isDead") && !_anim.GetBool("isWin") && PlayerT.position.y>0.4f)
        {
            transform.position += transform.forward * Time.deltaTime * speed; 
            rb.useGravity = true;
            
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        
        
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Barrier")
        {
            _anim.SetBool("isDead", true);
            gameCenter.EnableGameOver();
            gameCenter.EnableRestart2();
        }

        if(collision.gameObject.tag == "FinishLine")
        {
            _anim.SetBool("isRunning", false);
            _anim.SetBool("isWin", true);
            gameCenter.EnableYouWin();
            gameCenter.EnableRestart();
            gameCenter.EnableNext();

            cameraController.FinishAngle();
        }

       
     
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Plane" && _anim.GetBool("isRunning"))
        {
            collision.gameObject.isStatic = false;
            Rigidbody plane = collision.gameObject.GetComponent<Rigidbody>();
            plane.GetComponent<MeshRenderer>().material = redMat;
            StartCoroutine(FallingPlane(collision, plane));

        }
    }

    private void RotatePlayer()
    {
        Touch finger = Input.GetTouch(0);

        rotationValue += finger.deltaPosition.x * rotationSpeed * Time.deltaTime;
        if (rotationValue < minRotValue)
        {
            rotationValue = minRotValue;
        }
        else if (rotationValue > maxRotValue)
        {
            rotationValue = maxRotValue;
        }
        
        transform.eulerAngles = new Vector3(0, rotationValue, 0);
        
    }

    IEnumerator FallingPlane(Collision collision, Rigidbody plane)
    {
        
        yield return new WaitForSeconds(0.4f);
        plane.constraints = RigidbodyConstraints.None;
        plane.useGravity = true;
        plane.transform.position = new Vector3(plane.transform.position.x, -0.50f, plane.transform.position.z);
        
    }
}