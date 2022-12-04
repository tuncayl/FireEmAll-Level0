using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class promote : Icommand
{
    types _types;
    TextType _textTypes;
    Vector3 _pos = Vector3.zero;
    string _key;
    public promote(types type, TextType textType, Vector3 pos, string key)
    {
        _types = type;
        _textTypes = textType;
        _pos = pos;
        _key = key;
    }
    public void Execute()
    {
       
        GameManager.Insantance.Pool.PInstantiate
            (_types, _pos);
        GameManager.Insantance.SetText(_textTypes);
        GameManager.Insantance.npc_Controller.NextState(_key);
    }
    public void Undo()
    {

    }
}
