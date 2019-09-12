
# SaveMerge 1.0
# By TKGP
# NexusMods: https://www.nexusmods.com/darksouls3/mods/409
# GitHub: https://github.com/JKAnderson/SaveMerge

A basic DS3 save editor built specifically for moving characters between save files and transferring other players' saves to your account.
Requires .NET 4.8 - Windows 10 users should already have this:
https://dotnet.microsoft.com/download/thank-you/net48


# Instructions

First, find the save file(s) you want to edit. The standard Steam location is as follows:
C:\Users\<your username>\AppData\Roaming\DarkSoulsIII\<encoded Steam ID>\DS30000.sl2
You can go straight to the Roaming folder by entering %appdata% in the File Explorer address bar.

Drag and drop the save file onto either the left or right panel in SaveMerge; you can open two different saves at once to move characters between them.
To move a character, click on it, copy it with Ctrl+C, then click on the desired slot and paste it with Ctrl+V. To delete a character, click on it and press the Delete key.

Saves with a different Steam ID than your own can't normally be loaded; to fix this you need to know your SteamID3.
You can check it by loading one of your own saves, or look it up on a website like this one: https://steamidfinder.com/
SteamID3s usually look like this: [U:1:12345678]
The part that's needed for the save is just the last number, in this case 12345678. Type or paste it into the text box labeled SteamID3.

After editing, use the Save buttons at the bottom of the window to save either of the opened files.
The original file will be backed up with the .bak extension, if you need to restore it.


# Bannability

To the best of my knowledge, moving characters and altering Steam IDs is perfectly safe on its own.
HOWEVER, many mules and other saves uploaded on Nexus have invalid data such as inconsistent soul memory, impossible key items, etc, which will get you banned regardless.
Use this tool at your own risk.


# Credits

Atvaark - save research  
Meowmaritus - app icon


# Libraries

SoulsFormats by Me
https://github.com/JKAnderson/SoulsFormats