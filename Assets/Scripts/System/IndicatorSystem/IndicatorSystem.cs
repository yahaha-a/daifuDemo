using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public enum IndicatorType
    {
        Line,
        Sector
    }
    
    public interface IIndicatorSystem : ISystem
    {
        AttackRangeIndicator CreateIndicator(Transform root);
        
        void CreateSectorMesh(Mesh mesh, Vector3 position, float radius, float innerRadius, float angleDegree, int segments);

        public void CreateMoveSectorMesh(Mesh mesh, Vector3 position, Vector3 rightDirection, float radius,
            float innerRadius, float angleDegree, int segments);
        
        void CreateLine(LineRenderer lineRenderer, float length, float width, Vector2 direction, Vector2 startPosition);
    }
    
    public class IndicatorSystem : AbstractSystem, IIndicatorSystem
    {
        private static ResLoader _resLoader = ResLoader.Allocate();
        
        private GameObject _attackRangeIndicatorPrefab;
        
        protected override void OnInit()
        {
            _attackRangeIndicatorPrefab = _resLoader.LoadSync<GameObject>("AttackRangeIndicator");
        }

        public AttackRangeIndicator CreateIndicator(Transform root)
        {
            var indicator = _attackRangeIndicatorPrefab.InstantiateWithParent(root);
            indicator.Show();
            return indicator.GetComponent<AttackRangeIndicator>();
        }
        
        public void CreateSectorMesh(Mesh mesh, Vector3 position, float radius, float innerRadius, float angleDegree, int segments)
        {
            int verticesCount = segments * 2 + 2;
            Vector3[] vertices = new Vector3[verticesCount];
            float angleRad = Mathf.Deg2Rad * angleDegree;
            float angleCur = angleRad;
            float angleDelta = angleRad / segments;

            float rotationOffset = Mathf.Deg2Rad * - angleDegree / 2;

            for (int i = 0; i < verticesCount; i += 2)
            {
                float cosA = Mathf.Cos(angleCur + rotationOffset);
                float sinA = Mathf.Sin(angleCur + rotationOffset);

                vertices[i] = position + new Vector3(radius * cosA, radius * sinA, 0);
                vertices[i + 1] = position + new Vector3(innerRadius * cosA, innerRadius * sinA, 0);
                angleCur -= angleDelta;
            }

            int triangleCount = segments * 6;
            int[] triangles = new int[triangleCount];
            for (int i = 0, vi = 0; i < triangleCount; i += 6, vi += 2)
            {
                triangles[i] = vi;
                triangles[i + 1] = vi + 3;
                triangles[i + 2] = vi + 1;
                triangles[i + 3] = vi + 2;
                triangles[i + 4] = vi + 3;
                triangles[i + 5] = vi;
            }

            Vector2[] uvs = new Vector2[verticesCount];
            for (int i = 0; i < verticesCount; i++)
            {
                uvs[i] = new Vector2(vertices[i].x / radius / 2 + 0.5f,
                    vertices[i].y / radius / 2 + 0.5f);
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
        }

        public void CreateMoveSectorMesh(Mesh mesh, Vector3 position, Vector3 rightDirection, float radius,
            float innerRadius, float angleDegree, int segments)
        {
            float directionAngleRad = Mathf.Atan2(rightDirection.y, rightDirection.x);
            float directionAngleDegree = directionAngleRad * Mathf.Rad2Deg;

            int verticesCount = segments * 2 + 2;
            Vector3[] vertices = new Vector3[verticesCount];
    
            float angleRad = Mathf.Deg2Rad * angleDegree;
            float startAngle = directionAngleRad - angleRad / 2;
            float angleDelta = angleRad / segments;

            for (int i = 0; i < verticesCount; i += 2)
            {
                float currentAngle = startAngle + angleDelta * (i / 2);
        
                Vector3 outerPoint = new Vector3(
                    Mathf.Cos(currentAngle) * radius,
                    Mathf.Sin(currentAngle) * radius,
                    0
                );
        
                Vector3 innerPoint = new Vector3(
                    Mathf.Cos(currentAngle) * innerRadius,
                    Mathf.Sin(currentAngle) * innerRadius,
                    0
                );

                vertices[i] = position + outerPoint;
                vertices[i + 1] = position + innerPoint;
            }

            int[] triangles = new int[segments * 6];
            for (int i = 0, vi = 0; i < triangles.Length; i += 6, vi += 2)
            {
                triangles[i] = vi;
                triangles[i + 1] = vi + 1;
                triangles[i + 2] = vi + 2;

                triangles[i + 3] = vi + 1;
                triangles[i + 4] = vi + 3;
                triangles[i + 5] = vi + 2;
            }

            Vector2[] uvs = new Vector2[verticesCount];
            for (int i = 0; i < verticesCount; i++)
            {
                Vector3 localPos = vertices[i] - position;
                uvs[i] = new Vector2(
                    localPos.x / (radius * 2) + 0.5f,
                    localPos.y / (radius * 2) + 0.5f
                );
            }

            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            mesh.RecalculateNormals();
        }

        public void CreateLine(LineRenderer lineRenderer, float length, float width, Vector2 direction, Vector2 startPosition)
        {
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;

            Vector2 endPoint = startPosition + direction.normalized * length;

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, endPoint);
        }
    }
}