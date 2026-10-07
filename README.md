# Ben 10 Game

A Windows desktop game written in C# (Windows Forms, .NET Framework) where you play as Ben Tennyson.
The characters, enemies and animations were all made step by step by hand. When Ben finds the
Omnitrix watch, he transforms into Four Arms.

## ▶ Download and play

**[Download Ben10-Game.zip](https://github.com/nosaiba-waleed/ben10-game/raw/main/download/Ben10-Game.zip)**

1. Unzip it and open the `Ben10-Game` folder.
2. Double-click `ben10.exe`.
3. If Windows shows a blue SmartScreen warning, click **More info → Run anyway**.

Keep all the files together: the game needs its pictures next to `ben10.exe`.
Needs Windows with .NET Framework 4.7.2 or newer (already on most Windows 10/11 PCs).

## Controls

| Key | Action |
|-----|--------|
| ← → | Move |
| ↑ | Jump |
| ↓ | Move down |
| X | Shoot |
| P | Punch |
| L | Climb the ladder |
| S | Four Arms punch (after transforming) |

## Run from the source code

1. Install [Visual Studio](https://visualstudio.microsoft.com/) with the **.NET desktop development** workload.
2. Open `ben10.sln`.
3. Press **F5** (Start) to build and play.

## Project layout

- `Form1.cs` – the main game: drawing, animations, enemies, controls and transformations
- `Program.cs` – application entry point
- `bin/Debug/` – the image files (`.png`, `.jpg`, `.jpeg`) the game loads at runtime
- `download/Ben10-Game.zip` – the ready-to-play game

The game loads its images by file name from the folder next to `ben10.exe`, so keep the images
in `bin/Debug/` (or copy them next to the exe if you build a Release version).

## Requirements

- Windows
- .NET Framework 4.7.2 or newer
