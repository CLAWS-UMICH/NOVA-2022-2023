using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBarController : MonoBehaviour
{

    [SerializeField] GameObject bar;

    void Start() {
        Animator animator = bar.GetComponent<Animator>();
    }

    public void DropBar() {
        if (bar != null) {
            Animator animator = bar.GetComponent<Animator>();

            if (animator != null) {
                bool isOpen = animator.GetBool("MenuGaze");

                animator.SetBool("MenuGaze", !isOpen);
            }
        }
    }
}
