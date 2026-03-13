# ZombieHit-Krafton-Assessment


# ZombieHit - Mobile Action Game

**ZombieHit** is a fast-paced, 60-second top-down survival game built for Android using Unity. The objective is simple: navigate an enclosed arena, drift your car, and crash into as many zombies as possible before the timer runs out. 

This project was built with a strong focus on mobile optimization, custom touch controls, and lightweight, math-based physics calculations to ensure a buttery smooth 60 FPS experience on mobile devices.

---

## Gameplay Features

* **Custom Swipe Controls:** Designed specifically for Android. Touching the screen automatically accelerates the vehicle, while swiping left or right steers.
* **Dynamic Chase Camera:** A custom camera system that smoothly calculates offset and uses `Quaternion.Slerp` to lock behind and rotate with the car, ensuring directional controls never invert when driving backward.
* **60-Second Time Trial:** A core game loop featuring a start screen, a 60-second countdown timer, a dynamic score tracker, and a Game Over sequence.
* **Atmospheric Polish:** Features mobile-optimized hard shadows, a dark asphalt material, and linear environment fog to hide map edges and set a moody aesthetic.

---

## Technical Implementation & Problem Solving

This project relies on several clever programming solutions to optimize performance and prevent common game-breaking bugs:

### 1. The "Donut" Spawner (Annulus Spawning)
To prevent the "AFK Exploit" where zombies might spawn directly on top of the player's car, the spawner utilizes `Random.insideUnitCircle` combined with a **Minimum** and **Maximum** radius. This guarantees zombies only spawn in a "donut" shape around the player—far enough to require driving, but strictly inside the arena walls.

### 2. Zero-Friction Boundary Walls
The arena is contained by four invisible boundary walls. Instead of standard collisions that would stop the car dead in its tracks, the walls are coated in a custom **Zero-Friction Physics Material**. This allows the car to smoothly slide along the glass boundaries, keeping gameplay fast and fluid.

### 3. Math-Based Zombie AI & Containment
To avoid heavy mesh colliders and physics bugs (like kinematic ghosting), zombie containment is handled purely through mathematics:
* **Containment:** In the `Update()` loop, the script checks the zombie's `Vector3.Distance` from the center of the map. If it exceeds 22 meters, it instantly calculates a `LookRotation` to march the zombie 180 degrees back into the arena.
* **Hit Detection:** Instead of relying on rigidbodies crashing into each other, the zombies constantly measure their distance to the car. If the car enters the minimum distance threshold, the script triggers the crash logic, saving significant physics processing power.

### 4. UI & Mobile Optimization
* **Anchored UI:** All UI elements (Score, Timer, FPS counter, Menus) are strictly anchored using Unity's `RectTransform` to ensure they scale and position perfectly across all mobile screen aspect ratios.
* **Performance Lock:** The application is hardcoded to `Application.targetFrameRate = 60` to bypass default mobile 30 FPS caps.
* **Rigidbody Interpolation:** Enabled on the player vehicle to sync the graphics engine with the physics engine, completely eliminating visual stuttering on mobile screens.

---

## How to Play (Installation)

1. Download the latest **`ZombieHit.apk`** file from the Releases tab (or [Insert Google Drive Link Here]).
2. Transfer the `.apk` file to your Android device.
3. Tap the file to install (you may need to "Allow installation from unknown sources" in your Android settings).
4. **Controls:** Tap anywhere on the screen to hit the gas. Swipe your thumb left or right to steer!

---

## Project Structure

* **`Assets/`** - Contains all scripts, scenes, materials, and prefabs.
  * **`Scripts/`** - Contains the custom `GameManager`, `ZombieController`, `TopDownCamera`, and modified `PrometeoCarController`.
* **`Packages/`** - Unity package dependencies.
* **`ProjectSettings/`** - Android build configurations and input settings.
*(Note: The `Library` cache folder is intentionally omitted via `.gitignore` for a lightweight repository).*

---

## Video Demonstration
Watch a quick 2-minute breakdown of the gameplay, features, and mechanics here:
**[Insert YouTube / Drive Video Link Here]**

---

## Credits / Assets Used
* **Game Engine:** Unity 2022 LTS (or your current version)
* **Car Controller:** Prometeo Car Controller (Heavily modified for mobile swipe inputs)
* **Code & Architecture:** Built by [Your Name] for the final assessment.
This project relies on several clever programming solutions to optimize performance and prevent common game-breaking bugs:

### 1. The "Donut" Spawner (Annulus Spawning)
To prevent the "AFK Exploit" where zombies might spawn directly on top of the player's car, the spawner utilizes `Random.insideUnitCircle` combined with a **Minimum** and **Maximum** radius. This guarantees zombies only spawn in a "donut" shape around the player—far enough to require driving, but strictly inside the arena walls.

### 2. Zero-Friction Boundary Walls
The arena is contained by four invisible boundary walls. Instead of standard collisions that would stop the car dead in its tracks, the walls are coated in a custom **Zero-Friction Physics Material**. This allows the car to smoothly slide along the glass boundaries, keeping gameplay fast and fluid.

### 3. Math-Based Zombie AI & Containment
To avoid heavy mesh colliders and physics bugs (like kinematic ghosting), zombie containment is handled purely through mathematics:
* **Containment:** In the `Update()` loop, the script checks the zombie's `Vector3.Distance` from the center of the map. If it exceeds 22 meters, it instantly calculates a `LookRotation` to march the zombie 180 degrees back into the arena.
* **Hit Detection:** Instead of relying on rigidbodies crashing into each other, the zombies constantly measure their distance to the car. If the car enters the minimum distance threshold, the script triggers the crash logic, saving significant physics processing power.

### 4. UI & Mobile Optimization
* **Anchored UI:** All UI elements (Score, Timer, FPS counter, Menus) are strictly anchored using Unity's `RectTransform` to ensure they scale and position perfectly across all mobile screen aspect ratios.
* **Performance Lock:** The application is hardcoded to `Application.targetFrameRate = 60` to bypass default mobile 30 FPS caps.
* **Rigidbody Interpolation:** Enabled on the player vehicle to sync the graphics engine with the physics engine, completely eliminating visual stuttering on mobile screens.

---

## 🎮 How to Play (Installation)

1. Download the latest **`ZC_4.apk`** file.
2. Transfer the `.apk` file to your Android device.
3. Tap the file to install (you may need to "Allow installation from unknown sources" in your Android settings).
4. **Controls:** Tap anywhere on the screen to hit the gas. Swipe your thumb left or right to drift!

---

## 📂 Project Structure

* **`Assets/`** - Contains all scripts, scenes, materials, and prefabs.
  * **`Scripts/`** - Contains the custom `GameManager`, `ZombieController`, `TopDownCamera`, and modified `PrometeoCarController`.
* **`Packages/`** - Unity package dependencies.
* **`ProjectSettings/`** - Android build configurations and input settings.
*(Note: The `Library` cache folder is intentionally omitted via `.gitignore` for a lightweight repository).*

---

## 📹 Video Demonstration
Watch a quick 2-minute breakdown of the gameplay, features, and mechanics here:
**https://youtube.com/shorts/UdnSe3nkefA**

---

## 🤝 Credits / Assets Used
* **Game Engine:** Unity 2022 LTS
* **Car Controller:** Prometeo Car Controller
* **Code & Architecture:** Built by Ashish Verma for the final assessment.
