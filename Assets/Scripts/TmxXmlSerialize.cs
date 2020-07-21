using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
namespace TmxTile
{
    [System.Serializable]
    public class map
    {
        [XmlAttribute("version")]
        public string version;
        [XmlAttribute("tiledversion")]
        public string tiledversion;
        [XmlAttribute("orientation")]
        public string orientation;
        [XmlAttribute("renderorder")]
        public string renderorder;
        [XmlAttribute("compressionlevel")]
        public int compressionlevel;
        [XmlAttribute("width")]
        public int width;
        [XmlAttribute("height")]
        public int height;
        [XmlAttribute("tilewidth")]
        public int tilewidth;
        [XmlAttribute("tileheight")]
        public int tileheight;
        [XmlAttribute("infinite")]
        public int infinite;
        [XmlAttribute("nextlayerid")]
        public int nextlayerid;
        [XmlAttribute("nextobjectid")]
        public int nextobjectid;
        public TileSetXml tileset;
        public LayerXml layer;
    }

    [System.Serializable]
    public class LayerXml
    {
        //<layer id = "1" name="Tile Layer 1" width="32" height="32">
        [XmlAttribute("id")]
        public int id;
        [XmlAttribute("name")]
        public string name;
        [XmlAttribute("width")]
        public int width;
        [XmlAttribute("height")]
        public int height;
        public LayerDataXml data;
    }

    [System.Serializable]
    public class LayerDataXml
    {
        //<data encoding = "csv" >
        [XmlAttribute("encoding")]
        public string encoding;
        [XmlText]
        public string tileIdxData;
    }

    [System.Serializable]
    public class TileSetXml
    {
        //<tileset firstgid="1" source="sprite_palette.tsx"/>
        [XmlAttribute("firstgid")]
        public int firstgid;
        [XmlAttribute("source")]
        public string source;
    }
}
public class TmxXmlSerialize : MonoBehaviour
{
    public TmxTile.map TemplateMap;
    [ContextMenu("SerializeXml")]
    private void SerializeXml()
    {
        TmxTile.map tmxTileData = new TmxTile.map();
        tmxTileData.version = "1.2";
        tmxTileData.tiledversion = "1.3.2";
        tmxTileData.orientation = "orthogonal";
        tmxTileData.renderorder = "right-down";
        tmxTileData.compressionlevel = -1;

        tmxTileData.width = 32;
        tmxTileData.height = 32;
        tmxTileData.tilewidth = 32;
        tmxTileData.tileheight = 32;
        tmxTileData.infinite = 0;
        tmxTileData.nextlayerid = 2;
        tmxTileData.nextobjectid = 1;

        tmxTileData.tileset = new TmxTile.TileSetXml();
        tmxTileData.tileset.firstgid = 1;
        tmxTileData.tileset.source = "sprite_palette.tsx";
        tmxTileData.layer = new TmxTile.LayerXml();
        tmxTileData.layer.id = 1;
        tmxTileData.layer.name = "Tile Layer 1";
        tmxTileData.layer.width = 32;
        tmxTileData.layer.height = 32;
        tmxTileData.layer.data = new TmxTile.LayerDataXml();
        tmxTileData.layer.data.encoding = "csv";
        tmxTileData.layer.data.tileIdxData = @"
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,1,2,2,2,2,2,2,2,2,2,2,2,3,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,9,10,10,10,10,10,10,10,10,10,10,10,11,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,17,18,18,18,18,18,18,18,18,18,18,18,19,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,1,2,3,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,3,0,0,0,0,0,0,
0,0,0,0,0,0,0,9,10,11,0,0,0,0,0,0,0,0,0,0,0,0,0,9,10,11,0,0,0,0,0,0,
0,0,0,0,0,0,0,9,10,11,0,0,0,0,0,0,0,0,0,0,0,0,0,9,10,11,0,0,0,0,0,0,
0,0,0,0,0,0,0,9,10,11,0,0,0,0,0,0,0,0,0,0,0,0,0,9,10,11,0,0,0,0,0,0,
0,0,0,0,0,0,0,9,10,11,0,0,0,0,0,0,0,0,0,0,0,0,0,9,10,11,0,0,0,0,0,0,
0,0,0,0,0,0,0,9,10,11,0,0,0,0,0,0,0,0,0,0,0,0,0,9,10,11,0,0,0,0,0,0,
0,0,0,0,0,0,0,9,10,28,2,2,2,2,2,2,2,2,2,2,2,2,2,29,10,11,0,0,0,0,0,0,
0,0,0,0,0,0,0,9,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,11,0,0,0,0,0,0,
0,0,0,0,0,0,0,17,18,18,18,18,18,18,18,18,18,18,18,18,18,18,18,18,18,19,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,9,10,11,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,9,10,11,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,1,2,2,2,2,29,10,28,2,2,2,2,3,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,9,10,10,10,10,10,10,10,10,10,10,10,11,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,17,18,18,18,18,18,18,18,18,18,18,18,19,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
";
        

        SerializeXmlByData(TemplateMap, "guns.xml");
    }

    public void SerializeXmlByData(TmxTile.map _mapData,string strXmlFileName)
    {
        XmlOperation.Serialize(_mapData, strXmlFileName);
    }
}
