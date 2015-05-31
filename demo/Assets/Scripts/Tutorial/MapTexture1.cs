﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Map
{
    public class MapTexture1
    {
        private int _textureScale;

        public MapTexture1(int textureScale)
        {
            _textureScale = textureScale;
        }

        public void AttachTexture(GameObject plane, Map1 map)
        {
            int textureWidth = (int)Map1.Width * _textureScale;
            int textureHeight = (int)Map1.Height * _textureScale;

            Texture2D texture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGB565, true);
            texture.SetPixels(Enumerable.Repeat(Color.white, textureWidth * textureHeight).ToArray());

            var lines = map.Graph.edges.Where(p => p.v0 != null).Select(p => new[]
            {
                p.v0.point.x, p.v0.point.y,
                p.v1.point.x, p.v1.point.y
            }).ToArray();

            foreach (var line in lines)
                DrawLine(texture, line[0], line[1], line[2], line[3], Color.black);

            var points = map.Graph.centers.Select(p => p.point).ToList();
            foreach (var p in points)
                texture.SetPixel((int)(p.x * _textureScale), (int)(p.y * _textureScale), Color.red);

            texture.Apply();

            plane.GetComponent<Renderer>().material.mainTexture = texture;
            //plane.transform.localPosition = new Vector3(Map.Width / 2, Map.Height / 2, 1);
        }

        public void DrawTwoGraph(GameObject plane, Map1 map)
        {
            int textureWidth = (int)Map1.Width * _textureScale;
            int textureHeight = (int)Map1.Height * _textureScale;

            Texture2D texture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGB565, true);
            texture.SetPixels(Enumerable.Repeat(Color.gray, textureWidth * textureHeight).ToArray());

            var lines = map.Graph.edges.Where(p => p.v0 != null).Select(p => new[]
            {
                p.v0.point.x, p.v0.point.y,
                p.v1.point.x, p.v1.point.y
            }).ToArray();

            foreach (var line in lines)
                DrawLine(texture, line[0], line[1], line[2], line[3], Color.white);

            //绘制节点
            foreach (var line in lines)
            {
                texture.SetPixel((int)(line[0] * _textureScale), (int)(line[1] * _textureScale), Color.blue);
                texture.SetPixel((int)(line[2] * _textureScale), (int)(line[3] * _textureScale), Color.blue);
            }

            var points = map.Graph.centers.Select(p => p.point).ToList();
            foreach (var p in points)
                texture.SetPixel((int)(p.x * _textureScale), (int)(p.y * _textureScale), Color.red);

            texture.Apply();

            plane.GetComponent<Renderer>().material.mainTexture = texture;
        }

        private void DrawLine(Texture2D texture, float x0, float y0, float x1, float y1, Color color)
        {
            texture.DrawLine((int)(x0 * _textureScale), (int)(y0 * _textureScale), (int)(x1 * _textureScale),
                (int)(y1 * _textureScale), color);
        }
    }
}