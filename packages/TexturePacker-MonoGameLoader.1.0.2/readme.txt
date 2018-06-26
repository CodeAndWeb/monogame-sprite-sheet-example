Background
==========

TexturePacker (http://www.codeandweb.com) is a very easy to use application for 
creating Texture Atlases / Sprite Sheets.  The idea is that you draw the 
individual frames of animation for your game characters as separate image files 
and then drag and drop them into TexturePacker – whereupon you end up with a 
single image file and accompanying data file ready to load into your game.  
Using a single sprite sheet is much more efficient than loading lots of 
individual images in game development. 

Now this maybe nothing new to a lot of people, however what you might not know, 
is that the latest version of TexturePacker has an option for exporting the 
sprite sheet and data file in a format targeted at MonoGame.  The next step is 
to then use this Nuget package in your MonoGame app in order to use the data 
files created by TexturePacker to create your animations with just a few lines 
of code.

See this tutorial for an example of how to use this Nuget with either Xamarin 
Studio (targetting iOS) or Visual Studio (targetting Windows 8 store apps)....

http://randolphburt.co.uk/2014/08/08/animation-made-easy-in-monogame-with-texturepacker