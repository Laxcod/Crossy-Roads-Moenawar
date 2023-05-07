using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class PlayCountdown : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text tmptext;
    public UnityEvent OnStart;
    public UnityEvent OnEnd;

    private void Start() {
        OnStart.Invoke();
        var sequence = DOTween.Sequence();
        tmptext.transform.localScale = Vector3.zero;
        tmptext.text = "3";
        
        sequence.Append( tmptext.transform.DOScale(
            Vector3.one,
            1).OnComplete( () => 
            {
                tmptext.transform.localScale = Vector3.zero;
                tmptext.text = "2";
            }));

        sequence.Append( tmptext.transform.DOScale(
            Vector3.one,
            1).OnComplete( () => 
            {
                tmptext.transform.localScale = Vector3.zero;
                tmptext.text = "1";
            }));
            
        sequence.Append( tmptext.transform.DOScale(
            Vector3.one,
            1).OnComplete( () => 
            {
                tmptext.transform.localScale = Vector3.zero;
                tmptext.text = "GO!!!";
                OnEnd.Invoke();
            }));
    }
}
