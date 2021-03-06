﻿using UnityEngine;
using System.Collections;

public class ShadowManager : MonoBehaviour {

    public SpriteRenderer TopShadow;
    GameObject TopLine;
    bool topDicovered;
    public SpriteRenderer MidShadow;
    GameObject MidLine;
    bool midDicovered;
    GameObject Player;

    void Start()
    {
        Player = GameObject.Find("Player");
        MidLine = GameObject.Find("MidLine");
        TopLine = GameObject.Find("TopLine");
        TopShadow.gameObject.SetActive(true);
        MidShadow.gameObject.SetActive(true);
    }

    void Update()
    {
        if (!midDicovered && Player.transform.position.y > MidLine.transform.position.y)
        {
            midDicovered = true;
            StartCoroutine("Discover", MidShadow);
        }
        if (!topDicovered && Player.transform.position.y > TopLine.transform.position.y)
        {
            topDicovered = true;
            StartCoroutine("Discover", TopShadow);
        }
    }

    IEnumerator Discover (SpriteRenderer shadow)
    {
        float alpha = 1f;
        while(alpha > 0f)
        {
            alpha -= Time.deltaTime;
            shadow.color = new Color(shadow.color.r, shadow.color.g, shadow.color.b, alpha);
            yield return new WaitForEndOfFrame();
        }
    }
}
