using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Text;
using System.Xml;
[System.Serializable]
public class TileReplace
{
    public string strSource;
    public string strReplace;
}
public class TileToXml : MonoBehaviour
{
    public List<Tilemap> LstTiledMap;
    public List<TileReplace> LstTileReplace;
    public bool ReplaceTag;
    public string GenerateFileName;
    private Dictionary<string, string> m_dicCacheReplace;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void _Cache()
    {
        if (ReplaceTag == true)
        {
            m_dicCacheReplace = new Dictionary<string, string>();
            foreach (TileReplace str in LstTileReplace)
            {
                if (m_dicCacheReplace.ContainsKey(str.strSource) != true)
                {
                    if (m_dicCacheReplace.ContainsValue(str.strReplace) != true)
                    {
                        m_dicCacheReplace[str.strSource] = str.strReplace;
                    }
                    else
                    {
                        Debug.Assert(false, string.Format("contain value"));
                    }
                }
                else
                {
                    Debug.Assert(false, string.Format("contain key"));
                }
            }
        }
    }
    [ContextMenu("GenerateIt")]
    void GenerateXmlFileFromTiledMap()
    {
        _Cache();

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
    private void _makeXmlFile(int width, int height, string strContent, string strFileName, int tilewidth = 16, int tileheight = 16, int infinite = 0, int nextlayerid = 2, int nextobjectid = 1)
    {
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.IndentChars = "\t";
        if (strFileName.Length == 0)
        {
            strFileName = "tempXml";
        }
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
        writer.WriteValue("MeowTileSet.tsx");
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
                        strBuf.Append("1");

                        Tile _tile = (_base) as Tile;

                        Debug.Log(_tile.sprite.name);
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
                    strBuf.Append(", ");
                }
            }
            strBuf.Append("\n");
        }
        foreach (string str in setNotInclude)
        {
            strNotInclude.Append(string.Format("{0}, ", str));
        }

        _makeXmlFile(bounds.size.x, bounds.size.y, strBuf.ToString(), strGenerateFileName);
    }
}
