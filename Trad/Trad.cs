using UnityEngine;
using MelonLoader;
using TMPro;
using System;
using System.IO;
using System.Reflection;
using System.Collections;
using UnityEngine.Rendering;

// For these reference as in the image
// https://imgur.com/IRG9FZC


namespace Translation
{
    public class Trad : MelonMod
    {
        private SuperTextMesh textMeshPro;
        //I was trying to alter the MainText that appears as area title but it got really bugged out so I am keeping the relevant code as comment for someone more competent than me to fix it 
        // private GameObject mainTextObject; // Replaced eventObject
        private string resourceName = "Translation.Resources.grabby.png";
        private string resourceTitle = "Translation.Resources.logo_doubleShakeCred.png";
        private Texture2D textureLoader;
        private SuperTextMesh loadingTextMeshPro;
        private SuperTextMesh shopTextMeshPro;
        private SuperTextMesh repTextMeshPro;
        //Again text of the area title
        // private SuperTextMesh mainTextMeshPro; // Added mainTextMeshPro

        private bool drawTexture = false;

        //Some small flavor text for the translation delete it if you want
        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Doubleshake translation");
            MelonLogger.Msg("Hello world yada yada");
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {


            if (sceneName == "_pregame")
            {
                GameObject Prefinder = GameObject.Find("HUD/viewport/presents");
                if (Prefinder != null)
                {
                    textMeshPro = Prefinder.GetComponent<SuperTextMesh>();
                    if (textMeshPro != null)
                    {
                        textMeshPro.text = "presents";
                    }
                    else
                    {
                        MelonLogger.Msg("TextMeshProUGUI component is null.");
                    }
                }
                else
                {
                    MelonLogger.Msg("Finder GameObject is null.");
                }
            }


            // Title screen relevant areas
            //The image there is just if you want to put your name as credits for translating the game on the title screen
            if (sceneName == "Level 1 Caliko Coast V3")
            {
                GameObject finder = GameObject.Find("_Important/HUD/TitleScreen/Press [] To Continue/press");
                if (finder != null)
                {
                    textMeshPro = finder.GetComponent<SuperTextMesh>();
                    if (textMeshPro != null)
                    {
                        textMeshPro.text = "Press";
                    }
                    else
                    {
                        MelonLogger.Msg("TextMeshProUGUI component is null.");
                    }
                }
                else
                {
                    MelonLogger.Msg("Finder GameObject is null.");
                }

                GameObject finderTwo = GameObject.Find("_Important/HUD/TitleScreen/Press [] To Continue/to continue");
                if (finderTwo != null)
                {
                    textMeshPro = finderTwo.GetComponent<SuperTextMesh>();
                    if (textMeshPro != null)
                    {
                        textMeshPro.text = "to start";
                    }
                    else
                    {
                        MelonLogger.Msg("TextMeshProUGUI component is null.");
                    }
                }
                else
                {
                    MelonLogger.Msg("Finder GameObject is null.");
                }

             

                GameObject titleScreenImageFinder = GameObject.Find("_Important/HUD/TitleScreen/Image");
                if (titleScreenImageFinder != null)
                {
                    UnityEngine.UI.Image imageComponent = titleScreenImageFinder.GetComponent<UnityEngine.UI.Image>();
                    if (imageComponent != null)
                    {
                        textureLoader = LoadTexture(resourceTitle);
                        if (textureLoader != null)
                        {
                            imageComponent.sprite = Sprite.Create(textureLoader, new Rect(0, 0, textureLoader.width, textureLoader.height), new Vector2(0.5f, 0.5f));
                        }
                        else
                        {
                            MelonLogger.Msg($"Texture not loaded: {resourceTitle}");
                        }
                    }
                    else
                    {
                        MelonLogger.Msg("Image component not found on the TitleScreen Image GameObject.");
                    }
                }
                else
                {
                    MelonLogger.Msg("TitleScreen Image GameObject is null.");
                }
            }






        }

        // Main UI relevant area
        public override void OnUpdate()
        {
            // CheckEventVisibility();
            ApplyTranslation();
        }
        //Code for the area text
        /* private void CheckEventVisibility()
         {
             mainTextObject = GameObject.Find("_Important/HUD/Event!/MainText");

             if (mainTextObject != null)
             {
                 mainTextMeshPro = mainTextObject.GetComponent<SuperTextMesh>();

                 if (mainTextMeshPro != null)
                 {
                     CanvasRenderer canvasRenderer = mainTextObject.GetComponent<CanvasRenderer>();
                     if (canvasRenderer != null && !canvasRenderer.cull)
                     {
                         mainTextMeshPro.text = "Caliko Coast";
                     }
                     else
                     {
                         MelonLogger.Msg("MainText is found but not visible.");
                     }
                 }
                 else
                 {
                     MelonLogger.Msg("MainText SuperTextMesh component is null.");
                 }
             }
             else
             {
                 MelonLogger.Msg("MainText GameObject is null or not found.");
             }
         }
        */
       
        //Ui part 2
        
        public override void OnGUI()
        {

            //Bug report dont really need to translate it
            GameObject repFinder = GameObject.Find("_Important/HUD/Pause Menu/menu_options/bg/Report/title");
            if (repFinder != null)
            {
                repTextMeshPro = repFinder.GetComponent<SuperTextMesh>();
                if (repTextMeshPro != null)
                {
                    repTextMeshPro.text = "Report a bug or send feedback? Go to:\r\n<c=cyan>  www.rightstickstudios.com/ds-feedback\r\n<c=yellow>  www.rightstickstudios.com/ds-bugreport";
                }
            }
            //Loading text
            GameObject loadeFinder = GameObject.Find("GameHandler/Loader/Super Text");
            if (loadeFinder != null)
            {
                loadingTextMeshPro = loadeFinder.GetComponent<SuperTextMesh>();
                if (loadingTextMeshPro != null)
                {
                    loadingTextMeshPro.text = "<w>Loading!";
                }
            }
            // For some reason the shop name is not on the StreamingAssets
            GameObject shopFinder = GameObject.Find("_Important/HUD/menu_shop/bg/shop name");
            if (shopFinder != null)
                shopTextMeshPro = shopFinder.GetComponent<SuperTextMesh>();
            if (shopTextMeshPro != null)
            {
                shopTextMeshPro.text = "Caliko Shop";
            }




            if (drawTexture)
            {
                DrawTextureOnGUI();
                drawTexture = false;
            }
        }
        //Boss grab image 
        private void ApplyTranslation()
        {
            GameObject targetObject = GameObject.Find("_Important/HUD/FieldView/SuperGrabby/Image");

            if (targetObject != null)
            {
                UnityEngine.UI.Image imageComponent = targetObject.GetComponent<UnityEngine.UI.Image>();
                if (imageComponent != null)
                {
                    textureLoader = LoadTexture(resourceName);
                    if (textureLoader != null)
                    {
                        imageComponent.sprite = Sprite.Create(textureLoader, new Rect(0, 0, textureLoader.width, textureLoader.height), new Vector2(0.5f, 0.5f));
                    }
                }
            }
        }

        private void DrawTextureOnGUI()
        {
            if (textureLoader != null)
            {
                Graphics.DrawTexture(new Rect(10, 10, textureLoader.width, textureLoader.height), textureLoader);
            }

        }

        private Texture2D LoadTexture(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(buffer);

                    return texture;
                }
                else
                {
                    return null;
                }
                //Boss grab image end
            }
        }
    }
}
