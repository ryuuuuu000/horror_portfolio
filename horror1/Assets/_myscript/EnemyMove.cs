using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent agent;

    public Transform[] points;

    private int destPoint = 0;

    private Animator animator;

    private new AudioSource audio;

    Vector3 playerPos;
    GameObject player;
    float distance;

    [SerializeField] float audiodeistance;
    [SerializeField] float trackingRange = 3f;
    [SerializeField] float quitRange = 5f;
    [SerializeField] bool tracking = false;
    [SerializeField] float atakkupos = 1f;

    GameObject particleburess;

    public float ikitime = 1.5f;

    public float riplytime = 5f;

    public AudioClip voissound;

    
    void Start()

    {
        agent = GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();

        audio = GetComponent<AudioSource>();

        audio.Stop();

        agent.autoBraking = false;

        player = GameObject.Find("Player");

        particleburess = GameObject.Find("wind");

        particleburess.SetActive(false);

    }



    void MonsterJunkai()
    {
        if (tracking)
        {
            //追跡の時、quitRangeより距離が離れたら中止
            if (distance > quitRange)
                tracking = false;

            //Playerを目標とする
            agent.destination = playerPos;

        }
        else
        {
            //PlayerがtrackingRangeより近づいたら追跡開始
            if (distance < trackingRange)
                tracking = true;

            if (distance < trackingRange)
            {
                audio.Play();
            }

            // エージェントが現在の巡回地点に到達したら
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                // 次の巡回地点を設定する処理を実行

                GotoNextPoint();

        }
    }



    void GotoNextPoint()
    {
        // 地点がなにも設定されていないときに返します
        if (points.Length == 0)
            return;

        // エージェントが現在設定された目標地点に行くように設定します
        agent.destination = points[destPoint].position;

        // 配列内の次の位置を目標地点に設定し、
        // 必要ならば出発地点にもどります
        destPoint = (destPoint + 1) % points.Length;
    }



    void Activetrue()
    {
        particleburess.SetActive(true);
    }

    void riplymonster()
    {
        animator.SetFloat("Atakku", 0f);
        GetComponent<NavMeshAgent>().isStopped = false;
        particleburess.SetActive(false);
    }


    void Atakkumon()
    {
        animator.SetFloat("Atakku", 2f);
        GetComponent<NavMeshAgent>().isStopped = true;
        Invoke("Activetrue", ikitime);
        audio.PlayOneShot(voissound);
    }





    void Update()
    {
        //Playerとこのオブジェクトの距離を測る
        playerPos = player.transform.position;
        distance = Vector3.Distance(this.transform.position, playerPos);


        if (distance < atakkupos)
        {
            Atakkumon();
            Invoke("riplymonster", riplytime);
            Invoke("GotoNextPoint", riplytime);
        }


        if (distance > audiodeistance)
        {
            audio.Stop();
        }


        MonsterJunkai();

    }
}