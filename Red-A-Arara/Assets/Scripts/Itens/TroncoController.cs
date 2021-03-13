using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroncoController : MonoBehaviour
{
    private Animator animator;
    private float delayTime = 3f;
    private bool isCount = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCount) {
            delayTime -= Time.deltaTime;
        }

        if (delayTime <= 0)
        {
            animator.Play(AnimationTagsConstants.CaindoTronco);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag(TagsConstants.Player))
        {
            if (UtilController.Instance.ReturnDirection(collision2D.contacts) == HitDirection.Top)
            {
                isCount = true;
            }
        }
    }

    public void DestroyTronco()
    {
        Destroy(gameObject);
    }
}
