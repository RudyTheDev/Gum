using System.IO;
using Assembly_CSharp.Screens;
using SkiaSharp;
using UnityEngine;

/// <summary>
/// Runs Gum the way a game would: initialize once with the project, then Update and Draw every frame.
/// Gum draws with Skia into a CPU raster surface, which is uploaded to a texture and drawn over the screen.
/// </summary>
public class GumManager : MonoBehaviour
{
    [SerializeField] string _gumProjectRelativePath = "Gum/Project/Unity Demo.gumj";

    UnityGumService _gum;
    SKSurface _surface;
    Texture2D _texture;
    int _width;
    int _height;

    void Start()
    {
        _gum = UnityGumService.Default;

        CreateSurface(Screen.width, Screen.height);

        string projectPath = Path.Combine(Application.dataPath, _gumProjectRelativePath);
        _gum.Initialize(_surface.Canvas, _width, _height, projectPath);

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

    void OnGUI()
    {
        if (_texture == null || Event.current.type != EventType.Repaint)
            return;

        // Skia rows run top-down, Unity texture rows bottom-up: flip V.
        GUI.DrawTextureWithTexCoords(new Rect(0, 0, _width, _height), _texture, new Rect(0, 1, 1, -1));
    }

    void OnDestroy()
    {
        if (_gum != null && _gum.IsInitialized)
            _gum.Uninitialize();

        _surface?.Dispose();
        _surface = null;

        if (_texture != null)
            Destroy(_texture);
    }

    void CreateSurface(int width, int height)
    {
        _width = Mathf.Max(1, width);
        _height = Mathf.Max(1, height);

        _surface?.Dispose();
        _surface = SKSurface.Create(new SKImageInfo(_width, _height, SKColorType.Rgba8888, SKAlphaType.Premul));

        if (_texture != null)
            Destroy(_texture);
        _texture = new Texture2D(_width, _height, TextureFormat.RGBA32, false);
    }
}
