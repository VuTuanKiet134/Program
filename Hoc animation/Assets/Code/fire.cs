using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class fire : MonoBehaviour
{

    Rigidbody2D m_rb;
    public float speed ;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }
    
}
