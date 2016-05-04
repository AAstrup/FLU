using UnityEngine;
using System.Collections;

public class ShadowManager : MonoBehaviour {

    SpriteRenderer TopShadow;
    GameObject TopLine;
    bool topDicovered;
    SpriteRenderer MidShadow;
    GameObject MidLine;
    bool midDicovered;
    GameObject Player;

    void Start()
    {
        Player = GameObject.Find("Player");
        MidLine = GameObject.Find("MidLine");
        MidShadow = GameObject.Find("MidShadow").GetComponent<SpriteRenderer>();
        TopLine = GameObject.Find("TopLine");
        TopShadow = GameObject.Find("TopShadow").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!midDicovered && Player.transform.position.y > MidLine.transform.position.y)
        {
            print("DISCOVER" +Time.time);
            midDicovered = true;
            StartCoroutine("Discover", MidShadow);
        }
//        print("midDicovered " + midDicovered);
//        print("Player.transform.position.y "+ Player.transform.position.y+ " vs.  MidLine.transform.position.y " + MidLine.transform.position.y);
        if (!topDicovered && Player.transform.position.y > TopLine.transform.position.y)
        {
            topDicovered = true;
            StartCoroutine("Discover", TopShadow);
        }
    }

    IEnumerator Discover (SpriteRenderer shadow)
    {
        print("Triggered");
        float alpha = 1f;
        while(alpha > 0f)
        {
            alpha -= Time.deltaTime;
            shadow.color = new Color(shadow.color.r, shadow.color.g, shadow.color.b, alpha);
            yield return new WaitForEndOfFrame();
        }
    }
}
