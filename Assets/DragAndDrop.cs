using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool _dragging;

    private Vector2 _offset;

    public static bool mouseButtonReleased;

    public GameObject mergeVFX;

    private void OnMouseDown()
    {
        _dragging = true;

        _offset = GetMousePos() - (Vector2)transform.position;
    }

    private void OnMouseDrag()
    {
        if (!_dragging) return;

        var mousePosition = GetMousePos();

        transform.position = mousePosition - _offset;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        _dragging = false;
    }

    private void OnMouseUp()
    {
        mouseButtonReleased = true;
    }

    private Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        string thisGameobjectName;
        string collisionGameobjectName;

        thisGameobjectName = gameObject.name.Substring(0, name.IndexOf("_"));
        collisionGameobjectName = collision.gameObject.name.Substring(0, name.IndexOf("_"));

        if (mouseButtonReleased && thisGameobjectName == "penguin" && thisGameobjectName == collisionGameobjectName)
        {
            Instantiate(Resources.Load("racoon_Object"), transform.position, Quaternion.identity);

            GameObject explosion = Instantiate(mergeVFX, transform.position, transform.rotation);
            Destroy(explosion, .75f);
            mouseButtonReleased = false;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (mouseButtonReleased && thisGameobjectName == "racoon" && thisGameobjectName == collisionGameobjectName)
        {
            Instantiate(Resources.Load("rabbit_Object"), transform.position, Quaternion.identity);
            GameObject explosion = Instantiate(mergeVFX, transform.position, transform.rotation);
            Destroy(explosion, .75f);
            mouseButtonReleased = false;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
