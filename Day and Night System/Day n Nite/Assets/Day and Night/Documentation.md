# **Documentation**

## **Required Packages**:

- ### Textmesh Pro
___
## **How To Use**:
___
## **Main Scripts**:
### **Time Manager**:
### Date Settings:
- Date in Month
    - This controls what day you are current in. Given the current season.
    - If you set it to 18, season to 2 and year to 3, the date would be 18th Summer 3
- Season
    - This controls what season you are currently in.
        1) Spring
        2) Summer
        3) Autumn
        4) Winter
- Year
    - This controls what year of the game you are currently in.
- Hours
    - Current hour in the day.
- Minutes
    - Current minute of the hour.
- Seconds
    - Current second of the minute.

### Tick Settings
- Advance Time Increment (ATI)
    - This is how fast the day goes by. Default for this is 4.24 which means each day would last about 17 minutes. But this can be changed if you want so the can can be longer or shorter.
### Events
- #### On Date Changed
    - This will be called every `0.05f` when `Tick()` is called when time is advancing. You can hookup anything to this event that takes in a `DateTime` variable.

- You could for instance create a event that is called when the season changes you could add an Event into the `TimeManager` Script under the `[Header("Events")]` thats called `OnSeasonChanged` that takes in a `int` which you can then invoke in `AdvanceSeason()`. This could also be done for all of the different functions in `Time Advacement` where each of them fires a different event.
___
### **Clock Manager**
This script is called when the [`OnDateChanged`](#on-date-changed) is called and then updates the UI and lights accordingly.
- Date
    - This is where you would put your text to display the current Date.
- Time
    - This is where you would put your text to display the current Time.
- Season
    - This is where you would put your text to display the current Season.
- Week
    - This is where you would put your text to display the current Week.
- Sun Light
    - This is a reference to the sun in the world, so it can move and change intensity depending on the position and time of day.
- Day Intensity
    - How bright the day would be.
- Night Intensity
    - How bright the night would be.
- Day Night Curve
    - How soon/late the sun will rise/set each day.
- Sun Height Curve
    - How high the sun is per season.
        - Spring 60
        - Summer 80
        - Autumn 40
        - Winter 20