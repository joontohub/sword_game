using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLineRenderer : MonoBehaviour {

    // Line Drawn event handler
    public delegate void LineDrawnHandler(Vector3 begin, Vector3 end, Vector3 depth);
    public event LineDrawnHandler OnLineDrawn;

    bool dragging;
    Vector3 start;
    Vector3 end;
    Camera cam;

    public Material lineMaterial;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        dragging = false;
    }

    private void OnEnable()
    {
        Camera.onPostRender += PostRenderDrawLine;
    }

    private void OnDisable()
    {
        Camera.onPostRender -= PostRenderDrawLine;
    }

    // Update is called once per frame
    // 이 부분을 동작 실행될 때 칼날 지점 . 
    void Update () {
        if (!dragging && Input.GetMouseButtonDown(0))
        {
            start = cam.ScreenToViewportPoint(Input.mousePosition);
            dragging = true;
        }
        // 이 부분은 칼날 움직이는 거 . 
        if (dragging)
        {
            end = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        // 이 부분은 칼날 끝날때 . 
        if (dragging && Input.GetMouseButtonUp(0))
        {
            // Finished dragging. We draw the line segment
            end = cam.ScreenToViewportPoint(Input.mousePosition);
            dragging = false;

            var startRay = cam.ViewportPointToRay(start);
            var endRay = cam.ViewportPointToRay(end);

            // Raise OnLineDrawnEvent
            OnLineDrawn?.Invoke(
                startRay.GetPoint(cam.nearClipPlane),
                endRay.GetPoint(cam.nearClipPlane),
                endRay.direction.normalized);
        }
    }
    

    /// <summary>
    /// Draws the line in viewport space using start and end variables
    /// </summary>
    private void PostRenderDrawLine(Camera cam)
    {
        if (dragging && lineMaterial)
        {
            GL.PushMatrix();
            lineMaterial.SetPass(0);
            GL.LoadOrtho();
            GL.Begin(GL.LINES);
            GL.Color(Color.black);
            GL.Vertex(start);
            GL.Vertex(end);
            GL.End();
            GL.PopMatrix();
        }
    }
}
