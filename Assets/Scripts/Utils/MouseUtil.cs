using System;
using UnityEngine;

static class MouseUtil
{
    const float MAX_DISTANCE = Mathf.Infinity;

    public static MouseClickEvent GetMousePositionInWorld()
    {
        return GetMousePositionInWorld(MAX_DISTANCE);
    }

    public static MouseClickEvent GetMousePositionInWorld(float maxDistance)
    {
        LayerMask layerMask = ~0;
        return GetMousePositionInWorld(maxDistance, layerMask);
    }

    public static MouseClickEvent GetMousePositionInWorld(LayerMask layerMask)
    {
        return GetMousePositionInWorld(MAX_DISTANCE, layerMask);
    }

    public static MouseClickEvent GetMousePositionInWorld(float maxDistance, LayerMask layerMask)
    {
        MouseClickEvent mouseClickEvent = new MouseClickEvent();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData, MAX_DISTANCE, layerMask))
        {
            mouseClickEvent.point = hitData.point;
            mouseClickEvent.hitGameObject = hitData.collider.gameObject;
        }

        return mouseClickEvent;
    }
}

class MouseClickEvent
{
    public GameObject hitGameObject { get; set; }
    public Vector3 point { get; set; } = Vector3.zero;
}