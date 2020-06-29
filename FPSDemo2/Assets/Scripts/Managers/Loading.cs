using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Slider progressSlider;
    public GameObject hint;

    private int level;
    private AsyncOperation op = null;
    private float loadingValue = 1.0f;
    private float loadingSliderSpeed = 2;
    private float add = 0.01f;
    CanvasGroup hintCanvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        hintCanvasGroup = hint.GetComponent<CanvasGroup>();
        progressSlider.value = 0.0f;
        level = PlayerPrefs.GetInt("Level", 0);
        StartCoroutine(StartLoading());
    }

    private IEnumerator StartLoading()
    {
        op = SceneManager.LoadSceneAsync("Level 0" + (level + 1));
        op.allowSceneActivation = false;
        yield return op;

        /*
        while(!op.isDone)
        {
            while (op.progress < 0.9f)
            {
                toProgress = (int)op.progress * 100;
                while (displayProgress < toProgress)
                {
                    ++displayProgress;
                    progressSlider.value = displayProgress;
                    yield return new WaitForEndOfFrame();
                }
            }
            if(op.progress >= 0.9f)
            {
                displayProgress = 100;
                progressSlider.value = displayProgress;
                if(Input.anyKeyDown)
                {
                    op.allowSceneActivation = true;
                }
            }

            yield return null;
        }*/
        //op.allowSceneActivation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (hintCanvasGroup.alpha == 0.0f)
        {
            add = 0.01f;
        }
        if (hintCanvasGroup.alpha == 1.0f)
        {
            add = -0.01f;
        }
        hintCanvasGroup.alpha += add;

        loadingValue = op.progress;
        if(loadingValue >= 0.9f)
        {
            loadingValue = 1.0f;
        }

        if(loadingValue != progressSlider.value)
        {
            progressSlider.value = Mathf.Lerp(progressSlider.value, loadingValue,
                Time.deltaTime * loadingSliderSpeed);
            if(Mathf.Abs(loadingValue - progressSlider.value) < 0.01f)
            {
                progressSlider.value = loadingValue;
            }
        }

        /*
        if((int)(progressSlider.value * 100) == 100)
        {
            op.allowSceneActivation = true;
        }
        */

        if(Input.anyKeyDown)
        {
            op.allowSceneActivation = true;
        }
    }
}
