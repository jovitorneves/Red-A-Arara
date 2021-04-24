using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RochaController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb2D;
    [SerializeField]
    private float speed = 3.5f;
    private bool isCaindo = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        IsFalling();
        if (!isCaindo)
        {
            rb2D.velocity = new Vector2(1 * speed, rb2D.velocity.y);
            anim.Play(AnimationTagsConstants.GirandoRocha);
        }
        else
        {
            anim.Play(AnimationTagsConstants.ParadoRocha);
        }
    }

    private void IsFalling()
    {
        if (rb2D.velocity.y < -0.1)
            isCaindo = true;
        else
            isCaindo = false;
    }
}
