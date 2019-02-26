# WeaponsOfChoice[Beta]
A revamped version of WeaponsAndOutfits(https://github.com/ErnestUTN/Rimworld_WeaponsAndOutfits) . This time the weapons configuration is not part of the apparel outfit system. 

![alt text](https://github.com/ErnestUTN/WeaponsOfChoice/blob/Beta/About/Preview_1.png)
![alt text](https://github.com/ErnestUTN/WeaponsOfChoice/blob/Beta/About/Preview_2.png)

## Additional Features

You can now choose among your allowed filtered equippables some "priority" equippables. Click on the dropdown to the right of the filter in the dialog menu and choose your top 3 equippables you want your pawn to pay special attention to. You can leave one or two unchosen dropdowns , not problem, the rest of the filtered equippables will be treated as non-prioritized and will be similar to apparel in the outfits menu. [Read the Equip Logic for more info]

Added a Reset Priority button to set them all to none

## Equip logic
The following two propositions governs the logic behind the picking and equipping of the searched weapon:

1- Priority Weapons ignores filters quality and health % -> Means that not only will be the first weapons to search before attempting to search in the non-priority list, but also the pawn completely disregards quality and health % as said. 

3-  A pawn that has equipped a priority weapon in level 2 won't try to search for a weapon in level 1 Priority (lvl 1 > lvl 2 > lvl 3)

## Compatibility
So far, compatible with the same mods as WeaponsAndOutfits ( *Make sure to disable WeaponsAndOutfits if using this one* ) . 

During the week I will integrate BPC (Better Pawn Control) within it. (Actually Voult might need to adapt his code to make it work, so ask him if he can make a compatibility patch). 

Anyway in the Modified BPC assembly section here I have a workaround for you.

## Modified BPC assembly

There is a folder with a recompiled version of Voult's BPC project  with additional modifications so as to integrate it within the mod. Replace this assembly into the one  you currently have in BPC mod folder and your policies will also affect Weapon Presets . Remember to make a backup of the original assembly..
It works but needs testing anyway.

**IMPORTANT**: __Make sure that weapon of choice is enabled before better pawn control with this assembly; failing to do so will throw exceptions (This is probably because I didn't write any "fail-safe" code to be executed in case WeaponOfChoice wasn't present in the mod list). i.e : If you use this assembly, you must use this mod.__

## Known Issues

1- When opening the weapon dialog menu and attempting to create a new weapon preset it might take a couple of seconds before something happens. If you see this happen frequently add it as an issue in github, here.

2- You might need to save the loaded game with the mod and then reload it again to make any eventual error to stop happening.

## Install Instructions

1- Download from https://github.com/ErnestUTN/WeaponsOfChoice/releases the WeaponofChoice- Beta zip and decompress it into the Rimworld's mod folder.. make sure there aren't nested folders like Mods/WeaponofChoiceBeta/WeaponOfChoiceBeta.
2- If you have the BetterPawnControl mod and want the assign policy to affect the WeaponOfChoice column, replace the dll file located in the assemblies folder of the BetterPawnControl mod : /BetterPawnControl/Assemblies with dll provided in the prerelease
3- You should also download the languages zip and decompress it making it to overwrite the languages folder of the BetterPawnControl mod.
4- Make a Backup of these two just in case.
5- Play. If necessary, save a different savegame and reload it again if any error arises.
6- Feel free to create an issue if the above persists.


# Credits

As before, huge thanks to the discord rimworld modding group for their help in the modding process. If you are modding and you're stuck at it, pay a visit to them ;).

Ludeon Forum: https://ludeon.com/forums/index.php?topic=48068.msg453787#msg453787
