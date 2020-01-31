using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Characters/Expressions")]
public class Expressions : ScriptableObject
{
    public string characterName;
    public List<string> expressionsNames;
    public List<Sprite> expressionImages;
    public Dictionary<string, Sprite> expressions;
    private void OnEnable()
    {
        Dictionary<string, Sprite> expr = new Dictionary<string, Sprite>();
        for (int i = 0; i < expressionsNames.Count; i++)
        {
            expr.Add(expressionsNames[i], expressionImages[i]);
        }
        expressions = expr;
    }
}
