using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCardMovement : MonoBehaviour
{
    public GameObject FollowObject;
    public float moveSpeed;
    private Vector3 targetPos;
    private Vector3 targetScale;
    private Quaternion targetRotate;

    public GameObject DiscardLocation;
    public float DiscardScale;
    public float DiscardMoveSpeed;
    public float timeUntilDestroy;
    private float DestroyTimer;

    private void Start()
    {
        DestroyTimer = 0f;
        DiscardLocation = GameObject.Find("Discard Image");
    }

    public void SetDummy(GameObject dummy)
    {
        FollowObject = dummy;
    }

    private void Update()
    {
        if (FollowObject != null)
        {
            targetPos = new Vector3(FollowObject.transform.position.x, FollowObject.transform.position.y, FollowObject.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, targetPos, moveSpeed);

            targetScale = new Vector3(FollowObject.transform.localScale.x, FollowObject.transform.localScale.y, FollowObject.transform.localScale.z);
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, targetScale, moveSpeed);
        }
        else
        {
            targetPos = new Vector3(DiscardLocation.transform.position.x, DiscardLocation.transform.position.y, DiscardLocation.transform.position.z);
            targetScale = new Vector3(DiscardScale, DiscardScale, 1.0f);
            targetRotate = Quaternion.Euler(new Vector3(0.0f, 0.0f, -16.0f));
            this.transform.position = Vector3.Lerp(this.transform.position, targetPos, DiscardMoveSpeed);
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, targetScale, DiscardMoveSpeed);
            this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, targetRotate, DiscardMoveSpeed);

            DestroyTimer += Time.deltaTime;
            if (DestroyTimer >= timeUntilDestroy)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
