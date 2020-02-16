using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.IO;
public class BlenderCommandLine : MonoBehaviour
{
    public string BlenderPath = "";
    public GameObject ReferenceObject;


    public Process p;

    [TextArea(3, 20)]
    public string script;

    [ContextMenu("test")]
    void test(){




    }
    [ContextMenu("test02")]
    void test02()
    {

    }
    [ContextMenu("execute")]
    void executeBlender()
    {
        string bclpath = Application.dataPath + "/BlenderCommandLine/";
        string blenderDirectory = System.IO.Path.GetDirectoryName(BlenderPath);

        string refObjpath = GetObjectFullpath(ReferenceObject);

        List<(string,string)> rep = new List<(string, string)>();

        UnityEngine.Debug.Log("ref " + refObjpath);
        rep.Add(("REF_OBJECT", refObjpath));
        string replacedScript = Replace_reserved_word(script,rep);
        string pythonscriptpath = bclpath + "data/bpyscript.py";
        WriteScript(replacedScript,pythonscriptpath);



        p = new Process();
        UnityEngine.Debug.Log(p.StartInfo.WorkingDirectory);

        p.StartInfo.FileName = bclpath + "data/script.bat";
        p.StartInfo.Arguments = rapDQ(blenderDirectory) + " " + rapDQ(bclpath + "data/void.blend") + " " + pythonscriptpath;

        UnityEngine.Debug.Log(p.StartInfo.Arguments);
        p.Start();
  
    }
    private string GetObjectFullpath(GameObject obj){
        int instanceID = obj.GetInstanceID();
        string path = AssetDatabase.GetAssetPath(instanceID);
        string fullPath = System.IO.Path.GetFullPath(path);
        return fullPath;
    }

    private void WriteScript(string script,string filepath){
        
        StreamWriter sw = new StreamWriter(filepath, false);
        sw.Write(script);
        sw.Flush();// StreamWriterのバッファに書き出し残しがないか確認
        sw.Close();// ファイルを閉じる
    }
    private string Replace_reserved_word(string script,List<(string,string)> rep){
        
        foreach ((string,string) v in rep){
            script = script.Replace(v.Item1,repEscape(rapDQ(v.Item2)));
        }
        return script;
    }
    private string repEscape(string str){
        return str.Replace(@"\", @"\\");
    }
    private string rapDQ(string str){
        return "\"" + str + "\"";
    }
}
