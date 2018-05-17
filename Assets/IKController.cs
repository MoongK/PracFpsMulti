using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IKController : NetworkBehaviour {

    Animator anim;
    public Transform lookTarget;
    float angleX = 0f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void OnAnimatorIK(int layerIndex)
    {
        if (!isLocalPlayer)
            return;

        anim.SetLookAtWeight(0f);

        float mouseX = Input.GetAxisRaw("Mouse X");
        angleX  += mouseX;

        if (Input.GetAxisRaw("Mouse Y") > 0f)
        {
            anim.SetBoneLocalRotation(HumanBodyBones.UpperChest, Quaternion.Lerp(anim.GetBoneTransform(HumanBodyBones.UpperChest).localRotation,    // From
                                                                             Quaternion.Euler(DataScript.PlayerUpPose), // To
                                                                             10f * Input.GetAxisRaw("Mouse Y") * Time.deltaTime));  // Speed
        }
        else
        {
            anim.SetBoneLocalRotation(HumanBodyBones.UpperChest, Quaternion.Lerp(anim.GetBoneTransform(HumanBodyBones.UpperChest).localRotation,    //From
                                                                             Quaternion.Euler(DataScript.PlayerDownPose),   // To
                                                                             10f * -Input.GetAxisRaw("Mouse Y") * Time.deltaTime));  // Speed
        }

        print("origin : " + anim.GetBoneTransform(HumanBodyBones.UpperChest).localRotation);
        print("up : " + Quaternion.Euler(DataScript.PlayerUpPose));
        print("down : " + Quaternion.Euler(DataScript.PlayerDownPose));

    }
}
