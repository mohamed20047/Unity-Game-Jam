# Larchrow 16x16 Effects & Projectiles (31 effects, 63 animations, 270 frames)

Animated pixel-art VFX for top-down and side-view games: projectiles in 8 directions, impacts and explosions,
melee slashes, magic, status loops and small ambient effects. Frames are 16x16 (two effects are 32x32, see below),
use only the 32-colour Larchrow palette (the same as our tilesets, *RPG Item Icons* and *16x16 Characters & Monsters*),
have a 1px dark outline (#120e1a) on the outside and hard pixels (alpha is only 0 or 255).

## Effects
| Group | Effects | Animations (frames, fps) |
|---|---|---|
| Projectiles (7) | arrow, fireball, ice_shard, magic_bolt | fly_right, fly_down_right, fly_down, fly_down_left, fly_left, fly_up_left, fly_up, fly_up_right (arrow 2 frames 8 fps; fireball 4/12; ice_shard 4/10; magic_bolt 4/12), loop |
| | rock, energy_orb, throwing_knife | fly: rock 4/12 (spins), energy_orb 4/10 (pulses), throwing_knife 8/16 (spins), loop |
| Impacts (8) | hit_spark, fire_burst, ice_shatter, magic_burst | burst: 4/14, 5/12, 5/12, 5/12, play once |
| | explosion_small, explosion_medium, **explosion_large (32x32)** | explode: 6, 7 and 8 frames at 12 fps, play once |
| | lightning_strike | strike: 5/14, play once |
| Melee (2) | slash | slash_right, slash_left, slash_up, slash_down: 4/18, play once |
| | claw_swipe | swipe: 4/14, play once |
| Magic (5) | heal_sparkle, magic_aura, shield_bubble | loop: 6/8, 4/8, 4/8 |
| | teleport | out (vanish) and in (appear): 6/12 each, play once; `in` = `out` reversed |
| | **level_up (32x32)** | burst: 8/12, play once |
| Status (4) | status_poison, status_burn, status_freeze, status_stun | loop: 4 frames at 6, 10, 6, 8 fps |
| Ambient (5) | smoke_puff, dust_step, water_splash | puff 6/10, puff 4/12, splash 5/12, play once |
| | pickup_sparkle, coin_glint | loop: 6/10 each |

**32x32 effects:** `explosion_large` and `level_up` use 32x32 frames (they are in their own sheets and in
`larchrow_fx_combined_32x32.png`). Centre them on a 16x16 character: offset (-8, -8).
**Directions:** the up/left/down projectile frames are exact 90-degree turns of the right-facing frames, diagonals are turns of
up_right; slash_left mirrors slash_right. Ring effects (aura, shield) keep a transparent centre so the character shows through.

## Contents
```
PNG/<category>/<effect>/<effect>_<animation>.png      one horizontal strip per animation (16px or 32px per frame)
Spritesheet/per_effect/<effect>_16x16.png             one sheet per effect: 1 row per animation, 8 columns (128px wide)
Spritesheet/per_effect/<effect>_48x48_3x.png          same, scaled 3x (32x32 effects: _32x32.png / _96x96_3x.png)
Spritesheet/larchrow_fx_combined_16x16.png            all 16x16 effects in one sheet (128x976)
Spritesheet/larchrow_fx_combined_32x32.png            the two 32x32 effects (256x64)
Spritesheet/larchrow_fx_combined_*_3x.png             same, 3x
animation_index.csv / animation_index.json            every animation: frame size, frames, fps, loop, sheet row, pixel rects
Godot4/larchrow_fx/                                   sheets + one SpriteFrames .tres per effect + demo scene
Palette/                                              the 32 colours as .png, .gpl (GIMP/Aseprite) and .hex (Lospec)
Previews/                                             labelled overview (3x), one GIF per effect, showcase.gif, battle.gif
screenshots/, cover_*.png                             store images (characters and maps are from our other packs, not included)
qa-report.json                                        automatic checks (see below)
```
Empty cells in a sheet (e.g. columns 5-8 of a 4-frame row) are fully transparent.

## Godot 4 (tested with Godot 4.3)
1. Copy `Godot4/larchrow_fx/` into your project so it ends up at `res://larchrow_fx/` (the .tres files use that path).
2. For crisp pixels set *Project Settings > Rendering > Textures > Canvas Textures > Default Texture Filter* to **Nearest**.
3. Add an `AnimatedSprite2D`, set *Sprite Frames* to `res://larchrow_fx/spriteframes/<effect>.tres`, pick an animation.
   Looping effects have loop on; one-shots have loop off, so connect `animation_finished` to `queue_free()`.
4. `demo.tscn` shows all 31 effects (one-shots replay) plus a fireball that flies across and ends in a large explosion (`demo_fx.gd`).

## Other engines
Use the per-effect sheets with a 16x16 grid (32x32 for the two large effects; no spacing, no margin) and
`animation_index.csv` for rows, or the PNG strips. Unity: Sprite Mode Multiple, Grid By Cell Size, Filter Point,
Compression None. Scale only by whole numbers with nearest-neighbour filtering.

## Quality checks (qa-report.json)
Palette identical to our other pixel packs; only palette colours; no semi-transparent pixels; frame sizes as documented;
every frame not empty and with a closed 1px outer outline; nothing touches the frame edge; frame counts consistent; no
static animations; direction frames are exact rotations/mirrors; strips, sheets, combined sheets, 3x copies, index and
Godot files match; all SpriteFrames and the demo scene load in Godot 4.3.

## Origin
Every frame is drawn by Larchrow's own Python code (numpy + Pillow): hand-written pixel templates, shape rules
(flames, rings, radial explosion heat bands, cloud puffs) on the shared palette, then reviewed by eye frame by frame.
No generative AI, no third-party assets, nothing traced or copied from existing games.

## License
See LICENSE.txt: royalty-free for commercial and non-commercial use; do not resell or redistribute the effects as-is.
