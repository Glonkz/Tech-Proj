Dual Media Player
A desktop media player application built using C# and the .NET Framework, utilizing the LibVLCSharp library for high-performance multimedia playback.

Authors
Jaspreet Bath

Justin Chik

Description
This application is a Windows Forms-based media player designed to play a wide variety of video and audio files. It leverages the LibVLC engine to provide native support for modern codecs and hardware acceleration without requiring external codec packs.

Features
Format Support: Plays major video and audio formats including MP4, MKV, AVI, WMV, MP3, and AAC.

Playback Controls: Standard Play, Pause, and Mute functionality.

Synchronized Timeline: A seekable trackbar that updates in real-time with the video and allows users to jump to specific timestamps.

Volume Control: Integrated slider for real-time audio adjustment.

File Management: Built-in file browser to load media from local storage.

Resource Management: Automated cleanup of unmanaged LibVLC resources upon application exit.

Technical Requirements
Framework: .NET Framework

Library: LibVLCSharp (v3.x or higher)

Hardware Acceleration: Supports GPU decoding via DxVA2, D3D11VA, and NVDEC.

Setup and Installation
Open the project in Visual Studio.

Restore NuGet packages to install LibVLCSharp.WinForms and VideoLAN.LibVLC.Windows.

Ensure the libvlc folder and required DLLs are present in the build output directory.

Build and run the project using the Any CPU or x64 configuration.

Usage
Click File > Open to select a media file.

Use the Play/Pause buttons to control playback.

Drag the Timeline Slider to seek through the video.

Adjust the Volume Slider or use the Mute button to manage audio output.

Exit the application using the Quit button or the window close icon to ensure all media resources are properly disposed.
