using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using RSG;

public static class TimeManager
{
    static float timeCorrection = 0;

    public static bool isPaused = false; 

    static PromiseTimer promiseTimer = new PromiseTimer();

    /// <summary>
    /// Occurs when game is paused: true - if pause switched on.
    /// </summary>
    public static event Action<bool> onPauseChanged=(bool b)=>{};

    public static void Reset(){
        promiseTimer = new PromiseTimer();
    }

    public static void SetGameTime(float correctGameTime) {
        timeCorrection = correctGameTime - Time.time;
    }

    /// <summary>
    /// Time in the game world since current game session start
    /// </summary>
    /// <returns>The time.</returns>
    public static float GameTime()
    {
        return Time.time + timeCorrection;
    }
    
    public static void Pause(){
        isPaused = true;
        onPauseChanged(true);
        Time.timeScale = 0.0f;
    }
    
    public static void Resume(){
        isPaused = false;

        Time.timeScale = 1.0f;
        onPauseChanged(false);
    }

    public static void PauseOrResume() {
        if (isPaused) {
            Resume();
        } else {
            Pause();
        }
    }

    static float lastUpdateTime = 0;

    public static void UpdateTime() {
        promiseTimer.Update(Time.time - lastUpdateTime);
        lastUpdateTime = Time.time;
    }

    public static IPromise Wait(float seconds) {
        return promiseTimer.WaitFor(seconds);
    }
}
