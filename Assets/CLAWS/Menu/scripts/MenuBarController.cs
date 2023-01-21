using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBarController : MonoBehaviour
{

    [SerializeField] GameObject bar;
    [SerializeField] float time = 4f;

    void Start() {
        Animator animator = bar.GetComponent<Animator>();
    }

    public void DropBar() {
        if (bar != null) {
            Animator animator = bar.GetComponent<Animator>();

            if (animator != null) {
                bool isOpen = animator.GetBool("MenuGaze");

                animator.SetBool("MenuGaze", !isOpen);

                StartCoroutine(WaitTwo());

                animator.SetBool("MenuGaze", !isOpen);
            }
        }
    }

    IEnumerator WaitTwo() {
        Animator animator = bar.GetComponent<Animator>();
        yield return new WaitForSeconds(time);
        animator.SetBool("MenuGaze", false);
    }
}
