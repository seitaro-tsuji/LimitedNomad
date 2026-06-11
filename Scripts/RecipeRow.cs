using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeRow : MonoBehaviour
{
    [SerializeField] private Transform materialsContainer;
    [SerializeField] private MaterialIcon materialPrefab;
    [SerializeField] private CraftButton craftButton;

    public void Setup(CraftRecipe recipe)
    {
        //中身を全て消す
        foreach(Transform child in materialsContainer)
        {
            Destroy(child.gameObject);
        }

        //クラフトボタン作成
        craftButton.Setup(recipe.result);

        //素材分生成
        foreach(var material in recipe.materials)
        {
            Debug.Log($"Material: {material.type} x {material.amount}");

            var icon = Instantiate(materialPrefab, materialsContainer);
            icon.Setup(material);
        }
    }
}
