using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Samples.Helpers;
using YamlDotNet.Serialization.NamingConventions;


public class TestDocument : MonoBehaviour
{
    [ContextMenu("SerializeTile")]
    private void SerializeTile()
    {
        TileArray TileDataArray = new TileArray();
        TileDataArray.m_TileSpriteArray = new TileData[2];
        for ( int nIdx = 0; nIdx < 2; nIdx++ )
        {
            TileDataArray.m_TileSpriteArray[nIdx] = new TileData();
            TileDataArray.m_TileSpriteArray[nIdx].m_RefCount = "1";
            TileDataArray.m_TileSpriteArray[nIdx].m_Data = new TileSprite();
            TileDataArray.m_TileSpriteArray[nIdx].m_Data.fileID = "7474831727883183029";
            TileDataArray.m_TileSpriteArray[nIdx].m_Data.guid = "a14ddc1346027884da625e655cf4ccca";
            TileDataArray.m_TileSpriteArray[nIdx].m_Data.type = "3";
        }
        var serializer = new SerializerBuilder().Build();
        var yaml = serializer.Serialize(TileDataArray);
        int a = 0;
    }
    [ContextMenu("DeserializeTile")]
    private void DeserializeTile()
    {

        string strDes = @"
  m_TileSpriteArray:
  - m_RefCount: 1
    m_Data:
      fileID: 7474831727883183029
      guid: a14ddc1346027884da625e655cf4ccca
      type: 3
  - m_RefCount: 1
    m_Data:
      fileID: 7474831727883183029
      guid: a14ddc1346027884da625e655cf4ccca
      type: 3
  dda:
    1: 1
    2: 2
";
        var input = new StringReader(strDes);

        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(new NullNamingConvention())
            .Build();

        TileArray order = deserializer.Deserialize<TileArray>(input);
        int a = 0;
    }
}
