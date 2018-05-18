using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IKController : NetworkBehaviour {

    Animator anim;

    Quaternion changeToThisRot;
    Vector3 CharacterAngle;

    float angleSpeed;

    private void Start()
    {
        anim = GetComponent<Animator>();
        changeToThisRot = anim.GetBoneTransform(HumanBodyBones.UpperChest).localRotation;
        CharacterAngle = new Vector3(changeToThisRot.x, changeToThisRot.y, changeToThisRot.z);
        angleSpeed = GetComponent<PlayerController>().angleSpeed;
    }


    private void OnAnimatorIK(int layerIndex)
    {
        if (!isLocalPlayer)
            return;

        anim.SetLookAtWeight(0f);

        float mouseY = Input.GetAxisRaw("Mouse Y");

        if (mouseY > 0f)
            CharacterAngle = Vector3.MoveTowards(CharacterAngle, DataScript.PlayerUpPose, angleSpeed * mouseY * Time.deltaTime);
        else if(mouseY < 0f)
            CharacterAngle = Vector3.MoveTowards(CharacterAngle, DataScript.PlayerDownPose, angleSpeed * -mouseY * Time.deltaTime);

        changeToThisRot = Quaternion.Euler(CharacterAngle);
        anim.SetBoneLocalRotation(HumanBodyBones.UpperChest, changeToThisRot);

    }
}
