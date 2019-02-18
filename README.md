# WeaponsOfChoice[Beta]
A revamped version of WeaponsAndOutfits(https://github.com/ErnestUTN/Rimworld_WeaponsAndOutfits) . This time the weapons configuration is not part of the apparel outfit system. 

![alt text](https://github.com/ErnestUTN/WeaponsOfChoice/blob/Beta/About/Preview_1.png)
![alt text](https://github.com/ErnestUTN/WeaponsOfChoice/blob/Beta/About/Preview_2.png)

## Additional Features

You can now choose among your allowed filtered equippables some "priority" equippables. Click on the dropdown to the right of the filter in the dialog menu and choose your top 3 equippables you want your pawn to pay special attention to. You can leave one or two unchosen dropdowns , not problem, the rest of the filtered equippables will be treated as non-prioritized and will be similar to apparel in the outfits menu. [Read the Equip Logic for more info]

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

## Known Issues

1- When opening the weapon dialog menu and attempting to create a new weapon preset it might take a couple of seconds before something happens. If you see this happen frequently add it as an issue in github, here.

# Credits

As before, huge thanks to the discord rimworld modding group for their help in the modding process. If you are modding and you're stuck at it, pay a visit to them ;).

Ludeon Forum: https://ludeon.com/forums/index.php?topic=48068.msg453787#msg453787
