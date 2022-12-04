using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;


public class NpcController : MonoBehaviour
{
    [Header("NPC LİST")]
    public List<GameObject> Npcs;

    [Header("Anim List")]
    public List<string> AnimNames;
    Dictionary<string, int> AnimHash = new Dictionary<string, int>();
    /// <summary>Current Animator</summary>
    [HideInInspector] public Animator currentAnimator;

    /// <summary>UNİTY EVENT </summary>
    public UnityEvent<States, int> StateEvent { get { if (state == null) state = new UnityEvent<States, int>(); return state; } }
    private UnityEvent<States, int> state;


    /// <summary>Current State</summary>
    public States currentState;


    private void Start()
    {
        GameManager.Insantance.AnimStringtoHash(AnimNames, AnimHash);
        StateEvent.AddListener(EndPath);

    }
    #region PathEvent
    /// <summary>the method that will work when path end</summary>
    public void EndPath(States state, int defaltsit)
    {
        GameManager.Insantance.door.Play("close");
        currentState = state;
        StartCoroutine(NpcTrack(state, defaltsit));
    }
    IEnumerator NpcTrack(States state, int sitanim)
    {
        currentAnimator.Play(AnimHash["StandToSit"]);
        yield return new WaitForSeconds(currentAnimator.GetCurrentAnimatorStateInfo(0).length + 1.2f);
        currentAnimator.Play(sitanim);
        GameManager.Insantance.paper.SelectPaper(state);
        yield return null;
    }
    #endregion



    #region NextSte
    /// <summary>Our method of passing to the next character when we aredone with our character</summary>
    public void NextState(string key="")
    {
        StartCoroutine(NpcNext(key));
    }
    IEnumerator NpcNext(string key="")
    {
        if (Npcs.Count > 0)
        {
            if (currentAnimator != null)
            {
                Transform npc = currentAnimator.transform;
                Npcs.RemoveAt(0);
                currentAnimator.Play(AnimHash[key]);
                yield return new WaitForSeconds(3f);
                npc.transform.DOMove(new Vector3(2.59200001f, 0.5f, -2.92499995f), 1.5f).SetEase(Ease.Linear).
                OnComplete(
                    () => Destroy(npc.gameObject)
                );
            }

            if (Npcs.Count != 0)
            {
                GameManager.Insantance.door.Play();
                Instantiate(Npcs[0], Vector3.zero, Quaternion.identity);
            }
            else
            {
                GameManager.Insantance.Finish();
            }

        }

    }
    #endregion



}

