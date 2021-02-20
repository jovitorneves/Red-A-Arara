﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoController : BaseEnemyController
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Transform posicaoA, posicaoB;

    private Collision2D collision2DCurrent;
    private Animator animator;

    private Player playerScript;

    [SerializeField]
    private bool isLookLeft = false;

    private float distancia = 0f;

    private readonly float distanciaHumanoAndPointMax = 1.5f;

    private readonly float speed = 1f;

    private Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Player>();
        animator = GetComponent<Animator>();

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

        float disA = gameObject.transform.position.x - posicaoA.position.x;
        float disB = gameObject.transform.position.x - posicaoB.position.x;
        disA = disA > 0 ? (disA * 1) : disA * -1;
        disB = disB > 0 ? (disB * 1) : disB * -1;

        DistanciaPlayerIntervalHumano();

        if (playerScript.isAlive)
        {
            if (player.gameObject.transform.position.x >= posicaoA.position.x &&
                player.gameObject.transform.position.x <= posicaoB.position.x)
            {
                if (disA >= distanciaHumanoAndPointMax &&
                    disB >= distanciaHumanoAndPointMax)
                    PuloRule();
                else
                    MoveHumano();
            }
            else
                MoveHumano();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(posicaoA.position, posicaoB.position);
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        collision2DCurrent = collision2D;

        //Debug.Log("DIRECAO HUMANO: " + UtilController.Instance.ReturnDirection(collision2D.contacts));

        //if (collision2D.gameObject.CompareTag(TagsConstants.Chao) && isJump)
        //{
        //    isJump = false;
        //}

        //if (player.gameObject.transform.position.x >= posicaoA.position.x &&
        //    player.gameObject.transform.position.x <= posicaoB.position.x &&
        //    playerScript.isAlive &&
        //    !isJump)
        //    Invoke(MethodNameTagsConstants.Pulo, delayTime);

    }

    private void DistanciaPlayerIntervalHumano()
    {
        distancia = gameObject.transform.position.x - player.transform.position.x;
    }

    private void MoveHumano()
    {
        if (transform.position == posicaoA.position)
        {
            nextPos = posicaoB.position;
            Flip();
        }
        if (transform.position == posicaoB.position)
        {
            nextPos = posicaoA.position;
            Flip();
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        animator.Play(AnimationTagsConstants.IdleHumano);
    }

    private void PuloRule()
    {
        if (!playerScript.isAlive) return;

        if (player.gameObject.transform.position.x < posicaoA.position.x ||
            player.gameObject.transform.position.x > posicaoB.position.x)
            return;

        animator.Play(AnimationTagsConstants.CapturaHumano);

        if (transform.position == posicaoA.position)
        {
            nextPos = posicaoB.position;
            Flip();
        }
        if (transform.position == posicaoB.position)
        {
            nextPos = posicaoA.position;
            Flip();
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1;
        gameObject.transform.localScale = newScale;
        isLookLeft = !isLookLeft;
    }
}