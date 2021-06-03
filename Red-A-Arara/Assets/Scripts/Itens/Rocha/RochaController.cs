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
    private bool isParada = false;

    private float DESTROY_GAMEOBJECT_TIME = 3.5f;

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
        if (isParada)
        {
            DESTROY_GAMEOBJECT_TIME -= Time.deltaTime;

            //if (DESTROY_GAMEOBJECT_TIME <= 0)//retirar caso queira q a rocha suma dps de um tempo
                //Destroy(gameObject);
        }
        else
        {
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isParada) return;
        if (collision.gameObject.CompareTag(TagsConstants.RochaPara) ||
            collision.gameObject.CompareTag(TagsConstants.Player))
        {
            rb2D.velocity = new Vector2(0, 0);
            anim.Play(AnimationTagsConstants.ParadoRocha);
            isParada = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isParada) return;
        if (collision.gameObject.CompareTag(TagsConstants.RochaPara) ||
            collision.gameObject.CompareTag(TagsConstants.Player))
        {
            rb2D.velocity = new Vector2(0, 0);
            anim.Play(AnimationTagsConstants.ParadoRocha);
            isParada = true;
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
