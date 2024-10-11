using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int x, y;
    public bool isMine;
    public bool isRevealed = false;

    private FloodFillGrid gridManager;
    private SpriteRenderer spriteRenderer;

    public void Init(int x, int y, bool isMine, FloodFillGrid gridManager)
    {
        this.x = x;
        this.y = y;
        this.isMine = isMine;
        this.gridManager = gridManager;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gameObject.AddComponent<BoxCollider2D>();
    }

    public void OnMouseDown()
    {
        if (isMine)
        {
            SetColor(Color.red);
        }
        else
        {
            gridManager.FloodFill(x, y);
        }
    }

    public void reveal()
    {
        isRevealed = true;
        SetColor(Color.grey);
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

}
