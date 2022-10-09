using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.AS
{
    public static class Rand
    {
        #region Collections
        public static List<T> GetRandomElements<T>(List<T> sourceElements, int randomIndexesCount, System.Predicate<T> searchCondition, bool isRandomElementsMustBeDifferent = true, bool isLogErrors = true)
        {
            List<T> properElements = new List<T>(sourceElements.Count);
            foreach (T x in sourceElements)
            {
                if (searchCondition(x))
                {
                    properElements.Add(x);
                }
            }
            return GetRandomElements<T>(properElements, randomIndexesCount, isRandomElementsMustBeDifferent, isLogErrors);
        }
        public static List<T> GetRandomElements<T>(List<T> sourceElements, int randomIndexesCount, bool isRandomElementsMustBeDifferent = true, bool isLogErrors = true)
        {
            List<int> randomIndexes;
            return GetRandomElements<T>(sourceElements, randomIndexesCount, out randomIndexes, isRandomElementsMustBeDifferent, isLogErrors);
        }
        public static List<T> GetRandomElements<T>(List<T> sourceElements, int randomIndexesCount, out List<int> randomIndexes, bool isRandomElementsMustBeDifferent = true, bool isLogErrors = true)
        {
            randomIndexes = GetRandomIndexes(sourceElements.Count, randomIndexesCount, isRandomElementsMustBeDifferent, isLogErrors);
            List<T> randomElements = new List<T>(randomIndexesCount);
            for (int i = 0; i < randomIndexesCount; i++)
            {
                randomElements.Add(sourceElements[randomIndexes[i]]);
            }
            return randomElements;
        }
        
        public static List<int> GetRandomIndexes(int sourceElementsCount, int randomIndexesCount, bool isRandomIndexesMustBeDifferent = true, bool isLogErrors = true)
        {
            if (randomIndexesCount <= 0)
            {
                Utilities.LogError("Attempt to get 0 or less then 0 randomElements", isLogErrors);
                return null;
            }
            if (randomIndexesCount > sourceElementsCount && isRandomIndexesMustBeDifferent)
            {
                Utilities.LogError("Attempt to get incorrect randomIndexesCount. RandomIndexesCount must be less then sourceElementsCount in isRandomIndexesMustBeDifferent mode", isLogErrors);
                return null;
            }

            List<int> randomIndexList = new List<int>(randomIndexesCount);
            if (isRandomIndexesMustBeDifferent)
            {
                List<ushort> availableIndexes = new List<ushort>(sourceElementsCount);
                for (ushort i = 0; i < sourceElementsCount; i++)
                {
                    availableIndexes.Add(i);
                }
                for (int i = 0; i < randomIndexesCount; i++)
                {
                    int ind = UnityEngine.Random.Range(0, availableIndexes.Count);
                    randomIndexList.Add(availableIndexes[ind]);
                    availableIndexes.RemoveAt(ind);
                }
            }
            else
            {
                for (int i = 0; i < randomIndexesCount; i++)
                {
                    randomIndexList.Add(GetRandomIndex(randomIndexesCount));
                }
            }
            return randomIndexList;
        }
        public static T GetRandomElement<T>(List<T> sourceElements)
        {
            return sourceElements[GetRandomIndex(sourceElements.Count)];
        }
        public static int GetRandomIndex(int elementsCount)
        {
            if(elementsCount < 1)
            {
                Debug.LogError("Incorrect elemets count");
            }
            return UnityEngine.Random.Range(0, elementsCount);
        }
        #endregion

        #region Range
        public static int RandomPoint(this IDeltaRange<int> range)
        {
            return RandomPointInRange(range);
        }
        public static int RandomPointInRange(IDeltaRange<int> range)
        {
            return Random.Range(range.MinBorder, range.MaxBorder);
        }
        public static int RandomPoint(this Vector2Int range)
        {
            return RandomPointInRange(range);
        }
        public static int RandomPointInRange(Vector2Int range)
        {
            return Random.Range(range.x, range.y);
        }
        public static float RandomPoint(this IDeltaRange<float> range)
        {
            return RandomPointInRange(range);
        }
        public static float RandomPointInRange(IDeltaRange<float> range)
        {
            return Random.Range(range.MinBorder, range.MaxBorder);
        }
        public static float RandomPoint(this Vector2 range)
        {
            return RandomPointInRange(range);
        }
        public static float RandomPointInRange(Vector2 range)
        {
            return Random.Range(range.x, range.y);
        }
        #endregion

        #region Direction/Point
        public static Vector3 RandomPointInBounds(Bounds bounds)
        {
            return RandomPointInBounds(bounds.center, bounds.size);
        }
        public static Vector3 RandomPointInBounds(Vector3 center, Vector3 size)
        {
            Vector3 randomPoint = Vector3.zero;

            randomPoint.x = UnityEngine.Random.Range(-size.x / 2, size.x / 2);
            randomPoint.y = UnityEngine.Random.Range(-size.y / 2, size.y / 2);
            randomPoint.z = UnityEngine.Random.Range(-size.z / 2, size.z / 2);
            return randomPoint + center;
        }
        public static Vector3 RandomPointInSphere(Vector3 center, float radius)
        {
            return center + UnityEngine.Random.insideUnitSphere * radius;
        }
        public static Quaternion RandomRotationYAxis()
        {
            return Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), Vector3.up);
        }

        #endregion

    }
}