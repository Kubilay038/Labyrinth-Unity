using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballMove : MonoBehaviour
{
    public UnityEngine.UI.Text time, heart, statu;
    public UnityEngine.UI.Button btn;
    private Rigidbody rg;
    public float speed;
    public float timeMeter;
    public int heartMeter;
    bool gameActive = true;
    bool gameDone = false;
    private GameManager gm;
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        gm = GameObject.FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if (gameActive && !gameDone)
        {
            timeMeter -= Time.deltaTime;
            time.text = (int)timeMeter + "";
            if (timeMeter < 0)
            {
                gameActive = false;
            }
        }
        else if(!gameDone)
        {
            statu.text = "Game Over";
            btn.gameObject.SetActive(true);
        }
    }
    void FixedUpdate()
    {
        if (gameActive && !gameDone)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 force = new Vector3(horizontal, 0, vertical);
            rg.AddForce(force * speed);
        }
        else
        {
            rg.velocity = Vector3.zero;
            // rg.angularvelocity = Vector3.zero;
        }
    }
    void OnCollisionEnter(Collision cls)
    {
        string objName = cls.gameObject.name;
        if (objName.Equals("Finish"))
        {
            gameDone = true;
            statu.text = "Finish The Game";
            btn.gameObject.SetActive(true);
            //gm.restartGame();
        }
        else if(!objName.Equals("Plane"))
        {
            heartMeter -= 1;
            heart.text = heartMeter + "";
            if (heartMeter==0)
            {
                gameActive = false;
            }
        }
    }
}
