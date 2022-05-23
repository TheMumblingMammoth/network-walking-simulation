using UnityEngine;
using UnityEditor;
using System;
 
public class P16Import : AssetPostprocessor {
    void OnPreprocessTexture() {
        TextureImporter importer = assetImporter as TextureImporter;
        String name = importer.assetPath.ToLower();
        if (name.Substring(name.Length - 4, 4)==".png") {
            importer.filterMode = FilterMode.Point;
            importer.spritePixelsPerUnit = 16f; /// <<=====
            importer.textureCompression = TextureImporterCompression.Uncompressed;
            importer.spritePivot = new Vector2(0,0);
        }
    }
}