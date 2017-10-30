<h1> Work in progres</h2>
<h1>SDF Shader Unity</h1>

Signed Distance Filed is a techniques that allow to render smooth text on big font size.
This repo rapresen a implentation of his technique in Unity.
The technique is based on this Valve paper 

<h2>SDF atlas</h2>
Using Hiero we can create an atlas with modified character starting from  a .ttf file
In the FontCreator folder I added a hiero settings file to use as a template.
Exporting the fon will generate a png and a fontsettings file that we will pass to Unity

<h2>Font Importer</h2>
After import both the files we can click on the fontsettings file o generate a Font

<h2>Shader</h2>
The SDF shader read the alpha value of the atlas to evaluate the distance from the character in the sprite.
Using this information we are available to scale on every size he font without loosing any info
