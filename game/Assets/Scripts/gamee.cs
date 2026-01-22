using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamee : MonoBehaviour
{
    [SerializeField] GameObject knife;
    [SerializeField] GameObject cucumber;
    [SerializeField] Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(
            knife.transform.position,
            cucumber.transform.position
        );

        if (distance < 0.1f)
        {
            Debug.Log("knife is cucumber area");
        }
    }
}
