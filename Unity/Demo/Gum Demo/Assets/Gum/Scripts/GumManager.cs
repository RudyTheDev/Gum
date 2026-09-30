using System.IO;
using Assembly_CSharp.Screens;
using Gum;
using SkiaSharp;
using UnityEngine;

/// <summary>
/// Runs Gum the way a game would: initialize once with the project, then push input, Update and Draw every frame.
/// Gum draws with Skia into a CPU raster surface, which is uploaded to a texture shown on a sprite
/// in front of the UI camera (an overlay camera that renders only its own layer).
/// </summary>
public class GumManager : MonoBehaviour
{
    [SerializeField] string _gumProjectRelativePath = "Gum/Project/Unity Demo.gumj";
    [SerializeField] Camera _uiCamera;
    [SerializeField] Shader _spriteShader;

    GumService _gum;
    GumUnityInput _input;
    SKSurface _surface;
    Texture2D _texture;
    SpriteRenderer _spriteRenderer;
    int _width;
    int _height;

    void Start()
    {
        _gum = GumService.Default;

        CreateSpriteRenderer();
        CreateSurface(Screen.width, Screen.height);

        string projectPath = Path.Combine(Application.dataPath, _gumProjectRelativePath);
        _gum.Initialize(_surface.Canvas, _width, _height, projectPath);
        _input = new GumUnityInput(_gum);

        // GraphicalUiElement.AddToRoot() is NET6_0_OR_GREATER-only; this is what it does.
        var screen = new DemoScreenGumRuntime();
        _gum.Root.Children.Add(screen);

        Debug.Log($"Gum initialized at {_width}x{_height} with {projectPath}");
    }

    void Update()
    {
        if (_gum == null || !_gum.IsInitialized)
            return;

        if (Screen.width != _width || Screen.height != _height)
        {
            CreateSurface(Screen.width, Screen.height);
            _gum.SystemManagers.Canvas = _surface.Canvas;
            _gum.HandleResize(_width, _height);
        }

        _input.Push(_height);
        _gum.Update(Time.timeAsDouble);

        SKCanvas canvas = _surface.Canvas;
        canvas.Clear(SKColors.Transparent);
        _gum.Draw();
        canvas.Flush();

        using (SKPixmap pixmap = _surface.PeekPixels())
        {
            _texture.LoadRawTextureData(pixmap.GetPixels(), pixmap.BytesSize);
        }
        _texture.Apply(false);
    }

    void OnDestroy()
    {
        _input?.Dispose();

        if (_gum != null && _gum.IsInitialized)
            _gum.Uninitialize();

        _surface?.Dispose();
        _surface = null;

        DestroyTextureAndSprite();

        if (_spriteRenderer != null)
        {
            Destroy(_spriteRenderer.sharedMaterial);
            Destroy(_spriteRenderer.gameObject);
        }
    }

    void CreateSpriteRenderer()
    {
        var spriteObject = new GameObject("Gum UI");
        spriteObject.layer = _uiCamera.gameObject.layer;
        spriteObject.transform.SetParent(_uiCamera.transform, false);
        spriteObject.transform.localPosition = new Vector3(0, 0, _uiCamera.nearClipPlane + 1);
        // Skia rows run top-down, Unity texture rows bottom-up: flip vertically.
        spriteObject.transform.localScale = new Vector3(1, -1, 1);

        _spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
        _spriteRenderer.sharedMaterial = new Material(_spriteShader);
    }

    void CreateSurface(int width, int height)
    {
        _width = Mathf.Max(1, width);
        _height = Mathf.Max(1, height);

        _surface?.Dispose();
        _surface = SKSurface.Create(new SKImageInfo(_width, _height, SKColorType.Rgba8888, SKAlphaType.Premul));

        DestroyTextureAndSprite();
        _texture = new Texture2D(_width, _height, TextureFormat.RGBA32, false)
        {
            filterMode = FilterMode.Point,
            wrapMode = TextureWrapMode.Clamp,
        };

        // One world unit per pixel, and an orthographic size that makes the view exactly the screen's height.
        _spriteRenderer.sprite = Sprite.Create(_texture, new Rect(0, 0, _width, _height), new Vector2(0.5f, 0.5f),
            1, 0, SpriteMeshType.FullRect);
        _uiCamera.orthographic = true;
        _uiCamera.orthographicSize = _height / 2f;
    }

    void DestroyTextureAndSprite()
    {
        if (_spriteRenderer != null && _spriteRenderer.sprite != null)
        {
            Destroy(_spriteRenderer.sprite);
            _spriteRenderer.sprite = null;
        }

        if (_texture != null)
        {
            Destroy(_texture);
            _texture = null;
        }
    }
}
