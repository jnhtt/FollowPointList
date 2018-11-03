using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private static readonly Vector3 UP_OFFSET = new Vector3(0f, 0.1f, 0f);

    [SerializeField]
    private Camera refCamera;
    [SerializeField]
    private Mover mover;
    private List<Vector3> pathPointList;
    private bool selected;

    private void Awake()
    {
        pathPointList = new List<Vector3>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            SelectMover(Input.mousePosition);
        } else if (Input.GetMouseButton(0)) {
            DrawPath(Input.mousePosition);
        } else if (Input.GetMouseButtonUp(0)) {
            mover.StartMove();
            selected = false;
        }
    }

    private void SelectMover(Vector3 screenPos)
    {
        int layerMask = 1 << LayerMask.NameToLayer("Mover");
        Ray ray = refCamera.ScreenPointToRay(screenPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, layerMask)) {
            pathPointList.Clear();
            pathPointList.Add(hit.collider.gameObject.transform.position + UP_OFFSET);
            mover.ShowPath(true);
            selected = true;
        }
    }

    private void DrawPath(Vector3 screenPos)
    {
        if (!selected) {
            return;
        }
        int layerMask = 1 << LayerMask.NameToLayer("Ground");
        Ray ray = refCamera.ScreenPointToRay(screenPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, layerMask)) {
            pathPointList.Add(hit.point + UP_OFFSET);
            mover.SetPathPointList(pathPointList);
        }
    }
}
