[Back](../../../../README.md)
# **Documentation**
## **Contents**
1) ### [Required Packages](#required-packages-1)
2) ### [How To Use](#how-to-use-1)
3) ### [Main Scripts](#main-scripts-1)
    1) #### [Crafting Book](#crafting-book-1)
4) ### [Scriptable Objects](#scriptable-objects-1)
    1) #### [Item List](#item-list-1)
    2) #### [Crafting Recipie](#crafting-recipie-1)
    3) #### [Items](#items-1)
5) ### [Other Scripts](#other-scripts-1)
    1) #### [Item Button](#set-item-button-1)
    2) #### [Set Item Button](#set-item-button-1)
    3) #### [Item Holder](#item-holder-1)
## **Required Packages**:

- ### Textmesh Pro
___
## **How To Use**:
#### [Back To Top](#documentation)
___
## **Main Scripts**:
#### [Back To Top](#documentation)
### **Crafting Book**:
When the game starts it will go through all the recipies in the [`Item List`](#so_itemlist) and add a button to the crafting book with all the appropiate infomation using the function `SetInfo`.
### Book Settings:
- Item List
    - This is a reference to the Scriptable Object which holds the data for recipies you can craft.
- Item Template
    - This is a reference to the button which will instanciate when the game starts, so that it displays the correct infomation.
- Book Pages
    - This is a reference to the pages, so it knows where to spawn the buttons for crafting different items.
___
### **Crafting Table**:
In `Update` it checks to see if the player is pressing E which is only to test the recipes, this can be switched out in your game to something else depeding on how your player interacts with the crafting. This calls the function `Craft` which then crafts the selected item.

In the function `Craft` it checks to see if you have selected a recipe if you haven't it will just not do anything. But if you have it will then wait for the time given from the recipe, and after that time, it will then craft the item requested, if you have all the correct items. 
### Crafting Table Settings:
- Placed Items Area
    - This is the area in which items need to be placed to allow you to craft, the size of this area can be increased and decreased. Defaults are 1,0.6,2.
- Item List
    - A reference to the item list of craftable items.
- Item Spawn Point
    - Where the item thats crafted will spawn.
- Items Mask
    - Will only detect items with this mask
___
## **Scriptable Objects**:
#### [Back To Top](#documentation)
### **SO_ItemList**:
This scriptable object holds the data for all of the items and item recipes that can be crafted.
- All Items
    - Holds all the possible items that can be in the game.
- All Recipes
    - This is all the recipes that are in the game, that can be used by the player.
- Learnt Recipes
    - This is just a placeholder for a idea what you could add. As you could add a way to learn recipes and only display those and let the player craft only learnt recipes.
### **SO_CraftingRecipe**:
This scriptable object holds the data for the item recipe.
- Item To Craft
    - This is the item that will be made when the crafting has taken place after the time given.
- Number Of Items Produced
    - This is how many of a certian item will be made.
- Items Needed
    - A list of all the reqired items needed to make the selected item.
- Time To Craft
    - How long it takes to craft this item. This could be linked up to UI to show how long it has left before finishing the crafting.

### **SO_Items**:
This holds the data for the items in the game.
- Item Name
    - This is just the name for the current item.
- Item Picture
    - The item picture that could show in the inventory or to show what item your about to craft.
- Type
    - The type of item about to craft, as displayed in [`Item Type`](#item-type)
- Is Cookable
    - This allows items to be cookable, for instance if you added a cooking system in and wanted certian items to be cooked in order to get other items.
- Item Prefab
    - The item prefab that you want to be linked to this specific item.
___
## **Other Scripts**:
#### [Back To Top](#documentation)
### **Set Item Button**:
This script is in charge of setting the correct infomation on the corisponding button, so it displays the right infomation, and when clicked sets the correct recipe to craft.
___
### **Item Type**:
This is just a class that holds a enum to determin the different types of items. This could be changed in the future to hold more or less depending on your use case.
___
### **ItemHolder**:
This just goes onto item prefabs to hold the Scriptable Objects data for the specific item. 
___