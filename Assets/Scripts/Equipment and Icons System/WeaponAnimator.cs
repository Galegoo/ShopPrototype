using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    private SpriteRenderer weaponSpriteRenderer;
    private Transform weaponTransform;
    private Vector3 weaponInitialPosition;
    private Vector3 weaponInitialScale;
    [SerializeField] private Vector3 leftPosition;
    [SerializeField] private Vector3 rightPosition;
    [SerializeField] private Vector3 downPosition;
    [SerializeField] private float SideScaleX = 1f;
    [SerializeField] private int orderInLayerUp = 1;
    [SerializeField] private int orderInLayerDown = -1;
    [SerializeField] private int orderInLayerSide = 1;

    private void Start()
    {
        weaponSpriteRenderer = GetComponent<SpriteRenderer>();
        weaponTransform = GetComponent<Transform>();
        weaponInitialPosition = weaponTransform.localPosition;
        weaponInitialScale = weaponTransform.localScale;
    }

    private void Update()
    {
        Vector2 moveDirection = new Vector2(PlayerController.CheckInputX(), PlayerController.CheckInputY());

        if (moveDirection.y > 0) // Moving up
        {
            SetWeaponLayer(orderInLayerUp);
            weaponSpriteRenderer.flipX = false;
            ResetWeapon();
        }
        else if (moveDirection.y < 0) // Moving down
        {
            SetWeaponLayer(orderInLayerDown);
            weaponSpriteRenderer.flipX = true;
            SetWeaponDownPosition();
            //ResetWeapon();
        }
        else if (moveDirection.x < 0) // Moving left
        {
            SetWeaponLayer(orderInLayerSide);
            SetWeaponLeftPosition();
        }
        else if(moveDirection.x > 0)// Moving right
        {
            SetWeaponLayer(orderInLayerSide);
            SetWeaponRightPosition();
        }
    }

    private void SetWeaponLayer(int order)
    {
        weaponSpriteRenderer.sortingOrder = order;
    }

    private void ResetWeapon()
    {
        weaponTransform.localScale = weaponInitialScale;
        weaponTransform.localPosition = weaponInitialPosition;
    }

    private void SetWeaponLeftPosition()
    {
        weaponTransform.localPosition = leftPosition;
        weaponTransform.localScale = new Vector3(SideScaleX, weaponTransform.localScale.y, weaponTransform.localScale.z);
    }
    private void SetWeaponRightPosition()
    {
        weaponTransform.localPosition = rightPosition;
        weaponTransform.localScale = new Vector3(SideScaleX, weaponTransform.localScale.y, weaponTransform.localScale.z);
    }
    private void SetWeaponDownPosition()
    {
        weaponTransform.localPosition = downPosition;
        weaponTransform.localScale = weaponInitialScale;
    }
}
