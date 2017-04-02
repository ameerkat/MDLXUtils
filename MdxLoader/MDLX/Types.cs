using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdxLoader.MDLX
{
    public struct Float2
    {
        public float x;
        public float y;
    }

    public struct Float3
    {
        public float x;
        public float y;
        public float z;
    }

    // This is the same as float 3 but it's more meaningful
    public struct Color
    {
        public float B;
        public float G;
        public float R;
    }

    public struct Float4
    {
        public float x;
        public float y;
        public float z;
        public float w;
    }

    public enum InterpolationType : uint
    {
        None = 0,
        Linear = 1,
        Hermite = 2,
        Bezier = 3
    }

    [Flags]
    public enum NodeFlag : uint
    {
        Helper = 0,
        DontInheritTranslation = 1,
        DontInheritRotation = 2,
        DontInheritScaling = 4,
        Billboarded = 8,
        BillboardedLockX = 16,
        BillboardedLockY = 32,
        BillboardedLockZ = 64,
        CameraAnchored = 128,
        Bone = 256,
        Light = 512,
        EventObject = 1024,
        Attachment = 2048,
        ParticleEmitter = 4096,
        CollisionShape = 8192,
        RibbonEmitter = 16384,
        Unshaded_EmitterUsesMdl = 32768,
        SoftPrimitivesFarZ_EmitterUsesTga = 65536,
        LineEmitter = 131072,
        Unfogged = 262144,
        ModelSpace = 524288,
        XYQuad = 1048576
    }

    public enum SequenceFlags : uint
    {
        Looping = 0,
        NonLooping = 1
    }

    [Flags]
    public enum TextureFlags : uint
    {
        None = 0,
        WrapWidth = 1,
        WrapHeight = 2
    }

    public enum FilterMode : uint
    {
        None = 0,
        Transparent = 1,
        Blend = 2,
        Additive = 3,
        AddAlpha = 4,
        Modulate = 5,
        Modulate2x = 6
    }

    [Flags]
    public enum ShadingFlags : uint
    {
        Unshaded = 1,
        SphereEnvironmentMap = 2,
        Unknown_4 = 4,
        Unknown_8 = 8,
        TwoSided = 16,
        Unfogged = 32,
        NoDepthTest = 64,
        NoDepthSet = 128
    }

    [Flags]
    public enum MaterialFlags : uint
    {
        ConstantColor = 1,
        Unknown_2 = 2,
        Unknown_4 = 4,
        SortPrimitivesNearZ = 8,
        SortPrimitivesFarZ = 16,
        FullResolution = 32
    }
}
