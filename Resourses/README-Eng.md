# Documentation

[Рус](https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/README.md) | Eng

**Application Link:** https://dmitriy-olw.itch.io/ar-mr-app-for-medical-files

## **About the Project**

**Name:** AR Application for Thyroid Phantom Visualization.

**Project Goal:** Create an interactive AR application for visualizing 3D models and medical scans (MRI, CT, ultrasound) of a thyroid phantom with hand-tracking control via Quest 3 headset.

**Demo Video:**  
[![Demo Video](https://youtu.be/K7Ahoak30Ks)](https://youtu.be/K7Ahoak30Ks)

**Application Screenshots:**

<div align="center">
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/Preview.png" height="100" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(1).png" height="100" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(2).png" height="100" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(5).png" height="100" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(6).png" height="100" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(7).png" height="100" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(9).png" height="100" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(10).png" height="100" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(12).png" height="100" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(13).png" height="100" />
</div>

#### This application was developed as part of the Hackathon "AR for Thyroid Phantom" at the Fifth Open Conference of Young Scientists of the Center for Diagnostics and Telemedicine. **The presented project won second place.** The application was developed by the team "Sezriv Ralizonki". Team members: Dmitry Konstantinov, Oleg Morev, Maxim Pishchagin, Ivan Semichev.

### **Diploma Confirming the Award:**

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Sertifikat/Sertificat_.jpg" height="500">

<br>

### **Test Data:** Resourses/Тестовые данные/

### **Key Features:**

- **Hand Control:** Use of hand tracking and gestures for interface interaction.
- **Data Visualization:**
  - Display of 3D organ models (thyroid phantom, neck organs, upper torso, and head);
  - Loading and processing medical scans (DICOM / .dcm – folder containing this data) with rendering settings (transparency, isosurfaces), slice adjustments, and light/radiation transmission settings;
  - Visualization modes: anatomical atlas, schematic representation.
- **AR Mode:** Overlaying 3D models onto the real environment via Quest 3 Passthrough camera.
- Integration of Quest 3 as a visualization device with a PC as the computing device.

**Application:** Medical training, diagnostics, presentation of medical data in AR.

1. **Description of Approach and Methods Used**

   - **Approach:** Use of hand tracking and gestures for AR application control. Using a computer as a station for loading, processing, and computing data, and the **Quest 3** headset as a visualization device, displaying the real world via Passthrough and enabling application control via hands and gestures.

   - **Methods:**
     - Dataset Work: **UnityVolumeRendering**;
     - Hand tracking, gestures, UI element control, model and scan visualization settings, and core AR application functionality: **MetaSDK**.
   - **Model Architecture:** Use of object-oriented programming methods.

2. **Resources Used for Solution Development**

   - **Technical Resources:**
     - Engine: **Unity 2022.3.59f1**;
     - AR Platform: **MetaSDK**;
     - Dataset Work - Scans: **UnityVolumeRendering**;
     - Ultrasound Preprocessing: Custom Python scripts (see Resourses/Python_Scripts/).
 
  - **Data:**
    - **Sketchfab:** 3D models;
    - **Asset Store:** SDK;
    - **Provided Data:** MRI, CT, ultrasound scan datasets; 3D models of the thyroid phantom;
    - **Ultrasound Data Preprocessing** using Python scripts to create a dataset with DICOM files (see Resourses/Python_Scripts/).

  - **Tools:**
    - **Unity 2022.3.59f1;**
    - **Blender;**
    - **GitHub;**
    - **JetBrains Rider;**
    - **Visual Studio;**
    - **JetBrains PyCharm;**
    - **MetaSDK;**
    - **UnityVolumeRendering.**

3. **Required Dependencies and Installation Instructions:**

   - To access the Quest 3 Passthrough camera from a PC, enable developer features in Meta Quest Link (see Fig. 2.1). This requires two-factor authentication on your Meta account and creating an organization under this account.
   - During development, the MetaSDK source code was modified. To ensure correct project functionality when opened in Unity, it must be edited again. Locate the script: `SpatialAnchorSpawnerBuildingBlock` in the project and replace its content with the code from `/Resourses/Other/`. On the MainScene, the inspector values for this script should match those shown in Fig. 2.2.
   - For correct dataset loading when opening the project in Unity, move the file `SimpleITKCSharpNative.dll` to the directory: `..\Assets\UnityVolumeRendering\3rdparty\SimpleITK\`. Then, in the Unity top menu, find the "Volume Rendering" tab and click "Settings". Ensure the button at the bottom says "Disable SimpleITK". If it says "Enable SimpleITK", click it and confirm the change (see Fig. 2.3). The file `SimpleITKCSharpNative.dll` is included in the project files (located in Resourses/Other/SimpleITKCSharpNative.7z, requires extraction).

### **Application Usage Instructions**

Upon loading, the user sees a menu with the "New Anchor Point" button (Fig. 1). Clicking it will display a thyroid phantom model near the index finger of the left hand.

Figure 1. New Anchor Point Menu:

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(9).jpeg" height="200">

The user must position the model to align with the real-world thyroid phantom. Then, make a gesture with the right hand: pinch the thumb and index finger together. The model will lock in place, and the main menu for controlling the application's core functions will appear.

In the main menu, users can choose to reset the anchor point or enter display mode (Fig. 2).

Figure 2. Main Menu:

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(10).jpeg" height="200">

**In the display mode menu**, three visualization options are available (Fig. 3).

Figure 3. Display Mode Menu (No Dataset Loaded):

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(11).jpeg" height="200">

**The first option displays the thyroid phantom dataset using MRI, CT, and ultrasound scans.** Before viewing, load the dataset by clicking "Load Dataset" in the main menu. In the dataset loading menu (Fig. 4), navigate to the folder containing DICOM or .dcm files. (Preprocess data beforehand—one dataset per folder).

Figure 4. Dataset Loading Menu:

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(12).jpeg" height="200">

After selection, click the corresponding button for CT, MRI, or ultrasound. For ultrasound scans, preprocessing with Python scripts is required (see Resourses/Python_Scripts/).

The dataset will load and process, enabling visualization and access to the dataset interaction menu (Fig. 5).

Figure 5. Dataset Settings Menu:

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(13).jpeg" height="200">

This menu includes submenus:
- Manual Position Calibration: Adjust dataset position and rotation via sliders (Fig. 6);

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(1).jpeg" height="200">  
 
- Light/Radiation Transmission: Change rendering modes (standard, transparency, isosurface) and adjust brightness/absorption coefficients (Fig. 7);

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(2).jpeg" height="200">  

- Volume Slice: Enable/disable volume slicing, reset defaults, adjust plane position/rotation/size, and enable slice-by-slice navigation (Fig. 8);

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(3).jpeg" height="200">  

- Flat Slice: Create/disable a slice plane, reset defaults, and adjust position/rotation (Fig. 9).

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(4).jpeg" height="200">  

**Second Mode:** Anatomical atlas visualization. Toggle model parts to view organs individually (Fig. 10).  
**Third Mode:** Schematic anatomical visualization. Toggle model parts to view organs individually (Fig. 10).

Figure 10. Anatomical Atlas and Schematic Visualization Menus:

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(5).jpeg" height="200">

With all visualizations enabled, the menu appears as shown in Fig. 11.

Figure 11. Visualization Menu (All Modes Enabled):

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(6).jpeg" height="200">

**Ultrasound Scan Preprocessing**  
For ultrasound scans, preprocess using Python scripts (see Resourses/Python_Scripts/).  
Step 1: Convert video scans to frames (first.py).  
Step 2: Convert frames to DICOM files (second.py).

### **Append**

Modified MetaSDK Source Code: /Resourses/Other/SpatialAnchorSpawnerBuildingBlock

Figure 2.1. Meta Quest Link Setup:

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(7).jpeg" height="300">

Figure 2.2. Script Inspector View in Unity:

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(8).jpeg" height="100">

Figure 2.3. SimpleITKCSharpNative Module Verification:

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(1).png" height="200">

(File is archived—extract from Resourses/Other/SimpleITKCSharpNative.7z)

### **Additional Screenshots:**

<div align="center">
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/Preview.png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(1).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(2).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(3).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(4).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(5).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(6).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(7).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(8).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(9).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(10).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(11).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(12).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(13).png" height="200" />
</div>
