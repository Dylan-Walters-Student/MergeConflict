# Merge Conflict
Merge Conflict is a fun, interactive, and casual game to play and learn the Meta headset's features such as passthrough and hand tracking.


# Game Design Choices 
Some game design choices may seem odd, but after some play testing and general understanding of games here is why I chose what I did!

### No Penalties!?
Why did I not add penalties to the game? I believe having penalties such as health for dropping balls would be a cool idea! I wanted to keep this game simple and that would not have complicated things. The reason I did not was because of bugs and issues I ran into with the players controllers and hands. I spent the majority of my time on this game trying to fix issues with these and did not want a bad user controller experience. In the end I did not have time to add these features, and this may be something I add later.

### One Menu at a Time 
Why would I only show one menu at a time? During the early fazes of development I made each of the menus (Settings, main play menu, about, scoreboard) Show up at the same time or all on one screen. This got cluttered very quickly and was not very user friendly. Having a simple menu specified to one topic is a nice way to clear the clutter.

### Time Sensitive-
After some playtesting not having a time limit could get boring as you could always win. 

### Settings-
Settings were added to allow the user to change the table height and game time. These two features are simple things that each player may want different. Changing the game time does affect the game but generally the goal is for the best player experience, giving them the option can make the experience better.

### Scoreboard-
A game is not complete without a purpose. Having a score allows people to play against their friends and compete to see who is the best.

### Balls, not blocks?
I chose balls instead of my original idea of blocks (or any other object) As having balls would cause larger gaps in the box making the user debate on where to place the objects.

# Object interactions 
There are a total of 7 types of balls. Only balls of the same size and color can merge together. I used Velocity tracking on each ball as I found it to be the smoothest type of ball movement available in unity.
