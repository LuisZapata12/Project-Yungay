using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fade : MonoBehaviour
{
    private TMP_Text text;
    public float speed;
    private float timer;
    public float timeToShow;
    private bool once = false;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!once)
        {
            timer += Time.deltaTime;
        }

        if (timer > timeToShow)
        {
            timer = 0;
            StartCoroutine(FadeText());
            once = true;
        }
    }

    IEnumerator FadeText()
    {
        float a = text.color.a;
        while (text.color.a > 0)
        {
            a -= speed * Time.deltaTime;
            text.color = new Color(text.color.r, text.color.g, text.color.b, a);
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
        yield break;
    }
}
