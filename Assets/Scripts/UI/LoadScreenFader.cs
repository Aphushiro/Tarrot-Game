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

    bool shouldUpdate = false;


    public void BeginFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut ()
    {
        shouldUpdate = true;
        fadeMod = -1f;

        text.SetActive(false);
        yield return new WaitForSeconds(fadeTime);
        panel.SetActive(false);
        fadeAlpha = 0f;
        shouldUpdate = false;
    }

    public void BeginFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn ()
    {
        shouldUpdate = true;
        fadeMod = 1f;

        panel.SetActive(true);
        yield return new WaitForSeconds(fadeTime);
        text.SetActive(true);

        fadeAlpha = 1f;
        shouldUpdate = false;
    }

    private void LateUpdate()
    {
        if (shouldUpdate == false) { return; }
        fadeAlpha += Time.deltaTime * fadeMod;

        foreach (Image sprite in toFade)
        {
            Color c = sprite.color;
            sprite.color = new Color(c.r, c.g, c.b, fadeAlpha);
        }
    }
}
