using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdxLoader.MDLX
{
    public static class MdxChunks
    {
        public static class Headers
        {
            public const string Root = "MDLX";
            public const string Version = "VERS";
            public const string Model = "MODL";
            public const string Sequence = "SEQS";
            public const string GlobalSequence = "GLBS";
            public const string Texture = "TEXS";
            public const string Layers = "LAYS";
            public const string Materials = "MTLS";
            public const string TextureAnimation = "TXAN";
            public const string Geoset = "GEOS";

            // Geoset Chunks
            public const string GeosetVertexPosition = "VRTX";
            public const string GeosetVertexNormals = "NRMS";
            public const string GeosetFaceTypeGroups = "PTYP";
            public const string GeosetFaceGroups = "PCNT";
        }
    }

    public static class MdxAnimation
    {
        public static class Headers
        {
            // Geoset
            public const string GeosetTranslation = "KGTR";
            public const string GeosetRotation = "KGRT";
            public const string GeosetScaling = "KGSC";
            public const string GeosetAlpha = "KGAO";
            public const string GeosetColor = "KGAC";

            // Texture
            public const string TextureTranslation = "KTAT";
            public const string TextureRotation = "KTAR";
            public const string TextureScaling = "KTAS";

            // Camera
            public const string CameraPositionTranslation = "KCTR";
            public const string CameraTargetTranslation = "KTTR";
            public const string CameraRotation = "KCRL";

            // Material
            public const string MaterialTextureId = "KMTF";
            public const string MaterialAlpha = "KMTA";

            // Attachment
            public const string AttachmentVisibility = "KATV";

            // Light
            public const string LightVisibility = "KLAV";
            public const string LightColor = "KLAC";
            public const string LightIntensity = "KLAI";
            public const string LightAmbientColor = "KLBC";
            public const string LightAmbientIntensity = "KLBI";

            // Particle Emitters
            public const string ParticleEmitterVisibility = "KPEV";
            public const string ParticleEmitter2Visibility = "KP2V";
            public const string ParticleEmitter2EmissionRate = "KP2E";
            public const string ParticleEmitter2Width = "KP2W";
            public const string ParticleEmitter2Length = "KP2N";
            public const string ParticleEmitter2Speed = "KP2S";

            // Ribbon Emitter
            public const string RibbonEmitterVisibility = "KRVS";
            public const string RibbonEmitterHeightAbove = "KRHA";
            public const string RibbonEmitterHeightBelow = "KRHB";
        }
    }
}
