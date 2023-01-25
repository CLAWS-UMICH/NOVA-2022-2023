using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBarController : MonoBehaviour
{

    [SerializeField] GameObject bar;
    [SerializeField] GameObject backplate;
    [SerializeField] float time = 4f;

    void Start() {
        Animator animator = bar.GetComponent<Animator>();
        backplate.SetActive(true);
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

        // Wait 1 second and disable backplate
        yield return new WaitForSeconds(1);
        backplate.SetActive(false);

        // Wait till bar goes back up
        yield return new WaitForSeconds(1);
        animator.SetBool("MenuGaze", false);
        backplate.SetActive(true);
    }
}
