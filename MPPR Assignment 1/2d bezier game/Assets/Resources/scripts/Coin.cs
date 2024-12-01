using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public CoinCounter CoinCounter;
    public Transform target;
    private float duration = 1f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Destroy(gameObject);
            CoinCounter.coins += 1;
            Debug.Log("coins:" + CoinCounter.coins);
            StartCoroutine(MoveToTarget());
        }
    }
    IEnumerator MoveToTarget()
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = target.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            t = Mathf.Clamp01(t);

            Vector3 newPos = Interpolation.Lerp(startPos, targetPos, t, Interpolation.EasingType.EaseIn);
            transform.position = newPos;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target.position;
        Destroy(gameObject);
    }

}
