using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#define DWORD UInt32

namespace MdxLoader.MDLX
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

        // The number of sequences is the ChunkSize / 132
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

        // Number of global sequences is ChunkSize / 4
        public GlobalSequence[] GlobalSequences;
    }

    public struct GlobalSequence
    {
        public UInt32 Duration;
    }

    [Header(MdxChunks.Headers.Texture)]
    public struct TextureChunk
    {
        public UInt32 ChunkSize;
        // Number of textures is ChunkSize / 268
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
        public TextureAnimation TextureAnimation;
    }

    [Header(MdxChunks.Headers.Geoset)]
    public struct GeosetChunk
    {
        public UInt32 ChunkSize;
    }

    public struct Geoset
    {
        public UInt32 InclusiveSize;
         
    }

    public struct GeosetAnimationChunk
    {

    }

    public struct BoneChunk
    {

    }

    public struct LightChunk
    {

    }

    public struct HelperChunk
    {

    }

    public struct AttachmentChunk
    {

    }

    public struct PivotPointChunk
    {

    }

    public struct ParticleEmitterChunk
    {

    }

    public struct ParticleEmitter2Chunk
    {

    }

    public struct RibbonEmitterChunk
    {

    }

    public struct EventObjectChunk
    {

    }

    public struct CameraChunk
    {

    }

    public struct CollisionShapeChunk
    {

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
        public Track<T>[] Tracks;
    }

    public class GeosetAnimation
    {
        [Header(MdxAnimation.Headers.GeosetTranslation)]
        public AnimationChunk<Float3> GeosetTranslation;

        public AnimationChunk<Float4> GeosetRotation;
        public AnimationChunk<Float3> GeosetScaling;
        public AnimationChunk<float> GeosetAlpha;
        public AnimationChunk<Color> GeosetColor;
    }

    public class TextureAnimation
    {
        public AnimationChunk<Float3> TextureTranslation;
        public AnimationChunk<Float4> TextureRotation;
        public AnimationChunk<Float3> TextureScaling;
    }

    public class CameraAnimation
    {
        public AnimationChunk<Float3> CameraPositionTranslation;
        public AnimationChunk<Float3> CameraTargetTranslation;
        public AnimationChunk<float> CameraRotation;
    }

    public class MaterialAnimation
    {
        public AnimationChunk<UInt32> MaterialTextureId;
        public AnimationChunk<float> MaterialAlpha;
    }

    public class AttachmentAnimation
    {
        public AnimationChunk<float> AttachmentVisibility;
    }

    public class LightAnimation
    {
        public AnimationChunk<float> LightVisibility;
        public AnimationChunk<Color> LightColor;
        public AnimationChunk<float> LightIntensity;
        public AnimationChunk<Color> LightAmbientColor;
        public AnimationChunk<float> LightAmbientIntensity;
    }

    public class ParticleEmittersAnimation
    {
        public AnimationChunk<float> ParticleEmitterVisibility;
        public AnimationChunk<float> ParticleEmitter2Visibility;
        public AnimationChunk<float> ParticleEmitter2EmissionRate;
        public AnimationChunk<float> ParticleEmitter2Width;
        public AnimationChunk<float> ParticleEmitter2Length;
        public AnimationChunk<float> ParticleEmitter2Speed;
    }

    public class RibbonEmitterAnimation
    {
        public AnimationChunk<float> RibbonEmitterVisibility;
        public AnimationChunk<float> RibbonEmitterHeightAbove;
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
