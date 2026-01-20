using UnityEngine;

public class Anim : MonoBehaviour
{
    Animator anim;
    [SerializeField] bool isoo = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Action");
            isoo = true;
        }

    }
}
