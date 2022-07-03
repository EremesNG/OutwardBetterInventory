# Better Inventory

## Features
Item context menu (right click) additons:
- ``Salvage``: This is functionally equivalent to putting the item into the crafting menu and attempting to "craft" it. 
The result is usually iron scraps, linen, palladium, etc.
- ``Send to Stash``: Sends the item to your stash. This option works only if you are in a town with an active stash (so either a free one or you own a house there).
- ``Send to Host's Stash``: Sends the item to the host's stash (ie. the player whose world you are in). This option works only if you are not the host in a multiplayer session.
- ``Send to [Player name]``: Sends the item to the other player. This option works only if you are in multiplayer and have one or more co-op partners connected.

## Configuration
It is strongly recommended to use [Outward Config Manager](https://outward.thunderstore.io/package/Mefino/Outward_Config_Manager/).
- **Send to Stash**: Enables or disables the Send to Stash action. Default is off.
- **Send to Host Stash**: Enables or disables the Send to Host's Stash action. Default is off.
- **Send to Other Player**: Enables or disables the Send to Other Player action. Default is off.
- **Send to Other Player max distance**: Sets the maximum distance for the Send to Other Player action (in metres). Default is 10.

## Planned features:
- Drag and drop items and skills onto quickslots to assign
- Skill menu filters (offensive, sigils, boons, mana spells, stamina skills, etc)
- Custom skill menu filters (based on user specified criteria)
- Optional: Button to cast all spells in a category (useful for boons, probably better with custom filters)
- Localization
- Some more salvage recipes (bows for example)
- Filter items and skills by name
- Compatibility with any mods that add or change stashes

## What to do when the mod doesn't work as intended?
There is a slight chance that the mod may break (and break your game) in various exciting ways.
If anything untoward happens (or nothing happens, which is also a problem), please do **one** of the following:
- Open a [GitHub issue](https://github.com/Faeryn/OutwardBetterInventory/issues/new)
- Report it to me on the [Outward Modding Community](https://discord.gg/zKyfGmy7TR) Discord

I'd love if you also attached a list of mods you are using, and the log from `Outward\Outward_Defed\output_log.txt`.

## Special thanks
- **sinai** for Quest Event data
- **Nielsjuh** for helping me test the multiplayer bits

## Changelog
### v1.1.2
- Support for more than 2 players (for example Raid Mode mod)

### v1.1.1
- 'Send to Host's Stash' option in the item context menu

### v1.1.0
- 'Send to Stash' option in the item context menu
- 'Send to Other Player' option in the item context menu

### v1.0.1
- Salvage option now doesn't show up for items that (definitely) cannot be salvaged. Note that some items with 'Salvage' option might still fail to salvage (filtering them out is work in progress).

### v1.0.0
- 'Salvage' option in the item context menu