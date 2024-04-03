# GameJam

## Ideas
### Game Loop:
1. Falling Transition 
  - Fish falling into the bottom
2. Starting Scene
  - Enable player control
  - Disable health depletion
  - Have a "starting line" in the middle (with help text etc.)
  - Have a shop on the side
	- Shop opens when fish touches it
3. Game Starts (once player cross the starting line)
  - Enable health depletion
  - Hide shop and starting line
  - Fish starts appearing
4. Game 
  - As the fish moves up, background moves down
  - Other fish will spawn and move randomly in the screen
  - Items will appear and float in a spot (or move in a pattern)
5. Game Over (if health reaches 0 or player fail to catch a wall and hit the bottom of screen)
  - Return to step 1 transition

### Menu:
Shop:
- Half Transparent canvas
- Interact via mouse click
Pause:
- Pause game
- Countdown when you exits OR a period of slow down when you exits

### Additional Ideas:
Death Alterntaive
  - Player will fall down to the bottom (no animation)
  - If it hits fish mid way, you have second chance
  - If reach bottom, go back to start

## TODO Items
### MVP
- [X] Game Stage Scene
### Visual
- [ ] Slow time VFX
- [ ] Mine/Fish death animation/sound
- [ ] HP Art
### GamePlay
- [X] Reduce HP with time
- [ ] Refine HP logic
- [X] Item Template
### System
- [ ] Pause 
- [ ] Menu and all (game over, game start etc.)
  - [ ] Game Over Menu
  - [ ] Game Start Menu
  - [ ] Pasue Menu
