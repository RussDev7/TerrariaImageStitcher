# Terraria Image Stitcher - Danny Russ
[![Build status](https://ci.appveyor.com/api/projects/status/apba6df8vwmfge9n?svg=true)](https://ci.appveyor.com/project/RussDev7/terrariaimagestitcher) [![GitHub Version](https://img.shields.io/github/tag/RussDev7/TerrariaImageStitcher.svg?label=GitHub)](https://github.com/RussDev7/TerrariaImageStitcher) [![Contributors](https://img.shields.io/github/contributors/RussDev7/TerrariaImageStitcher)](https://github.com/RussDev7/TerrariaImageStitcher)

![TIS-2](https://user-images.githubusercontent.com/33048298/182255984-25f066f8-3a30-4bec-906a-6f066dcd7646.png)  

Terraria Image Stitcher is a tool for combining images created from Terraria's in-game non-packed screenshot tool.

**Terraria has added the ability for in-game screenshots for since the release of [1.3.0.1](https://terraria.wiki.gg/wiki/Desktop_1.3.0.1). This tool aims to aid in the stitching (combining) of the many images created without Image Packing. Very Light and portable application will save you much time and headaches!**

## How To Use
 1. Download and compile the source by running `build.bat`.
 2. Within application, click `Add Photos`.
 3. Navigate to directory `\Documents\My Games\Terraria\Captures`.
 4. Find the folder containing the images you wish to stitch.
 5. Select all photos in this directory (missing one will result in error message).
 6. Within application, click "Save Location" Navigate to a place you wish to save the output image (don't add file extension).
 7. Within application, select your output image conversion format.
 8. Within application, click `Stitch Images` button.

## How It Works And How It Knows Image Size?
<details>
  <summary>Open Me!</summary>
  <p></p>
  This tool works by first getting the total amount of selected images.
  
  It then goes through each file name based on the first number before the "-" in the name. Example: 2875-1635.png.  
  From here we need to create a new image. We can use this file name number to calculate the final images height & width. How? Simple,  
  
  **In this example:**  
  2875  
  2875  
  3001  
  3001  
  3127  
  3127  
  3253  
  3253
  
  Number of images with **xxxx: 2**  
  Number of image groups with a new **xxxx: 4**  
  This means the **width is 4** and the **height is 2**.  
  
  We then add the first 2 images based on name going from top to bottom, and repeat left to right for the next 3 (for a width of 4).  
  
  Then we crop out the empty space and walah! It's stitched! And yes, doing this sounds easy on paper, but the process is much harder!  
  Let's just enjoy the fact I suffered for you!
</details>

## Features
 - Combines ALL images into one single stitched image
 - Single instance application, no API's attached
 - Simple yet efficient GUI

## Requirements
 1. .NET Framework 4.5.2 | Windows 7 or later
 2. Terraria for PC

## Preview
![release done and tested](https://user-images.githubusercontent.com/33048298/182271342-97e068e4-82b1-4446-aecf-137ae80f69e8.PNG)  
  
![StitcherAbout](https://user-images.githubusercontent.com/33048298/182271715-085ae7d6-b3b7-4df5-ad7f-359ffc794bad.PNG)

## Important Links

- [Forums Link](https://forums.terraria.org/index.php?threads/terrariaimagestitcher-image-stitcher-for-terrarias-image-packer.104761/)
- [Startup Guide](https://forums.terraria.org/index.php?threads/terrariaimagestitcher-image-stitcher-for-terrarias-image-packer.104761/)
- [Discord](https://discord.gg/fEK6eE7W)

## Download

- [GitHub Releases](https://github.com/RussDev7/TerrariaImageStitcher/releases)
- [Terraria Forums](https://forums.terraria.org/index.php?threads/terrariaimagestitcher-image-stitcher-for-terrarias-image-packer.104761/)

## Support & Credits

- [Donations](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=imthedude030@gmail.com&lc=US&item_name=Donation&currency_code=USD&bn=PP%2dDonationsBF)
