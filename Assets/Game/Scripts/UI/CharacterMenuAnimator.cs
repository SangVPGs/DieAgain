using UnityEngine;

public class CharacterMenuAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public void ShowAndPlayIdle()
    {
        gameObject.SetActive(true);
        animator.SetBool("IsDead", false);
    }

    public void ShowAndPlayDie()
    {
        gameObject.SetActive(true);
        animator.SetBool("IsDead", true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}