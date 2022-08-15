using UnityEngine;


public class PlayerController_ZigZag : Manager<PlayerController_ZigZag>
{
    Rigidbody rb;

    bool isRight = false;
    public bool isOnPlatform = false;
    Vector3 move;

    public float moveSpeed;
    public LayerMask platformLayer;

    Vector3 startPos;
    Quaternion startRot;

    [SerializeField] float t = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = rb.transform.position;
        startRot = rb.transform.rotation;
    }
    void Update()
    {
        isOnPlatform = ISONGROUND();
       if (ISONGROUND())
       {
            rb.velocity = move * moveSpeed;
       }
       else
       {
            if(GameManager.Instance.currentGameState == GameManager.GameState.PLAYING)
            {
                GameManager.Instance.GameOver();
            }
            else if(GameManager.Instance.currentGameState == GameManager.GameState.GAMEOVER)
            {
                ResetPosition();
            }  
       }

    }
    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) && ISONGROUND())
        {
            DoTouch();
        }
    }
    private void DoTouch()
    {
        if (GameManager.Instance.currentGameState == GameManager.GameState.PLAYING)
        {
            
            if (isRight)
            {
                
                
                Vector3 desiredMoveDirection = new Vector3();

                desiredMoveDirection.y = 90f;
                //desiredMoveDirection.y = rb.transform.rotation.z;
                rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(desiredMoveDirection), t);
                //rb.rotation = new Quaternion(0, 0.70711f , 0, 0.70711f) ;
                 rb.constraints = RigidbodyConstraints.FreezeRotationY;
                rb.constraints = RigidbodyConstraints.FreezeRotationZ;
                //(0.50000, 0.50000, 0.50000, 0.50000)
                move = Vector3.right;//new Vector3(moveSpeed, 0f, 0f);
            }
            else
            {
                 

                Vector3 desiredMoveDirection = new Vector3();
                //desiredMoveDirection.x = rb.transform.rotation.x;
                desiredMoveDirection.y = 0f;
                //desiredMoveDirection.y = rb.transform.rotation.z;
                rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(desiredMoveDirection), t);
                //rb.rotation = new Quaternion(0, 0, 0, 1);
                rb.constraints = RigidbodyConstraints.FreezeRotationY;
                rb.constraints = RigidbodyConstraints.FreezeRotationZ;
                //(0.00000, 0.00000, 0.70711, 0.70711)
                move = Vector3.forward;//new Vector3(0f, 0f, moveSpeed);
            }
            ChangeState();
        }
    }
    public void ChangeState()
    {
        isRight = !isRight;
    }
    bool ISONGROUND()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f, platformLayer);
        for (int i = 0; i < colliders.Length; i++){
            if(colliders[i].gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }
    public void ResetPosition()
    {
        rb.isKinematic = true;
        rb.transform.position = startPos;
        rb.transform.rotation = startRot;
        move = new Vector3(0,0,0);
        isRight = false;
        rb.isKinematic = false;
    }
}
