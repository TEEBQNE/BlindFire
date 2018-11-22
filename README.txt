# README

If you have any questions or any parts of the game do not work, please do not hesitate to contact me at: 
tjchapman1998@gmail.com

I am unable to push the builds to Github as they are over the maximum size for an individual commit. 
If you would like to build the game yourself, select File->Build Settings->Target Platform->Windows->Build and Run. 
Sorry for this inconvenience. 


## Thanks & Screen Resolution

First i'd like to say thanks for trying out Blind Fire. The game is intended to be played on two devices, one being a 
Windows machine with a monitor(1440x900) where the polarizing filter is stripped out and the other a Samsung s2 
tablet(2048x1536). As both of these are rather hard to come by, simply open the executable and select these dimensions 
for the window size when prompted. The reason for the sizes of the screens being fixed is because this game is intended 
to be played in a gaming arcade where the screen size would never change. **I would advice running one instance of the 
game in Unity with the resolution set to 2048x1536 as my computer would not allow a windowed built version of the game 
to be run at this screen resolution. It must be run at this resolution as the UI is fit to this specific size. The 
other instance can be run at 1440x900 with the executable.**

## Start Up

To start the game, open two instances of the game, one being of size 1440x900, which will be the host and one of 2048x1536 
(It might be easier to run this in the Unity project itself with these dimensions selected as this is a rather odd 
resolution to run on a computer) which will be the player connecting to this host. The host player must be played on a 
Windows machine as the library we used to read in Xbox 360 input is not cross platform. If at anytime we decided to 
release the game, we would make sure to replace the library with one that is cross platform. To properly start the game, 
first start the server on the 1440x900 instance then connect to it using the 2048x1536 instance.

## Controls

* *Top Down Shooter Player (1440x900)* *
This player can move with the joystick, shoot with the right trigger, dash with the left trigger and collect resource 
flowers with the bumpers. Their objective is to destroy all flying enemies and survive until the last boss spawns.

* *Tower Defense Player (2048x1536)* *
This player is suppose to use a tablet, so all controls are touch inputs. As you are most likely going to test this on 
a computer, all inputs are done using the mouse. They are able to build, place, sell, upgrade and retarget turrets using 
the UI. All interactions the player does are networked to the other player so all turret placement, upgrade, destruction, 
retargeting, etc. shows up on the other game. This player is also able to place bombs which damage all enemies including 
the out of lane enemies the other player is fighting and can heal/revive the other player if needed. **The controls can 
be a bit odd on a computer as they were intended to be for a tablet/other touch screen enabled devices. At times, buying 
turrets might take more than one click.**

## Example Gameplay Video
[Here](https://tylerchapman.me/Games) is a video on my website that shows some sped up gameplay as well as some commentary
by me. This game is rather complex and is not quite finished. We had around three to four weeks to complete this project 
and I was the only programmer that worked on it aside from the controller input. I would continue working on this project 
but I would redo most of the networking as I have just completed a formal Networking course instead of teaching myself 
Unity's UNet, so I would most likely replace and improve that code. I would also extend the gameplay so that the player's 
could switch between roles and fight harder and more unique bosses each successive fight until they reach the most powerful 
enemy which they would have to work together in some way to defeat. I would say for the time I had and being the only 
programmer, I made a decently fun yet relatively confusing game.
