using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
using System;

public class FontImporter : MonoBehaviour
{
    private static int scaleW;
    private static int scaleH;
    private static int charCount;
    [MenuItem("Assets/FontImporter/CreateFont")]
    public static void CreateFont()
    {
        TextAsset fntInfo = Selection.activeObject as TextAsset;
        if (fntInfo != null)
        {
            string targetPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(fntInfo));
            Font newFont = new Font(fntInfo.name);
            Material newMaterial = new Material(Shader.Find("GUI/SDF Text Shader"));
            AssetDatabase.CreateAsset(newMaterial, Path.Combine(targetPath, "SDF Font Material.mat"));
            AssetDatabase.CreateAsset(newFont,Path.Combine(targetPath, newFont.name + ".fontsettings"));
            newFont.material = newMaterial;

            string[] lines = fntInfo.text.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);

            ParseInfoLine(lines[0], newFont);
            ParseCommonLine(lines[1], newFont);
            ParsePageLine(lines[2], newFont);
            ParseCharsNumberLine(lines[3],newFont);

            CharacterInfo[] newCharactersInfo = new CharacterInfo[charCount];

            for (int i = 4; i < charCount+4; i++)
            {
                ParseCharSpecsLine(lines[i], ref newCharactersInfo[i-4]);
            }

            newFont.characterInfo = newCharactersInfo;
            AssetDatabase.SaveAssets();
        }
    }

    private static void ParseInfoLine(string line,Font mFont)
    {
        Dictionary<string, string> infoDictionary = new Dictionary<string, string>();
        ParseLine(line, ref infoDictionary);
    }

    private static void ParseCommonLine(string line,Font mFont)
    {
        Dictionary<string, string> commonDictionary = new Dictionary<string, string>();
        ParseLine(line, ref commonDictionary);
        
        Int32.TryParse(commonDictionary["scaleW"], out scaleW);
        Int32.TryParse(commonDictionary["scaleH"], out scaleH);

        int lineHeight;//lineHeight
        Int32.TryParse(commonDictionary["lineHeight"], out lineHeight);
    }

    private static void ParsePageLine(string line, Font mFont)
    {
        Dictionary<string, string> pageDictionary = new Dictionary<string, string>();
        ParseLine(line, ref pageDictionary);
    }

    private static void ParseCharsNumberLine(string line, Font mFont)
    {
        Dictionary<string, string> charsNumberDictionary = new Dictionary<string, string>();
        ParseLine(line, ref charsNumberDictionary);
        Int32.TryParse(charsNumberDictionary["count"],out charCount);
    }

    private static void ParseCharSpecsLine(string line, ref CharacterInfo charInfo)
    {
        Dictionary<string, string> charsSpecsDictionary = new Dictionary<string, string>();
        ParseLine(line, ref charsSpecsDictionary);

        int index;//id
        Int32.TryParse(charsSpecsDictionary["id"],out index);
        charInfo.index = index;        

        int uvW;//width
        Int32.TryParse(charsSpecsDictionary["width"], out uvW);
        charInfo.uv.width = (float)uvW/scaleW;

        int uvH;//height
        Int32.TryParse(charsSpecsDictionary["height"], out uvH);
        charInfo.uv.height = (float)uvH/scaleH;

        int uvX;//x
        Int32.TryParse(charsSpecsDictionary["x"], out uvX);
        charInfo.uv.x = (float)uvX / (float)scaleW;

        int uvY;//y
        Int32.TryParse(charsSpecsDictionary["y"], out uvY);
        charInfo.uv.y = 1- ((float)uvY / scaleH) - charInfo.uv.height;

        int vertX;//xoffset
        Int32.TryParse(charsSpecsDictionary["xoffset"], out vertX);
        charInfo.vert.x = vertX ;

        int vertY;//yoffset
        Int32.TryParse(charsSpecsDictionary["yoffset"], out vertY);
        charInfo.vert.y = -vertY;

        int vertW;
        Int32.TryParse(charsSpecsDictionary["width"], out vertW);
        charInfo.vert.width = vertW ;

        int vertH;
        Int32.TryParse(charsSpecsDictionary["height"], out vertH);
        charInfo.vert.height = -vertH;

        int advance;//xadvance
        Int32.TryParse(charsSpecsDictionary["xadvance"], out advance);
        charInfo.advance = advance;

        bool flipped;
    }


    private static void ParseLine(string line, ref Dictionary<string,string> _dictionary)
    {       
        string lineText = line.Substring(line.IndexOf(" ")+1);
        string[] infoData = lineText.Split(null);
        foreach (string l in infoData)
        {
            
            if (!string.IsNullOrEmpty(l))
            {
                string[] keyValue = l.Split(new char[] { '=' });
                _dictionary.Add(keyValue[0], keyValue[1]);
            }
            
        }
    }

   
}
