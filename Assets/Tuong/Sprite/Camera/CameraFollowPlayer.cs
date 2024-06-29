using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraFollowPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerTransform;

    [Header("Flip Rotation Start")]
    [SerializeField] private float _flipYRotationTime = 0.5f;

    private Coroutine turnCoroutine;

    private Player player;
    private bool isFacingRight;

    private void Awake()
    {
        player = _playerTransform.GetComponent<Player>();
        isFacingRight = player.IsFacingRight;
    }

    private void Update()
    {
        transform.position = _playerTransform.position;       
    }

    public void CallTurn()//gọi lại
    {
        turnCoroutine = StartCoroutine(FlipYLerf());
    }    

    private IEnumerator FlipYLerf()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotationAmount = DetermineEndRoation();
        float yRotation = 0f;
        //Thoi gian troi qua
        float elaspedTime = 0f;
        while(elaspedTime < _flipYRotationTime)
        {
            elaspedTime += Time.deltaTime;

            yRotation = Mathf.Lerp(startRotation, endRotationAmount, (elaspedTime / _flipYRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            yield return null;
        }
    }
    private float DetermineEndRoation()
    {
        isFacingRight = !isFacingRight;

        if(isFacingRight)
        {
            return 180f;
        }
        else
        {
            return 0f;
        }
    }

}
