using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public abstract class GUIWindow : MonoBehaviour
{
    public GUIReferenceManager guiReference;
    public CanvasGroup canvasGroup;

    private void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void OpenWindow()
    {
        transform.DOComplete();
        this.gameObject.SetActive(true);
        //Debug.Log("Opening the window" + canvasGroup);
        canvasGroup.DOFade(1, 1);

    }

    //old open
    public virtual void OpenWindow(float endValue, float duration)
    {
        transform.DOComplete();
        this.gameObject.SetActive(true);
        //Debug.Log("Opening the window" + canvasGroup);
        canvasGroup.DOFade(endValue, duration);
    }

    //open an object with direction
    public virtual void OpenObject(Transform thing, Vector3 comeInPosition, float duration)
    {
        transform.DOComplete();
        Vector3 originPos = thing.localPosition;
        thing.position += comeInPosition;
        thing.DOLocalMove(originPos, duration, false).SetEase(Ease.OutBounce);
    }
    // open object with direction and delay
    public virtual void OpenObject(Transform thing, Vector3 comeInPosition, float duration, float delay)
    {
        transform.DOComplete();
        Vector3 originPos = thing.localPosition;
        thing.position += comeInPosition;
        thing.DOLocalMove(originPos, duration, false).SetEase(Ease.OutBounce).SetDelay(delay);
    }
    // open object with direction and delay + ease
    public virtual void OpenObject(Transform thing, Vector3 comeInPosition, float duration, float delay, Ease ease)
    {
        transform.DOComplete();
        Vector3 originPos = thing.localPosition;
        thing.position += comeInPosition;
        thing.DOLocalMove(originPos, duration, false).SetEase(ease).SetDelay(delay);
    }

    // open object with scale and delay
    public virtual void OpenObject(Transform thing, float comeInScale, float duration, float delay)
    {
        transform.DOComplete();
        Vector3 origScale = thing.localScale;
        thing.localScale *= comeInScale;
        thing.DOScale(origScale, duration).SetDelay(delay);
    }

    //open with direction
    public virtual void OpenWindow(Vector3 comeInPosition, float duration)
    {
        transform.DOComplete();
        this.gameObject.SetActive(true);
//        if (GetComponent<CanvasGroup>() != null)
//        {
//            this.GetComponent<CanvasGroup>().blocksRaycasts = true;
//        }
//        else
//        {
//            Debug.LogError("Thier is no canvas group on this object?");
//        }
        Vector3 originPos = transform.position;
        //Debug.Log("Original position on " + transform.name + " = " + originPos);
        transform.position = originPos + comeInPosition;
        if (GetComponent<CanvasGroup>() != null)
            canvasGroup.alpha = 1;
        transform.DOMove(originPos, duration, false).SetEase(Ease.OutElastic);
    }
    //open with direction, set ease
    public virtual void OpenWindow(Vector3 comeInPosition, float duration, Ease ease)
    {
        transform.DOComplete();
       gameObject.SetActive(true);
//        if (GetComponent<CanvasGroup>() != null)
//        {
//            this.GetComponent<CanvasGroup>().blocksRaycasts = true;
//        }
//        else
//        {
//            Debug.LogError("There is no canvas group on this object");
//        }
        Vector3 originPos = transform.position;
        //Debug.Log("Original position on " + transform.name + " = " + originPos);
        transform.position = originPos + comeInPosition;
        if (GetComponent<CanvasGroup>() != null)
            canvasGroup.alpha = 1;
        transform.DOMove(originPos, duration, false).SetEase(ease);
    }

    //open with direction, overlay
    public virtual void OpenWindow(Vector3 comeInPosition, float duration, bool overlay)
    {
        transform.DOComplete();
        this.gameObject.SetActive(true);
//        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        Vector3 originPos = transform.position;
        //Debug.Log("Original position on " + transform.name + " = " + originPos);
        transform.position = originPos + comeInPosition;
//        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (GetComponent<CanvasGroup>() != null)
            canvasGroup.alpha = 1;
        transform.DOMove(originPos, duration, false).SetEase(Ease.OutElastic).OnComplete(() =>
        {
            
        });
    }

    //open with direction, delay
    public virtual void OpenWindow(float delay, Vector3 comeInPosition, float duration)
    {
        transform.DOComplete();
        this.gameObject.SetActive(true);
//        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        Vector3 originPos = transform.position;
       // Debug.Log("Original position on " + transform.name + " = " + originPos);
        transform.position = originPos + comeInPosition;
        if (GetComponent<CanvasGroup>() != null)
            canvasGroup.alpha = 1;
     
        transform.DOMove(originPos, duration, false).SetEase(Ease.OutElastic).SetDelay(delay);
    }

    //mexican wave opening. arrrrrrriba!
    public virtual void OpenWindow(Vector3 comeInPosition, float duration, float delayBetweenObjectsForMexicanWaveEffect)
    {
        transform.DOComplete();
        this.gameObject.SetActive(true);
//        if (GetComponent<CanvasGroup>() != null)
//        {
//            this.GetComponent<CanvasGroup>().blocksRaycasts = true;
//        }
//        else
//        {
//            Debug.LogError("Thier is no canvas group on this object?");
//        }
        //Vector3 originPos = transform.position;
        //Debug.Log("Original position on " + transform.name + " = " + originPos);
        //transform.position = originPos + comeInPosition;
        if (GetComponent<CanvasGroup>() != null)
            canvasGroup.alpha = 1;
        //transform.DOMove(originPos, duration, false).SetEase(Ease.OutElastic);
        foreach (Transform child in GetComponentInChildren<Transform>())
        {
            OpenObject(child, comeInPosition, duration, delayBetweenObjectsForMexicanWaveEffect * child.GetSiblingIndex());
        }
    }

    //mexican wave opening. arrrrrrriba! + ease
    public virtual void OpenWindow(Vector3 comeInPosition, float duration, float delayBetweenObjectsForMexicanWaveEffect, Ease ease)
    {
        transform.DOComplete();
        this.gameObject.SetActive(true);
//        if (GetComponent<CanvasGroup>() != null)
//        {
//            this.GetComponent<CanvasGroup>().blocksRaycasts = true;
//        }
//        else
//        {
//            Debug.LogError("Thier is no canvas group on this object?");
//        }
        //Vector3 originPos = transform.position;
        //Debug.Log("Original position on " + transform.name + " = " + originPos);
        //transform.position = originPos + comeInPosition;
        if (GetComponent<CanvasGroup>() != null)
            canvasGroup.alpha = 1;
        //transform.DOMove(originPos, duration, false).SetEase(Ease.OutElastic);
        foreach (Transform child in GetComponentInChildren<Transform>())
        {
            OpenObject(child, comeInPosition, duration, delayBetweenObjectsForMexicanWaveEffect * child.GetSiblingIndex(), ease);
        }
    }

    // CLOSE ------------------------------------------------------------------------------------
    #region old close methods
    public virtual void CloseWindow()
    {
        canvasGroup.DOFade(0, 1).OnComplete(() => this.gameObject.SetActive(false));
    }

    public virtual void CloseWindow (float endValue, float duration)
    {
        canvasGroup.DOFade(endValue, duration).OnComplete(() => this.gameObject.SetActive(false));
    }

    public virtual void CloseWindow (float endValue, float duration, float delay)
    {
        canvasGroup.DOFade(endValue, duration).SetDelay(delay).OnComplete(() => this.gameObject.SetActive(false));
    }
#endregion

    public virtual void CloseWindow (Vector3 comeInPosition, float duration)
    {
        transform.DOComplete();
        Vector3 originPos = transform.position;
        transform.DOMove(originPos + comeInPosition, duration, false).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (GetComponent<CanvasGroup>() != null)
            {
                canvasGroup.alpha = 0;
            }
            transform.position = originPos;
            gameObject.SetActive(false);
        });
    }
    //close move, with overlay off
    public virtual void CloseWindow(Vector3 comeInPosition, float duration, bool overlay)
    {
        transform.DOComplete();
        Vector3 originPos = transform.position;
        transform.DOMove(originPos + comeInPosition, duration, false).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (GetComponent<CanvasGroup>() != null)
                canvasGroup.alpha = 0;
            transform.position = originPos;
//            GetComponent<CanvasGroup>().blocksRaycasts = false;
        });
    }
    //close, move with delay
    public virtual void CloseWindow(float delay, Vector3 comeInPosition, float duration)
    {
        transform.DOComplete();
        Vector3 originPos = transform.position;
        transform.DOMove(originPos + comeInPosition, duration, false).SetDelay(delay).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (GetComponent<CanvasGroup>() != null)
                canvasGroup.alpha = 0;
            transform.position = originPos;
            //GetComponent<CanvasGroup>().blocksRaycasts = false;
        });
    }

    //close, move, overlay off and deactivate
    public virtual void CloseWindow(GameObject killObject, Vector3 comeInPosition, float duration, bool overlay)
    {
        transform.DOComplete();
        Vector3 originPos = transform.position;
        transform.DOMove(originPos + comeInPosition, duration, false).SetEase(Ease.Linear).OnComplete(() =>
        {
            //if (GetComponent<CanvasGroup>() != null)
            //    canvasGroup.alpha = 0;
            transform.position = originPos;
            //GetComponent<CanvasGroup>().blocksRaycasts = false;
            killObject.SetActive(false);
            gameObject.SetActive(false);
        });
    }


    public virtual void closeObject(Transform thing, Vector3 comeInPosition, float duration)
    {
        transform.DOComplete();
        Vector3 originPos = thing.localPosition;
        thing.DOLocalMove(originPos + comeInPosition, duration, false).SetEase(Ease.Linear).OnComplete(() =>
        {
            thing.localPosition = originPos;
            if (thing.GetComponent<Image>())
            {
                //thing.GetComponent<Image>().color = new Color(255f,255f,255f,0f);
            }
            //thing.gameObject.SetActive(false);
        });
    }
}
