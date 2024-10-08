# DIY Unity Based Headunit <> Purchase Full Project [Here](https://flux-11.gitbook.io/headunit/purchase-full-project/how-to-purchase)
Check out our trello board for upcoming features and ideas [here](https://trello.com/b/Qg1K2m1b)!

---

### Help Needed!
- Currently asking for suggestions on the Fuel Tank Level Indicator. I don't love the current one but it works.
- Currently asking for suggestions on the App Library upcoming feature and what apps would be useful.
- Currently asking for better LiDAR / Camera setups for the 3D world view.
- Currently asking for insight on how helpful internet tethering to the headunit would be, and if you would use it much.

[**Documentation**](https://flux-11.gitbook.io/headunit) |
[**Request / Ask**](https://github.com/ffluxxus/unity-headunit/discussions)

:wave: This repo is maintained by [@ffluxxus](https://github.com/ffluxxus). If you see anything wrong or missing, please [file an issue](https://github.com/ffluxxus/unity-headunit/issues/new/choose)! :+1:
😔 This repo cannot continue with updates until I purchase the hardware needed to use all the features.

[![License](.github/licensebadge.svg)](/LICENSE.md)
[![Build](https://github.com/ffluxxus/unity-headunit/actions/workflows/main.yml/badge.svg)](https://github.com/ffluxxus/unity-headunit/actions/workflows/main.yml)
[![Discord](.github/discordbadge.svg)](https://fluxus.000.pe) 

---

If you are looking for hardware, extra source code, and previews go to [our documentation](https://flux-11.gitbook.io/headunit)

For support of other platforms please create an issue report.
I cannot upload the entire project to Unity due to security reasons, but you can contact me to purchase it and I will clear it up.

![ShowcaseImage](https://github.com/ffluxxus/unity-headunit/blob/main/showcase/image_2024-07-15_232121984.png?raw=true)
*ui is currently in-development and WILL change in the future*

### Description
Unity-Headunit is an Unity3D Game Engine based headunit using C# as its main component + C++ (arduino). Main goal is to create a universal headunit that runs on any x86 linux system (not rpi).

### Unity Settings
 - Version: `2021.3.22f1`
 - Platform: `Windows, Mac, Linux`
 - Target Platform: `Linux` | `Windows Intel 64-bit` is used during testing
 - Overrides: `None`

### Supported Systems
 - [YouYeeToo X1 x86 SBC](https://amazon.com/dp/B0CCY2RBCS/)
 - [Beelink S12 x64 Mini](https://amazon.com/dp/B0BZCQVJTH/)
 - Any x86 / x64 System

### Supported Functionalities
 - Realtime 3D World View by LiDAR + Camera Scanning
 - Manual and Automatic Gearbox's with Gear Indicators
 - Mileage and Fuel Level Indicators
 - Engine Warning Light Indicators
 - Any supported Unity3D resolutions
 - CarPlay supports the basic 480p resolution (max 60FPS)
 - Audio playback from CarPlay
 - Wired / Wireless CarPlay & Android Auto
 - Touchscreen and buttons input
 - Bluetooth Connection
 - Automatic launch after device hotplug
 - Automatic / Manual detection of connected devices
 - User-friendly UI
 - Development / Debug options
 - Swap Android Auto / CarPlay to Spotify Application
 - Supports any web browser based applications and allows touch, keyboard, and audio input / output
 - Reverse Cameras through USB
 - Customizable Camera Angles on Car Model
 - Headlight, Turn Signal, etc. Control through Buttons in World Space
 - and more...

### Supported Platforms

 - Ubuntu
 - Windows `(most features / functions will not work under this operating system)`

### Parts List (outdated)
 - [Neptune 4 Printer](https://www.amazon.com/dp/B0C745237N)
 - [LiDAR Sensor](https://www.amazon.com/dp/B088BBJ9SQ)
 - [USB Hub 4-port](https://www.amazon.com/dp/B0CN3F9Y1Z)
 - [Bluetooth 5.0](https://www.amazon.com/dp/B08DFBNG7F)
 - [Arduino MEGA](https://www.amazon.com/dp/B01H4ZDYCE)
 - [GPS Module](https://www.amazon.com/dp/B01D1D0F5M/)
 - [RFID Module](https://www.amazon.com/dp/B07VLDSYRW)
 - [Lowlight Wideangle USB Camera](https://www.amazon.com/dp/B0C36ZVQ5G)
 - [NVMe SSD](https://www.amazon.com/dp/B0822Y6N1C)

### License
CC0-1.0

### Used Repositories
 - [UnityWebBrowser](https://github.com/Voltstro-Studios/UnityWebBrowser) | Used for Carplay / Spotify System
 - [Unity Engine Personal](https://unity.com/) 
 - [3D Model Showcase Project](https://github.com/LeoBlanchette/Unity3dModelShowcase) | Used Camera System, Post Processing Effects, used as base project
 - [Arduino OBD](https://github.com/stanleyhuangyc/ArduinoOBD) | Used for retrieving OBD data from vehicle
 - [Arduino Communication](https://mauznemo.de/smart_miata_prev/) | Used for alot of the Arduino Communication code and more
 - [Node Carplay](https://github.com/rhysmorgan134/node-CarPlay) | Used for CarPlay implementation

### Remarks
**This software is not certified by Google Inc, Apple Inc, nor any other major company. It is created for R&D purposes and may not work as expected by the original authors. Do not use while driving. You use this software at your own risk.**

*AndroidAuto is registered trademark of Google Inc.*
*Carplay is registered trademark of Apple Inc.*
*This software has no registered trademarks or copyrights under its name. Nor is it affiliated with any brand or company.*
