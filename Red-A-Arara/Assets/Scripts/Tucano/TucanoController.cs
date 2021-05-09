using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TucanoController : BaseEnemyController
{

    [SerializeField]
    private float hitPoints;
    [SerializeField]
    private float maxHitPoints = 5;
    [SerializeField]
    private HealthbarController healthbar;


    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Transform posicaoA, posicaoB;

    private Animator animator;

    private Player playerScript;

    [SerializeField]
    private bool isLookLeft = false;

    private float distancia = 0f;

    private bool isMove = true;

    private readonly float speed = 4f;

    private Vector3 nextPos;

    private float delayTime = 3f;
    private bool isAtordoada = false;
    private int cocoCount = 0;
    private float delayAtordoadoTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Player>();
        animator = GetComponent<Animator>();
        hitPoints = maxHitPoints;
        healthbar.SetHealth(hitPoints, maxHitPoints);

        if (posicaoA.Equals(null) || posicaoB.Equals(null))
            return;

        posicaoA.position = new Vector3(posicaoA.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        posicaoB.position = new Vector3(posicaoB.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (posicaoA.Equals(null) || posicaoB.Equals(null))
            return;

        if (isDead) return;

        float disA = gameObject.transform.position.x - posicaoA.position.x;
        float disB = gameObject.transform.position.x - posicaoB.position.x;
        disA = disA > 0 ? (disA * 1) : disA * -1;
        disB = disB > 0 ? (disB * 1) : disB * -1;

        DistanciaPlayerIntervalTucano();
        delayTime -= Time.deltaTime;

        if (isAtordoada)
            delayAtordoadoTime -= Time.deltaTime;

        if (delayTime <= 0 && isAtordoada)
            isAtordoada = false;

        if (isAtordoada) return;

        if (isMove)
        {
            //Move o tucano sempre
            if (delayTime <= 0)
                MoveTucano();
            //Move o tucano só quando player estiver perto
            //if (player.gameObject.transform.position.x >= posicaoA.position.x &&
            //    player.gameObject.transform.position.x <= posicaoB.position.x)
            //    if (delayTime <= 0)
            //        MoveTucano();
            //else
            //    if (delayTime <= 0)
            //        MoveTucano();
        } else
            animator.Play(AnimationTagsConstants.IdleTucano);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(posicaoA.position, posicaoB.position);
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag(TagsConstants.Player))
        {
            var playerController = collision2D.gameObject.GetComponent(typeof(Player)) as Player;
            if (!playerController.isAlive) return;
            if (UtilController.Instance.ReturnDirection(collision2D.contacts) != HitDirection.Top)
                return;

            hitPoints -= 1;
            healthbar.SetHealth(hitPoints, maxHitPoints);
            if (hitPoints <= 0)
                TucanoMorto();
        }
        if (collision2D.gameObject.CompareTag(TagsConstants.CocoPartido))
        {
            if (!isAtordoada)
                cocoCount++;

            hitPoints -= 2;
            healthbar.SetHealth(hitPoints, maxHitPoints);
            if (hitPoints <= 0)
                TucanoMorto();

            if (cocoCount < 2)
            {
                animator.Play(AnimationTagsConstants.AtordoadoTucano);
                isAtordoada = true;
                delayAtordoadoTime = Time.deltaTime * 2;
                SoundManager.Instance.PlayFxAtordoado();
            }
        }
    }

    private void TucanoMorto()
    {
        animator.Play(AnimationTagsConstants.MortoTucano);
        isDead = true;
        SoundManager.Instance.PlayFxCobraDie();
    }

    private void DistanciaPlayerIntervalTucano()
    {
        distancia = gameObject.transform.position.x - player.transform.position.x;
    }

    private void MoveTucano()
    {
        if (transform.position == posicaoA.position)
        {
            isMove = playerScript.isAlive;
            delayTime = 3f;
            nextPos = posicaoB.position;
            Flip();
        }
        if (transform.position == posicaoB.position)
        {
            isMove = playerScript.isAlive;
            delayTime = 3f;
            nextPos = posicaoA.position;
            Flip();
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        animator.Play(AnimationTagsConstants.VoandoTucano);
        if (delayTime == 3)
            animator.Play(AnimationTagsConstants.IdleTucano);
    }

    private void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1;
        gameObject.transform.localScale = newScale;
        isLookLeft = !isLookLeft;
    }

    public void deadTucano()
    {
        Destroy(gameObject);
    }
}
