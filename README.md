# Bastion Auto Splitter
By **DevilSquirrel**, valentinoIAN, and Vulajin

## What it Does

The autosplitter is currently capable of starting and ending your splits, resetting your splits whenever you start a new game, as well as splitting whenever you use a Skyway or end a level by other means (Falling off Wharf District, for example). The splitter can also split at a few special locations (like picking up the Battering Ram). There is now an IL mode for official individual level timings.

## Options (In Layout Settings)

**IL Mode** - If this is checked, your (single) split will start when you enter a level from the Bastion, and will end when exiting. Resets are all manual. Turning IL mode on disables all other settings.  

**Start** - Toggles automatically starting splits.  
**Reset** - Toggles automatically resetting.  
**Split** - Toggles splitting after completing each level (and ending the game).  

**Split after Sole Regret** - Toggles splitting when you leave Rondy's Bar.  
**Split Tazal I** - Toggles splitting when you load the map with the Battering Ram pickup on it.  
**Split upon picking up the Battering Ram** - Toggles splitting upon picking up the Battering Ram.  


## How to Install

- Go to the [releases](https://github.com/Synthian/LiveSplit.Bastion/releases) section in this repository.
- Download the latest LiveSplit.Bastion.dll
- Place the LiveSplit.Bastion.dll inside the Components folder. i.e. C:\Program Files (x86)\LiveSplit\Components\
- Open LiveSplit and edit your layout. Add the autosplitter to your layout. (Add -> Control -> Bastion Auto Splitter)
- Edit your layout to modify Auto Splitter settings. (Default settings work for most people)
- Play and enjoy!

## Special Thanks

I'd like to personally thank DevilSquirrel for the huge help with the auto splitter. Without his insane know how and hard work, this tool would not be available. Devil provided nearly all of the code, I merely just worked with him to fix it up. Vulajin was the one who found the variables in the code that we use to determine when to split, so he was also a huge help.