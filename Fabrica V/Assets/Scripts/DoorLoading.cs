using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLoading : MonoBehaviour
{
    [SerializeField] private Transform camToEnable;
    [SerializeField] private Transform camToDisable;

    [SerializeField] private Transform playerOldPosition;
    [SerializeField] private Transform playerNewPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySoundEffect("Door");
            var playerTransform = collision.GetComponent<Transform>();
            var game2Char = collision.GetComponent<Game2_Character>();
            var movePointTransform = game2Char.movePoint;
            StartCoroutine(LoadingScene.Instance.FadeOutGame2(1f, 0.5f, game2Char));
            StartCoroutine(ChangePlayerPosition(1f, playerTransform, movePointTransform));
            StartCoroutine(LoadingScene.Instance.FadeInGame2(1f, 0.5f, game2Char));
        }
                
    }

    private IEnumerator ChangePlayerPosition(float time, Transform player, Transform movePoint)
    {
        yield return new WaitForSecondsRealtime(time);
        player.position = playerNewPosition.position;
        movePoint.position = playerNewPosition.position;
        camToDisable.gameObject.SetActive(false);
        camToEnable.gameObject.SetActive(true);
    }
}
