using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace TestTask
{
    public class ArtLoader : MonoBehaviour
    {
        private static string resourceUri = "https://picsum.photos";

        [SerializeField]
        private int spriteWidth;
        [SerializeField]
        private int spriteHeight;

        private Image background;
        private RectTransform rt;

        private void Awake()
        {
            rt = GetComponent<RectTransform>();
            background = GetComponent<Image>();
        }

        private void Start()
        {
            SetImage();
        }

        public void SetImage()
        {
            LoadSprite(spriteWidth, spriteHeight, (sprite) => background.sprite = sprite);
        }

        private void LoadTexture(int width, int height, Action<Texture> postAction)
        {
            string fullURI = $"{resourceUri}/{width}/{height}";
            StartCoroutine(GetTexture(fullURI,postAction));
        }

        private void LoadSprite(int width, int height, Action<Sprite> postAction)
        {
            LoadTexture(width, height, (texture) =>
            {
                Sprite sprite = Sprite.Create((Texture2D)texture, new Rect(Vector2.zero, new Vector2(width, height)), Vector2.one / 2);
                postAction?.Invoke(sprite);
            });
        }

        private IEnumerator GetTexture(string url, Action<Texture> postAction)
        {
            //Debug.Log($"[{GetType().Name}.{nameof(GetTexture)}] Loading texture from {url}");
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning($"[{GetType().Name}.{nameof(GetTexture)}] Could not load texture from {url}: {www.error}");
            }
            else
            {
                Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                postAction?.Invoke(myTexture);
            }
        }

    }
}
