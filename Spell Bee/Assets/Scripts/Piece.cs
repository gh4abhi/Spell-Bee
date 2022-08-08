using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Piece : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] Manager reference;
    private bool _dragging;
    private Vector2 _offset, originalPos;

    void Awake()
    {
        originalPos = transform.position;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (!_dragging)
            return;
        var mousePostion = GetMousePosition();
        transform.position = mousePostion - _offset;
    }

    void OnMouseDown()
    {
        _dragging = true;
        _offset = GetMousePosition() - (Vector2)transform.position;
    }

    void OnMouseUp()
    {
        Vector2 parPos = (Vector2)parent.position;
        if (Vector2.Distance(transform.position, parPos) < 3)
        {
            Destroy(gameObject);
            reference._count++;
            Debug.Log(reference._count);
            if (reference._count == 6)
            {
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneIndex - 1);
            }
        }
        else
            transform.position = originalPos;
        _dragging = false;

    }

    Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
