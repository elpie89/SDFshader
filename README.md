<h1>SDF Shader Unity</h1>

Signed Distance Filed is a shading technique really usefull with bitmap font
This repo rapresent an implentation of this technique in Unity.
The SDF shader is based on this Valve paper http://www.valvesoftware.com/publications/2007/SIGGRAPH2007_AlphaTestedMagnification.pdf

The goal of this projec is to keep a reference on how to use the SDF shader in his entire pipeline.

<h2>SDF Bitmap</h2>
Using Hiero create a bitmap atlas containg SDF information, starting fromm a regualr TTF font file.
In the FontCreator folder there is a hiero settings file to use as a template.
Export the Bitmap to generate a png and a fontsettings files to read from Unity.

![Alt text](/ReadMeImages/hiero.jpg?raw=true "Hiero Tool")

<h2>Font Importer</h2>
After import both the files,click on the fontsettings file to generate a Font, using the BitmapFontGenerator button.

![Alt text](/ReadMeImages/BitmapFontGenerator.jpg?raw=true "BitmnapFontGeneratorTool")

<h2>Shader</h2>
The SDF shader read the alpha value of the atlas to evaluate the distance from the character in the sprite.
Using this information we are available to scale on every size the font without loosing any quality.

![Alt text](/ReadMeImages/SDFfontBitmap.jpg?raw=true "SDF Bitmap")

<h3>Result</h3>

![Alt text](/ReadMeImages/result.jpg?raw=true "Result comparision")

1-classic font vectorial file.
2-SDF bitmap font
3-legacy bitmap font

On this image the best result came from a Text that use a TTF file.
But sometime, maybe for custom Cartoon style text, we need to create a custom font.

As you can see with an image of 512x512 pixels the classic approach it's really dirty.
In that case we need a big texture to render clean characters.
And of course a bigger texture means more resources.

The SDF shader allow an infinite scale of the charachters without any lack of quality.

Of course this technicque could be used also for other purposes.
Generally if we have a 2 asset monochromatic, and we need to scale it, with an SDF echnique we could have ani infinite scale 