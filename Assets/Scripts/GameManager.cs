using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    /// <summary>define singelton</summary>
    public static GameManager Insantance;

    /// <summary>Level Path</summary>
    [Header("Path referance")]
    public PathCreator Path;


    /// <summary>Npc Control </summary>
    [Header("Npc Control referance")]
    public NpcController npc_Controller;
    /// <summary>Object Pooling </summary>
    [Header("Object Pooling")]
    public Objectpool Pool;

    /// <summary>Door Referance </summary>
    [Header("Npc Control referance")]
    public Animation door;
    /// <summary>Paper objcet</summary>
    [Header("Paper referance")]
    public Paper paper;
    [Header("Serizaled Text Class ")]
    /// <summary>text LİST</summary>
    [SerializeField] List<TextControl> textControls;
    [SerializeField] TextMeshProUGUI Correctbad;
    [SerializeField] GameObject TextObject;

    /// <summary>level Count</summary>
    public static int Level = default;

    /// <summary>Vibration Propety</summary>
    [Header("Vibration setting")]
    public Image VibrationImage;
    public bool VibraitonB = true;
    public bool VibrationProperty
    {
        get => VibraitonB;
        set
        {
            VibraitonB = value;
            if (VibraitonB) VibrationImage.color = new Color(255, 255, 255, 1f);
            else VibrationImage.color = new Color(255, 255,255, 0.4f);
        }
    }
    ////////////////
    private void Awake()
    {
        Application.targetFrameRate=60;
        if (Insantance == null) Insantance = this;
        else if (Insantance != null) Destroy(this);
    }

    private void Start()
    {
        
        door.Play();
        npc_Controller.NextState();
    }

    /// <summary>String to Hash ,Performance</summary>
    public void AnimStringtoHash(List<string> list, Dictionary<string, int> animHash)
    {
        foreach (string i in list)
        {
            animHash.Add(i, Animator.StringToHash(i));
        }
    }
    /// <summary>using enums to improve readability</summary>
    public void SetText(TextType Type)
    {
        Correctbad.transform.localPosition = Vector3.zero;
        int index = textControls.FindIndex(x => x.type == Type);
        Correctbad.color = textControls[index].color;
        Correctbad.text = textControls[index].text;
        Correctbad.transform.DOLocalMove(new Vector3(0, 300, 0), 1.4f).SetEase(Ease.InQuad).OnComplete(
            () => Correctbad.text = ""
        );

    }
    /// <summary>Finish Method</summary>
    public void Finish()
    {
        Pool.PInstantiate(types.moneyfall, new Vector3(-0.540000021f, 2.53999996f, -0.819999993f));
        SetText(TextType.hrtask);
        if (VibrationProperty)
        {
            Vibration.Init();
            Vibration.Vibrate();
        }
    }
    /// <summary>Vibration onof</summary>
    public void VibrationOnOf() => VibrationProperty = !VibrationProperty;

}

[System.Serializable]
public class TextControl
{
    public TextType type;
    public string text;
    public Color color;
}
