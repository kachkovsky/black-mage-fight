using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Bot : MonoBehaviour
{
    [HideInInspector]
    public double[] a = null;

    const int N = 8;
    const int N1 = N - 1;
    const int N2 = N / 2;
    const int S = N * N;

    double[] ClearMatrix() {
        return new double[S / 4 * S * S * S * 4]; // hero, blackMage/1st heart, blackMage/2nd heart, blackMage/3rd heart, blackMageIndex
        // heroX = 0..3, heroY = 0..3, heroX <= heroY
        // blackMageIndex = 0..3 (a,b,c,d)
    }

    [ContextMenu("Generate")]
    public void Generate() {
        a = ClearMatrix();
    }

    void swap(ref int a, ref int b) {
        a ^= b ^= a ^= b;
    }

    // desicion = 0..3 (a,b,c,d)
    double GetResult(int heroX, int heroY, int ax, int ay, int bx, int by, int cx, int cy, int dx, int dy, int blackMageIndex, int decision) {
        double result = 0;
        if (decision == blackMageIndex) {
            result += 2.5;
        } else {
            result += 3;
        }
        int targetX = -1, targetY = -1;
        if (decision == 0) {
            targetX = ax; targetY = ay;
            ax = dx; ay = dy; if (blackMageIndex == 4) blackMageIndex = 0;
        }
        if (decision == 1) {
            targetX = bx; targetY = by;
            bx = dx; by = dy; if (blackMageIndex == 4) blackMageIndex = 1;
        }
        if (decision == 2) {
            targetX = cx; targetY = cy;
            cx = dx; cy = dy; if (blackMageIndex == 4) blackMageIndex = 2;
        }
        if (decision == 3) {
            targetX = dx; targetY = dy;
        }
        result -= Math.Abs(heroX - targetX) + Math.Abs(heroY - targetY);
        heroX = targetX;
        heroY = targetY;
        if (heroX >= 4) {
            heroX = N1 - heroX; ax = N1 - ax; bx = N1 - bx; cx = N1 - cx;
        }
        if (heroY >= 4) {
            heroY = N1 - heroY; ay = N1 - ay; by = N1 - by; cy = N1 - cy;
        }
        if (heroX > heroY) {
            swap(ref heroX, ref heroY);
            swap(ref ax, ref ay);
            swap(ref bx, ref by);
            swap(ref cx, ref cy);
        }
        if (bx < ax || bx == ax && by <= ay) {
            swap(ref ax, ref bx);
            swap(ref ay, ref by);
            if (blackMageIndex == 0) blackMageIndex = 1;
            if (blackMageIndex == 1) blackMageIndex = 0;
        }
        if (cx < bx || cx == bx && cy <= by) {
            swap(ref bx, ref cx);
            swap(ref by, ref cy);
            if (blackMageIndex == 1) blackMageIndex = 2;
            if (blackMageIndex == 2) blackMageIndex = 1;
        }
        if (bx < ax || bx == ax && by <= ay) {
            swap(ref ax, ref bx);
            swap(ref ay, ref by);
            if (blackMageIndex == 0) blackMageIndex = 1;
            if (blackMageIndex == 1) blackMageIndex = 0;
        }
        int index = heroX | (heroY << 2) | (ax << 4) | (ay << 7) | (bx << 10) | (by << 13) | (cx << 16) | (cy << 19) | (blackMageIndex << 22);
        result += a[index];
        return result;
    }

    [ContextMenu("Step")]
    public void Step() {
        double[] b = ClearMatrix();
        for (int heroX = 0; heroX < N2; heroX++) {
            for (int heroY = heroX; heroY < N2; heroY++) {
                for (int ax = 0; ax < N; ax++) {
                    for (int ay = 0; ay < N; ay++) {
                        if (ax == heroX && ay == heroY) {
                            continue;
                        }
                        for (int bx = 0; bx < N; bx++) {
                            for (int by = 0; by < N; by++) {
                                if (bx < ax || bx == ax && by <= ay) {
                                    continue;
                                }
                                if (bx == heroX && by == heroY) {
                                    continue;
                                }
                                for (int cx = 0; cx < N; cx++) {
                                    for (int cy = 0; cy < N; cy++) {
                                        if (cx < bx || cx == bx && cy <= by) {
                                            continue;
                                        }
                                        if (cx == heroX && cy == heroY) {
                                            continue;
                                        }
                                        for (int blackMageIndex = 0; blackMageIndex < 4; blackMageIndex++) {
                                            double result = 0;
                                            double cnt = 0;
                                            for (int dx = 0; dx < N; dx++) {
                                                for (int dy = 0; dy < N; dy++) {
                                                    if (dx == heroX && dy == heroY || dx == ax && dy == ay || dx == bx && dy == by || dx == cx && dy == cy) {
                                                        continue;
                                                    }
                                                    double best = double.NegativeInfinity;
                                                    int bestDecision = -1;
                                                    for (int decision = 0; decision < 4; decision++) {
                                                        double cand = GetResult(heroX, heroY, ax, ay, bx, by, cx, cy, dx, dy, blackMageIndex, decision);
                                                        if (cand > best) {
                                                            best = cand;
                                                            bestDecision = decision;
                                                        }
                                                    }
                                                    result += best;
                                                    ++cnt;
                                                }
                                            }
                                            int index = heroX | (heroY << 2) | (ax << 4) | (ay << 7) | (bx << 10) | (by << 13) | (cx << 16) | (cy << 19) | (blackMageIndex << 22);
                                            b[index] = result;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        a = b;
    }

    [ContextMenu("PrintResult")]
    public void PrintResult() {
        int heroX = Hero.instance.Position.x;
        int heroY = Hero.instance.Position.y;
        var bonuses = FindObjectsOfType<Heart>().Select(h => new Bonus(h.Position.x, h.Position.y, false, h.Position)).ToList();
        var bmp = BlackMage.instance.Position;
        bonuses.Add(new Bonus(bmp.x, bmp.y, true, bmp));
        var blackMageIndex = 3;
        int ax = bonuses[0].x;
        int ay = bonuses[0].y;
        int bx = bonuses[1].x;
        int by = bonuses[1].y;
        int cx = bonuses[2].x;
        int cy = bonuses[2].y;
        int dx = bonuses[3].x;
        int dy = bonuses[3].y;

        double best = double.NegativeInfinity;
        int bestDecision = -1;
        for (int decision = 0; decision < 4; decision++) {
            double cand = GetResult(heroX, heroY, ax, ay, bx, by, cx, cy, dx, dy, blackMageIndex, decision);
            if (cand > best) {
                best = cand;
                bestDecision = decision;
            }
        }
        var target = bonuses[bestDecision].cell;
        Debug.LogFormat("Result: {0}, goto ({1}, {2})", best, target.x, target.y);
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.V)) {
            PrintResult();
        }
    }
}
