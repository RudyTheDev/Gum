//Code for Meadow/Controls/ScrollBar (Container)
using Assembly_CSharp.Components.Meadow.Controls;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using RenderingLibrary.Graphics;
using System.Linq;
namespace Assembly_CSharp.Components.Meadow.Controls;
partial class ScrollBarRuntime : ContainerRuntime
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Meadow/Controls/ScrollBar", typeof(ScrollBarRuntime));
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(global::Gum.Forms.Controls.ScrollBar)] = typeof(ScrollBarRuntime);
    }
    public global::Gum.Forms.Controls.ScrollBar FormsControl => FormsControlAsObject as global::Gum.Forms.Controls.ScrollBar;
    public enum ScrollBarCategory
    {
    }
    public enum OrientationCategory
    {
        Vertical,
        Horizontal,
    }

    ScrollBarCategory? _scrollBarCategoryState;
    public ScrollBarCategory? ScrollBarCategoryState
    {
        get => _scrollBarCategoryState;
        set
        {
            _scrollBarCategoryState = value;
            if(value != null)
            {
                if(Categories.ContainsKey("ScrollBarCategory"))
                {
                    var category = Categories["ScrollBarCategory"];
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.ApplyState(state);
                }
                else
                {
                    var category = ((global::Gum.DataTypes.ElementSave)this.Tag).Categories.FirstOrDefault(item => item.Name == "ScrollBarCategory");
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.ApplyState(state);
                }
            }
        }
    }

    OrientationCategory? _orientationCategoryState;
    public OrientationCategory? OrientationCategoryState
    {
        get => _orientationCategoryState;
        set
        {
            _orientationCategoryState = value;
            if(value != null)
            {
                if(Categories.ContainsKey("OrientationCategory"))
                {
                    var category = Categories["OrientationCategory"];
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.ApplyState(state);
                }
                else
                {
                    var category = ((global::Gum.DataTypes.ElementSave)this.Tag).Categories.FirstOrDefault(item => item.Name == "OrientationCategory");
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.ApplyState(state);
                }
            }
        }
    }
    public ContainerRuntime TrackInstance { get; protected set; }
    public ButtonRuntime ThumbInstance { get; protected set; }

    public ScrollBarRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
    {
        if(fullInstantiation)
        {
            var element = ObjectFinder.Self.GetElementSave("Meadow/Controls/ScrollBar");
            element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
        }



    }
    public override void AfterFullCreation()
    {
        if (FormsControl == null)
        {
            FormsControlAsObject = new global::Gum.Forms.Controls.ScrollBar(this);
        }
        TrackInstance = this.GetGraphicalUiElementByName("TrackInstance") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        ThumbInstance = this.GetGraphicalUiElementByName("ThumbInstance") as Assembly_CSharp.Components.Meadow.Controls.ButtonRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
