using UnityEngine;
[RequireComponent (typeof(SpriteRenderer))]
public class Unit : MonoBehaviour
{
    public UnitData data;
    [ExecuteInEditMode]
    private void OnValidate()
    {
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (data != null && GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().sprite = data.sprite;
        }
    }
}
