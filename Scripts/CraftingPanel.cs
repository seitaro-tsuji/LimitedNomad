using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftingPanel : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Button closeButton;  //閉じるボタン
    [SerializeField] private GameObject buttons;  //アイテム、ポーズボタンなど
    [SerializeField] private CraftRecipeDatabase recipeDatabase;    //レシピ一覧
    [SerializeField] private RecipeRow recipeRowPrefab;  //レシピのprefab
    [SerializeField] private Transform recipeListContent; //レシピ一覧の入れ物
    private List<CraftRecipe> recipes;  //表示するレシピ一覧

    private PlayerController _controller;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);//最初は非表示
        closeButton.onClick.AddListener(Close);

        _controller = player.GetComponent<PlayerController>();
        recipes = recipeDatabase.recipes;
    }

    public void ShowRecipes()
    {
        //中身を全て消す
        foreach(Transform child in recipeListContent)
        {
            Destroy(child.gameObject);
        }

        //レシピを生成
        foreach(var recipeRow in recipes)
        {
            RecipeRow row = Instantiate(recipeRowPrefab, recipeListContent);
            row.Setup(recipeRow);
        }
    }

    public void DetectPlayer() //プレイヤーを検知したときopen
    {
        gameObject.SetActive(true);  //パネル表示
        buttons.SetActive(false);  //item,pauseボタンを無効にする
        _controller.Set_IsControllable(false);  //操作を無効化する
        ShowRecipes();
    }

    private void Close()
    {
        gameObject.SetActive(false);  //パネル消す
        buttons.SetActive(true);  //item,pauseボタンを有効にする
        _controller.Set_IsControllable(true);  //操作を有効化する
    }
}
