using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject _currentPlayer;
    private bool isWin;

    private void FixedUpdate()
    {
        FollowPlayer(_currentPlayer);

        if (isWin) {
        gameObject.transform.rotation =
        Quaternion.Lerp(transform.rotation,
        Quaternion.Euler(45, transform.rotation.y, transform.rotation.z), 0.075f);
        }
    }

    public void FollowPlayer(GameObject currentPlayer)
    {
        
        gameObject.transform.position =
        Vector3.Lerp(transform.position,
        new Vector3(_currentPlayer.transform.position.x, transform.position.y, _currentPlayer.transform.position.z-8.5f), 0.075f);
        
    }

    public void FinishAngle()
    {
        isWin = true;
    }
}
