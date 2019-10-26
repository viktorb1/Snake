using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnApple : MonoBehaviour
{
	[SerializeField] private GameObject applePrefab = null;
	[SerializeField] private Transform borderTop = null;
	[SerializeField] private Transform borderBottom = null;
	[SerializeField] private Transform borderLeft = null;
	[SerializeField] private Transform borderRight = null;
	[SerializeField] private GameObject snakeHead = null;
	float x, y;

    public void Start()
    {
    	generateNewCoords();
		Instantiate(applePrefab, new Vector2(x,y), Quaternion.identity);
    }


    public Vector3 generateNewCoords() {
    	x = (int)Random.Range((int)borderLeft.position.x, (int)borderRight.position.x) + 0.5f;
		y = (int)Random.Range((int)borderBottom.position.y,  (int)borderTop.position.y) + 0.5f;

    	while (snakeHead.GetComponent<Snake>().isTaken[(int)x + 7, (int)y + 3]) {
    		x = (int)Random.Range((int)borderLeft.position.x, (int)borderRight.position.x) + 0.5f;
			y = (int)Random.Range((int)borderBottom.position.y,  (int)borderTop.position.y) + 0.5f;
    	}

		return new Vector3(x, y, 0);
    }
}