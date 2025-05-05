using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Necesario para trabajar con el New Input System

public class GunSystem : MonoBehaviour
{
    #region General Variables
    [Header("General References")]
    [SerializeField] Camera fpsCam; //Referencia a la c�mara desde cuyo centro se dispara (Raycast desde centro c�mara)
    [SerializeField] RaycastHit hit; //Referencia a la info de impacto de los disparos (informaci�n de impacto Raycast)
    [SerializeField] LayerMask interactableLayer; //Referencia a la Layer que puede impactar el disparo
    [SerializeField] AudioSource weaponSound; //Referencia al AudioSource del arma

    [Header("Interactable Stats")]
    public float range; //Alcance de disparo (longitud del Raycast)
    public float shootingCooldown; //Tiempo de enfriamiento del arma
    public int damage; //Da�o del arma

    [Header("State Bools")]
    [SerializeField] bool shooting; //Verdadero cuando ESTAMOS DISPARANDO
    [SerializeField] bool canShoot; //Verdadero cuando PODEMOS DISPARAR

    [Header("Feedback & Graphics")]
    [SerializeField] GameObject muzzleFlash; //Objeto feedback del fogonazo
    [SerializeField] bool attackIsSounding; //Si es verdadero, el sonido de disparo ya suena, por lo que no hay que repetirlo
    #endregion

    private void Awake()
    {
        weaponSound = GetComponent<AudioSource>();
        attackIsSounding = false;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Inputs()
    {
        
    }

    void Shoot()
    {
        
    }

    void ResetShoot()
    {
        
    }

    //M�todo usado en el New Input System
    public void OnShoot(InputAction.CallbackContext context)
    {
        
    }
}
