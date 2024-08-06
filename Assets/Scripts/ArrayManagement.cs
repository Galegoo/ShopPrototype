using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayManagement : MonoBehaviour
{
    public static void AddElementToArray(ref GameObject[] array)
    {
        Array.Resize(ref array, array.Length + 1);
    }

    public static void RemoveElementFromArray(ref GameObject[] array, int index)
    {
        if (index < 0 || index >= array.Length)
            return;

        for (int i = index; i < array.Length - 1; i++)
        {
            array[i] = array[i + 1];
        }

        Array.Resize(ref array, array.Length - 1);
    }
}
