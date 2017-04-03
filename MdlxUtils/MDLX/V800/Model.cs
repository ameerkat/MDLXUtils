using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdxLoader.MDLX.V800
{
    // This model is in part based on the MDX file format documentation from Magos
    // for this document refer to MagosMdxFormat.txt and in part from GhostWolf's
    // MDX Specifications thread on hiveworkshop which is here
    // https://www.hiveworkshop.com/threads/mdx-specifications.240487/

    #region Primary Model Chunks

    [Header(MdxChunks.Headers.Root)]
    public struct MdxModel
    {
        public VersionChunk Version;
        public ModelChunk Model;
        public SequenceChunk Sequence;
        public GlobalSequenceChunk GlobalSequence;
        public TextureChunk Texture;
        public TextureAnimationChunk TextureAnimation;
        public GeosetChunk Geoset;
        public GeosetAnimationChunk GeosetAnimation;
        public BoneChunk Bone;
        public LightChunk Light;
        public HelperChunk Helper;
        public AttachmentChunk Attachment;
        public PivotPointChunk PivotPoint;
        public ParticleEmitterChunk ParticleEmitter;
        public ParticleEmitter2Chunk ParticleEmitter2;
        public RibbonEmitterChunk RibbonEmitter;
        public EventObjectChunk EventObject;
        public CameraChunk Camera;
        public CollisionShapeChunk Collision;
    }

    [Header(MdxChunks.Headers.Version)]
    public struct VersionChunk
    {
        public UInt32 ChunkSize;

        [ExpectedDWORDValueAttribute(800)]
        public UInt32 Version; // Currently 800
    }

    [Header(MdxChunks.Headers.Model)]
    public struct ModelChunk
    {
        public UInt32 ChunkSize;

        [ArraySize(80)]
        public Char[] Name;

        [ArraySize(260)]
        public Char[] AnimationFileName;

        public float BoundRadius;
        public Float3 MinimumExtent;
        public Float3 MaximumExtent;
        public UInt32 BlendTime;
    }

    [Header(MdxChunks.Headers.Sequence)]
    public struct SequenceChunk
    {
        public UInt32 ChunkSize;

        [ArraySize("{ChunkSize}/132")]
        public Sequence[] Sequences;
    }

    public struct Sequence
    {
        [ArraySize(80)]
        public Char[] Name;

        public UInt32 IntervalStart;
        public UInt32 IntervalEnd;
        public float MoveSpeed;
        public SequenceFlags Flags;
        public float Rarity;
        public UInt32 SyncPoint;

        public float BoundsRadius;
        public Float3 MinimumExtent;
        public Float3 MaximumExtent;
    }

    [Header(MdxChunks.Headers.GlobalSequence)]
    public struct GlobalSequenceChunk
    {
        public UInt32 ChunkSize;

        [ArraySize("{ChunkSize}/4")]
        public UInt32[] GlobalSequencesByDuration;
    }

    [Header(MdxChunks.Headers.Texture)]
    public struct TextureChunk
    {
        public UInt32 ChunkSize;

        [ArraySize("{ChunkSize}/268")]
        public Texture[] Textures;
    }

    public struct Texture
    {
        public UInt32 ReplaceableId;
        
        [ArraySize(260)]
        public Char[] FileName;
        public TextureFlags Flags;
    }

    [Header(MdxChunks.Headers.TextureAnimation)]
    public struct TextureAnimationChunk
    {
        public UInt32 ChunkSize;
        public TextureAnimationWrapper[] TextureAnimations;
    }

    public struct TextureAnimationWrapper
    {
        public UInt32 InclusiveSize;

        // Parse by header, unordered and optional
        public TextureAnimation TextureAnimation;
    }

    #region Geoset

    [Header(MdxChunks.Headers.Geoset)]
    public struct GeosetChunk
    {
        public UInt32 ChunkSize;
        public Geoset[] Geosets;
    }

    public struct Geoset
    {
        public UInt32 InclusiveSize;

        [Header(MdxChunks.Headers.GeosetVertexPositions)]
        public GeosetEnumerableChunk<Float3> VertexPositions;

        [Header(MdxChunks.Headers.GeosetVertexNormals)]
        public GeosetEnumerableChunk<Float3> VertexNormals;

        [Header(MdxChunks.Headers.GeosetFaceTypeGroups)]
        public GeosetEnumerableChunk<FaceType> FaceTypeGroups;

        [Header(MdxChunks.Headers.GeosetFaceGroups)]
        public GeosetEnumerableChunk<UInt32> FaceGroupsByNumberOfIndices;

        [Header(MdxChunks.Headers.GeosetFaces)]
        public GeosetEnumerableChunk<GeosetFace> Faces;

        [Header(MdxChunks.Headers.GeosetVertexGroups)]
        public GeosetEnumerableChunk<byte> VertexGroupsByMatrixGroup;

        [Header(MdxChunks.Headers.GeosetMatrixGroups)]
        public GeosetEnumerableChunk<UInt32> MatrixGroupsBySize;

        [Header(MdxChunks.Headers.GeosetMatrices)]
        public GeosetEnumerableChunk<UInt32> MatrixIndices;

        public UInt32 MaterialId;
        public UInt32 SelectionGroup;
        public GeosetSelectionFlags SelectionFlags;
        public float BoundsRadius;
        public Float3 MinimumExtent;
        public Float3 MaximumExtent;
        public UInt32 NumberOfExtents;
        [ArraySize("{NumberOfExtents}")]
        public GeosetExtent[] Extents;

        public UInt32 UVASHeader; // "UVAS"
        public UInt32 NumberOfTextureVertexGroups;

        [Header(MdxChunks.Headers.GeosetTextureVertexPositions)]
        public GeosetEnumerableChunk<Float2> TexturePositions;
    }

    public struct GeosetExtent 
    {
        public Float3 MinimumExtent;
        public Float3 MaximumExtent;
    }

    public struct GeosetEnumerableChunk<T> 
    {
        public UInt32 Header;
        public UInt32 Length;

        [ArraySize("{Length}")]
        public T[] Data;
    }

    public struct GeosetFace
    {
        public UInt16 Index1;
        public UInt16 Index2;
        public UInt16 Index3;
    }

    #endregion Geoset

    [Header(MdxChunks.Headers.GeosetAnimations)]
    public struct GeosetAnimationChunk
    {
        public UInt32 ChunkSize;
        [ArraySize("{ChunkSize}")]
        public GeosetAnimationDefinition[] GeosetAnimations;
    }

    public struct GeosetAnimationDefinition
    {
        public UInt32 InclusiveSize;

        public float Alpha;
        public GeosetAnimationFlags Flags;
        public Color Color;
        public UInt32 GeosetId;

        public GeosetAnimation AnimationData;
    }

    [Header(MdxChunks.Headers.Bone)]
    public struct BoneChunk
    {
        public UInt32 ChunkSize;

        // TODO Need a way to calculate the length, or just do it with a list
        public Bone[] Bones;
    }

    public struct Bone
    {
        public Node Node;
        public UInt32 GeosetId;
        public UInt32 GeosetAnimationId;
    }

    [Header(MdxChunks.Headers.Light)]
    public struct LightChunk
    {
        public UInt32 ChunkSize;

        public Light[] Lights;
    }

    public struct Light
    {
        public UInt32 InclusiveSize;
        public Node Node;
        public LightType Type;
        public UInt32 AttenuationStart;
        public UInt32 AttenuationEnd;
        public Color Color;
        public float Intensity;
        public Color AmbientColor;
        public float AmbientIntensity;

        public LightAnimation AnimationData;
    }

    [Header(MdxChunks.Headers.Helper)]
    public struct HelperChunk
    {
        public UInt32 ChunkSize;

        // Chunk size / node size
        public Node[] Helpers;
    }

    [Header(MdxChunks.Headers.Attachment)]
    public struct AttachmentChunk
    {
        public UInt32 ChunkSize;
        public Attachment[] Attachments;
    }

    public struct Attachment
    {
        public UInt32 InclusiveSize;
        public Node Node;

        [ArraySize(260)]
        public Char[] Path;

        // 0-indexed
        public UInt32 AttachmentId;

        public AttachmentAnimation AnimationData;
    }

    [Header(MdxChunks.Headers.Pivot)]
    public struct PivotPointChunk
    {
        public UInt32 ChunkSize;
        
        [ArraySize("{ChunkSize}/12")]
        public Float3[] Positions;
    }

    [Header(MdxChunks.Headers.ParticleEmitter)]
    public struct ParticleEmitterChunk
    {
        public UInt32 ChunkSize;
        public ParticleEmitter[] Emitters;
    }

    public struct ParticleEmitter
    {
        public UInt32 InclusiveSize;
        public Node Node;
        public float EmissionRate;
        public float Gravity;
        public float Longitude;
        public float Latitude;

        [ArraySize(260)]
        public Char[] SpawnModelFileName;

        float LifeSpan;
        float InitialVelocity;

        public ParticleEmittersAnimation AnimationData;
    }

    [Header(MdxChunks.Headers.ParticleEmitter2)]
    public struct ParticleEmitter2Chunk
    {
        public UInt32 ChunkSize;
        public ParticleEmitter2[] Emitters;
    }

    public struct ParticleEmitter2 
    {
        public UInt32 InclusiveSize;
        public Node Node;
        public float Speed;
        public float Variation;
        public float Gravity;
        public float Lifespan;
        public float EmissionRate;
        public float Length;
        public float Width;

        public ParticleEmitter2FilterMode FilterMode;
        public UInt32 Rows;
        public UInt32 Columns;
        public ParticleEmitter2HeadOrTail HeadOrTail;
        public float TailLength;
        public float Time;
        
        [ArraySize(3)]
        public Color[] SegmentColor;

        [ArraySize(3)]
        public Byte[] SegmentAlpha;

        [ArraySize(3)]
        public float[] SegmentScaling;

        public UInt32 HeadIntervalStart;
        public UInt32 HeadIntervalEnd;
        public UInt32 HeadIntervalRepeat;
        public UInt32 HeadDecayIntervalStart;
        public UInt32 HeadDecayIntervalEnd;
        public UInt32 HeadDecayIntervalRepeat;
        public UInt32 TailIntervalStart;
        public UInt32 TailIntervalEnd;
        public UInt32 TailIntervalRepeat;
        public UInt32 TailDecayIntervalStart;
        public UInt32 TailDecayIntervalEnd;
        public UInt32 TailDecayIntervalRepeat;

        public UInt32 TextureId;
        public ParticleEmitter2Squirt Squirt;

        public UInt32 PriorityPlane;
        public UInt32 ReplaceableId;

        public ParticleEmittersAnimation AnimationData;
    }

    [Header(MdxChunks.Headers.RibbonEmitter)]
    public struct RibbonEmitterChunk
    {
        public UInt32 ChunkSize;
        public RibbonEmitter[] Emitters;
    }

    public struct RibbonEmitter
    {
        public UInt32 InclusiveSize;
        public Node Node;

        public float HeightAbove;
        public float HeightBelow;
        public float Alpha;
        public Color Color;
        public float LifeSpan;

        public UInt32 TextureSlot;

        public UInt32 EmissionRate;
        public UInt32 Rows;
        public UInt32 Columns;
        public UInt32 MaterialId;
        public float Gravity;

        public RibbonEmitterAnimation AnimationData;
    }

    [Header(MdxChunks.Headers.EventObject)]
    public struct EventObjectChunk
    {
        public UInt32 ChunkSize;
        public EventObject[] EventObjects;
    }

    [Header(MdxChunks.Headers.Tracks)]
    public struct Tracks
    {
        public UInt32 NumberOfTracks;
        public UInt32 GlobalSequenceId;
        [ArraySize("{NumberOfTracks}")]
        public UInt32[] TracksByTime;
    }

    public struct EventObject
    {
        public Node Node;
        public Tracks Tracks;
    }

    [Header(MdxChunks.Headers.Camera)]
    public struct CameraChunk
    {
        public UInt32 ChunkSize;
        public Camera[] Cameras;
    }

    public struct Camera
    {
        public UInt32 InclusiveSize;

        [ArraySize(80)]
        public Char[] Name;

        public Float3 Position;
        public UInt32 FieldOfView;
        public UInt32 FarClippingPlane;
        public UInt32 NearClippingPlane;
        public Float3 TargetPosition;

        public CameraAnimation AnimationData;
    }

    [Header(MdxChunks.Headers.CollisionShape)]
    public struct CollisionShapeChunk
    {
        public UInt32 ChunkSize;
        public CollisionShape[] Shapes;
    }

    public struct CollisionShape
    {
        public Node Node;
        public CollisionShapeType Type;
        // 1 if Box, 2 if Sphere
        public Float3[] Positions;

        // only if type 2
        float BoundsRadius;
    }

    #endregion Primary Model Chunks

    #region Animation

    public struct Track<T>
    {
        public UInt32 Time;
        public T Value;
        public T InTan;
        public T OutTan;
    }

    public struct AnimationChunk<T>
    {
        public UInt32 Header;
        public UInt32 NumberOfTracks;
        public InterpolationType InterpolationType;
        public UInt32 GlobalSequenceId;

        [ArraySize("{NumberOfTracks}")]
        public Track<T>[] Tracks;
    }

    public class GeosetAnimation
    {
        [Header(MdxAnimation.Headers.GeosetTranslation)]
        public AnimationChunk<Float3> GeosetTranslation;

        [Header(MdxAnimation.Headers.GeosetRotation)]
        public AnimationChunk<Float4> GeosetRotation;

        [Header(MdxAnimation.Headers.GeosetScaling)]
        public AnimationChunk<Float3> GeosetScaling;

        [Header(MdxAnimation.Headers.GeosetAlpha)]
        public AnimationChunk<float> GeosetAlpha;

        [Header(MdxAnimation.Headers.GeosetColor)]
        public AnimationChunk<Color> GeosetColor;
    }

    public class TextureAnimation
    {
        [Header(MdxAnimation.Headers.TextureTranslation)]
        public AnimationChunk<Float3> TextureTranslation;

        [Header(MdxAnimation.Headers.TextureRotation)]
        public AnimationChunk<Float4> TextureRotation;

        [Header(MdxAnimation.Headers.TextureScaling)]
        public AnimationChunk<Float3> TextureScaling;
    }

    public class CameraAnimation
    {
        [Header(MdxAnimation.Headers.CameraPositionTranslation)]
        public AnimationChunk<Float3> CameraPositionTranslation;

        [Header(MdxAnimation.Headers.CameraTargetTranslation)]
        public AnimationChunk<Float3> CameraTargetTranslation;

        [Header(MdxAnimation.Headers.CameraRotation)]
        public AnimationChunk<float> CameraRotation;
    }

    public class MaterialAnimation
    {
        [Header(MdxAnimation.Headers.MaterialTextureId)]
        public AnimationChunk<UInt32> MaterialTextureId;

        [Header(MdxAnimation.Headers.MaterialAlpha)]
        public AnimationChunk<float> MaterialAlpha;
    }

    public class AttachmentAnimation
    {
        [Header(MdxAnimation.Headers.AttachmentVisibility)]
        public AnimationChunk<float> AttachmentVisibility;
    }

    public class LightAnimation
    {
        [Header(MdxAnimation.Headers.LightVisibility)]
        public AnimationChunk<float> LightVisibility;

        [Header(MdxAnimation.Headers.LightColor)]
        public AnimationChunk<Color> LightColor;

        [Header(MdxAnimation.Headers.LightIntensity)]
        public AnimationChunk<float> LightIntensity;

        [Header(MdxAnimation.Headers.LightAmbientColor)]
        public AnimationChunk<Color> LightAmbientColor;

        [Header(MdxAnimation.Headers.LightAmbientIntensity)]
        public AnimationChunk<float> LightAmbientIntensity;
    }

    public class ParticleEmittersAnimation
    {
        [Header(MdxAnimation.Headers.ParticleEmitterVisibility)]
        public AnimationChunk<float> ParticleEmitterVisibility;

        [Header(MdxAnimation.Headers.ParticleEmitter2Visibility)]
        public AnimationChunk<float> ParticleEmitter2Visibility;

        [Header(MdxAnimation.Headers.ParticleEmitter2EmissionRate)]
        public AnimationChunk<float> ParticleEmitter2EmissionRate;

        [Header(MdxAnimation.Headers.ParticleEmitter2Width)]
        public AnimationChunk<float> ParticleEmitter2Width;

        [Header(MdxAnimation.Headers.ParticleEmitter2Length)]
        public AnimationChunk<float> ParticleEmitter2Length;

        [Header(MdxAnimation.Headers.ParticleEmitter2Speed)]
        public AnimationChunk<float> ParticleEmitter2Speed;
    }

    public class RibbonEmitterAnimation
    {
        [Header(MdxAnimation.Headers.RibbonEmitterVisibility)]
        public AnimationChunk<float> RibbonEmitterVisibility;

        [Header(MdxAnimation.Headers.RibbonEmitterHeightAbove)]
        public AnimationChunk<float> RibbonEmitterHeightAbove;

        [Header(MdxAnimation.Headers.RibbonEmitterHeightBelow)]
        public AnimationChunk<float> RibbonEmitterHeightBelow;
    }

    #endregion Animation

    public struct Node
    {
        public UInt32 InclusiveSize;

        [ArraySize(80)]
        public Char[] Name;
        public UInt32 ObjectId;
        public UInt32 ParentId;
        public NodeFlag Flags;

        public GeosetAnimation AnimationData;
    }

    public struct LayerChunk
    {
        public UInt32 NumberOfLayers;
    }

    public struct Layer
    {
        public UInt32 InclusiveSize;
        public FilterMode FilterMode;
        public ShadingFlags ShadingFlags;
        public UInt32 TextureId;
        public UInt32 TextureAnimationId;
        public UInt32 CoordId;
        public float Alpha;

        MaterialAnimation AnimationData;
    }

    [Header(MdxChunks.Headers.Materials)]
    public struct MaterialChunk
    {
        public UInt32 ChunkSize;

        // Use ChunkSize - InclusiveSize to parse
        public Material[] Materials;
    }

    public struct Material
    {
        public UInt32 InclusiveSize;
        public UInt32 PriorityPlane;
        public MaterialFlags Flags;

        public LayerChunk Layers;
    }
}
