using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Puzzle puzzlePrefab;
    public Sprite[] sprites;
    private Puzzle[,] puzzles = new Puzzle[4, 4];
    private bool isFinished;
    int[] checkShuffle = new int[16];
    void Start()
    {
        SpawnPuzzle();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFinished) {
            int correct = 0;
            foreach (var puzzle in puzzles) {
                if (puzzle != null) {
                    if (puzzle.IsCorrect()) {
                        correct++;
                    }
                }
            }
            if (correct == puzzles.Length) {
                isFinished = true;
                Debug.Log("Game Finished");
                GetComponent<Timer>().StopTimer();
            }
        }
    }

    void SpawnPuzzle() {
        int n = 0;
        for (int y = 3; y >= 0; y--) {
            for (int x = 0; x <= 3; x++) {
                Puzzle puzzle = Instantiate(puzzlePrefab, new Vector2(x, y), Quaternion.identity);
                puzzle.Init(x, y, n + 1, sprites[n], ClickToSwap);
                puzzles[x, y] = puzzle;
                n++;
            }
        }
    }

    int getDx(int x, int y) {
        if (x < 3 && puzzles[x + 1, y].IsEmpty()) {
            return 1;
        }
        if (x > 0 && puzzles[x - 1, y].IsEmpty()) {
            return -1;
        }
        return 0;
    }

    int getDy(int x, int y) {
        if (y < 3 && puzzles[x, y + 1].IsEmpty()) {
            return 1;
        }
        if (y > 0 && puzzles[x, y - 1].IsEmpty()) {
            return -1;
        }
        return 0;
    }

    void ClickToSwap(int x, int y) {
        int dx = getDx(x, y);
        int dy = getDy(x, y);
        Swap(x, y, dx, dy);
    }

    void Swap(int x, int y, int dx, int dy) {
        
        var from = puzzles[x, y];
        var to = puzzles[x + dx, y + dy];

        if (from.index != 16) {
            FindObjectOfType<AudioManager>().Play("SE");
        }

        puzzles[x, y] = to;
        puzzles[x + dx, y + dy] = from;

        to.UpdatePos(x, y);
        from.UpdatePos(x + dx, y + dy);

    }


    // public void Shuffle() {
    //     GetComponent<Timer>().StopTimer();
    //     GetComponent<Timer>().ResetTimer();
        
    //     for (int y = 3; y >= 0; y--) {
    //         for (int x = 0; x <= 3; x++) {
    //             int randomX = Random.Range(0, 4);
    //             int randomY = Random.Range(0, 4);
    //             var from = puzzles[x, y];
    //             var to = puzzles[randomX, randomY];
    //             puzzles[x, y] = to;
    //             puzzles[randomX, randomY] = from;
    //             to.UpdatePos(x, y);
    //             from.UpdatePos(randomX, randomY);
    //         }
    //     }


    //     GetComponent<Timer>().StartTimer();
    // }
    public void Shuffle() {
        GetComponent<Timer>().StopTimer();
        GetComponent<Timer>().ResetTimer();
        for (int r = 0; r < 20; r++) {
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    if(puzzles[i, j].IsEmpty()) {
                        Vector2 pos = getValidMove(i, j);
                        Swap(i, j, (int)pos.x, (int)pos.y);
                    }
                }
            }
        }
        this.isFinished = false;
        GetComponent<Timer>().StartTimer();
    }

    Vector2 getValidMove(int x, int y) {
        Vector2 pos = new Vector2();
        do {
            int n = Random.Range(0, 4);
            if (n == 0)
                pos = Vector2.left;
            else if (n == 1)
                pos = Vector2.right;
            else if (n == 2) 
                pos = Vector2.up;
            else
                pos = Vector2.down;
        } while (!(isValidRange(x +(int) pos.x) && isValidRange(y + (int) pos.y)) || isRepeatMove(pos));
        lastMove = pos;
        return pos;
    }

    bool isValidRange(int n) {
        return n >= 0 && n <= 3;
    }

    bool isRepeatMove(Vector2 pos) {
        return pos * -1 == lastMove;
    }

    Vector2 lastMove;

    public void ExitGame() {
        Application.Quit();
    }

}
