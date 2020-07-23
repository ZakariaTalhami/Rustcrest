using System;
using UnityEngine;

static class MouseUtil
{
    const float MAX_DISTANCE = Mathf.Infinity;

    public static Vector3 getMousePositionInWorld()
    {
        LayerMask layerMask = ~0;
        return getMousePositionInWorld(MAX_DISTANCE, layerMask);
    }

    public static Vector3 getMousePositionInWorld(float maxDistance)
    {
        LayerMask layerMask = ~0;
        return getMousePositionInWorld(maxDistance, layerMask);
    }

    public static Vector3 getMousePositionInWorld(LayerMask layerMask)
    {
        return getMousePositionInWorld(MAX_DISTANCE, layerMask);
    }

    public static Vector3 getMousePositionInWorld(float maxDistance, LayerMask layerMask)
    {
        Vector3 mousePosInWorld = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData, MAX_DISTANCE, layerMask))
        {
            mousePosInWorld = hitData.point;
        }

        return mousePosInWorld;
    }
}