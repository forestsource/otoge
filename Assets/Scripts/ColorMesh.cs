using UnityEngine;
using System.Collections;
 
// メッシュ色変更
public class ColorMesh : MonoBehaviour {
    public Color color;    // 変更後のメッシュの色
 
    void Update() {
        // メッシュの色を変更
        var mesh = GetComponent<MeshFilter>().mesh;
        var colors = mesh.colors;
 
        for ( int i = 0 ; i < mesh.colors.Length ; ++i ) {
            colors[i] = color;
        }
 
        mesh.colors = colors;
    }
}