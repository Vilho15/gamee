using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamee : MonoBehaviour
{
    [SerializeField] GameObject knife;
    [SerializeField] GameObject cucumber;
 
    [SerializeField] GameObject cucumber2;
    [SerializeField] Animator anim;
    [SerializeField] bool isoo = false;
    [SerializeField] bool isoo2 = false;
    bool cucumberSwapped = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Action");

            StartCoroutine(SetIsooAfterDelay(1f));
        }
        float distance = Vector3.Distance(
            knife.transform.position,
            cucumber.transform.position
        );
        Debug.Log(distance);
        if (isoo == true && distance < 160f)
        {
            Debug.Log("knife is cucumber area");
            Debug.Log(knife.transform.position);
            Debug.Log(cucumber.transform.position);
     
         isoo2 = true;
           

        }

        if (isoo2 && !cucumberSwapped)
        {
            cucumberSwapped = true;

            cucumber2.transform.position = cucumber.transform.position;
            cucumber.SetActive(false);
            cucumber2.SetActive(true);

            Debug.Log("Cucumber swapped");
        }

    }
    IEnumerator SetIsooAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isoo = true;
        Debug.Log("Isoo is now true after delay");
    }
}
