using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    [RequireComponent(typeof(RectTransform))]
    public class UILineRenderer : Graphic
    {
        [SerializeField] 
        private float _thickness = 0.1f;

        private IEnumerable<Vector2> _verticesPositions = new List<Vector2>();
        private int _count = 0;
        private RectTransform _rectTransform;

        public void SetVertexPositions(IEnumerable<Vector2> positions)
        {
            _rectTransform = (RectTransform)transform;
            _verticesPositions = positions;
            _count = positions.Count();
            SetAllDirty();
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
            
            if (_count < 2 || _thickness <= 0) 
            {
                return;
            }

            UIVertex[] vertices = CreateVertices(vh);
            CreateTriangles(vertices, vh);
        }

        private void CreateTriangles(UIVertex[] vertices, VertexHelper vh)
        {
            int iterations = vertices.Length / 2 - 1;
            for (int i = 0; i < iterations; i++)
            {
                int index = i * 2;
                vh.AddTriangle(index + 0, index + 1, index + 3);
                vh.AddTriangle(index + 3, index + 2, index + 0);
            }
        }

        private UIVertex[] CreateVertices(VertexHelper vh)
        {
            int index = 0;
            Vector2 secondFormerPosition = Vector2.zero;
            Vector2 formerPosition = Vector2.zero;
            UIVertex[] vertices = new UIVertex[_count * 2];
            
            foreach (Vector2 position in _verticesPositions)
            {
                int indexTimes2 = index * 2;
                int formerIndexTimes2 = indexTimes2 - 2;
                Vector2 connection = position - formerPosition;
                
                if (index == 1)
                {
                    CreatePerpendicularVertices(ref vertices, formerIndexTimes2, connection, formerPosition, vh);
                }
                else if (index > 1)
                {
                    Vector2 formerConnection = formerPosition - secondFormerPosition;
                    CreateCornerVertices(ref vertices, formerIndexTimes2, connection, formerConnection, formerPosition, vh);
                }

                secondFormerPosition = formerPosition;
                formerPosition = position;
                index++;
                
                if (index == _count)
                {
                    CreatePerpendicularVertices(ref vertices, indexTimes2, connection, position, vh);
                }
            }

            return vertices;
        }

        private void CreateCornerVertices(
            ref UIVertex[] points,
            int index,
            Vector2 connection,
            Vector2 formerConnection,
            Vector2 position,
            VertexHelper vh)
        {
            Vector2 posFormer0 = points[index - 2].position;
            Vector2 posFormer1 = points[index - 1].position;
            
            Vector2 perpendicular = Vector2.Perpendicular(connection).normalized;
            Vector2 pos0 = position + perpendicular * 0.5f * _thickness;
            Vector2 pos1 = position - perpendicular * 0.5f * _thickness;

            Vector2 intersection0 = GetIntersection(posFormer0, posFormer0 + formerConnection, pos0, pos0 + connection);
            Vector2 intersection1 = GetIntersection(posFormer1, posFormer1 + formerConnection, pos1, pos1 + connection);
            points[index] = CreateVertex(intersection0, vh);
            points[index + 1] = CreateVertex(intersection1, vh);
        }

        private Vector2 GetIntersection(Vector2 a0, Vector2 a1, Vector2 b0, Vector2 b1)
        {            
            Vector2 direction0 = a1 - a0;
            Vector2 direction1 = b1 - b0;
            float cross = direction0.x * direction1.y - direction0.y * direction1.x;

            if (Mathf.Approximately(cross, 0f))
            {
                cross = 1;
            }

            float t = ((b1.x - a1.x) * direction1.y - (b1.y - a1.y) * direction1.x) / cross;
            return a1 + t * direction0;
        }

        private void CreatePerpendicularVertices(
            ref UIVertex[] points,
            int index,
            Vector2 connection,
            Vector2 position,
            VertexHelper vh)
        {
            Vector2 perpendicular = Vector2.Perpendicular(connection).normalized;
            Vector2 pos0 = position + perpendicular * 0.5f * _thickness;
            Vector2 pos1 = position - perpendicular * 0.5f * _thickness;
            points[index] = CreateVertex(pos0, vh);
            points[index + 1] = CreateVertex(pos1, vh);
        }

        private UIVertex CreateVertex(
            Vector2 position,
            VertexHelper vh)
        {
            UIVertex result = UIVertex.simpleVert;
            result.color = color;
            result.position = position;
            vh.AddVert(result);
            return result;
        }
    }
}