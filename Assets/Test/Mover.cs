using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private PathDraw pathDraw;
    private PathMove pathMove;

    private void Awake()
    {
        pathMove = new PathMove();
    }

    public void SetPathPointList(List<Vector3> pathPointList)
    {
        pathMove.SetPath(pathPointList);
        pathDraw.SetPath(transform, pathPointList);
    }

    public void StartMove()
    {
        pathMove.StartMove(8f);
        ShowPath(true);
    }

    public void ShowPath(bool flag)
    {
        pathDraw.Show(flag);
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        if (pathMove != null && !pathMove.IsEnd) {
            transform.position = pathMove.GetPosition(deltaTime, RemovePoint);
        }
    }

    private void RemovePoint()
    {
        if (pathDraw != null) {
            pathDraw.Remove();
        }
    }
}
