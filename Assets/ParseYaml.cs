using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.RepresentationModel;
public class YamlParse
{
    public string YamlKey { get; set; }
    public string YamlValue { get; set; }
    public YamlSubClass SubClass { get; set; }
}
public class YamlSubClass
{
    public string YamlSubKey { get; set; }
    public List<YamlElement> Items { get; set; }
}
public class YamlElement
{
    public string strKey { get; set; }
    public string strValue { get; set; }
}
public class ParseYaml : MonoBehaviour
{
    public string strParse = @"
            YamlKey:    yamlKey
            YamlValue: YamlValue
            SubClass:
                YamlSubKey: subkey

            Items:
                - strKey: eleKey

            strValue: eleValue

            strKey: eleKey

            strValue: eleValue

";
    private const string Document = @"---
            receipt:    Oz-Ware Purchase Invoice
            date:        2007-08-06
            customer:
                given:   Dorothy
                family:  Gale

            items:
                - part_no:   A4786
                  descrip:   Water Bucket (Filled)
                  price:     1.47
                  quantity:  4

                - part_no:   E1628
                  descrip:   High Heeled ""Ruby"" Slippers
                  price:     100.27
                  quantity:  1

            bill-to:  &id001
                street: |-
                        123 Tornado Alley
                        Suite 16
                city:   East Westville
                state:  KS

            ship-to:  *id001

            specialDelivery: >
                Follow the Yellow Brick
                Road to the Emerald City.
                Pay no attention to the
                man behind the curtain.
...";
    [ContextMenu("ParseYaml")]
    void ParseYamlString()
    {
        //YamlParse _parse = new YamlParse();
        //_parse.YamlKey = "yamlKey";
        //_parse.YamlValue = "YamlValue";
        //_parse.SubClass = new YamlSubClass();
        //_parse.SubClass.YamlSubKey = "subkey";
        //_parse.SubClass.Items = new List<YamlElement>();
        //YamlElement _ele = new YamlElement();
        //_ele.strKey = "eleKey";
        //_ele.strValue = "eleValue";
        //_parse.SubClass.Items.Add(_ele);
        //YamlElement _ele1 = new YamlElement();
        //_ele1.strKey = "eleKey";
        //_ele1.strValue = "eleValue";
        //_parse.SubClass.Items.Add(_ele1);

        //var serializer = new SerializerBuilder().Build();
        //var yaml = serializer.Serialize(_parse);
        //output.WriteLine(yaml);

        //var yamlStre = new YamlStream();
        var input = new StringReader(strParse);
        //yamlStre.Load(input);
        int a = 0;
        int bb = 0;
    }
}
