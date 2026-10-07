# Ben 10 Game

A Windows desktop game written in C# (Windows Forms, .NET Framework) where you play as Ben Tennyson.
The characters, enemies and animations were all made step by step by hand. When Ben finds the
Omnitrix watch, he transforms into Four Arms.

## How to run

1. Install [Visual Studio](https://visualstudio.microsoft.com/) with the **.NET desktop development** workload.
2. Open `ben10.sln`.
3. Press **F5** (Start) to build and play.

## Project layout

- `Form1.cs` – the main game: drawing, animations, enemies, controls and transformations
- `Program.cs` – application entry point
- `bin/Debug/` – the image files (`.png`, `.jpg`, `.jpeg`) the game loads at runtime

The game loads its images by file name from the folder next to `ben10.exe`, so keep the images
in `bin/Debug/` (or copy them next to the exe if you build a Release version).

## Requirements

- Windows
- .NET Framework (the version set in `ben10.csproj`)
