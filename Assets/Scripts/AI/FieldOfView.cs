using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// soure: https://www.youtube.com/watch?v=j1-OyLo77ss
public class FieldOfView : MonoBehaviour
{
    public delegate void EnterView();
    public event EnterView OnEnterView;
    public delegate void ExitView();
    public event ExitView OnExitView;

    public AudioSource Enemy_music_source;
    public AudioClip Enemy_music;
    private bool check = false;

    [SerializeField, Range(0.0f, 50.0f)] public float radius;
    
    [SerializeField, Range(0.0f, 360.0f)] public float angle;

    [SerializeField] public GameObject playerRef;

    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;

    public bool canSeePlayer;

    private void Start()
    {
        // not sure if I wanna do this
        //playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
            if (check == false)
            {
                Enemy_music_source.Stop();
            }
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        bool oldCanSeePlayer = canSeePlayer;

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                    if(check == false){
                        check = true;
                        Enemy_music_source.clip=Enemy_music;
                        Enemy_music_source.Play();
                    }
                }
                else{
                    canSeePlayer = false;
                    check=false;
                }
            }
            else{
                canSeePlayer = false;
                check=false;
            }
        }
        else if (canSeePlayer){
            canSeePlayer = false;
            check=false;
        }

        if(oldCanSeePlayer != canSeePlayer)
        {
            if (canSeePlayer)
                OnEnterView();
            else
                OnExitView();
        }
    }
}
