using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Simple event system using physics raycasts under rendering to RenderTexture.
/// </summary>
[AddComponentMenu("Event/RenderTexture Physics Raycaster")]
[RequireComponent(typeof(Camera))]
/// <summary>
/// Raycaster for casting against 3D Physics components under rendering to RenderTexture.
/// </summary>
public class RTPhysicsRaycaster : PhysicsRaycaster
{
    RaycastHit[] m_Hits;

    /// <summary>
    /// Returns a ray going from camera through the event position and the distance between the near and far clipping planes along that ray.
    /// </summary>
    /// <param name="eventData">The pointer event for which we will cast a ray.</param>
    /// <param name="ray">The ray to use.</param>
    /// <param name="eventDisplayIndex">The display index used.</param>
    /// <param name="distanceToClipPlane">The distance between the near and far clipping planes along the ray.</param>
    /// <returns>True if the operation was successful. false if it was not possible to compute, such as the eventPosition being outside of the view.</returns>
    protected new bool ComputeRayAndDistance(PointerEventData eventData, ref Ray ray, 
        ref int eventDisplayIndex, ref float distanceToClipPlane)
    {
        if (eventCamera == null)
            return false;

        var eventPosition = Display.RelativeMouseAt(eventData.position);
        if (eventPosition != Vector3.zero)
        {
            // We support multiple display and display identification based on event position.
            eventDisplayIndex = (int)eventPosition.z;

            // Discard events that are not part of this display so the user does not interact with multiple displays at once.
            if (eventDisplayIndex != eventCamera.targetDisplay)
                return false;
        }
        else
        {
            // The multiple display system is not supported on all platforms, when it is not supported the returned position
            // will be all zeros so when the returned index is 0 we will default to the event data to be safe.
            eventPosition = eventData.position;
        }

        // normalized viewport position
        eventPosition.x = eventPosition.x / (float)Screen.width;
        eventPosition.y = eventPosition.y / (float)Screen.height;
        eventPosition.z = 0;
        // Cull ray casts that are outside of the view rect. (case 636595)
        if (!eventCamera.rect.Contains(eventPosition))
            return false;

        ray = eventCamera.ViewportPointToRay(eventPosition);
        // compensate far plane distance - see MouseEvents.cs
        float projectionDirection = ray.direction.z;
        distanceToClipPlane = Mathf.Approximately(0.0f, projectionDirection)
            ? Mathf.Infinity
            : Mathf.Abs((eventCamera.farClipPlane - eventCamera.nearClipPlane) / projectionDirection);
        return true;
    }

    public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
    {
        Ray ray = new Ray();
        int displayIndex = 0;
        float distanceToClipPlane = 0;
        if (!ComputeRayAndDistance(eventData, ref ray, ref displayIndex, ref distanceToClipPlane))
            return;

        int hitCount = 0;

        if (m_MaxRayIntersections == 0)
        {
            m_Hits = Physics.RaycastAll(ray, distanceToClipPlane, finalEventMask);
            hitCount = m_Hits.Length;
        }
        else
        {
            if (m_LastMaxRayIntersections != m_MaxRayIntersections)
            {
                m_Hits = new RaycastHit[m_MaxRayIntersections];
                m_LastMaxRayIntersections = m_MaxRayIntersections;
            }

            hitCount = Physics.RaycastNonAlloc(ray, m_Hits, distanceToClipPlane, finalEventMask);
        }

        if (hitCount != 0)
        {
            if (hitCount > 1)
                System.Array.Sort(m_Hits, 0, hitCount, RaycastHitComparer.instance);

            for (int b = 0, bmax = hitCount; b < bmax; ++b)
            {
                var result = new RaycastResult
                {
                    gameObject = m_Hits[b].collider.gameObject,
                    module = this,
                    distance = m_Hits[b].distance,
                    worldPosition = m_Hits[b].point,
                    worldNormal = m_Hits[b].normal,
                    screenPosition = eventData.position,
                    displayIndex = displayIndex,
                    index = resultAppendList.Count,
                    sortingLayer = 0,
                    sortingOrder = 0
                };
                resultAppendList.Add(result);
            }
        }
    }

    private class RaycastHitComparer : IComparer<RaycastHit>
    {
        public static RaycastHitComparer instance = new RaycastHitComparer();
        public int Compare(RaycastHit x, RaycastHit y)
        {
            return x.distance.CompareTo(y.distance);
        }
    }

}