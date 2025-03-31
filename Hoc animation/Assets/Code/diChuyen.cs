using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class diChuyen : MonoBehaviour {
    public Rigidbody2D rb;
    public Animator anim;
    public int tocDo = 6;
    public float traiPhai;
    public bool nhinbenphai = true;
    private int sonhay = 0;
    


    void Start()
    {

    }

    
    void Update()
    {   
        traiPhai = Input.GetAxisRaw("Horizontal");// A = -1, D = 1, W = 0
        rb.linearVelocity = new Vector2(traiPhai * tocDo, rb.linearVelocity.y);
                           
        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && sonhay < 2)
        {
            anim.SetFloat("dichuyen" , 0);
            
            if(sonhay == 1 && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
            {
                anim.SetBool("xoay", true);
                rb.AddForce(new Vector2(0, 30), ForceMode2D.Impulse);
            }
            else {
            rb.AddForce(new Vector2(0, 14), ForceMode2D.Impulse);} 
            sonhay ++;
            Debug.Log(sonhay);
          
         }

        if (nhinbenphai == true && traiPhai == -1)
        {
            transform.localScale = new Vector3(-6, 6, 1);
            nhinbenphai = false;
        }
        else if (nhinbenphai == false && traiPhai == 1)
        {
            transform.localScale = new Vector3(6, 6, 1);
            nhinbenphai = true;
        }
       
       //Animation
       if(transform.position.y <= -2.832391f){

       anim.SetFloat("dichuyen", Mathf.Abs(traiPhai));}
       if(Input.GetMouseButtonDown(0))
       {
           anim.SetTrigger("tancong" );
       }
    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            sonhay = 0;
           anim.SetBool("xoay", false);
        }
    }

}

