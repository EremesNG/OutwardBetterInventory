# Better Inventory

## Features
Adds the following features to the inventory menu:
- 'Salvage' option in the item context menu (right click): This is functionally equivalent to putting the item into the crafting menu and attempting to "craft" it. 
The result is usually iron scraps, linen, palladium, etc.

### What to do when the mod doesn't work as intended?
There is a slight chance that the mod may break (and break your game) in various exciting ways.
If anything untoward happens (or nothing happens, which is also a problem), please do **one** of the following:
 - Open a [GitHub issue](https://github.com/Faeryn/OutwardBetterInventory/issues/new)
 - Report it to me on the [Outward Modding Community](https://discord.gg/zKyfGmy7TR) Discord

I'd love if you also attached a list of mods you are using, and the log from `Outward\Outward_Defed\output_log.txt`.

 
## Planned features:
- Drag and drop items and skills onto quickslots to assign
- Skill menu filters (offensive, sigils, boons, mana spells, stamina skills, etc)
- Custom skill menu filters (based on user specified criteria)
- Optional: Button to cast all spells in a category (useful for boons, probably better with custom filters)
- Optional: Send item to nearby stash (only when in house or in town with house)
- Optional: Send item to co-op partner
- Localization
- Some more salvage recipes (bows for example)

There is a high chance that I will put the optional (read: potentially "cheaty") improvements into another mod.

## Changelog

### v1.0.1
- Salvage option now doesn't show up for items that (definitely) cannot be salvaged. Note that some items with 'Salvage' option might still fail to salvage (filtering them out is work in progress).

### v1.0.0
- 'Salvage' option in the item context menu