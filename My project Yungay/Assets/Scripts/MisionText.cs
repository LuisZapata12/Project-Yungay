using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MisionText : MonoBehaviour
{
    [TextArea(10, 15)]
    public List<string> misions = new List<string>();
    public static int currentMision = 0;
    TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CinematicTutorial.playTutorial)
        {
            text.text = misions[currentMision];
        }
    }
}
