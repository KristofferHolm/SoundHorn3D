using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObjectManager : MonoBehaviour
{
    private static SoundObjectManager instance;
    public static SoundObjectManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundObjectManager>();
            }
            return instance;
        }
    }

    public SoundObjectCategory CheckCategory(AudioClip clip)
    {
        foreach (var category in Categories)
        {
            foreach (var sound in category.Sounds)
            {
                if (sound.Equals(clip))
                    return category.Category;
            }
        }
        return SoundObjectCategory.none;
    }




    public List<SoundCategory> Categories;
}
[Serializable]
public class SoundCategory
{
    public SoundObjectCategory Category;
    public List<AudioClip> Sounds;
}
public enum SoundObjectCategory
{
    ticks,
    friction,
    open,
    close,
    pitch,
    tempo,
    continuous,
    none
}