using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDifficulty : MonoBehaviour
{
	[SerializeField] private Button easy = null;
	[SerializeField] private Button medium = null;
	[SerializeField] private Button hard = null;

	void OnGUI() {
		if (Snake.lvl == Snake.Difficulty.easy) {
			easy.Select();
		} else if (Snake.lvl == Snake.Difficulty.medium) {
			medium.Select();
		} else if (Snake.lvl == Snake.Difficulty.hard) {
			hard.Select();
		}

	}

    public void MakeEasy() {
    	Snake.lvl = Snake.Difficulty.easy;
    }

    public void MakeMedium() {
    	Snake.lvl = Snake.Difficulty.medium;
    }

    public void MakeHard() {
    	Snake.lvl = Snake.Difficulty.hard;
    }
}
