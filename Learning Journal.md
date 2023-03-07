# Learning Journal
### Problem:
Store infomation about items and what ones that you can craft at the given time.

### Solution:
Used a Scriptable object to store the items that can be crafted, using this i selected one out the list to craft, and check the relivent items are on the crafting table, if they are i then remove them from the list, and when the list is 0 then the item can be crafted, which then removes the items and spawns the crafted item.


### Problem:
Getting the book to generate buttons that set the onclick event. But got a error saying cannot convert 'void' to 'UnityEngine.Events.UnityAction'
Looked up the issue and its because the AddListen function was then passing the SelectCraftingRecipe to the listener which was void.

### Solution:
In the end i didnt use events, i just made the book a child of the crafting table due to the fact it would be all one system anyway and using events was complacating things.


### Problem:
I also encountered a issue with the Physics.OverlapCube where it wasnt detecting some objects that were on the table, after doing some debugging, i used gizmos to draw where the collider was, and to my surprise it was where it said it was. 

### Solution:
So out of ideas i tried using a Vector3 instead of a collider and its .size which worked.

### Problem:
Had to make it so it listed all the required items in a list, where it shpwed the amount you need and the item type.

### Solution
I used a dictionary to to check if there was a entry for that required item, if not it will add to the dictionary, but if there was already one in there it would increase it by 1. Which then meant i had a dictionary full of items and the amount they need, which i used a foreach loop to then convert them to a string and add them to a text box.

### Problem:
The mesh generator wasnt generating the meshes, when the callback was being called in the current thread.

### Solution:
It was because the thread wasnt being started.

### Problem:
Getting the different colours in a shader graph to correctly fade between 2 colours depending on the fade distance and normals.

### Problem:
The sun not rotating properly around, and snapping to certian position.

### Solution:
Using a animation curve and lerping between two Quaternion's using the animation curve, it now smoothly rotates around and changes its angle depending on the season.
