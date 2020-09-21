using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;

public class UnitychanController : MonoBehaviour
{
    private Animator myAnimator;
    private Rigidbody myRigidbody;
    private float velocityZ = 16f;
    private float velocityX = 10f;
    private float velocityY = 10f;
    private float movableRange = 3.4f;
    private float coefficient = 0.99f;
    private bool isEnd = false;
    private GameObject stateText;
    private GameObject scoreText;
    private int score = 0;
    private bool isLButtonDown = false;
    private bool isRButtonDown = false;
    private bool isJButtonDown = false;
    // Start is called before the first frame update
    void Start()
    {
        this.myAnimator = GetComponent<Animator>();
        this.myAnimator.SetFloat("Speed",1);
        this.myRigidbody  = GetComponent<Rigidbody>();
        this.stateText = GameObject.Find("GameResultText");
        this.scoreText = GameObject.Find("ScoreText");

    }

    // Update is called once per frame
    void Update()
    {
        float inputVelocityX = 0;
        float inputVelocityY = 0;
        if ((Input.GetKey("z") || this.isLButtonDown) && -this.movableRange < this.transform.position.x)
        {
            inputVelocityX = -this.velocityX;
        } else if ((Input.GetKey("x") || this.isRButtonDown) && this.transform.position.x < this.movableRange)
        {
            inputVelocityX = this.velocityX;
        }
        if ((Input.GetKeyDown(KeyCode.Space) || this.isJButtonDown) && this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump",true);
            inputVelocityY = this.velocityY;
        } else {
            inputVelocityY = this.myRigidbody.velocity.y;
        }
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump",false);

        }
        this.myRigidbody.velocity = new Vector3(inputVelocityX,inputVelocityY,this.velocityZ);

        if (this.isEnd)
        {
            this.velocityZ *= this.coefficient;
            this.velocityX *= this.coefficient;
             this.velocityY *= this.coefficient;
             this.myAnimator.speed *= this.coefficient;
        }
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;
       
      



    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConetag")
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }
        if (other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "CLEAR";
        }
        if (other.gameObject.tag == "CoinTag")
        {
            this.score += 10;
            this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";
            GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject);
        }
    }

        public void GetMyJumpButtonDown()
        {
            this.isJButtonDown = true;
        }

        public void GetMyJumpButtonUp()
        {
            this.isJButtonDown = false;
        }

        public void GetMyLeftButtonDown()
        {
            this.isLButtonDown = true;
        }

        public void GetMyLeftButtonUp()
        {
            this.isLButtonDown = false;
        }
    public void GetMyRightButtonDown()
        {
            this.isRButtonDown = true;
        }
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }



    
}
