using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadScreenFader : MonoBehaviour
{
    public Image[] toFade;
    public GameObject panel;
    public GameObject text;

    float fadeAlpha = 1f;
    float fadeTime = 1f;

    float fadeMod = 1f;
    bool fadeIn = true;

    bool shouldUpdate = false;

    

    public IEnumerator FadeOut ()
    {
        shouldUpdate = true;
        fadeMod = -1f;

        text.SetActive(false);
        yield return new WaitForSeconds(fadeTime);
        panel.SetActive(false);
    }

    public IEnumerator FadeIn ()
    {
        shouldUpdate = true;
        fadeMod = 1f;

        panel.SetActive(true);
        yield return new WaitForSeconds(fadeTime);
        text.SetActive(true);


        shouldUpdate = false;
    }

    private void Update()
    {
        if (shouldUpdate == false) { return; }

        foreach (Image sprite in toFade)
        {
            Color c = sprite.color;
            sprite.color = new Color(c.r, c.g, c.b, fadeAlpha);
        }
    }
}
