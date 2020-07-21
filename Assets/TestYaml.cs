using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
public class TestYaml : MonoBehaviour
{
    //private readonly ITestOutputHelper output;
    // Start is called before the first frame update
    public GameObject prefabTest;

    //"Assets/TileInfo/tmw_desert_spacing.png.meta";
    public string PathOfSprite;

    [ContextMenu("GenerateIt")]
    void _CacheSpriteFileID()
    {
        Dictionary<string, string> _spriteFileID = new Dictionary<string, string>();
        if(PathOfSprite == null || PathOfSprite.Length == 0)
        {
            //PathOfSprite = "Assets/TileInfo/tmw_desert_spacing.png.meta";
        }
        string parsePath = GetFileContextByPath(PathOfSprite);
        // Setup the input
        var input = new StringReader(parsePath);

        var yaml = new YamlStream();
        yaml.Load(input);

        if(yaml.Documents.Count > 0)
        {
            // Examine the stream
            YamlMappingNode mapping =(YamlMappingNode)yaml.Documents[0].RootNode;

            foreach(KeyValuePair<YamlNode,YamlNode> _child in mapping.Children)
            {
                if(_child.Key.NodeType == YamlNodeType.Scalar)
                {
                    YamlScalarNode _childKeyNode = (YamlScalarNode)_child.Key;
                    if(_childKeyNode.Value.Equals("TextureImporter") == true)
                    {
                        if( _child.Value.NodeType == YamlNodeType.Mapping )
                        {
                            YamlMappingNode _childValueNode = (YamlMappingNode)_child.Value;
                            foreach (KeyValuePair<YamlNode, YamlNode> _TIChild in _childValueNode.Children)
                            {
                                if(_TIChild.Key.NodeType == YamlNodeType.Scalar)
                                {
                                    YamlScalarNode _childSubTIKey = (YamlScalarNode)_TIChild.Key;
                                    if(_childSubTIKey.Value.Equals("internalIDToNameTable") == true)
                                    {
                                        if(_TIChild.Value.NodeType == YamlNodeType.Sequence )
                                        {
                                            YamlSequenceNode _childSprite = (YamlSequenceNode)_TIChild.Value;
                                            foreach(YamlNode _nodeSprite in _childSprite.Children)
                                            {
                                                if(_nodeSprite.NodeType == YamlNodeType.Mapping)
                                                {
                                                    YamlMappingNode _mapSprite =(YamlMappingNode)_nodeSprite;
                                                    string strKey = "";
                                                    string strValue = "";
                                                    bool bKey = true;
                                                    foreach (KeyValuePair<YamlNode, YamlNode> _childSpecSprite in _mapSprite.Children)
                                                    {
                                                        if(bKey == true)
                                                        {
                                                            YamlMappingNode _childSpec = (YamlMappingNode)_childSpecSprite.Value;
                                                            foreach (KeyValuePair<YamlNode, YamlNode> _first in _childSpec.Children)
                                                            {
                                                                YamlScalarNode _SpriteKeyNode = (YamlScalarNode)_first.Value;
                                                                //strKey = _first.Value
                                                                strKey = _SpriteKeyNode.Value;
                                                                bKey = false;
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            strValue = (string)_childSpecSprite.Value;
                                                            _spriteFileID.Add(strValue, strKey);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                int ad = 0;
            }
            int bb = 0;
        }
        bool dada = false;

    }

    [ContextMenu("GenerateFromBrick")]
    void _GenerateFromBrick()
    {
        Dictionary<string, string> _spriteFileID = new Dictionary<string, string>();
        if (PathOfSprite == null || PathOfSprite.Length == 0)
        {
            //PathOfSprite = "Assets/TileInfo/tmw_desert_spacing.png.meta";
        }
        string parsePath = GetFileContextByPath("Assets/TileInfo/Brick.prefab");
        // Setup the input
        var input = new StringReader(parsePath);

        var yaml = new YamlStream();
        yaml.Load(input);

        if (yaml.Documents.Count > 0)
        {
            // Examine the stream
            YamlMappingNode mapping = (YamlMappingNode)yaml.Documents[5].RootNode;
            string strCon = mapping.ToString();

            int a = 0;
        }
    }

    string getPrefabContext(GameObject objPrefab)
    {
        string strPrefab = AssetDatabase.GetAssetPath(objPrefab);

        return GetFileContextByPath(strPrefab);
    }

    public static string GetFileContextByPath(string strPath)
    {
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(strPath);
        //Debug.Log(reader.ReadToEnd());
        string str = reader.ReadToEnd();
        reader.Close();
        return str;
    }

}
