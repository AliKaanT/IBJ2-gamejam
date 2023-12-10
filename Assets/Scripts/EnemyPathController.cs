using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.IO;

public class EnemyPathController : MonoBehaviour
{
    [SerializeField] private Transform pathParent;
    [SerializeField] private PathType pathType;

    private Transform startPos;

    private void Start()
    {
        StartPatrolling();
    }

    private void StartPatrolling()
    {
        startPos = transform;

        Vector3[] pathArray = new Vector3[pathParent.childCount + 1];

        for (int i = 0; i < pathArray.Length - 1; i++)
        {
            pathArray[i] = pathParent.GetChild(i).position;
        }

        pathArray[pathArray.Length - 1] = startPos.position;

        transform.DOPath(pathArray, 7f, pathType, PathMode.TopDown2D).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).SetLookAt(0.01f).SetSpeedBased(true);
    }


    public void StopPatrolling()
    {
        transform.DOKill();
    }
}
