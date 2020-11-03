using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class playermovemt : MonoBehaviourPun
{
    // Start is called before the first frame update
   
   
    public Text playerwontext;
    public int speed;
    public bool onejump;
    public Button[] leftandright;

    Vector3 startpos;


    // Update is called once per frame

    void Start()
    {
        startpos = this.transform.position;
        onejump = true;
       

    }

    private void Update()
    {
       
    }
    // player jumps on clicking over it
    private void OnMouseDown()
    {
      
        if (onejump)
        {
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * speed,ForceMode2D.Impulse);
            onejump = false;
            base.photonView.RequestOwnership();

        }

    }

    // on pressing leftmove btn;
    public void leftmove()
    {
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed, ForceMode2D.Impulse);
        base.photonView.RequestOwnership();

    }

    // on pressing rightmove btn;

    public void rightmove()
    {
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed, ForceMode2D.Impulse);
        base.photonView.RequestOwnership();

    }


    public void onquit()
        {
        Application.Quit();
        }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="ground")
        {
            onejump = true;
        }
        if(collision.gameObject.tag=="greenbar")
        {
            speed = 0;
            playerwontext.text = "Player 1 won";
            leftandright[0].interactable = false;
            leftandright[1].interactable = false;

            Invoke("methodInvoke", 1f);


        }
        if (collision.gameObject.tag == "bluebar")
        {
            speed = 0;
            leftandright[0].interactable = false;
            leftandright[1].interactable = false;

            playerwontext.text = "Player 2 won";
            Invoke("methodInvoke", 1f);
        }
    }
    private void methodInvoke()
    {

        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);

    }

}
