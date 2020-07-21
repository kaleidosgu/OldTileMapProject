using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TileSprite
{
    public string fileID { get; set; }
    public string guid { get; set; }
    public string type { get; set; }

}
public class TileData
{
    public string m_RefCount { get; set; }
    public TileSprite m_Data { get; set; }
}
public class TileArray
{
    public TileData[] m_TileSpriteArray { get; set; }

    private Dictionary<string, string> m_strMap;
    public void BuildMapper()
    {
        m_strMap = new Dictionary<string, string>();
        int nIdx = 1;
        foreach(TileData _data in m_TileSpriteArray)
        {
            string strIdx = string.Format("{0}",nIdx);
            m_strMap.Add(_data.m_Data.fileID, strIdx);
            nIdx++;
        }
    }
    public bool TryGetIndex(string strKey, out string strValue)
    {
        return m_strMap.TryGetValue(strKey, out strValue);
    }
}
//TextureImporter:
//  internalIDToNameTable:
//  - first:
//      213: 7474831727883183029
//    second: tmw_desert_spacing_0

public class internalIDToNameTableClass
{
    public Dictionary<string, string> first { get; set; }
    public string second { get; set; }
}

//TextureImporter:
//  internalIDToNameTable:
//  - first:
//      213: 7474831727883183029
//    second: tmw_desert_spacing_0
public class InternalTableElement
{
    public Dictionary<string, string> first { get; set; }
    public string second { get; set; }
}

public class TextureImporterClass
{
    public InternalTableElement[] internalIDToNameTable { get; set; }

}

public class SpriteDataClass
{
    public TextureImporterClass TextureImporter { get; set; }
    private Dictionary<string, string> m_buildDic;
    public void BuildDic()
    {
        m_buildDic = new Dictionary<string, string>();

        foreach (InternalTableElement element in TextureImporter.internalIDToNameTable)
        {
            Dictionary<string, string> _first = element.first;

            foreach (KeyValuePair<string, string> entry in _first)
            {
                m_buildDic.Add(element.second, entry.Value);
            }
        }
    }
    public bool TryGetFileID(string strSpriteName, out string strFile)
    {
        return m_buildDic.TryGetValue(strSpriteName, out strFile);
    }
}