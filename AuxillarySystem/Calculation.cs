using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MSFD.AS
{
    public static class Calculation
    {
        public static float Map(IDeltaRange<float> value, Vector2 outputRange)
        {
            return Map(value.Value, value.MinBorder, value.MaxBorder, outputRange.x, outputRange.y);
        }
        /// <summary>
        /// Transform value from range [inputRange.x:inputRange.y] to range [outputRange.x:outputRange.y]
        /// </summary>
        public static float Map(float value, Vector2 inputRange, Vector2 outputRange)
        {
            return Map(value, inputRange.x, inputRange.y, outputRange.x, outputRange.y);
        }
        /// <summary>
        /// Transform value from range [low:high] to range [low2:high2]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <param name="low2"></param>
        /// <param name="high2"></param>
        /// <returns></returns>
        public static float Map(float value, float low, float high, float low2, float high2)
        {
            //ѕоложение числа в исходном отрезке, от 0 до 1
            float percentage = (value - low) / (high - low);
            //Ќакладываем его на конечный отрезок
            float transformed = low2 + (high2 - low2) * percentage;
            return transformed;
        }
        /// <summary>
        /// Transform value from range [inputRange.x:inputRange.y] to range [outputRange.x:outputRange.y]
        /// </summary>
        public static int Map(int value, Vector2Int inputRange, Vector2Int outputRange)
        {
            return Map(value, inputRange.x, inputRange.y, outputRange.x, outputRange.y);
        }
        /// <summary>
        /// Transform value from range [low:high] to range [low2:high2]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <param name="low2"></param>
        /// <param name="high2"></param>
        /// <returns></returns>
        public static int Map(int value, int low, int high, int low2, int high2)
        {
            //ѕоложение числа в исходном отрезке, от 0 до 1
            float percentage = ((float)(value - low)) / (high - low);
            //Ќакладываем его на конечный отрезок
            int transformed = UnityEngine.Mathf.RoundToInt(low2 + (high2 - low2) * percentage);
            return transformed;
        }

        /// <summary>
        /// Return is value in range from [boundX:boundY] or in range from [boundY:boundX]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="boundX"></param>
        /// <param name="boundY"></param>
        /// <param name="isInclusive"></param>
        /// <returns></returns>
        public static bool InRange(float value, float boundX, float boundY, bool isInclusive = true)
        {
            float x = Mathf.Min(boundX, boundY);
            float y = Mathf.Max(boundX, boundY);
            if(isInclusive)
                return x <= value && value <= y;
            else
                return x < value && value < y;
        }
        /// <summary>
        /// Return is value in range from [boundX:boundY] or in range from [boundY:boundX]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="boundX"></param>
        /// <param name="boundY"></param>
        /// <param name="isInclusive"></param>
        /// <returns></returns>
        public static bool InRange(int value, int boundX, int boundY, bool isInclusive = true)
        {
            int x = Mathf.Min(boundX, boundY);
            int y = Mathf.Max(boundX, boundY);
            if (isInclusive)
                return x <= value && value <= y;
            else
                return x < value && value < y;
        }

    }
}