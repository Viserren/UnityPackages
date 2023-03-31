[Back](../../../../README.md)
# **Documentation**
## **Contents**
1) ### [Required Packages](#required-packages-1)
2) ### [How To Use](#how-to-use-1)
3) ### [Main Scripts](#main-scripts-1)
4) ### [Scriptable Objects](#scriptable-objects-1)
5) ### [Other Scripts](#other-scripts-1)
## **Required Packages**:

- ### N/A
___
## **How To Use**:
#### [Back To Top](#documentation)
You shouldn't need to touch any of the core scripts in this package, nor the Scriptable object scripts, as this can cause errors in the custom inspector.

### **ATTENTION When implementing into your own project make sure the "Data" folder is in the "Item-Weapon System/" directory and contains "Item", "Melee" and "Gun" folders as this is where it will create all of the different items and weapons.**
<br>

1) At the top of the screen in the bar, there should be a Items button, clicking that will bring up a dropdown for the Item Database. Appon clicking that, given that you have followed the above you shouldn't get any errors.
2) When you click Add (Item, Melee, Gun) it should add a new item to the list below, and when you click on it. There should be a info page appear on the right, which shows all the details of that object. Then you have the options to save and delete that current item.
3) once you have filled out the detials, if you go back to the folders, and go into "Data/(the item type you made) you should see a scriptable object, and when you click it, you should see in the inspector all of the detials for that item, layout nicely.
___