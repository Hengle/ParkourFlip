using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    
    #region Singleton

    public static Player Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    #endregion

    public Rigidbody _rb;
    public Collider _collider;
    public float speed;
    private float rotationPowerTime;
    private float frictionEffect;
    private float desiredPosX;
    private readonly  Vector3 _eulerAngleVelocity = new Vector3(0,0,460);
    public GameObject CamTarget;
    
    //Booleans
    private bool startBuilding; //Its for starting to game.
    public bool _isGrounded;
    public bool _isTurn;
    private bool isStop = true;
    public bool isDead;
    private bool canJump;

    //ForAnimation
    public Animator _Anim;
    public bool isWin;
    private bool isMoving;
    private bool isJump;
    private bool isFlying;
    private bool isBigObstacle;
    private bool isSmallObstacle;
    
    // Particle Effects
    private GameObject jumpingEffects;
    private GameObject landingEffects;
    private GameObject star;
   
    
    Vector3 CalculateLauncVelocity()
    {
        float displacementY = GameManager.Instance._nextTarget.position.y - _rb.position.y;
        Vector3 displacementXz = new Vector3(desiredPosX,0,GameManager.Instance._nextTarget.position.z-_rb.position.z);
        
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * GameManager.Instance.gravity * GameManager.Instance.height);
        Vector3 velocityXz = displacementXz / (Mathf.Sqrt(-2 * GameManager.Instance.height / GameManager.Instance.gravity) + Mathf.Sqrt(2 * (displacementY - GameManager.Instance.height) / GameManager.Instance.gravity));

        return velocityXz + velocityY;
    }

    
    private void Start()
    {
        _rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            DoFlip();
            
            if (_isGrounded && !isWin)
            {
                PlayerMovement();
            }
        }
    }

    void Update()
    {
        Animation();
        if (!startBuilding)
        {
            isMoving = false;
        }
        
        if (!isDead)
        {
            FlipControl();
            SmoothFlip();
        }
       
    }

    private void Animation()
    {
        _Anim.SetFloat("Speed" , rotationPowerTime);
        
        if (!isMoving)
        {
            _Anim.SetBool("IsMoving", false);
        }
        else
        {
            _Anim.SetBool("IsMoving", true);
            _Anim.SetBool("IsJump", false);
        }

        if (isFlying)
        {
            _Anim.SetBool("IsFlying", true);
        }
        else
        {
            _Anim.SetBool("IsFlying", false);
        }
        
        if (isBigObstacle)
        {
            _Anim.SetBool("isBigObstacle", true);
            speed -= Time.deltaTime * 400;
        }
        else
        {
            _Anim.SetBool("isBigObstacle", false);
            
           if (speed < 900)
            {
                speed += Time.deltaTime * 400;
            }
            else
            {
                speed = 900f;
            }
        }
        
        if (isSmallObstacle)
        {
            _Anim.SetBool("isSmallObstacle", true);
        }
        else
        {
            _Anim.SetBool("isSmallObstacle", false);
        }

        if (isWin)
        {
            _Anim.SetBool("IsWin", true);
        }
        
    }
    private void DoFlip()
    {
        if (!_isGrounded)
        {
            if (_isTurn || !_isTurn)
            {
                Quaternion deltaRotation = Quaternion.Euler(-_eulerAngleVelocity * rotationPowerTime  * Time.deltaTime);
                _rb.MoveRotation(_rb.rotation * deltaRotation);
            }

            if (isStop)
            {
                Quaternion friction = Quaternion.Euler(0,0,150 * rotationPowerTime* frictionEffect * Time.deltaTime);
                _rb.MoveRotation(_rb.rotation * friction );
            } 
        }    
    }
    private void FlipControl()
    {
        if (Input.GetMouseButtonDown(0) )
         {
             GameManager.Instance.StartGame();
             if (canJump)
             {
                 _isTurn = true;
             }
             isStop = false;
             if (isMoving)
             {
                 rotationPowerTime = .5f;
             }
              
             if (startBuilding)
             {
                 isJump = true;
                 if (canJump)
                 {
                     Lauch();
                 }
             }
             startBuilding = true; 
             
         }
        
        if (Input.GetMouseButtonUp(0))
         {
              _isTurn = false;
              isStop = true;
         }
      
    }

    private void SmoothFlip()
    {
        if (_isTurn)
        {
            rotationPowerTime += Time.deltaTime;
            if (rotationPowerTime >= .9f)
            {
                rotationPowerTime = .9f;
            }
        }
        else 
        {
            rotationPowerTime -= Time.deltaTime;
            if (rotationPowerTime <= 0) 
            {
                rotationPowerTime = 0;
            }
        }

        if (isStop)
        {
            if (rotationPowerTime > 0.9f)
            {
                frictionEffect = 1.22f;
            }
            else if (rotationPowerTime > 0.7f)
            {
                frictionEffect = 1.3f;
            }
            else if (rotationPowerTime > 0.4f)
            {
                frictionEffect = 1.6f;
            }
            else if (rotationPowerTime > 0.2f)
            {
                frictionEffect = 2f;
            }
        }
        else
        {
            frictionEffect = 1f;
        }
    }

    void Lauch()
    {
        
        if (_isGrounded && !GameManager.Instance.gameEnd)
        {
            desiredPosX = GameManager.Instance._nextTarget.position.x - _rb.position.x;
            
            if(desiredPosX > 90)
            {
                GameManager.Instance.height = Random.Range(30,40);
            }
            else if(desiredPosX >= 45 && desiredPosX <= 90)
            {
                GameManager.Instance.height = Random.Range(18,21);
            }
            else if(desiredPosX < 45)
            {
                GameManager.Instance.height = Random.Range(15,18);
            }
            
            Physics.gravity = Vector3.up * GameManager.Instance.gravity;
            _rb.useGravity = true;
            StartCoroutine(ParticleManager.Instance.JumpingEffects(jumpingEffects));
            CameraShake.Instance.isAnimationPlaying = true;
            _rb.velocity = CalculateLauncVelocity();
        }
    }
   
    public void PlayerMovement()
    {
        if (!isDead)
        {
            if (startBuilding)
            {
                if (!_isTurn)
                {
                    _rb.velocity = Vector3.right * speed * Time.deltaTime;
                    isMoving = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground" && startBuilding && !isDead)
        {
            rotationPowerTime = 0f;
            isJump = false;
            canJump = false;
            if (transform.eulerAngles.z > 45 && transform.eulerAngles.z < 315)
            {
               isDead = true;
            } 
            else if(transform.eulerAngles.z >= 45 && transform.eulerAngles.z <= 315 || transform.eulerAngles.z >= 30 && transform.eulerAngles.z <= 330
                    && GameManager.Instance.combo != 0)
            {
                GameManager.Instance.combo = 0;
                StartCoroutine(UIManager.Instance.ShowNearMissText());
            } 
            else if ((transform.eulerAngles.z >= 30 && transform.eulerAngles.z <= 330) || (transform.eulerAngles.z > 15 && transform.eulerAngles.z < 345)
                     && GameManager.Instance.combo != 0)
            {
               StartCoroutine(UIManager.Instance.ShowNormalText());

            }
            else if ((transform.eulerAngles.z < 15 || transform.eulerAngles.z > 345) && GameManager.Instance.combo != 0)
            {
                GameManager.Instance.combo = GameManager.Instance.combo + 1;
                StartCoroutine(UIManager.Instance.ShowPerfectText());
                StartCoroutine(ParticleManager.Instance.StarEffects(star));
            }

            if (!isDead)
            {
                StartCoroutine(ParticleManager.Instance.LandingEffects(landingEffects));
                CameraShake.Instance.isAnimationPlaying = true;
            }
        }

        if (other.gameObject.tag == "Jump")
        {
            canJump = true;
        }
        
        if (other.gameObject.tag == "Death")
        {
            isDead = true;
        }
        
        if (other.gameObject.tag == "ObstacleBig")
        {
            isBigObstacle = true;
        }
        
        if (other.gameObject.tag == "ObstacleSmall")
        {
            isSmallObstacle = true;
        }
        
    }
   
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isFlying = false;
            Vector3 zeroZ = new Vector3(1,1,0);
            GameManager.Instance.combo = 0;
            if (!isDead)
            {
                _isGrounded = true;
                transform.rotation = new Quaternion(0,0,0,0);
                transform.eulerAngles = Vector3.zero;
                

                if (!_isTurn)
                {
                    _rb.velocity = new Vector3(0,0,0);
                }
            }
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            _isGrounded = false;
            isMoving = false;
            isFlying = true;
        } 
        
        if (other.gameObject.tag == "ObstacleBig")
        {
            isBigObstacle = false;
        }
        
        if (other.gameObject.tag == "ObstacleSmall")
        {
            isSmallObstacle = false;
        }
    }
}
