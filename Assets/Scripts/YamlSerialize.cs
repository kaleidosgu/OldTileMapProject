using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YamlDotNet.RepresentationModel;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using UnityEngine.Tilemaps;
using System.Text;
using System.Xml;
public class YamlSerialize : MonoBehaviour
{
    //"Assets/Res/spriteRelate.txt"
    public string strSpriteRelate;
    public Tilemap[] LstTiledMap;
    public string GenerateFileName;
    public string strTsxFileName;
    private SpriteDataClass m_sprData;
    private TileArray tileArray;
    private TmxXmlSerialize m_tmxSerialize;
    [ContextMenu("SerializeSpriteRelate")]
    private void SerializeData()
    {
        var stream = new YamlStream();
        string input = GetFileContextByPath(strSpriteRelate);
        stream.Load(new StringReader(input));

        var deserializer = new DeserializerBuilder()
    .WithNamingConvention(new NullNamingConvention())
    .Build();

        m_sprData = deserializer.Deserialize<SpriteDataClass>(input);
        m_sprData.BuildDic();
        int a = 0;
    }

    //"Assets/Res/spriteFileData.txt"
    public string strSpriteFile;
    [ContextMenu("SerializeTileFile")]
    private void SerializeTileFile()
    {
        var stream = new YamlStream();
        string input = GetFileContextByPath(strSpriteFile);
        stream.Load(new StringReader(input));

        var deserializer = new DeserializerBuilder()
    .WithNamingConvention(new NullNamingConvention())
    .Build();

        tileArray = deserializer.Deserialize<TileArray>(input);
        tileArray.BuildMapper();
        int a = 0;
    }

    public static string GetFileContextByPath(string strPath)
    {
        StreamReader reader = new StreamReader(strPath);
        string str = reader.ReadToEnd();
        reader.Close();
        return str;
    }

