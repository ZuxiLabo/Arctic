﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Galaxy.API;

namespace Galaxy.API
{
    internal class image
    {
        internal static Texture2D CreateTextureFromBase64(string data)
        {
            Texture2D texture = new Texture2D(2, 2);
            Il2CppImageConversionManager.LoadImage(texture, Convert.FromBase64String(data));

            texture.hideFlags |= HideFlags.DontUnloadUnusedAsset;

            return texture;
        }

        internal static Sprite CreateSpriteFromBase64(string data)
        {
            Texture2D texture = CreateTextureFromBase64(data);

            Rect rect = new Rect(0.0f, 0.0f, texture.width, texture.height);

            Vector2 pivot = new Vector2(0.5f, 0.5f);
            Vector4 border = Vector4.zero;

            Sprite sprite = Sprite.CreateSprite_Injected(texture, ref rect, ref pivot, 100.0f, 0, SpriteMeshType.Tight, ref border, false);

            return sprite;
        }

        public static IEnumerator loadspriterest(Image Instance, string url)
        {
            var www = UnityWebRequestTexture.GetTexture(url);
            _ = www.downloadHandler;
            var asyncOperation = www.SendWebRequest();
            Func<bool> func = () => asyncOperation.isDone;
            yield return new WaitUntil(func);
            if (www.isHttpError || www.isNetworkError)
            {
               LogHandler.Error($"Error4 : {www.error}", "Err www");
               yield break;
            }
            var content = DownloadHandlerTexture.GetContent(www);
            var sprite2 = Instance.sprite = Sprite.CreateSprite(content,
                new Rect(0f, 0f, content.width, content.height), new Vector2(0f, 0f), 100000f, 1000u,
                SpriteMeshType.FullRect, Vector4.zero, false);
            if (sprite2 != null) Instance.sprite = sprite2;
        }
    }
}
