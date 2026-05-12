using System.Collections.Generic;
using UnityEngine;
//Scriptableobject
[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipes/Recipe")]
public class Recipe : ScriptableObject
{
    //reseptin nimi
    public string recipeName;
    //lista ainesosista 
    public List<IngredientType> ingredients;

}