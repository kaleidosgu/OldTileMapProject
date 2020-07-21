using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.RepresentationModel;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System;
public class EventStreamParserAdapter : IParser
{
    private readonly IEnumerator<ParsingEvent> enumerator;

    public EventStreamParserAdapter(IEnumerable<ParsingEvent> events)
    {
        enumerator = events.GetEnumerator();
    }

    public ParsingEvent Current
    {
        get
        {
            return enumerator.Current;
        }
    }

    public bool MoveNext()
    {
        return enumerator.MoveNext();
    }
}
public static class YamlNodeToEventStreamConverter
{
    public static IEnumerable<ParsingEvent> ConvertToEventStream(YamlStream stream)
    {
        yield return new StreamStart();
        foreach (var document in stream.Documents)
        {
            foreach (var evt in ConvertToEventStream(document))
            {
                yield return evt;
            }
        }
        yield return new StreamEnd();
    }

    public static IEnumerable<ParsingEvent> ConvertToEventStream(YamlDocument document)
    {
        yield return new DocumentStart();
        foreach (var evt in ConvertToEventStream(document.RootNode))
        {
            yield return evt;
        }
        yield return new DocumentEnd(false);
    }

    public static IEnumerable<ParsingEvent> ConvertToEventStream(YamlNode node)
    {
        var scalar = node as YamlScalarNode;
        if (scalar != null)
        {
            return ConvertToEventStream(scalar);
        }

        var sequence = node as YamlSequenceNode;
        if (sequence != null)
        {
            return ConvertToEventStream(sequence);
        }

        var mapping = node as YamlMappingNode;
        if (mapping != null)
        {
            return ConvertToEventStream(mapping);
        }

        throw new NotSupportedException(string.Format("Unsupported node type: {0}", node.GetType().Name));
    }

    private static IEnumerable<ParsingEvent> ConvertToEventStream(YamlScalarNode scalar)
    {
        yield return new Scalar(scalar.Anchor, scalar.Tag, scalar.Value, scalar.Style, false, false);
    }

    private static IEnumerable<ParsingEvent> ConvertToEventStream(YamlSequenceNode sequence)
    {
        yield return new SequenceStart(sequence.Anchor, sequence.Tag, false, sequence.Style);
        foreach (var node in sequence.Children)
        {
            foreach (var evt in ConvertToEventStream(node))
            {
                yield return evt;
            }
        }
        yield return new SequenceEnd();
    }

    private static IEnumerable<ParsingEvent> ConvertToEventStream(YamlMappingNode mapping)
    {
        yield return new MappingStart(mapping.Anchor, mapping.Tag, false, mapping.Style);
        foreach (var pair in mapping.Children)
        {
            foreach (var evt in ConvertToEventStream(pair.Key))
            {
                yield return evt;
            }
            foreach (var evt in ConvertToEventStream(pair.Value))
            {
                yield return evt;
            }
        }
        yield return new MappingEnd();
    }
}
public class NewSerialize : MonoBehaviour
{
    private void newSeri()
    {
        string input = TestYaml.GetFileContextByPath("Assets/TileInfo/Brick.prefab");
        var stream = new YamlStream();
        stream.Load(new StringReader(input));

        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(new CamelCaseNamingConvention())
            .Build();

        //var prefs = deserializer.Deserialize<YOUR_TYPE>(
        //    new EventStreamParserAdapter(
        //        YamlNodeToEventStreamConverter.ConvertToEventStream(stream)
        //    )
        //);

        int a = 0;
    }

    [ContextMenu("SerializeTile")]
    private void testTmxClass()
    {
        SpriteDataClass _ca = new SpriteDataClass();
        _ca.TextureImporter = new TextureImporterClass();
        _ca.TextureImporter.internalIDToNameTable = new InternalTableElement[2];
        _ca.TextureImporter.internalIDToNameTable[0] = new InternalTableElement();
        _ca.TextureImporter.internalIDToNameTable[0].first = new Dictionary<string, string>();
        _ca.TextureImporter.internalIDToNameTable[0].first.Add("213", "7474831727883183029");
        _ca.TextureImporter.internalIDToNameTable[0].second = "tmw_desert_spacing_0";
        _ca.TextureImporter.internalIDToNameTable[1] = new InternalTableElement();
        _ca.TextureImporter.internalIDToNameTable[1].first = new Dictionary<string, string>();
        _ca.TextureImporter.internalIDToNameTable[1].first.Add("213", "7474831727883183029");
        _ca.TextureImporter.internalIDToNameTable[1].second = "tmw_desert_spacing_0";

        var serializer = new SerializerBuilder().Build();
        var yaml = serializer.Serialize(_ca);
        int a = 0;
    }

    [ContextMenu("SerializeData")]
    private void SerializeData()
    {
        var stream = new YamlStream();
        string input = TestYaml.GetFileContextByPath("Assets/Res/spriteRelate.txt");
        stream.Load(new StringReader(input));

        var deserializer = new DeserializerBuilder()
    .WithNamingConvention(new NullNamingConvention())
    .Build();

        SpriteDataClass order = deserializer.Deserialize<SpriteDataClass>(input);
        int a = 0;
    }
}
