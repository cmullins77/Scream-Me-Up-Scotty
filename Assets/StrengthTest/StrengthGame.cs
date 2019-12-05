﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthGame : MonoBehaviour {

    public GameObject bar;
    public MicInput input;

    public GameObject timerBar;
    public float Time;

    public bool hitting;
    public bool done;

    public int score;
    public GameController gc;

    public int timeQuiet;

    public int gameLength;
    public int barLength;
    public float timeSpeed;
    public RectTransform timeBar;
	// Use this for initialization
	void Start () {
        startGame();
        gameLength = 900;
        timeSpeed = barLength / (gameLength * 1.0f);
        gc = (GameController)FindObjectOfType(typeof(GameController));
    }
	
	// Update is called once per frame
	void Update () {
        timeBar.sizeDelta = new Vector2(timeBar.sizeDelta.x - timeSpeed, timeBar.sizeDelta.y);
        if (timeBar.sizeDelta.x <= 0) {
            gc.currentScore += score;
            gc.goToNext();
        }
        if (!done) {
            float value = input.loudestSound;
            float current = input.MicLoudness;
            if (!hitting) {
                if (current > 0.1) {
                    hitting = true;
                }
            }
            if (hitting) {
                if (current < 0.1) {
                    timeQuiet--;
                    if (timeQuiet == 0) {
                        hitting = false;
                        done = true;
                        gc.currentScore += score;
                        gc.goToNext();
                    }
                } else {
                    timeQuiet = 20;
                }
            }
            hit(value);
        }
	}

    public void hit(float strength) {
        //0 = -103f
        //1 = 55f
        float strenVal;
        strenVal = 178.5f * strength - 103f;
        bar.transform.localPosition = new Vector3(bar.transform.localPosition.x, strenVal, 0);
        score = (int)(strength * 5000);
    }

    public void startGame() {
        hit(0);
        hitting = false;
        done = false;
        timeQuiet = 20;
    }
}
