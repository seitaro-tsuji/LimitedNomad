using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Crafting/Recipe Database")]
public class CraftRecipeDatabase : ScriptableObject
{
    public List<CraftRecipe> recipes;
}
