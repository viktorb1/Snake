using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour
{
	public enum Difficulty { easy, medium, hard }
	public static Difficulty lvl = Difficulty.medium;
	private Vector2 direction = Vector2.right;
	private List<Transform> tail = new List<Transform>();
	[SerializeField] private GameObject tailPrefab = null;
	private bool ateApple = false;
	public bool[,] isTaken = new bool[16,8];
	[SerializeField] private GameObject scoreValue = null;
	[SerializeField] private GameObject gameOver = null;
	[SerializeField] private GameObject finalScore = null;
    private Vector3 prev_head_pos;

    // Start is called before the first frame update
    void Start()
    {
    	float speed = 0.3f;

    	if (lvl == Difficulty.easy)
    		speed = 0.5f;
    	else if (lvl == Difficulty.medium)
    		speed = 0.3f;
    	else if (lvl == Difficulty.hard)
    		speed = 0.1f;

        InvokeRepeating("Move", speed, speed);  
    }

    void Move()
    {
        Vector3 prev = prev_head_pos;
    	prev_head_pos = transform.position;
        transform.Translate(direction);

        if (transform.position == prev && tail.Count > 0) {
            OnTriggerEnter2D(null);
        }

        if (ateApple) {
        	GameObject tailPiece = Instantiate(tailPrefab, prev_head_pos, Quaternion.identity);
        	tail.Insert(0, tailPiece.transform);
        	isTaken[(int)tailPiece.transform.position.x + 7, (int)tailPiece.transform.position.y + 3] = true;
        	ateApple = false;
        } else if (tail.Count > 0) {
        	isTaken[(int)tail.Last().position.x + 7, (int)tail.Last().position.y + 3] = false;
        	tail.Last().position = prev_head_pos;
        	tail.Insert(0, tail.Last());
        	isTaken[(int)tail.First().position.x + 7, (int)tail.First().position.y + 3] = true;
        	tail.RemoveAt(tail.Count - 1);
        }
    }


    void Update()
    {
    	if (Input.GetKey(KeyCode.RightArrow)) {
    		transform.rotation = Quaternion.Euler(0, 0, 0);
    	} else if (Input.GetKey(KeyCode.LeftArrow)) {
    		transform.rotation = Quaternion.Euler(0, 0, 180);
    	}
    	else if (Input.GetKey(KeyCode.UpArrow)) {
    		transform.rotation = Quaternion.Euler(0, 0, 90);
    	}
    	else if (Input.GetKey(KeyCode.DownArrow)) {
    		transform.rotation = Quaternion.Euler(0, 0, -90);
    	}
    }

    void OnTriggerEnter2D(Collider2D col) {
    	if (col && col.gameObject.tag == "ApplePrefab") {
    		col.gameObject.SetActive(false);
    		col.gameObject.transform.position = Camera.main.GetComponent<SpawnApple>().generateNewCoords();
    		col.gameObject.SetActive(true);
    		scoreValue.GetComponent<ScoreScript>().scoreValue += 1;
    		scoreValue.GetComponent<ScoreScript>().UpdateScore();
    		ateApple = true;
    	} else {
    		// game over
    		CancelInvoke("Move");
    		finalScore.GetComponent<ScoreScript>().scoreValue = scoreValue.GetComponent<ScoreScript>().scoreValue;
    		finalScore.GetComponent<ScoreScript>().UpdateScore();
    		gameOver.SetActive(true);
    	}

    }
}
