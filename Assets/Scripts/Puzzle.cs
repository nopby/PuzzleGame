using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public int index = 0;
    public int x;
    public int y;
    Action<int, int> swapFunc;
    public int correctPositionX;
    public int correctPositionY;

    void Start() {
        
    }

    void Update() {
        if (IsCorrect()) {
            this.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else {
            this.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public bool IsCorrect() {
        if (x == correctPositionX && y == correctPositionY) {
            return true;
        }
        else {
            return false;
        }
    }

    void ApplyCorrectPosition(int x, int y) {
        this.correctPositionX = x;
        this.correctPositionY = y;
    }

    public void Init(int x, int y, int index, Sprite sprite, Action<int, int> swapFunc) {
        ApplySprite(sprite);
        SetIndex(index);
        UpdatePos(x, y);
        ApplyCorrectPosition(x, y);
        this.swapFunc = swapFunc;
    }

    public void ApplySprite(Sprite sprite) {
        this.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    public void SetIndex(int index) {
        this.index = index;
    }
    void OnMouseDown() {
        Debug.Log(index);

        if (swapFunc != null) {
            swapFunc(x, y);
        }
    }

    public bool IsEmpty() {
        return index == 16;
    }

    public void UpdatePos(int x, int y) {
        this.x = x;
        this.y = y;
        StartCoroutine(Move());
    }

    public void UpdatePos() {
        this.gameObject.transform.localPosition = new Vector2(this.x, this.y);
    }

    IEnumerator Move() {
        float elapsedTime = 0;
        float duration = .2f;
        Vector2 start = this.gameObject.transform.localPosition;
        Vector2 end = new Vector2(x, y);

        while(elapsedTime < duration) {
            this.gameObject.transform.localPosition = Vector2.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        this.gameObject.transform.localPosition = end;
    }


}
