# Bastion Auto Splitter
By **DevilSquirrel**, valentinoIAN, and Vulajin

## What it Does

The autosplitter is currently capable of starting and ending your splits, resetting your splits whenever you start a new game, as well as splitting whenever a load screen begins. This splitter is currently unable to split upon using objects, so the split times will not match the classic Skyway split method.

## Options (In Layout Settings)

**Classic timing** - If this is checked, the splits will go off whenever you lose control at the end of each level (i.e. using a Skyway). If this is unchecked, splits will be triggered when the level load screen begins.  

**Start** - Toggles automatically starting splits.  
**End** - Toggles ending your splits upon input being disabled at the end of the game.  
**Reset** - Toggles automatically resetting.  
**Split** - Toggles splitting after completing each level.  

**Split Tazal I** - Toggles splitting when you load the map with the Battering Ram pickup on it.  
**Split upon picking up the Battering Ram** - Toggles splitting upon ... picking up the Battering Ram.  


## How to Install

- Go to the [releases](https://github.com/Synthian/LiveSplit.Bastion/releases) section in this repository.
- Download the latest LiveSplit.Bastion.dll and LiveSplit32.exe
- Place the LiveSplit32.exe inside your LiveSplit folder. i.e. C:\Program Files (x86)\LiveSplit
- You need to use the LiveSplit32.exe for the autosplitter to work properly with this game.
- Place the LiveSplit.Bastion.dll inside the Components folder. i.e. C:\Program Files (x86)\LiveSplit\Components\
- Open LiveSplit32.exe and edit your layout. Add the autosplitter to your layout. (Add -> Control -> Bastion Auto Splitter)
- Edit your layout to modify Auto Splitter settings. (Default settings work for most people)
- Play and enjoy!

## Special Thanks

I'd like to personally thank DevilSquirrel for the huge help with the auto splitter. Without his insane know how and hard work, this tool would not be available. Devil provided nearly all of the code, I merely just worked with him to fix it up. Vulajin was the one who found the variables in the code that we use to determine when to split, so he was also a huge help.