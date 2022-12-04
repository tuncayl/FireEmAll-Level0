using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Paper : MonoBehaviour
{
    public List<PaperFace> papers;
    public GameObject currentPaper;

    
    private Icommand hire;
    private Icommand fire;
    private Icommand dismis;
    private Icommand promote;

    private void Start()
    {
        /// <summary>I'm preparing my commands before the game starts(Peformance)</summary>
        hire = new hire(types.startfall, TextType.correct, new Vector3(-0.523999989f, 2.73000002f, -1.19200003f), "clap");
        dismis = new dismis(types.emojifx, TextType.bad, new Vector3(-0.319000006f, 1.57799995f, -1.5f), "disbelief");
        promote = new hire(types.startfall, TextType.correct, new Vector3(-0.523999989f, 2.73000002f, -1.19200003f), "clap");
        fire = new fire(types.angryfx, TextType.goodjob, new Vector3(-0.319000006f, 1.57799995f, -1.5f), "angry");
    }

    /// <summary>using enums to improve readability</summary>
    public void SelectPaper(States state)
    {
        int index = papers.FindIndex(x => x.state == state);
        
        papers[index].face.SetActive(true);
    }
    /// <summary>Run selected command</summary>
    public void ExecuteCommands(States state)
    {
        switch (state)
        {
            case States.hire:
                {
                    hire.Execute();
                    break;
                }
            case States.fire:
                {
                    fire.Execute();
                    break;
                }
            case States.dismis:
                {
                    dismis.Execute();
                    break;
                }
            case States.promote:
                {
                    promote.Execute();
                    break;
                }

            default:
                break;
        }
    }

}

/// <summary>our paper face here helps us to design the level easily</summary>
[System.Serializable]
public class PaperFace
{
    public States state;
    public GameObject face;

}
