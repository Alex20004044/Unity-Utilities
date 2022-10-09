using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;

namespace MSFD.AS
{
    public static class Coordinates
    {
        public static Bounds TotalBounds(Vector3[] points)
        {
            Bounds totalBounds = new Bounds(points[0], Vector3.zero);
            for (int i = 1; i < points.Length; i++)
            {
                totalBounds.Encapsulate(points[i]);
            }
            return totalBounds;
        }
        public static Bounds TotalBounds(Bounds[] bounds)
        {
            Bounds totalBounds = bounds[0];
            for (int i = 1; i < bounds.Length; i++)
            {
                totalBounds.Encapsulate(bounds[i]);
            }
            /*foreach (var x in bounds)
            {
                var b = x;
                if (b.max.x > totalBounds.max.x)
                    totalBounds.max = new Vector3(b.max.x, totalBounds.max.y, totalBounds.max.z);
                if (b.max.y > totalBounds.max.y)
                    totalBounds.max = new Vector3(totalBounds.max.x, b.max.y, totalBounds.max.z);
                if (b.max.z > totalBounds.max.z)
                    totalBounds.max = new Vector3(totalBounds.max.x, totalBounds.max.y, b.max.z);

                if (b.min.x < totalBounds.min.x)
                    totalBounds.min = new Vector3(b.min.x, totalBounds.min.y, totalBounds.min.z);
                if (b.min.y < totalBounds.min.y)
                    totalBounds.min = new Vector3(totalBounds.min.x, b.min.y, totalBounds.min.z);
                if (b.min.z < totalBounds.min.z)
                    totalBounds.min = new Vector3(totalBounds.min.x, totalBounds.min.y, b.min.z);
            }*/
            return totalBounds;
        }
        #region FindNearestPoint
        public static Vector3 FindRemotestPoint(Vector3 basePoint, Vector3[] points)
        {
            int p;
            return FindRemotestPoint(basePoint, points, out p);
        }
        public static Vector3 FindRemotestPoint(Vector3 basePoint, Vector3[] points, out int remotestPointIndex)
        {
            Vector3 remotestPoint = basePoint;
            float remotestDistSqr = float.PositiveInfinity;
            float distSqr;
            remotestPointIndex = 0;
            if (points.Length == 1)
            {
                remotestPoint = points[0];
            }
            else
            {
                for (int i = 0; i < points.Length; i++)
                {
                    distSqr = (basePoint - points[i]).sqrMagnitude;
                    if (distSqr > remotestDistSqr)
                    {
                        remotestPointIndex = i;
                        remotestPoint = points[i];
                        remotestDistSqr = distSqr;
                    }
                }
            }
            return remotestPoint;
        }

        public static Transform FindNearestTarget(Vector3 rootPoint, Transform[] targets)
        {
            int t;
            return FindNearestTarget(rootPoint, targets, out t);
        }
        public static Transform FindNearestTarget(Vector3 rootPoint, Transform[] targets, out int nearestTargetIndex)
        {
            FindNearestPoint(rootPoint, targets.Select((x) => x.position).ToArray(), out nearestTargetIndex);
            return targets[nearestTargetIndex];
        }
        public static Vector3 FindNearestPoint(Vector3 basePoint, Vector3[] points)
        {
            int p;
            return FindNearestPoint(basePoint, points, out p);
        }
        public static Vector3 FindNearestPoint(Vector3 basePoint, Vector3[] points, out int nearestPointIndex)
        {
            Vector3 nearestPoint = basePoint;
            float nearestDistSqr = float.PositiveInfinity;
            float distSqr;
            nearestPointIndex = 0;
            if (points.Length == 1)
            {
                nearestPoint = points[0];
            }
            else
            {
                for (int i = 0; i < points.Length; i++)
                {
                    distSqr = (basePoint - points[i]).sqrMagnitude;
                    if (distSqr < nearestDistSqr)
                    {
                        nearestPointIndex = i;
                        nearestPoint = points[i];
                        nearestDistSqr = distSqr;
                    }
                }
            }
            return nearestPoint;
        }
        #endregion
        #region Convert
        public enum ConvertV2ToV3Mode { y_to_y, y_to_z };
        public static Vector3 ConvertVector2ToVector3(Vector2 vector, float thirdCoordinateValue = 0, 
            ConvertV2ToV3Mode convertMode = ConvertV2ToV3Mode.y_to_z)
        {
            if (convertMode == ConvertV2ToV3Mode.y_to_z)
            {
                return new Vector3(vector.x, thirdCoordinateValue, vector.y);
            }
            else
            {
                return new Vector3(vector.x, vector.y, thirdCoordinateValue);
            }
        }
        public enum ConvertV3ToV2Mode { z_to_y, y_to_y };
        public static Vector2 ConvertVector3ToVector2(Vector3 vector, ConvertV3ToV2Mode convertMode = ConvertV3ToV2Mode.z_to_y)
        {
            if (convertMode == ConvertV3ToV2Mode.z_to_y)
            {
                return new Vector2(vector.x, vector.z);
            }
            else
            {
                return new Vector2(vector.x, vector.y);
            }
        }

        /// <summary>
        /// Return angle 0-360 from OX to direction (Direction magnitude must be <= 1)
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static float AngleFromDirection(Vector2 direction)
        {
            float angle = 0;
            float atg = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (direction.x == 0)
            {
                if (direction.y == 1)
                {
                    angle = 90f;
                }
                else if (direction.y == -1)
                {
                    angle = 270f;
                }
            }
            else
            {
                if (direction.y >= 0)
                {
                    angle = atg;
                }
                else
                {
                    angle = atg + 360f;
                }
            }
            return angle;
        }
        public static Vector3 ResetYAxis(Vector3 vector, float y = 0)
        {
            vector.y = y;
            return vector;
        }
        #endregion
        #region Spawn
        public static Vector3[] GetPointsAtCircle(Vector3 basePoint, Vector3 axis, Vector3 firstPointDirection, int pointsCount, float radius = 1)
        {
            axis.Normalize();
            firstPointDirection.Normalize();
            #region Input check
            if (Vector3.Dot(axis, firstPointDirection) == 1)
            {
                Debug.LogError("Axis and first point direction are parallel!");
                return null;
            }
            if(axis == Vector3.zero)
            {
                Debug.LogError("Axis is zero vector");
                return null;
            }
            if (firstPointDirection == Vector3.zero)
            {
                Debug.LogError("firstPointDirection is zero vector");
                return null;
            }
            #endregion
            Vector3[] points = new Vector3[pointsCount];
            float angleBetweenPoints = 360f / pointsCount;

            Plane plane = new Plane(axis, 0);
            Quaternion rotation = Quaternion.AngleAxis(angleBetweenPoints, axis);
            Vector3 direction = plane.ClosestPointOnPlane(firstPointDirection).normalized;
            
            for (int i = 0; i < pointsCount; i++)
            {
                points[i] = basePoint + direction * radius;
                direction = rotation * direction;
            }
            return points;
        }
        #endregion
    }
}