    [ContextMenu("GenerateIt")]
    void GenerateXmlFileFromTiledMap()
    {
        SerializeData();
        SerializeTileFile();
        int nFileIdx = 0;
        foreach (Tilemap _mapTile in LstTiledMap)
        {
            if (_mapTile != null)
            {
                string strFileName = string.Format("{0}_{1}", GenerateFileName, nFileIdx.ToString("000"));
                _generateOneTileMap(_mapTile, strFileName);
                nFileIdx++;
            }
        }
    }
    private void _generateOneTileMap(Tilemap _tileMap, string strGenerateFileName)
    {
        BoundsInt bounds = _tileMap.cellBounds;
        TileBase[] allTiles = _tileMap.GetTilesBlock(bounds);

        StringBuilder strBuf = new StringBuilder();
        StringBuilder strNotInclude = new StringBuilder();
        HashSet<string> setNotInclude = new HashSet<string>();
        for (int y = bounds.size.y - 1; y >= 0; y--)
        {
            for (int x = 0; x < bounds.size.x; x++)
            {
                int nIdx = y * bounds.size.x + x;
                TileBase _base = allTiles[nIdx];
                if (_base != null)
                {
                    if (_base is Tile)
                    {
                        string outFileID = "";
                        if( m_sprData.TryGetFileID(_base.name,out outFileID) == true)
                        {
                            string strIdx = "";
                            if(tileArray.TryGetIndex(outFileID, out strIdx) == true)
                            {
                                strBuf.Append(strIdx);
                            }
                        }
                    }
                    else
                    {
                        Debug.Assert(false);
                    }
                }
                else
                {
                    strBuf.Append("0");
                }

                if (x == bounds.size.x - 1 && y == 0)
                {
                }
                else
                {
                    strBuf.Append(",");
                }
            }
            strBuf.Append("\n");
        }
        Debug.Log(string.Format("[width:{0}][height:{1}]", _tileMap.size.x, _tileMap.size.y));
        Debug.Log(strBuf.ToString());
        foreach (string str in setNotInclude)
        {
            strNotInclude.Append(string.Format("{0}, ", str));
        }
        Debug.Log(strNotInclude.ToString());

        //_makeXmlFile(bounds.size.x, bounds.size.y, strBuf.ToString(), strGenerateFileName);
        _makeFileByData(strBuf.ToString(), strGenerateFileName, bounds.size.x, bounds.size.y);
    }
    private void _makeXmlFile(int width, int height, string strContent, string strFileName, int tilewidth = 32, int tileheight = 32, int infinite = 0, int nextlayerid = 2, int nextobjectid = 1)
    {

        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.IndentChars = "\t";
        XmlWriter writer = XmlWriter.Create(strFileName + ".tmx", settings);

        //map element
        writer.WriteStartElement("map");
        writer.WriteStartAttribute("version");
        writer.WriteValue("1.2");
        writer.WriteEndAttribute();

        writer.WriteStartAttribute("tiledversion");
        writer.WriteValue("1.3.1");
        writer.WriteEndAttribute();

        writer.WriteStartAttribute("orientation");
        writer.WriteValue("orthogonal");
        writer.WriteEndAttribute();

        writer.WriteStartAttribute("renderorder");
        writer.WriteValue("right-down");
        writer.WriteEndAttribute();

        writer.WriteStartAttribute("compressionlevel");
        writer.WriteValue("-1");
        writer.WriteEndAttribute();


        writer.WriteStartAttribute("width");
        writer.WriteValue(width.ToString());
        writer.WriteEndAttribute();

        writer.WriteStartAttribute("height");
        writer.WriteValue(height.ToString());
        writer.WriteEndAttribute();

        writer.WriteStartAttribute("tilewidth");
        writer.WriteValue(tilewidth.ToString());
        writer.WriteEndAttribute();

        writer.WriteStartAttribute("tileheight");
        writer.WriteValue(tileheight.ToString());
        writer.WriteEndAttribute();

        writer.WriteStartAttribute("infinite");
        writer.WriteValue(infinite.ToString());
        writer.WriteEndAttribute();

        writer.WriteStartAttribute("nextlayerid");
        writer.WriteValue(nextlayerid.ToString());
        writer.WriteEndAttribute();

        writer.WriteStartAttribute("nextobjectid");
        writer.WriteValue(nextobjectid.ToString());
        writer.WriteEndAttribute();

        //tileset
        writer.WriteStartElement("tileset");
        writer.WriteStartAttribute("firstgid");
        writer.WriteValue("1");
        writer.WriteEndAttribute();

        writer.WriteStartAttribute("source");
        writer.WriteValue(strTsxFileName);
        writer.WriteEndAttribute();

        writer.WriteEndElement();//tileset end

        //layer
        writer.WriteStartElement("layer");
        writer.WriteStartAttribute("id");
        writer.WriteValue("1");
        writer.WriteEndAttribute();

        writer.WriteStartAttribute("name");
        writer.WriteValue("Tile Layer 1");
        writer.WriteEndAttribute();

        writer.WriteStartAttribute("width");
        writer.WriteValue(width.ToString());
        writer.WriteEndAttribute();

        writer.WriteStartAttribute("height");
        writer.WriteValue(height.ToString());
        writer.WriteEndAttribute();

        //< data encoding = "csv" >
        writer.WriteStartElement("data");
        writer.WriteStartAttribute("encoding");
        writer.WriteValue("csv");
        writer.WriteEndAttribute();
        writer.WriteString(strContent);
        writer.WriteEndElement();//data end

        writer.WriteEndElement();//layer end


        writer.WriteEndElement();//map end
        writer.Flush();
        writer.Close();
    }

    private void _makeFileByData(string strContent, string strFileName,int width, int height)
    {
        m_tmxSerialize = GetComponent<TmxXmlSerialize>();
        m_tmxSerialize.TemplateMap.layer.data.tileIdxData = strContent;
        m_tmxSerialize.TemplateMap.layer.width = width;
        m_tmxSerialize.TemplateMap.layer.height = height;
        m_tmxSerialize.TemplateMap.width = width;
        m_tmxSerialize.TemplateMap.height = height;
        m_tmxSerialize.SerializeXmlByData(m_tmxSerialize.TemplateMap, strFileName );
    }
}
