using UnityEngine;

namespace CodeMonkey.Utils
{
    public static class UtilsClass
    {
        // Get Mouse Position in World with Z = 0f
        //public static Vector3 GetMouseWorldPosition() {
        //    Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, UnityEngine.Camera.main);
        //    vec.z = 0f;
        //    return vec;
        //}

        //public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, UnityEngine.Camera worldCamera) 
        //{
        //    Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        //    return worldPosition;
        //}

        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0f;
            return position;
        }
    }
}