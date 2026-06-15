using UnityEngine;

public class TestAnimator : MonoBehaviour
{
    public Animator anim;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetBool("IsWalking", true);
            anim.SetBool("IsRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsRunning", true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            anim.SetTrigger("Attack");
        }
    }
}