using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{
    [SerializeField] Animator animm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("getkeydown is working");
            if (animm == null)
            {
                Debug.Log("animm is null");
            }
            Debug.Log("getkeydown is working");
            animm.SetTrigger("idle");
        }
    }
}
