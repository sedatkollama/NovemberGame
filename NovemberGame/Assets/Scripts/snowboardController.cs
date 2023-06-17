using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class snowboardController : MonoBehaviour
{
    //float horizontalInput;
    //float verticalInput;
    //float steeringAngle;
    //public float speed;
    //public float torque;

    public Rigidbody rb;
    public Animator anim;
    public float forwardForce = 300f;
    public float sidewaysForce = 70f;
    public float snowFriction = 100f;
    public float snowFlowForce = 20f;
    public bool yatmaBasladi = false;
    public Vector3 rbSpeed;
    enum Direction { Forward, Back, Right, Left };
    Direction myDirection;
    [SerializeField] TMP_Text value_speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {      
        //rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        MyInput();
        Brake();
        rbSpeed = rb.velocity;
        value_speed.text = ((int)rbSpeed.z).ToString();       
        ChangeAnimationSpeed(rbSpeed.z);
        CheckLayDown(rbSpeed.z, yatmaBasladi);
    }

    void MyInput()
    {
        if (Input.GetKey("d"))
        {
            myDirection = Direction.Right;
            transform.Rotate(Vector3.back * Time.deltaTime * 20);//z-ekseni yatma
            transform.Rotate(Vector3.up * Time.deltaTime * 40);//y-ekseni snowboard arkasýný kaydýrma           
            //rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.Impulse);
            //rb.AddRelativeForce(0, 0, snowFlowForce * Time.deltaTime, ForceMode.VelocityChange);
            //velocityChange objenin kutlesini yok sayarak hizini degistirir       
        }
        if (Input.GetKey("a"))
        {
            myDirection = Direction.Left;
            transform.Rotate(Vector3.forward * Time.deltaTime * 20);//z-ekseni yatma
            transform.Rotate(Vector3.down * Time.deltaTime * 40);//y-ekseni snowboard arkasýný kaydýrma         
            //rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.Impulse);
            //rb.AddRelativeForce(0, 0, snowFlowForce * Time.deltaTime / 60, ForceMode.VelocityChange);
        }
        if (Input.GetKey("w"))
        {
            myDirection = Direction.Forward;
            transform.Rotate(Vector3.right * Time.deltaTime * 60);
            //rb.AddRelativeForce(0, 0, snowFlowForce * Time.deltaTime, ForceMode.Impulse);
        }
        if (Input.GetKey("s"))
        {
            myDirection = Direction.Back;
            transform.Rotate(Vector3.left * Time.deltaTime * 60);
        }   
    }

    private void FixedUpdate()
    {
        rb.AddForce(0, 0, forwardForce * Time.fixedDeltaTime);

        switch (myDirection)
        {
            case Direction.Forward:
                //rb.AddRelativeForce(0, 0, snowFlowForce * Time.fixedDeltaTime/60, ForceMode.Impulse);
                break;
            case Direction.Back:
                //rb.AddRelativeForce(0, 0, -snowFlowForce * Time.fixedDeltaTime /60, ForceMode.Impulse);
                break;
            case Direction.Right:
                rb.AddForce(sidewaysForce * Time.fixedDeltaTime/5, 0, 0, ForceMode.Impulse);
                rb.AddRelativeForce(0, 0, snowFlowForce * Time.fixedDeltaTime, ForceMode.Impulse);             
                break;
            case Direction.Left:
                rb.AddForce(-sidewaysForce * Time.fixedDeltaTime/5, 0, 0, ForceMode.Impulse);
                rb.AddRelativeForce(0, 0, snowFlowForce * Time.fixedDeltaTime , ForceMode.Impulse);               
                break;
            default:
                break;
        }
    }
    void Brake()
    {
        yatmaBasladi = false;
        if (transform.rotation.eulerAngles.y < 330f && transform.rotation.eulerAngles.y > 30f)
        {
            //30 derece gecince -z yonunde kuvvet uygula
            rb.AddForce(0, 0, -snowFriction * Time.deltaTime / 60, ForceMode.VelocityChange);
            yatmaBasladi = true;
            //Debug.Log(transform.rotation.eulerAngles.y);
        }
        if (transform.rotation.eulerAngles.y < 315f && transform.rotation.eulerAngles.y > 45f)
        {
            //30 derece gecince -z yonunde kuvvet uygula
            rb.AddForce(0, 0, -snowFriction * Time.deltaTime / 20, ForceMode.VelocityChange);
            yatmaBasladi = true;
            //Debug.Log(transform.rotation.eulerAngles.y);
        }

    }
    void ChangeAnimationSpeed(float hiz)
    {
        anim.SetFloat("speed", hiz);
    }
    void CheckLayDown(float hiz, bool laydown)
    {
        if (laydown && hiz > 55.0f)
        {
            //third animation will play
            anim.SetBool("bothRotateSpeed", true);
            anim.SetBool("rotateOn", false);
        }
        else if(laydown && hiz < 55.0f && hiz > 30.0f)
        {
            anim.SetBool("rotateOn", true); //second animation will play
            anim.SetBool("bothRotateSpeed", false); //third animation will stop
        }     
        else
        {
            //first animation will play
            anim.SetBool("rotateOn", false);
            anim.SetBool("bothRotateSpeed", false);
        }     
    } 
    public void FinishGame()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName("lastscene").name);
    }
}
