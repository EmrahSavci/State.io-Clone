using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Play.Review;
public class RateUs : MonoBehaviour
{
    private ReviewManager _reviewManager;

    private PlayReviewInfo _playReviewInfo;
    // ...

    void Start()
    {
       
    }
    public void Rate()
    {
        StartCoroutine(RateUsFunc());
    }
    public IEnumerator RateUsFunc()
    {
        _reviewManager = new ReviewManager();

        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        //if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        //{
        //    Debug.Log("Hata yok1");
        //    // Log error. For example, using requestFlowOperation.Error.ToString().
        //    yield break;
        //}
        _playReviewInfo = requestFlowOperation.GetResult();


        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null; // Reset the object
        //if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        //{
        //    Debug.Log("Hata yok2");
        //    Log error. For example, using requestFlowOperation.Error.ToString().
        //    yield break;
        //}

    }
}
