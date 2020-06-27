using System.Collections;
using UnityEngine;

public class Invisible : MonoBehaviour
{
    public Renderer tankRenderer;
    private Animator animator;
    private bool isVisible;

    
    // Start is called before the first frame update
    void Start()
    {
        this.isVisible = true;
        this.animator = GetComponent<Animator>();

        StartCoroutine(StartInvisibility());
    }

    public void ChangeVisibility()
    {
        if (this.isVisible)
        {
            Debug.Log("Goes to fadeout");
            this.animator.SetTrigger("FadeOut");
            this.animator.ResetTrigger("FadeIn");
        } else
        {
            Debug.Log("Goes to fadein");
            this.animator.SetTrigger("FadeIn");
            this.animator.ResetTrigger("FadeOut");
        }

        this.isVisible = !this.isVisible;
        /*
        if (Mathf.Approximately(tankRenderer.material.color.a, 0.0f)) {
            tankRenderer.material.color = new Color(
                tankRenderer.material.color.r, 
                tankRenderer.material.color.g, 
                tankRenderer.material.color.b, 
                1.0f);
        } else
        {
            tankRenderer.material.color = new Color(
                tankRenderer.material.color.r,
                tankRenderer.material.color.g,
                tankRenderer.material.color.b,
                0.0f);
        }
        */
    }

    IEnumerator StartInvisibility()
    {
        // yield return new WaitForSeconds(.5f);

        while (true)
        {
            float randomInterval = Random.Range(1f, 3f);
            yield return new WaitForSeconds(randomInterval);

            this.ChangeVisibility();
        }
    }
}
